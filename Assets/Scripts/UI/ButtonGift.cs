using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonGift : MonoBehaviour
{
    [SerializeField] private Button btn_gift;

    public event Action OnGift = delegate { };

    private void Start()
    {
        btn_gift.onClick.AddListener(delegate
        {
            OnGift();
        });
    }
}
