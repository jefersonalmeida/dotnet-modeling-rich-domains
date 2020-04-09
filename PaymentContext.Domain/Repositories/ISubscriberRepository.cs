using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface ISubscriberRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Subscriber subscriber);
    }
}