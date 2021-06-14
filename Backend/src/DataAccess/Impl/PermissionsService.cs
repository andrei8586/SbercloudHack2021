namespace DataAccess.Impl
{
    using System;
    using System.Linq;

    using DataAccess.Dto;

    internal class PermissionsService : IPermissionsService
    {
        private readonly AppDbContext dbContext;

        public PermissionsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool CheckPermision(long chatId, long userId, Func<ChatPolicy, bool> checkFn)
        {
            var memberPolicy = this.GetMemberPolicy(chatId, userId);
            if (memberPolicy != null)
            {
                if (checkFn(memberPolicy))
                {
                    return true;
                }
            }

            //TODO: Add permission check by group and chat default permissions and replace it
            return true;
        }

        public ChatPolicy GetChatPermissions(long chatId, long userId)
        {
            var resultPolicy = this.GetMemberPolicy(chatId, userId) ?? new ChatPolicy();

            //TODO: Add permission check by group and chat default permissions and replace it
            return ChatPolicy.CreateFullAccessPolicy();
        }

        private ChatPolicy GetMemberPolicy(long chatId, long userId)
        {
            return this.dbContext.ChatMembers
                .Where(x => x.ChatId == chatId && x.MemberId == userId)
                .Select(x => x.MemberPolicy)
                .FirstOrDefault();
        }

        private IQueryable<ChatPolicy> GetMemberGroupsInfo(long chatId, long userId)
        {
            return this.dbContext.ChatMembersGroups
                .Where(x => x.Chats.Any(y => y.Id == chatId) && x.GroupMembers.Any(y => y.Id == userId))
                .Select(x => x.GroupPolicy);
        }
    }
}
