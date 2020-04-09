using System;
using System.Linq.Expressions;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Queries
{
    public static class SubscriberQuery
    {
        public static Expression<Func<Subscriber, bool>> GetSubscriber(string document)
        {
            return x => x.Document.Number == document;
        }
    }
}