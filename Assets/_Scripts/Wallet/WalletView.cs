using UnityEngine;
using TMPro;
using DG.Tweening;
public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _soulsText;
    [SerializeField] private RectTransform _walletIcon;

    private IWalletService _wallet;

    private void Start()
    {
        _wallet = ServiceLocator.Get<IWalletService>();
        _wallet.OnBalanceChanged += UpdateUI;
        _wallet.OnNotEnoughFunds += PlayDenialAnimation;
        UpdateUI(_wallet.Souls);
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            _wallet.OnBalanceChanged -= UpdateUI;
            _wallet.OnNotEnoughFunds -= PlayDenialAnimation;
        }
    }
    private void UpdateUI(int currentSouls)
    {
        _soulsText.text = currentSouls.ToString();
        DOTween.Kill(_soulsText.transform);
        _soulsText.transform.localScale = Vector3.one;
        _soulsText.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 0.3f, 5, 0.5f);
    }

    private void PlayDenialAnimation()
    {
        if (_walletIcon != null)
        {
            DOTween.Kill(_walletIcon);
            _walletIcon.localRotation = Quaternion.identity;
            _walletIcon.DOPunchRotation(new Vector3(0, 0, 30f), 0.4f, 10, 1f);
        }
    }
}
