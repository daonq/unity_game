using UnityEngine;
using UnityEngine.UI;

public class ExitGift : MonoBehaviour
{
    public Image panelGift;
    private void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(delegate
        {
            panelGift.gameObject.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });
    }
}
