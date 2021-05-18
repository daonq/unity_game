using UnityEngine;
using UnityEngine.UI;

public class ItemHouse : MonoBehaviour
{
    [SerializeField] DetailHouse house;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnClickItemHouse(house);
            }
        });
    }
}

[System.Serializable]
public class DetailHouse
{
    public int id;
    public Sprite imgHouse;
    public int levelRQ;
    public int goldRQ;
    public int oilRQ;
}
