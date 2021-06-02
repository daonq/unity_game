using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            panel.transform.DOScale(0, 0.5f).SetEase(Ease.Flash);
        });
    }
}
