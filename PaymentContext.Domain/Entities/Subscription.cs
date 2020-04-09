using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expiredAt)
        {
            Active = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            ExpiredAt = expiredAt;
            _payments = new List<Payment>();
        }

        public bool Active { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? ExpiredAt { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void addPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidAt, "Subscription.Payments", "A data de pagamento deve ser futura")

            );

            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            UpdatedAt = DateTime.Now;
        }
        public void Inactivate()
        {
            Active = false;
            UpdatedAt = DateTime.Now;
        }
    }
}