namespace CarteiraDigital.Domain.Exceptions;

public class InvalidWalletAmountException : DomainException
{
    public InvalidWalletAmountException()
        : base("O valor deve ser maior que zero.")
    {
    }
}