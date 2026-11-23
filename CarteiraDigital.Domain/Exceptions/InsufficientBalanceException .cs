namespace CarteiraDigital.Domain.Exceptions;

public class InsufficientBalanceException : DomainException
{
    public InsufficientBalanceException()
        : base("Saldo insuficiente para realizar o saque.")
    {
    }
}