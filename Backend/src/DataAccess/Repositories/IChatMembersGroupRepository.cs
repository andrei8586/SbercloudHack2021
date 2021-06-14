namespace DataAccess.Repositories
{
    using DataAccess.Dto;

    public interface IChatMembersGroupRepository
    {
        long Create(string name, bool canAutomaticAddMembersToChats, params long[] userIdsToAdd);

        void Delete(long groupId);

        void AddToChat(long groupId, long chatId);

        void RemoveFromChat(long groupId, long chatId);

        void AddMember(long groupId, long userId);

        void RemoveMember(long groupId, long userId);

        /// <summary>
        /// Получает права доступа к чату, применяемые к указанному пользователю (без наследования от группы и настроек чата по умолчанию)
        /// </summary>
        /// <param name="groupId">Идентификатор члена чата</param>
        /// <returns></returns>
        ChatPolicy GetGroupPolicy(long groupId);

        /// <summary>
        /// Устанавливает права доступа к чату, применяемые к указанному пользователю
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="newPolicy"></param>
        /// <returns></returns>
        void UpdateGroupPolicy(long groupId, ChatPolicy newPolicy);
    }
}
