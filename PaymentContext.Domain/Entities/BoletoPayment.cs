using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barCode,
            string boletoNumber,
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
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}