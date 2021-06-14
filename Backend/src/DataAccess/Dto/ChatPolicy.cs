namespace DataAccess.Dto
{

    /// <summary>
    /// Политика чата
    /// </summary>
    public class ChatPolicy
    {
        /// <summary>
        /// Права на доступ к сообщениям чата
        /// </summary>
        public RecordAccessRights ChatMessagesRights { get; set; }

        /// <summary>
        /// Парава пользователя на доступ к работе с тредами
        /// </summary>
        /// <remarks>
        public ThreadAccessRights ThreadRights { get; set; }

        /// <summary>
        /// Права на доступ к списку пользователей чата
        /// </summary>
        /// <remarks>
        /// Read - Просмотор списка пользователей
        /// Write - добавление новых
        /// Edit - Изменение прав пользователя
        /// Delete - Исключение пользователя из группы
        /// </remarks>
        public RecordAccessRights MembersListRights { get; set; }

        /// <summary>
        /// Права на доступ к настройкам чата
        /// </summary>
        public UIAccessRights SettingsRights { get; set; }

        /// <summary>
        /// Права на удаление чата
        /// </summary>
        public bool CanDeleteChat { get; set; }

        /// <summary>
        /// Создает политику с полными правами
        /// </summary>
        /// <returns></returns>
        public static ChatPolicy CreateFullAccessPolicy()
        {
            return new ChatPolicy
            {
                ChatMessagesRights = RecordAccessRights.Full,
                MembersListRights = RecordAccessRights.Full,
                ThreadRights = ThreadAccessRights.Full
            };
        }
    }
}
