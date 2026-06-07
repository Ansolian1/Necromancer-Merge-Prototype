using UnityEngine;
using TMPro;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Animate(string message, Color color)
    {
        _text.text = message;
        _text.color = color;

        transform.DOMoveY(transform.position.y + 1.5f, 1f).SetEase(Ease.OutQuad);


        DOTween.To(() => _text.color, x => _text.color = x, new Color(color.r, color.g, color.b, 0f), 1f)
            .SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
