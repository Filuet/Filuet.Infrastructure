using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;
using Filuet.Infrastructure.Telegram.Entitites;

namespace Filuet.Infrastructure.Telegram.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория для хранения закешированных связей Username-ChatId пользователей, написавших боту Telegram
    /// </summary>
    public interface ITelegramBotChatLinksCacheRepository : IDeletableRepositoryAsync<TelegramBotChatLink>
    {
        /// <summary>
        /// Получить коллекцию связей TelegramBotChatLink из кеша по данным username
        /// </summary>
        /// <param name="usernames"></param>
        /// <returns></returns> 
        Task<ICollection<TelegramBotChatLink>> GetRangeAsync(ICollection<string> usernames);
    }
}