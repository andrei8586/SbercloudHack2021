namespace DataAccess.Dto
{
    using Common;

    /// <summary>
    /// Запись списка чатов
    /// </summary>
    public class ChatListItemDto : IHaveId<long>
    {
        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название чата
        /// </summary>
        public string ChatName { get; set; }

        /// <summary>
        /// Ссылка на логотип чата
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Тип чата
        /// </summary>
        public ChatType ChatType { get; set; }
    }
}
