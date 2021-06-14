namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using DataAccess.Dto;

    using Microsoft.EntityFrameworkCore;

    [Index(nameof(SendDate))]
    internal class ChatMessageEntity : IVirtualDeletableEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// Текст сообщения. МОжет быть пустым, если в чат отправляется 
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Чат, куда было отправлено сообщение
        /// </summary>
        [Required]
        public long OwnerChatId { get; set; }

        /// <summary>
        /// Чат, куда было отправлено сообщение
        /// </summary>
        [Required]
        public ChatEntity OwnerChat { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение явялется сообщением в треде, иначе null
        /// </summary>
        public long? ParentMessageId { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение явялется сообщением в треде, иначе null
        /// </summary>
        public ChatMessageEntity ParentMessage { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение было сформировано как копия (функция "Переслать сообщение"), иначе null
        /// </summary>
        public long? CopyFromId { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение было сформировано как копия (функция "Переслать сообщение"), иначе null
        /// </summary>
        public ChatMessageEntity CopyFrom { get; set; }

        /// <summary>
        /// Сообщение, ответом на которое будет текущее сообщение. Null, если это сообщение самостоятельное, а не является ответом на другое
        /// </summary>
        public long? ReplyToId { get; set; }

        /// <summary>
        /// Сообщение, ответом на которое будет текущее сообщение. Null, если это сообщение самостоятельное, а не является ответом на другое
        /// </summary>
        public ChatMessageEntity ReplyTo { get; set; }

        /// <summary>
        /// Автор сообщения
        /// </summary>
        [Required]
        public long AutorId { get; set; }

        /// <summary>
        /// Автор сообщения
        /// </summary>
        [Required]
        public UserEntity Autor { get; set; }

        /// <summary>
        /// дата и время отправки сообщения
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        [Column(TypeName = "jsonb")]
        public IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// Удалено ли сообщение. Сообщения физически не удаляем, иначе на них нельзя будет ссылаться
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Флаг, который говорит о том, что сообщение было отредактировано
        /// </summary>
        public bool IsEdited { get; set; }
    }
}
