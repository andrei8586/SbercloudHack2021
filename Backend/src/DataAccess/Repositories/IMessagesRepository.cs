namespace DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using DataAccess.Dto;

    public interface IMessagesRepository
    {
        /// <summary>
        /// Добавляет сообщение в чат
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="text">Текст сообщения. Может быть пустым, если сообщение содержит вложения</param>
        /// <param name="attachments">Вложения</param>
        /// <param name="replyToId">Сообщение, ответом на которое является текущее</param>
        /// <param name="parentMessageId">Родитетьское сообщение, если добавляемое сообщение явялется сообщением в треде</param>
        /// <returns>Идентификатор добавленного сообщения</returns>
        long Add(long chatId, string text, IList<Attachment> attachments = null, long? replyToId = null, long? parentMessageId = null);

        /// <summary>
        /// Удаляет сообщения с указанными идентификаторами
        /// </summary>
        /// <param name="messageIds">Идентификаторы удаляемых сообщений</param>
        void Delete(params long[] messageIds);

        /// <summary>
        /// Редакторует сообщение с указанным идентификатором
        /// </summary>
        /// <remarks>
        /// В аудиозаписях текст - это распознанный через апи текст
        /// </remarks>
        /// <param name="messageId">Идентификатор редактируемого сообщения</param>
        /// <param name="newText"></param>
        void Edit(long messageId, string newText);

        /// <summary>
        /// Пересылает сообщение в указанный чат
        /// </summary>
        /// <param name="messageId">Идентификатор пересылаемого сообщения</param>
        /// <param name="text">Комментарий к пересылаемому сообщению</param>
        /// <param name="newChatId">Идентификатор чата, куда пересылается сообщение. Если <see langword="null"/>, то чат стается прежним</param>
        /// <param name="parentMessageId">Родитетьское сообщение, если добавляемое сообщение явялется сообщением в треде</param>
        /// <returns>Идентификатор добавленного сообщения</returns>
        long CopyTo(long messageId, string text, long? newChatId = null, long? parentMessageId = null);

        /// <summary>
        /// Получает запрос на извлечение сообщений чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns></returns>
        IQueryable<ChatMessageDto> GetChatMessages(long chatId);

        /// <summary>
        /// Получает запрос на извлечение непрочитанных сообщений чата
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns></returns>
        IQueryable<ChatMessageDto> GetUnreadedChatMessages(long chatId, long userId);
    }
}
