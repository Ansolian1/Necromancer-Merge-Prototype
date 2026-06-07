using System;

public interface IWalletService
{
    int Souls { get; }
    event Action<int> OnBalanceChanged;
    event Action OnNotEnoughFunds;

    void Add(int amount);
    bool TrySpend(int amount);
    bool HasEnough(int amount);
}
public class WalletService : IWalletService
{
    private int _souls;

    public int Souls => _souls;

    public event Action<int> OnBalanceChanged;
    public event Action OnNotEnoughFunds;
    public WalletService(int startingSouls)
    {
        _souls = startingSouls;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;

        _souls += amount;
        OnBalanceChanged?.Invoke(_souls);
    }

    public bool HasEnough(int amount)
    {
        return _souls >= amount;
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0) return false;

        if (HasEnough(amount))
        {
            _souls -= amount;
            OnBalanceChanged?.Invoke(_souls);
            return true;
        }
        else
        {
            OnNotEnoughFunds?.Invoke();
            return false;
        }
    }
}
