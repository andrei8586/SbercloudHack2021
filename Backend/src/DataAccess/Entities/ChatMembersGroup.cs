namespace DataAccess.Entities
{
    using System.Collections.Generic;

    using DataAccess.Dto;

    /// <summary>
    /// Группа пользователей чата
    /// </summary>
    internal class ChatMembersGroup : IEntity
    {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Чаты, к которым относится группа
        /// </summary>
        public ICollection<ChatEntity> Chats { get; set; }

        /// <summary>
        /// Члены группы. Гленство в группе не доавляет автоматически в чат
        /// </summary>
        public ICollection<UserEntity> GroupMembers { get; set; }

        /// <summary>
        /// Добавлять ли пользователй группы автоматически во все чаты, куда добавлена эта группа
        /// </summary>
        public bool CanAutomaticAddMembersToChats { get; set; }

        /// <summary>
        /// Политика доступа группы к чату 
        /// </summary>
        public ChatPolicy GroupPolicy { get; set; }
    }
}
