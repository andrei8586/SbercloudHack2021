namespace DataAccess
{
    internal interface IVirtualDeletableEntity : IEntity
    {
        public bool IsDeleted { get; set; }
    }
}
