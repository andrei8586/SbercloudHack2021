namespace DataAccess.Dto
{
    /// <summary>
    /// Информация о члене чата
    /// </summary>
    public class ChatMemberDto
    {
        /// <summary>
        /// Является ли пользователь владельцем чата. Владелец чата не может быть из него удален а так же имеет полные права
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Информация о самом пользователе
        /// </summary>
        public UserDto Member { get; set; }
    }
}
