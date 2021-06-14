namespace DataAccess
{
    using System;

    /// <summary>
    /// Флаг прав доступа к записи таблицы
    /// </summary>
    [Flags]
    public enum UIAccessRights
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
        /// Есть права на редактирование записей раздела
        /// </summary>
        Edit = 2,

        /// <summary>
        /// Полный доступ
        /// </summary>
        Full = Read | Edit
    }
}
