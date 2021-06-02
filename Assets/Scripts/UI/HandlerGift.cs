using UnityEngine;
using UnityEngine.UI;

public class HandlerGift : MonoBehaviour
{
    [SerializeField] private Image panel;

    private void Awake()
    {
        GetComponent<ButtonGift>().OnGift += delegate
        {
            panel.gameObject.SetActive(true);
        };
    }
}
