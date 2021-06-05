using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PanelNotify : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI content;

    public static PanelNotify instance;
    public GameObject panel;
    public GameObject bg;

    private void Awake()
    {
        instance = this;
    }

    public void ShowContent(string str)
    {
        content.text = str;
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        panel.SetActive(true);
        bg.transform.DOScale(1, 0.2f).SetEase(Ease.Flash);
    }
}
