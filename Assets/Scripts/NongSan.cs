using UnityEngine;
using UnityEngine.UI;

public class NongSan : MonoBehaviour
{
    [SerializeField] DetailNongSan nongSan;
    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnClickNongSan(nongSan);
            }
        });

        if (toggle.isOn)
        {
            UIManager.instance.OnClickNongSan(nongSan);
        }
    }
}

[System.Serializable]
public class DetailNongSan
{
    public int id;
    public string title;
    public Sprite icon;
    public int unitPrice;
}
