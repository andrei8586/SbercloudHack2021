namespace DataAccess.Repositories
{
    using System.Linq;

    using DataAccess.Dto;

    /// <summary>
    /// Интерфейс репозитория членов чата
    /// </summary>
    public interface IChatMembersRepository
    {
        /// <summary>
        /// Добавляет пользователей в чат
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="userIdsToAdd">Идентификаторы пользвателей, которых нужно добавить</param>
        void AddMembers(long chatId, params long[] userIdsToAdd);

        /// <summary>
        /// Удаляет пользователй из чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="userIdsToRemove">Идентификаторы членов чата, которых нужно удалить</param>
        void RemoveMembers(long chatId, params long[] memberIdsToRemove);

        /// <summary>
        /// Удаляет текущего пользователя из чата
        /// </summary>
        /// <remarks>
        /// TODO: Удалять личный чат, если из него вышли все полььзователи. 
        /// TODO: Добавлять вышедшего из лоичного чата пользователя при поступлении сообщений
        /// </remarks>
        /// <param name="chatId">Идентификатор чата</param>
        void LeaveChat(long chatId);

        /// <summary>
        /// Получает права доступа к чату, применяемые к указанному пользователю (без наследования от группы и настроек чата по умолчанию)
        /// </summary>
        /// <param name="memberId">Идентификатор члена чата</param>
        /// <returns></returns>
        ChatPolicy GetMemberPolicy(long memberId);

        /// <summary>
        /// Устанавливает права доступа к чату, применяемые к указанному пользователю
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="newPolicy"></param>
        /// <returns></returns>
        void UpdateMemberPolicy(long memberId, ChatPolicy newPolicy);

        /// <summary>
        /// Получает перечисление по членам чата.
        /// Если указан <paramref name="search"/> то дополнительно фильтрует по нему
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="search">Строка поиска</param>
        /// <returns></returns>
        IQueryable<UserDto> GetChatMembers(long chatId, string search = null);
    }
}
