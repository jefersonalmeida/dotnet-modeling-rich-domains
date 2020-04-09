using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment(
            DateTime paidAt,
            DateTime expiredAt,
            decimal total,
            decimal totalPaid,
            string holder,
            Document document,
            Email email,
            Address address)
        {
            Number = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            PaidAt = paidAt;
            ExpiredAt = expiredAt;
            Total = total;
            TotalPaid = totalPaid;
            Holder = holder;
            Document = document;
            Email = email;
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor do pagamento")
            );
        }

        public string Number { get; private set; }
        public DateTime PaidAt { get; private set; }
        public DateTime ExpiredAt { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public string Holder { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
    }
}