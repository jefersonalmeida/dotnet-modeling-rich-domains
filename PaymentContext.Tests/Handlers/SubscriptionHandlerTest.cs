using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        // Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeSubscriberRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "email@test.com";
            command.BarCode = "1234567890";
            command.BoletoNumber = "123";
            command.PaymentNumber = "0001234567890";
            command.PaidAt = DateTime.Now;
            command.ExpiredAt = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;

            command.PayerHolder = "WAYNE CORP";
            command.PayerDocument = "99999999999";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";

            command.Street = "Rua";
            command.Number = "Numero";
            command.Complement = "Complemento";
            command.Neighborhood = "Bairro";
            command.City = "Cuiabá";
            command.State = "MT";
            command.Country = "BRA";
            command.ZipCode = "78000000";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}