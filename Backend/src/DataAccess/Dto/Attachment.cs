namespace DataAccess.Dto
{
    /// <summary>
    /// Тип прикрепленого файла
    /// </summary>
    public enum AttachmentType
    {
        /// <summary>
        /// Изображение
        /// </summary>
        Image,

        /// <summary>
        /// Документ
        /// </summary>
        Document,

        /// <summary>
        /// Музыка
        /// </summary>
        Audio,

        /// <summary>
        /// Аудиозапись. Следует различать музыку и аудиозапись, так как для аудиозаписи будет выполняться распознвание текста
        /// </summary>
        Voice
    }

    /// <summary>
    /// Прикрепленый файл
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Название файла (с расширением)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на файл
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Тип прикрепленого файла
        /// </summary>
        public AttachmentType Type { get; set; }
    }
}
