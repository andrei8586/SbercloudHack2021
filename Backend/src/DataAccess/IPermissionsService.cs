namespace DataAccess
{
    using System;

    using DataAccess.Dto;
    using DataAccess.Entities;

    internal interface IPermissionsService
    {
        bool CheckPermision(long chatId, long userId, Func<ChatPolicy, bool> checkFn);

        bool CheckPermision(ChatMemberEntity chatMember, Func<ChatPolicy, bool> checkFn);

        ChatPolicy GetChatPermissions(long chatId, long userId);

        ChatPolicy GetChatPermissions(ChatMemberEntity chatMember);
    }
}
