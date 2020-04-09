using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{

    [TestClass]
    public class SubscriberTest
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Subscriber _subscriber;
        public SubscriberTest()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("10465166040", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Rua", "123", "Complemento", "Bairro", "Cidade", "MT", "BRA", "78000000");
            _subscriber = new Subscriber(_name, _document, _email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PixPayment(
                "12345678",
                DateTime.Now,
                DateTime.Now.AddDays(5),
                10,
                10,
                "WAYNE CORP",
                _document,
                _email,
                _address);

            subscription.addPayment(payment);
            _subscriber.AddSubscription(subscription);
            _subscriber.AddSubscription(subscription);

            Assert.IsTrue(_subscriber.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _subscriber.AddSubscription(subscription);
            Assert.IsTrue(_subscriber.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PixPayment(
                "12345678",
                DateTime.Now,
                DateTime.Now.AddDays(5),
                10,
                10,
                "Wayne Corp",
                _document,
                _email,
                _address);

            subscription.addPayment(payment);
            _subscriber.AddSubscription(subscription);
            Assert.IsTrue(_subscriber.Valid);
        }
    }
}