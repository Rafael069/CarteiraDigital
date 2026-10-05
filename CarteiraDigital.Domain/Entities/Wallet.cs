namespace CarteiraDigital.Domain.Entities;

public class Wallet
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Saldo { get; private set; }
    public WalletStatus Status { get; private set; }

    public Wallet(Guid clienteId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        Saldo = 0;
        Status = WalletStatus.Ativa;
    }
}