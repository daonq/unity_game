using UnityEngine;

public class HandlerExit : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<ButtonExit>().OnExit += delegate
        {
            Application.Quit();
        };
    }
}
