using Filuet.Infrastructure.Telegram.Entitites;

namespace Filuet.Infrastructure.Telegram.Abstractions
{
    /// <summary>
    /// Интерфейс для работы с ботом телеграмма и кешем сущностей TelegramBotChatLink
    /// </summary>
    public interface ITelegramBotWithChatIdsCacheService
    {
        /// <summary>
        /// Контекст кеша связей Username-ChatId данного воркера
        /// </summary>
        ITelegramBotChatLinksCacheRepository BotChatLinksCacheRepository { get; set; }
        /// <summary>
        /// Отправка сообщения по chatId или userName из chatLink. Обновление chatId в кеше при необходимости
        /// </summary>
        /// <param name="chatLink">Обязательно должно содержать username</param>
        /// <param name="text">Сообщение для отправки</param>
        /// <returns></returns>
        Task SendTextAsync(TelegramBotChatLink chatLink, string text);
        /// <summary>
        /// Проверить, есть ли chatId у данных username
        /// </summary>
        /// <param name="usernamesToCheck">username, для которых нужно найти chatId</param>
        /// <returns>Коллекция username'ов, для которых нет chatId</returns>
        Task<ICollection<string>> CheckChatIdsExistAsync(ICollection<string> usernamesToCheck);
    }
}