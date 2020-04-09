using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName,
            string cardNumber,
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
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            TransactionId = transactionId;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string TransactionId { get; private set; }
    }
}