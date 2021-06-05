using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] DetailSeed seed;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnSelectSeed(seed);
            }
        });

        if (toggle.isOn)
        {
            UIManager.instance.OnSelectSeed(seed);
        }
    }
}

[System.Serializable]
public class DetailSeed
{
    public int id;
    public string title;
    public string titleVN;
    public int gold;
    public int exp;
    public int time;
    public string nameProduct;
    public string nameProductVN;
    public int levelUnlock;
    public Sprite spr1;
    public Sprite spr2;
    public Sprite spr3;
    public Sprite spr4;
    public Material mat;
}
