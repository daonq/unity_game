using UnityEngine;
using UnityEngine.UI;

public class HandlerGift : MonoBehaviour
{
    [SerializeField] private Image panel;

    private void Awake()
    {
        GetComponent<ButtonGift>().OnGift += delegate
        {
            DataGlobal.instance.AllowMouseDown = false;
            MainCamera.instance.camLock = true;
            panel.gameObject.SetActive(true);
        };
    }
}
