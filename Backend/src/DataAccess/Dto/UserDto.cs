namespace DataAccess.Dto
{
    using Common;

    /// <summary>
    /// DTO базовой информации о пользователе
    /// </summary>
    public class UserDto : IHaveId<long>
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
    }
}
