namespace DataAccess.Impl.Repositories
{
    using System;
    using System.Linq;

    using AutoMapper;

    using DataAccess.Dto;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Services;

    internal class ChatRepository : BaseRepository<ChatEntity>, IChatRepository
    {
        private const string CannotAccessToEditSettings = "У вас нет прав на изменение настроек чата";

        public ChatRepository(AppDbContext dbContext, IUserService userService, IPermissionsService permissionsService, IMapper mapper) 
            : base(dbContext, userService, permissionsService, mapper)
        {
        }

        public long Create(string name, bool isSupportChat, params long[] userIdsToAdd)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var chatEentity = new ChatEntity
            {
                Name = name,
                ChatType = isSupportChat ? ChatType.Support : ChatType.Group
            };

            var ownerId = this.UserService.GetCurrentUserId();

            var members = userIdsToAdd.Select(x => ChatMemberEntity.CreateFromUserId(x, chatEentity)).ToList();
            members.Add(ChatMemberEntity.CreateFromUserId(ownerId, chatEentity));

            this.DbSet.Add(chatEentity);
            this.DbContext.ChatMembers.AddRange(members);
            this.DbContext.SaveChanges();

            return chatEentity.Id;
        }

        public long CreatePeerToPeer(long twoUserId)
        {
            var chatEentity = new ChatEntity
            {
                // В таких чатах имя сата всегда равно имени собеседника
                ChatType = ChatType.PeerToPeer
            };

            var me = this.UserService.GetCurrentUserId();
            var members = new[] 
            {
                ChatMemberEntity.CreateFromUserId(me, chatEentity),
                ChatMemberEntity.CreateFromUserId(twoUserId, chatEentity) 
            };

            this.DbSet.Add(chatEentity);
            this.DbContext.ChatMembers.AddRange(members);
            this.DbContext.SaveChanges();

            return chatEentity.Id;
        }

        public void Delete(long chatId)
        {
            this.UpdateChatEntity(
                chatId, 
                policy => policy.CanDeleteChat, 
                "У вас нет прав на удаление чата", 
                chatEntity => chatEntity.IsDeleted = true);
        }

        public void SetLogoUrl(long chatId, string logoUrl)
        {
            this.ValidateChatTypeOrThrow(
                   chatId,
                   x => x != ChatType.PeerToPeer,
                   "В личных чатах нельзя менять логотип");

            this.UpdateChatEntity(
                chatId,
                policy => policy.SettingsRights.HasFlag(UIAccessRights.Edit),
                CannotAccessToEditSettings,
                chatEntity => chatEntity.LogoUrl = logoUrl);
        }

        public void ChangeName(long chatId, string newChatName)
        {
            this.ValidateChatTypeOrThrow(
                   chatId,
                   x => x != ChatType.PeerToPeer,
                   "В личных чатах нельзя менять название");

            this.UpdateChatEntity(
                chatId,
                policy => policy.SettingsRights.HasFlag(UIAccessRights.Edit),
                CannotAccessToEditSettings,
                chatEntity => chatEntity.Name = newChatName);
        }

        public void UpdateSettings(long chatId, ChatSettings newChatSettings)
        {
            this.UpdateChatEntity(
                chatId,
                policy => policy.SettingsRights.HasFlag(UIAccessRights.Edit),
                CannotAccessToEditSettings,
                chatEntity => chatEntity.Settings = newChatSettings);
        }

        public ChatSettings GetSettings(long chatId)
        {
            return this.DbSet
                .AsNoTracking()
                .Where(x => x.Id == chatId && !x.IsDeleted)
                .Select(x => x.Settings)
                .FirstOrDefault();
        }

        public IQueryable<ChatListItemDto> GetChats(string chatName)
        {
            var currentUserId = this.UserService.GetCurrentUserId();
            var query = this.DbContext.Chats
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Where(x => x.ChatType != ChatType.PeerToPeer || x.Members.Any(y => y.MemberId == currentUserId));

            if (!string.IsNullOrWhiteSpace(chatName))
            {
                query = query.Where(x => x.Name.Contains(chatName));
            }

            return this.ProjectTo<ChatListItemDto>(query);
        }

        public IQueryable<ChatListItemDto> GetUserChats(long userId, string search = null)
        {
            var currentUserId = this.UserService.GetCurrentUserId();

            var query = this.DbContext.Chats
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Where(x => x.Members.Any(y => y.MemberId == currentUserId));

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return this.ProjectTo<ChatListItemDto>(query);
        }

        private void UpdateChatEntity(long chatId, Func<ChatPolicy, bool> checkPermissionFn, string noPermissionsMessage, Action<ChatEntity> updateAction)
        {
            var chat = this.GetEntity(chatId);
            if (chat == null || chat.IsDeleted)
            {
                return;
            }

            this.ValidatePermissionsOrThrow(chatId, checkPermissionFn, noPermissionsMessage);

            updateAction(chat);

            this.DbContext.Update(chat);
            this.DbContext.SaveChanges();
        }
    }
}
