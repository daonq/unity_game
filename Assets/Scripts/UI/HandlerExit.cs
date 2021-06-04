using UnityEngine;

public class HandlerExit : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject PanelExitGame;

    private void Awake()
    {
        GetComponent<ButtonExit>().OnExit += delegate
        {
            Background.SetActive(false);   
            PanelExitGame.transform.localScale = new Vector3(1, 1, 1);
            DataGlobal.instance.AllowMouseDown = false;
            MainCamera.instance.camLock = true;
            DataGlobal.instance.ClickObject = true;
        };
    }
}
