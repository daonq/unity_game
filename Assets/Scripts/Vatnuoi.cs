using UnityEngine;
using UnityEngine.UI;

public class Vatnuoi : MonoBehaviour
{
    [SerializeField] DetailVatnuoi vatnuoi;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnClickVatnuoi(vatnuoi);
            }
        });
    }
}

[System.Serializable]
public class DetailVatnuoi
{
    public int id;
    public string title;
    public Sprite imageChuong1;
    public Sprite imageChuong2;
    public Sprite imageVatnuoi;
    public int gold;
    public string harvest;
    public int time;
    public int rewards;
    public int levelUnlock;
}
