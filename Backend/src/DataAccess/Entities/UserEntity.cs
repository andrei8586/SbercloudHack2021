namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public enum UserType
    {
        /// <summary>
        /// Частное лицо
        /// </summary>
        Individual,

        /// <summary>
        /// Юридическаое лицо
        /// </summary>
        Legal,

        /// <summary>
        /// Аноним
        /// </summary>
        Anonimus
    }

    internal class UserEntity : IVirtualDeletableEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// Имя/псевдоним пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на аватарку пользователя
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Чаты, в которых состоит пользователь
        /// </summary>
        public ICollection<ChatEntity> Chats { get; set; }

        /// <summary>
        /// Удален ли пользователь
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Аналогично <see cref="IsDeleted"/>, но забаненые пользователи сохраняют за собой членство в чатах
        /// </summary>
        public bool IsBanned { get; set; }
    }
}
