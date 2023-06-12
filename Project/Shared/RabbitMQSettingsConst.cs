using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
    public class RabbitMQSettingsConst
    {


        public const string OrderSaga = "order-saga-queue";

        public const string StockRollBackMessageQueueName = "stock-rollback-message-queue";
        public const string StockReservedEventQueueName = "stock-reserved-queue";
        public const string StockOrderCreatedEventQueueName = "stock-order-created-queue";
        public const string StockPaymentNotCompletedEventQueueName = "stock-payment-failed-queue";
        public const string OrderRequestCompletedEventQueueName = "order-request-completed-queue";
        public const string OrderRequestFailedEventQueueName = "order-request-failed-queue";
        public const string OrderPaymentFailedEventQueueName = "order-payment-failed-queue";
        public const string StockNotReservedEventQueueName = "stock-not-reserved-queue";
        public const string PaymentStockReservedRequestQueueName = "payment-stock-reserved-request-queue";
    }
}