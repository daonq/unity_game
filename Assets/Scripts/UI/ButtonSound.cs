using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Button btn_sound;

    public event Action OnSound = delegate { };

    private void Start()
    {
        btn_sound.onClick.AddListener(delegate
        {
            OnSound();
        });
    }
}
