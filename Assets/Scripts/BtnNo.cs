using UnityEngine;
using UnityEngine.UI;

public class BtnNo : MonoBehaviour
{
    public GameObject PanelExitGame;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            PanelExitGame.transform.localScale = Vector3.zero;
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
            DataGlobal.instance.ClickObject = false;
        });
    }
}
