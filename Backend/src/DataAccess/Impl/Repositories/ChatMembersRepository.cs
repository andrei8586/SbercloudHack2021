namespace DataAccess.Impl.Repositories
{
    using System;
    using System.Linq;

    using AutoMapper;

    using DataAccess.Dto;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    using Services;

    internal class ChatMembersRepository : BaseRepository<ChatMemberEntity>, IChatMembersRepository
    {
        public ChatMembersRepository(AppDbContext dbContext, IUserService userService, IPermissionsService permissionsService, IMapper mapper) 
            : base(dbContext, userService, permissionsService, mapper)
        {
        }

        public void AddMembers(long chatId, params long[] userIdsToAdd)
        {
            this.ValidateChatTypeOrThrow(
                chatId, 
                x => x != ChatType.PeerToPeer, 
                "В личные чаты нельзя добавлять новых членов");

            this.ValidatePermissionsOrThrow(
                chatId,
                x => x.MembersListRights.HasFlag(RecordAccessRights.Write),
                "У вас нет прав на добавление новых пользователя");

            var members = userIdsToAdd.Select(x => ChatMemberEntity.CreateFromUserId(x, chatId));

            this.DbSet.AddRange(members);
            this.SaveChanges();
        }

        public void RemoveMembers(long chatId, params long[] memberIdsToRemove)
        {
            var currentUserId = this.UserService.GetCurrentUserId();
            if (memberIdsToRemove.Any(x => x == currentUserId))
            {
                throw new InvalidOperationException("Нельзя удалять самого себя из чата");
            }

            this.ValidateChatTypeOrThrow(
                chatId,
                x => x != ChatType.PeerToPeer,
                "Из личных чатов нельзя удалять членов");

            this.ValidatePermissionsOrThrow(
                chatId,
                x => x.MembersListRights.HasFlag(RecordAccessRights.Delete),
                "У вас нет прав на исключение пользователей из чата");

            var members = memberIdsToRemove.Select(x => ChatMemberEntity.CreateFromId(x));

            this.DbSet.RemoveRange(members);
            this.SaveChanges();
        }

        public void LeaveChat(long chatId)
        {
            var currentUserId = this.UserService.GetCurrentUserId();
            var currentMember = this.DbSet.FirstOrDefault(x => x.MemberId == currentUserId && x.ChatId == chatId);

            if (currentMember == null)
            {
                return;
            }

            this.DbSet.Remove(currentMember);
            this.SaveChanges();
        }

        public ChatPolicy GetMemberPolicy(long memberId)
        {
            var member = this.GetEntity(memberId);
            if (member == null)
            {
                return null;
            }

            this.ValidatePermissionsOrThrow(
                member.ChatId,
                x => x.MembersListRights.HasFlag(RecordAccessRights.Edit), // Хоть Edit и выглядит не логично, но права нужно просматривать только тем, кто их редактирует
                "У вас нет прав на просмотр прав пользователя");

            return member.MemberPolicy;
        }

        public void UpdateMemberPolicy(long memberId, ChatPolicy newPolicy)
        {
            var member = this.GetEntity(memberId);
            if (member == null)
            {
                return;
            }

            this.ValidatePermissionsOrThrow(
                member.ChatId, 
                x => x.MembersListRights.HasFlag(RecordAccessRights.Edit),
                "У вас нет прав на изменение прав пользователя");

            member.MemberPolicy = newPolicy;

            this.DbSet.Update(member);
            this.SaveChanges();
        }

        public IQueryable<UserDto> GetChatMembers(long chatId, string search = null)
        {
            this.ValidateChatTypeOrThrow(
                chatId,
                x => x != ChatType.PeerToPeer,
                "В личных чатах нельзя посмотреть список членов");

            this.ValidatePermissionsOrThrow(
                chatId,
                x => x.MembersListRights.HasFlag(RecordAccessRights.Read),
                "У вас нет прав на просмотр списка пользователей чата");

            var query = this.DbSet.Where(x => x.ChatId == chatId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Member.Name.Contains(search));
            }

            return this.ProjectTo<UserDto>(query);
        }
    }
}
