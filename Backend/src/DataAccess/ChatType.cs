namespace DataAccess
{
    public enum ChatType
    {
        /// <summary>
        /// Личка
        /// </summary>
        PeerToPeer,

        /// <summary>
        /// Группа (группа и канал объеденены, так как это можно разруливать правами доступа)
        /// </summary>
        Group,

        /// <summary>
        /// Чат техподдержки (Служебный тип. При получении сообщения из него формируется отдельный чат типа PeerToPeer)
        /// </summary>
        Support
    }
}
