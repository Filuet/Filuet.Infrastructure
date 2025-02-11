using Filuet.Infrastructure.Telegram.Abstractions;
using Filuet.Infrastructure.Telegram.Entitites;
using TelegramBot = Telegram.Bot;
using Telegram.Bot;
using Filuet.Infrastructure.Abstractions.Helpers;

namespace Filuet.Infrastructure.Telegram.Services
{
    public class TelegramBotWithChatIdsCacheService : ITelegramBotWithChatIdsCacheService
    {
        /// <summary>
        /// Контекст кеша связей Username-ChatId данного воркера
        /// </summary>
        public ITelegramBotChatLinksCacheRepository BotChatLinksCacheRepository { get; set; }
        private ITelegramBotClient _telegramBotClient;
        /// <summary>
        /// Логгер
        /// </summary>
        protected static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="botChatLinksCacheRepository">Реализация контекста кеша связей Username-ChatId</param>
        /// <param name="telegramBotClient">Реализация клиента бота Telegram. В большинстве случаев используется TelegramBotClient</param>
        public TelegramBotWithChatIdsCacheService(ITelegramBotChatLinksCacheRepository botChatLinksCacheRepository, ITelegramBotClient telegramBotClient) {
            BotChatLinksCacheRepository = botChatLinksCacheRepository;
            _telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Отправка сообщения по chatId или userName из chatLink. Обновление chatId в кеше при необходимости
        /// </summary>
        /// <param name="chatLink">Обязательно должно содержать username</param>
        /// <param name="text">Сообщение для отправки</param>
        /// <returns></returns>
        public async Task SendTextAsync(TelegramBotChatLink chatLink, string text) {
            if (chatLink == null || string.IsNullOrEmpty(chatLink.Username)) {
                _logger.Error("Empty incoming chatLink parameter");
                return;
            }
            if (chatLink.ChatId == 0) {
                // ChatId не был сохранён в кеше ранее. Получаем его из API телеграмма и шлём сообщение
                _logger.Info($"Username {chatLink.Username} has chatId = 0. Getting from updates...");
                long chatId = await AcquireChatIdAsync(chatLink).ConfigureAwait(false);
                if (chatId == 0)
                    return;

                try {
                    await SendTextMessageAsync(chatId, text).ConfigureAwait(false);
                }
                catch (Exception e) {
                    _logger.Error(e);
                }
                return;
            }
            try {
                // ChatId есть в кеше, шлём сообщение
                await SendTextMessageAsync(chatLink.ChatId, text).ConfigureAwait(false);
            }
            catch (Exception e) {
                // Не получилось. Наверное старый чат был удалён. Пробуем получить новый chatId для этого username
                _logger.Error(e, $"Unable to send message to chatId = {chatLink.ChatId}. Try to send message to bot again. {e}");
                chatLink.ChatId = 0;
                await SendTextAsync(chatLink, text).ConfigureAwait(false);
                // Если упадёт и здесь, то знач пользователь не писал боту
            }
        }
        /// <summary>
        /// Получить новый chatId для данного chatLink с его username. Сохранение в кеше
        /// </summary>
        /// <param name="chatLink"></param>
        /// <returns>Новый chatId. Если неудалось получить, то 0</returns>
        private async Task<long> AcquireChatIdAsync(TelegramBotChatLink chatLink) {
            TelegramBot.Types.Update[] updates = null;
            try {
                updates = await _telegramBotClient.GetUpdatesAsync().ConfigureAwait(false);
            }
            catch (Exception e) {
                _logger.Error(e);
            }
            if (updates.IsNullOrEmpty()) {
                _logger.Error("Cant find any updates in chat");
                return 0;
            }

            var update = updates.FirstOrDefault(x => x.Message.Chat.Username == chatLink.Username);
            if (update == null) {
                _logger.Error($"Cant find chat with user {chatLink.Username}");
                return 0;
            }

            _logger.Info($"New chatId = {update.Message.Chat.Id} for username = {chatLink.Username} acquired");
            await BotChatLinksCacheRepository.DeleteAsync(chatLink).ConfigureAwait(false);
            chatLink.ChatId = update.Message.Chat.Id;
            await BotChatLinksCacheRepository.AddAsync(chatLink).ConfigureAwait(false);
            return update.Message.Chat.Id;
        }

        private async Task SendTextMessageAsync(long chatId, string text) {
            if (chatId == 0) {
                _logger.Error("Can't send message, chatId = 0");
                return;
            }
            await _telegramBotClient.SendTextMessageAsync(chatId, text).ConfigureAwait(false);
        }
        /// <summary>
        /// Проверить, есть ли chatId у данных username. Если чат есть у бота, но нет в кеше, то сохраняет в кеше chatId
        /// </summary>
        /// <param name="usernamesToCheck">username, для которых нужно найти chatId</param>
        /// <returns>Коллекция username'ов, для которых нет chatId</returns>
        public async Task<ICollection<string>> CheckChatIdsExistAsync(ICollection<string> usernamesToCheck) {
            ICollection<TelegramBotChatLink> chatLinks = await BotChatLinksCacheRepository.GetRangeAsync(usernamesToCheck).ConfigureAwait(false);
            IEnumerable<string> usernamesNoChatIds = new List<string>();

            if (chatLinks != null) {
                usernamesToCheck.Where(x => !chatLinks.Any(y => y.Username == x));
                usernamesNoChatIds = usernamesNoChatIds.Union(chatLinks.Where(x => x.ChatId == 0).Select(x => x.Username));
            }

            List<string> result = new List<string>();

            foreach (string usernameWoChatId in usernamesNoChatIds) {
                TelegramBotChatLink chatLink = new TelegramBotChatLink {
                    ChatId = 0,
                    Username = usernameWoChatId
                };
                long chatId = await AcquireChatIdAsync(chatLink).ConfigureAwait(false);
                if (chatId == 0)
                    result.Add(usernameWoChatId);
            }

            return result;
        }
    }
}