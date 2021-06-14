namespace DataAccess.Repositories
{
    using System.Linq;

    using DataAccess.Dto;

    /// <summary>
    /// Интерфейс репозитория чатов
    /// </summary>
    public interface IChatRepository
    {
        /// <summary>
        /// Создает новый чат
        /// </summary>
        /// <param name="name">название чата</param>
        /// <param name="isSupportChat">при <see langword="false"/> перед нами <see cref="ChatType.Group"/> в противном случае это <see cref="ChatType.Support"/></param>
        /// <param name="userIdsToAdd">Идентификаторы пользвателей, которых нужно добавить</param>
        /// <returns>Идентификатор созданного чата</returns>
        long Create(string name, bool isSupportChat, params long[] userIdsToAdd);

        /// <summary>
        /// Создает новый чат с другим пользователем (личка)
        /// </summary>
        /// <remarks>
        /// <param name="twoUserId">Идентификатор второго участника чата</param>
        /// <returns>Идентификатор созданного чата</returns>
        long CreatePeerToPeer(long twoUserId);

        /// <summary>
        /// Удаляет существующий чат
        /// </summary>
        /// TODO: Добавить запрет на удаление личных чатов пользователями
        /// </remarks>
        /// <param name="chatId">Идентификатор чата</param>
        void Delete(long chatId);

        /// <summary>
        /// Устанавливает логотип чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="logoUrl">Новая ссылка на логотип</param>
        void SetLogoUrl(long chatId, string logoUrl);

        /// <summary>
        /// Устанавливает новое название чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="newChatName">Новое название чата</param>
        void ChangeName(long chatId, string newChatName);

        /// <summary>
        /// Применяет новые настройки
        /// </summary>
        /// <remarks>
        /// Новые настройки полностью заменяют старые, 
        /// поэтому пред использованием старые настрйки сначала надо получить
        /// </remarks>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="newChatSettings"></param>
        void UpdateSettings(long chatId, ChatSettings newChatSettings);

        /// <summary>
        /// Получает настройки указанного чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        ChatSettings GetSettings(long chatId);
        /// <summary>
        /// Ищет глобально чаты, название которых соответствует параметру <paramref name="search"/>
        /// </summary>
        /// <param name="search">Строка поиска</param>
        /// <returns></returns>
        IQueryable<ChatListItemDto> GetChats(string search);

        /// <summary>
        /// Получает чаты, в которых состоит указанный пользователь.
        /// Если указан <paramref name="search"/> то дополнительно фильтрует по нему
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чьи чаты нужно получить</param>
        /// <param name="search">Строка поиска</param>
        /// <returns></returns>
        IQueryable<ChatListItemDto> GetUserChats(long userId, string search = null);

    }
}
