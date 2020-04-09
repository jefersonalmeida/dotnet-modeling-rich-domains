using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>,
    IHandler<CreatePixSubscriptionCommand>
    {
        private readonly ISubscriberRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(ISubscriberRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");
            }

            // verificar se documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            // verificar se email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Complement,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );

            // Gerar as entidades
            var subscriber = new Subscriber(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidAt,
                command.ExpiredAt,
                command.Total,
                command.TotalPaid,
                command.PayerHolder,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                address
            );
            // Relacionamentos
            subscription.addPayment(payment);
            subscriber.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, subscriber, subscription, payment);
            if (Invalid)
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(subscriber);

            // Enviar e-mail de boas-vindas
            _emailService.Send(
                subscriber.Name.ToString(),
                subscriber.Email.Address,
                "Bem-vindo ao sistema .net core",
                "Sua assinatura foi criada"
            );
            // Retornar informações
            return new ResultCommand(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");
            }

            // verificar se documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            // verificar se email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Complement,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );

            // Gerar as entidades
            var subscriber = new Subscriber(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.TransactionId,
                command.PaidAt,
                command.ExpiredAt,
                command.Total,
                command.TotalPaid,
                command.PayerHolder,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                address
            );
            // Relacionamentos
            subscription.addPayment(payment);
            subscriber.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, subscriber, subscription, payment);
            if (Invalid)
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(subscriber);

            // Enviar e-mail de boas-vindas
            _emailService.Send(
                subscriber.Name.ToString(),
                subscriber.Email.Address,
                "Bem-vindo ao sistema .net core",
                "Sua assinatura foi criada"
            );
            // Retornar informações
            return new ResultCommand(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePixSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");
            }

            // verificar se documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            // verificar se email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Complement,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );

            // Gerar as entidades
            var subscriber = new Subscriber(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PixPayment(
                command.TransactionId,
                command.PaidAt,
                command.ExpiredAt,
                command.Total,
                command.TotalPaid,
                command.PayerHolder,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                address
            );
            // Relacionamentos
            subscription.addPayment(payment);
            subscriber.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, subscriber, subscription, payment);
            if (Invalid)
                return new ResultCommand(false, "Não foi possível realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(subscriber);

            // Enviar e-mail de boas-vindas
            _emailService.Send(
                subscriber.Name.ToString(),
                subscriber.Email.Address,
                "Bem-vindo ao sistema .net core",
                "Sua assinatura foi criada"
            );
            // Retornar informações
            return new ResultCommand(true, "Assinatura realizada com sucesso");
        }
    }
}