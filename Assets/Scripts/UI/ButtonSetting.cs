using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonSetting : MonoBehaviour
{
    [SerializeField] private Button btn_setting;

    public event Action OnSetting = delegate { };

    private void Start()
    {
        btn_setting.onClick.AddListener(delegate
        {
            OnSetting();
        });
    }
}
