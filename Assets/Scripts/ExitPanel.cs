using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            if (DataGlobal.instance.ClickObject)
            {
                DataGlobal.instance.ClickObject = false;
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
            }
            panel.transform.localScale = Vector3.zero;
        });
    }
}
