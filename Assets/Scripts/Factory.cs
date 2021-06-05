using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    [SerializeField] DetailFactory factory;
    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnSelectFactory(factory);
            }
        });

        if (toggle.isOn)
        {
            UIManager.instance.OnSelectFactory(factory);
        }
    }
}

[System.Serializable]
public class DetailFactory
{
    public int id;
    public string title;
    public string titleVN;
    public string rawMat;
    public string rawMatVN;
    public int levelUnlock;
    public int gold;
    public int time1;
    public int count1;
    public int time2;
    public int count2;
    public int time3;
    public int count3;
    public int water;
    public int stone;
    public int wood;
    public string nameProduct;
    public string nameProductVN;
    public Sprite sp0;
    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;

    public int level2;
    public int gold2;
    public int mainhouse2;

    public int level3;
    public int gold3;
    public int mainhouse3;
}
