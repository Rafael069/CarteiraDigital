using CarteiraDigital.Domain.Exceptions;

namespace CarteiraDigital.Domain.Entities;
/// <summary>
/// Representa uma carteira digital de um cliente.
/// Responsável por manter o saldo e controlar as operações
/// financeiras respeitando as regras de negócio do domínio.
/// </summary>

public class Wallet
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Saldo { get; private set; }
    public WalletStatus Status { get; private set; }


    /// <summary>
    /// Inicializa uma nova carteira para o cliente informado.
    /// </summary>
    /// <param name="clienteId">Identificador do cliente proprietário.</param>
    public Wallet(Guid clienteId)
    {
        if (clienteId == Guid.Empty)
            throw new ArgumentException(
                "O identificador do cliente é obrigatório.",
                nameof(clienteId));

        Id = Guid.NewGuid();
        ClienteId = clienteId;
        Saldo = 0;
        Status = WalletStatus.Ativa;
    }

    /// <summary>
    /// Adiciona um valor ao saldo da carteira.
    /// </summary>
    public void Depositar(decimal valor)
    {
        ValidarCarteiraAtiva();
        ValidarValor(valor);

        Saldo += valor;
    }

    /// <summary>
    /// Retira um valor do saldo da carteira.
    /// </summary>
    public void Sacar(decimal valor)
    {
        if (valor > Saldo)
            throw new InsufficientBalanceException();
    }

    /// <summary>
    /// Verifica se a carteira está ativa para realizar operações.
    /// </summary>
    private void ValidarCarteiraAtiva()
    {
        if (Status != WalletStatus.Ativa)
            throw new InactiveWalletException();
    }

    /// <summary>
    /// Verifica se o valor informado é maior que zero.
    /// </summary>
    private void ValidarValor(decimal valor)
    {
        if (valor <= 0)
            throw new InvalidWalletAmountException();
    }

    /// <summary>
    /// Ativa a carteira digital.
    /// </summary>
    public void Ativar()
    {
        Status = WalletStatus.Ativa;
    }

    /// <summary>
    /// Inativa a carteira digital.
    /// </summary>
    public void Inativar()
    {
        Status = WalletStatus.Inativa;
    }
}