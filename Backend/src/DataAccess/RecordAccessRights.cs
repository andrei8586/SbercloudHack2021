namespace DataAccess
{
    using System;

    /// <summary>
    /// Флаг прав доступа к записи таблицы
    /// </summary>
    [Flags]
    public enum RecordAccessRights
    {
        /// <summary>
        /// Нет парав доступа. Пользователь не видит этот раздел
        /// </summary>
        None = 0,

        /// <summary>
        /// Есть права на просмотр раздела
        /// </summary>
        Read = 1,

        /// <summary>
        /// Есть права на добавление записей в раздел
        /// </summary>
        Write = 2,

        /// <summary>
        /// Есть права на редактирование записей раздела
        /// </summary>
        Edit = 4,

        /// <summary>
        /// Есть права на удаление записей раздела
        /// </summary>
        Delete = 8,

        /// <summary>
        /// Полный доступ
        /// </summary>
        Full = Read | Write | Edit | Delete
    }
}
