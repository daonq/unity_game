using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public GameObject bg;
    public Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(delegate
        {
            panel.SetActive(false);
            bg.transform.localScale = Vector3.zero;
            if (DataGlobal.instance.ClickObject)
            {
                DataGlobal.instance.ClickObject = false;
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
            }
        });
    }
}
