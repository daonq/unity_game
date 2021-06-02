using UnityEngine;
using TMPro;
using DG.Tweening;

public class PanelNotify : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI content;

    public static PanelNotify instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowContent(string str)
    {
        gameObject.transform.DOScale(1, 0.5f).SetEase(Ease.Flash);
        content.text = str;
    }
}
