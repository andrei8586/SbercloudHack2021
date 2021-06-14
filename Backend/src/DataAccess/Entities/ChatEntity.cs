namespace DataAccess.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DataAccess.Dto;

    internal class ChatEntity : IVirtualDeletableEntity
    {
        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название чата
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Тип чата
        /// </summary>
        [Required]
        public ChatType ChatType { get; set; }

        /// <summary>
        /// Ссылка на логотип чата
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Участники чата
        /// </summary>
        public ICollection<ChatMemberEntity> Members { get; set; }

        /// <summary>
        /// Группы чата
        /// </summary>
        public ICollection<ChatMembersGroup> Groups { get; set; }

        /// <summary>
        /// Сообщения чата (для <see cref="ChatType"/> = <see cref="ChatType.Support"/> всегда пусто)
        /// </summary>
        public ICollection<ChatMessageEntity> Messages { get; set; }

        /// <summary>
        /// Настройки чата
        /// </summary>
        [Column(TypeName = "jsonb")]
        public ChatSettings Settings { get; set; }

        /// <summary>
        /// Удален ли чат
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
