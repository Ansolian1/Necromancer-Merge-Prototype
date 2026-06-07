
using NUnit.Framework;

public class WalletServiceTests
{
    [Test]
    public void TrySpend_WithEnoughSouls_ReturnsTrueAndReducesBalance()
    {
        // 1. Arrange: Создаем кошелек с 100 душами на старте
        WalletService wallet = new WalletService(100);

        // 2. Act: Пытаемся потратить 40 душ
        bool result = wallet.TrySpend(40);

        // 3. Assert: Проверяем, что метод разрешил трату, и остаток стал 60
        Assert.IsTrue(result); // Ожидаем TRUE
        Assert.AreEqual(60, wallet.Souls); // Ожидаем, что 100 - 40 = 60
    }

    [Test]
    public void TrySpend_WithNotEnoughSouls_ReturnsFalseAndKeepsBalance()
    {
        // 1. Arrange: Создаем кошелек бомжа (10 душ)
        WalletService wallet = new WalletService(10);

        // 2. Act: Пытаемся купить крутого скелета за 50
        bool result = wallet.TrySpend(50);

        // 3. Assert: Метод должен отказать, а баланс должен остаться 10
        Assert.IsFalse(result);
        Assert.AreEqual(10, wallet.Souls);
    }

    [Test]
    public void Add_NegativeAmount_IsIgnored()
    {
        // 1. Arrange: 50 душ
        WalletService wallet = new WalletService(50);

        // 2. Act: Пытаемся прибавить отрицательное число, чтобы сломать игру
        wallet.Add(-100);

        // 3. Assert: Кошелек должен проигнорировать это, баланс всё еще 50
        Assert.AreEqual(50, wallet.Souls);
    }
}
