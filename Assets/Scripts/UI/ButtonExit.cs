using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonExit : MonoBehaviour
{
    [SerializeField] private Button btn_exit;

    public event Action OnExit = delegate { };

    private void Start()
    {
        btn_exit.onClick.AddListener(delegate
        {
            OnExit();
        });
    }
}
