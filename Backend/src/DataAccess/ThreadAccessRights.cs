namespace DataAccess
{
    public enum ThreadAccessRights
    {
        None = 0,
        ViewThread = 1,
        StartThread = 2,
        DelteThread = 4,
        AddMessage = 8,
        DeleteMessage = 16,
        Full = ViewThread | StartThread | DelteThread | DeleteMessage | AddMessage
    }
}
