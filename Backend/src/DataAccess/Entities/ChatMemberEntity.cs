namespace DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using DataAccess.Dto;

    using Microsoft.EntityFrameworkCore;

    [Index(nameof(MemberId), nameof(ChatId), IsUnique = true)]
    internal class ChatMemberEntity : IEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// Пользователь чата
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// Пользователь чата
        /// </summary>
        public UserEntity Member { get; set; }

        /// <summary>
        /// Чат, в который добавлен этот пользователь
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Чат, в который добавлен этот пользователь
        /// </summary>
        public ChatEntity Chat { get; set; }

        /// <summary>
        /// Время последнего прочитанного сообщения
        /// </summary>
        public DateTime LastReadTшme { get; set; }

        /// <summary>
        /// Права члена чата. Переопределяют права группы и права осчтупа к чату по умолчанию
        /// </summary>
        [Column(TypeName = "jsonb")]
        public ChatPolicy MemberPolicy { get; set; }

        public static ChatMemberEntity CreateFromId(long id)
        {
            return new ChatMemberEntity { Id = id };
        }

        public static ChatMemberEntity CreateFromUserId(long userId)
        {
            return new ChatMemberEntity { MemberId = userId };
        }

        public static ChatMemberEntity CreateFromUserId(long userId, ChatEntity chat)
        {
            return new ChatMemberEntity { MemberId = userId, Chat = chat };
        }

        public static ChatMemberEntity CreateFromUserId(long userId, long chatId)
        {
            return new ChatMemberEntity { MemberId = userId, ChatId = chatId };
        }
    }
}
