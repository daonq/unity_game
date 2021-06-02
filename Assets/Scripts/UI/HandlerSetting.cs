using UnityEngine;
using UnityEngine.UI;

public class HandlerSetting : MonoBehaviour
{
    [SerializeField] private Image backGround;

    private void Awake()
    {
        GetComponent<ButtonSetting>().OnSetting += delegate
        {
            backGround.gameObject.SetActive(!backGround.gameObject.activeSelf);
        };
    }
}
