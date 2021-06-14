namespace DataAccess.Dto
{
    /// <summary>
    /// Настройки чата
    /// </summary>
    public class ChatSettings
    {
        /// <summary>
        /// Отображать ли чат в результатах поиска
        /// </summary>
        public bool CanVisibleInSearch { get; set; }

        /// <summary>
        /// Политика, которая применяется ко всем ппользователям чата по умолчанию
        /// </summary>
        public ChatPolicy DefaultPolicy { get; set; }

        /// <summary>
        /// Показывать ли названия групп рядом с именем отправлившего сообщение
        /// </summary>
        public bool ShowGroupNames { get; set; }
    }
}
