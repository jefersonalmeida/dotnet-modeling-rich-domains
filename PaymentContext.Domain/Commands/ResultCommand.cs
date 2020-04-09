using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class ResultCommand : ICommandResult
    {
        public ResultCommand()
        {

        }

        public ResultCommand(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}