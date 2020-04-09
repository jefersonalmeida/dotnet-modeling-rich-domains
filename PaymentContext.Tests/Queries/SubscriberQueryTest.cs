using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriberQueryTest
    {
        // Red, Green, Refactor
        private IList<Subscriber> _subscribers;

        public SubscriberQueryTest()
        {
            _subscribers = new List<Subscriber>();
            for (int i = 0; i < 10; i++)
            {
                _subscribers.Add(new Subscriber(
                    new Name("Assinante", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "-email@test.com"))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = SubscriberQuery.GetSubscriber("12345678911");
            var subscriber = _subscribers.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, subscriber);
        }

        [TestMethod]
        public void ShouldReturnSubscriberWhenDocumentExists()
        {
            var exp = SubscriberQuery.GetSubscriber("11111111111");
            var subscriber = _subscribers.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, subscriber);
        }
    }
}