namespace CustomerService.Contract.Messages
{
    /// <summary>
    ///     Коды взаимодействия с шиной данных
    /// </summary>
    public class ServiceBusCodes
    {
        #region Сhat Service Codes

        /// <summary>
        /// Запрос на добавление покупателя
        /// </summary>
        public const int GetMessagesRequest = 40001;

        /// <summary>
        /// Ответ на запрос на добавление покупателя
        /// </summary>
        public const int GetMessagesReply = 40002;
        
        /// <summary>
        /// Запрос на добавление покупателя
        /// </summary>
        public const int SendMessageRequest = 40003;

        /// <summary>
        /// Ответ на запрос на удаление покупателя
        /// </summary>
        public const int SendMessageReply = 40004;
        
        /// <summary>
        /// Запрос на обновление покупателя
        /// </summary>
        public const int EditMessageRequest = 40005;

        /// <summary>
        /// Ответ на запрос на обновление покупателя
        /// </summary>
        public const int EditMessageReply = 40006;
        
        /// <summary>
        /// Запрос на получение покупателя
        /// </summary>
        public const int DeleteMessagesRequest = 40007;

        /// <summary>
        /// Ответ на запрос на получение покупателя
        /// </summary>
        public const int DeleteMessagesReply = 40008;
        
        /// <summary>
        /// Запрос на получение всех пользователей
        /// </summary>
        public const int UserTypingRequest = 40009;

        /// <summary>
        /// Ответ на запрос на получение всех пользователей
        /// </summary>
        public const int UserTypingReply = 40010;
        
        /// <summary>
        ///     Запрос на получение информации о покупателе и его заказам
        /// </summary>
        //public const int GetCustomerWithOrdersByIdRequest = 10011;

        /// <summary>
        ///     Ответ на запрос по получению покупателей с информацией по их заказам
        /// </summary>
        //public const int GetCustomerWithOrdersByIdReply = 10012;

        #endregion
        
    }
}