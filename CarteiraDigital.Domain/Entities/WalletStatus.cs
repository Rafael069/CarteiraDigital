namespace CarteiraDigital.Domain.Entities;

/// <summary>
/// Status 1 - Indica que a carteira está habilitada para operações.
/// Status 2 - Indica que a carteira está desabilitada para operações.
/// </summary>

public enum WalletStatus
{
    Ativa = 1,
    Inativa = 2
}