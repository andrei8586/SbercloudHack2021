namespace DataAccess.Impl.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using DataAccess.Dto;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    using Services;

    internal class MessagesRepository : BaseRepository<ChatMessageEntity>, IMessagesRepository
    {
        public MessagesRepository(AppDbContext dbContext, IUserService userService, IPermissionsService permissionsService, IMapper mapper) 
            : base(dbContext, userService, permissionsService, mapper)
        {
        }

        public long Add(long chatId, string text, IList<Attachment> attachments = null, long? replyToId = null, long? parentMessageId = null)
        {
            if (attachments?.Any(x => x.Type == AttachmentType.Voice) == true && !string.IsNullOrWhiteSpace(text))
            {
                throw new InvalidOperationException("Запрещено создавать голосвые сообщения с текстом");
            }

            if (parentMessageId.HasValue)
            {
                // Если тред еще не начат
                if (!this.DbSet.Any(x => x.ParentMessageId == parentMessageId.Value && x.OwnerChatId == chatId))
                {
                    this.ValidatePermissionsOrThrow(
                        chatId,
                        x => x.ThreadRights.HasFlag(ThreadAccessRights.StartThread),
                        "У вас нет прав на создание треда");
                }
                else
                {
                    this.ValidatePermissionsOrThrow(
                        chatId,
                        x => x.ThreadRights.HasFlag(ThreadAccessRights.AddMessage) || x.ThreadRights.HasFlag(ThreadAccessRights.StartThread),
                        "У вас нет прав для добавления сообщения в тред");
                }
                
            }
            else
            {
                this.ValidatePermissionsOrThrow(
                        chatId,
                        x => x.ChatMessagesRights.HasFlag(RecordAccessRights.Write),
                        "У вас нет прав для добавления сообщения в тред");
            }

            var currentUserId = this.UserService.GetCurrentUserId();

            var entity = new ChatMessageEntity 
            { 
                Attachments = attachments, 
                AutorId = currentUserId, 
                OwnerChatId = chatId, 
                ParentMessageId = parentMessageId, 
                ReplyToId = replyToId, 
                Text = text, 
                SendDate = DateTime.UtcNow 
            };

            this.DbSet.Add(entity);
            this.SaveChanges();

            return entity.Id;
        }

        public void Delete(params long[] messageIds)
        {
            throw new NotImplementedException();
            //var messages = this.DbSet.Where(x => messageIds.Contains(x.Id)).ToList();
            //var 
            //if (messages.Any(x => x.ParentMessageId.HasValue))
            //{
            //    this.ValidatePermissionsOrThrow(
            //            chatId,
            //            x => x.ThreadRights.HasFlag(ThreadAccessRights.DeleteMessage),
            //            "У вас нет прав на удаление сообщений из треда");
            //}
        }

        public void Edit(long messageId, string newText)
        {
            throw new NotImplementedException();
        }

        public long CopyTo(long messageId, string text, long? newChatId = null, long? parentMessageId = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChatMessageDto> GetChatMessages(long chatId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChatMessageDto> GetUnreadedChatMessages(long chatId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
