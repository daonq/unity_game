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
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        gameObject.transform.DOScale(1, 0.2f).SetEase(Ease.Flash);
        content.text = str;
    }
}
