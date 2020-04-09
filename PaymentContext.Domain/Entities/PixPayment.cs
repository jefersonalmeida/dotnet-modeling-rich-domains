using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PixPayment : Payment
    {
        public PixPayment(
            string transactionId,
            DateTime paidAt,
            DateTime expiredAt,
            decimal total,
            decimal totalPaid,
            string holder,
            Document document,
            Email email,
            Address address) : base(
                paidAt,
                expiredAt,
                total,
                totalPaid,
                holder,
                document,
                email,
                address)
        {
            TransactionId = transactionId;
        }
        public string TransactionId { get; private set; }

    }
}