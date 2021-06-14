namespace DataAccess.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ChatMessageDto : IHaveId<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// Текст сообщения. МОжет быть пустым, если в чат отправляется 
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение явялется сообщением в треде, иначе null
        /// </summary>
        public long? ParentMessageId { get; set; }

        /// <summary>
        /// Родитетьское сообщение, если текущее сообщение было сформировано как копия (функция "Переслать сообщение"), иначе null
        /// </summary>
        public long? CopyFromId { get; set; }

        /// <summary>
        /// Сообщение, ответом на которое будет текущее сообщение. Null, если это сообщение самостоятельное, а не является ответом на другое
        /// </summary>
        public long? ReplyToId { get; set; }

        /// <summary>
        /// Автор сообщения
        /// </summary>
        public UserDto Autor { get; set; }

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
