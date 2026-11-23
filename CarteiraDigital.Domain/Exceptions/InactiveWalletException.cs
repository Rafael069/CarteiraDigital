namespace CarteiraDigital.Domain.Exceptions;

public class InactiveWalletException : DomainException
{
    public InactiveWalletException()
        : base("Não é possível realizar operações em uma carteira inativa.")
    {
    }
}