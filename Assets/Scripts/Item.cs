using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public DetailItem item;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnSelectItem(item);
            }
        });

        if (toggle.isOn)
        {
            UIManager.instance.OnSelectItem(item);
        }
    }
}

[System.Serializable]
public class DetailItem
{
    public int id;
    public string des;
    public string desVN;
    public Sprite icon;
    public int unitPrice;
}
