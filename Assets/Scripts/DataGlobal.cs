using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DataGlobal : MonoBehaviour
{
    public static DataGlobal instance;

    private void Awake()
    {
        instance = this;
        tiengviet = Application.systemLanguage == SystemLanguage.Vietnamese ? true : false;
        ArrayAmount = new int[17];
        ArrayHaveOwnedItem = new int[3];
        levelCurrentOfFactory = new int[10];
        _level = PlayerPrefs.GetInt("level", 1);
        _gold = PlayerPrefs.GetInt("gold", 111);
        _water = PlayerPrefs.GetInt("water", 100);
        _stone = PlayerPrefs.GetInt("stone", 120);
        _wood = PlayerPrefs.GetInt("wood", 120);
        _star = PlayerPrefs.GetInt("star", 0);
        _oil = PlayerPrefs.GetInt("oil", 0);
        _levelHouse = PlayerPrefs.GetInt("levelHouse", 1);

        for (int i = 0; i < ArrayAmount.Length; i++)
        {
            ArrayAmount[i] = PlayerPrefs.GetInt("arrayAmount" + i);
        }

        for (int i = 0; i < levelCurrentOfFactory.Length; i++)
        {
            levelCurrentOfFactory[i] = PlayerPrefs.GetInt("levelCurrentOfFactory" + i);
        }

        for (int i = 0; i < ArrayHaveOwnedItem.Length; i++)
        {
            ArrayHaveOwnedItem[i] = PlayerPrefs.GetInt("arrayHaveOwnedItem" + i);
        }
        _firstGame = PlayerPrefs.GetInt("FirstGame");
    }

    private int _firstGame;

    public int GetFirstGame()
    {
        return _firstGame;
    }

    public void SetFirstGame(int firstGame)
    {
        _firstGame = firstGame;
    }

    private void Start()
    {
        img_slide.fillAmount = ((float)_star / (float)levelContain[_level - 1].star);
        txtGold.text = _gold.ToString();
        txtWood.text = _wood.ToString();
        txtLevel.text = _level.ToString();
        txtStone.text = _stone.ToString();
        txtWater.text = _water.ToString();
        txtOil.text = _oil.ToString();

        UpdateDataAmount();
    }

#if UNITY_EDITOR || UNITY_IOS
    private void OnApplicationQuit()
    {
        Debug.Log("QUIT");
        PlayerPrefs.SetInt("level", _level);
        PlayerPrefs.SetInt("gold", _gold);
        PlayerPrefs.SetInt("water", _water);
        PlayerPrefs.SetInt("stone", _stone);
        PlayerPrefs.SetInt("wood", _wood);
        PlayerPrefs.SetInt("star", _star);
        PlayerPrefs.SetInt("oil", _oil);
        PlayerPrefs.SetInt("levelHouse", _levelHouse);
        PlayerPrefs.SetInt("FirstGame", _firstGame);

        for (int i = 0; i < ArrayAmount.Length; i++)
        {
            PlayerPrefs.SetInt("arrayAmount" + i, ArrayAmount[i]);
        }

        for (int i = 0; i < levelCurrentOfFactory.Length; i++)
        {
            PlayerPrefs.SetInt("levelCurrentOfFactory" + i, levelCurrentOfFactory[i]);
        }

        for (int i = 0; i < ArrayHaveOwnedItem.Length; i++)
        {
            PlayerPrefs.SetInt("arrayHaveOwnedItem" + i, ArrayHaveOwnedItem[i]);
        }
    }
#elif UNITY_ANDROID
    private void OnApplicationPause()
    {
        Debug.Log("PAUSE");
        PlayerPrefs.SetInt("level", _level);
        PlayerPrefs.SetInt("gold", _gold);
        PlayerPrefs.SetInt("water", _water);
        PlayerPrefs.SetInt("stone", _stone);
        PlayerPrefs.SetInt("wood", _wood);
        PlayerPrefs.SetInt("star", _star);
        PlayerPrefs.SetInt("oil", _oil);
        PlayerPrefs.SetInt("levelHouse", _levelHouse);
        PlayerPrefs.SetInt("FirstGame", firstGame);

        for (int i = 0; i < ArrayAmount.Length; i++)
        {
            PlayerPrefs.SetInt("arrayAmount" + i, ArrayAmount[i]);
        }

        for (int i = 0; i < levelCurrentOfFactory.Length; i++)
        {
            PlayerPrefs.SetInt("levelCurrentOfFactory" + i, levelCurrentOfFactory[i]);
        }

        for (int i = 0; i < ArrayHaveOwnedItem.Length; i++)
        {
            PlayerPrefs.SetInt("arrayHaveOwnedItem" + i, ArrayHaveOwnedItem[i]);
        }
    }
#endif
    public bool ClickObject;

    public GameObject goldUI;

    public Text txtLevel;
    //public Text txtStar;
    public Text txtGold;
    public Text txtWood;
    public Text txtStone;
    public Text txtWater;
    public Text txtOil;

    public bool tiengviet;

    public Text[] listTextAmount;

    public bool AllowMouseDown; // Cho phep click vao object trong scene
    [SerializeField] private int _level; // Level hien tai cua nguoi choi
    [SerializeField] private int _gold; // Vang hien tai cua nguoi choi
    [SerializeField] private int _water; // Nuoc hien tai cua nguoi choi
    [SerializeField] private int _stone; // Da hien tai cua nguoi choi
    [SerializeField] private int _wood; // Go hien tai cua nguoi choi
    [SerializeField] private int _star;
    [SerializeField] private int _oil;
    [SerializeField] private int _levelHouse;
    public GameObject[] ArrayLand; // Danh sach o dat
    [HideInInspector] public int[] ArrayAmount; // So luong nong san trong kho
    public GameObject[] ArrayLandFactory; // Danh sach nha may
    public int[] ArrayHaveOwnedItem; // Danh sach item dang so huu
    public GameObject[] ArrayChuong;

    public LevelContain[] levelContain;

    public int[] levelCurrentOfFactory;
    public GameObject RoiIcon;
    public GameObject SeedObject;

    public List<DetailSeed> listSeed = new List<DetailSeed>();
    public List<DetailVatnuoi> listVatNuoi = new List<DetailVatnuoi>();
    public List<DetailFactory> listFactory = new List<DetailFactory>();
    public List<Sprite> listSeedsLucGieoHat = new List<Sprite>();

    public Sprite sprOdat;

    public List<Sprite> iconThuHoachFactory = new List<Sprite>();
    public List<Material> listMatFactory = new List<Material>();

    public int sound; // 0: on, 1: off

    public Image img_slide;

    public List<ContentOfHouse> listContentOFHOUSE = new List<ContentOfHouse>();

    public void UpdateDataAmount()
    {
        for (int i = 0; i < ArrayAmount.Length; i++)
        {
            listTextAmount[i].text = ArrayAmount[i].ToString();
        }
    }

    public void AddStar(int star)
    {
        _star += star;
        img_slide.fillAmount = ((float)_star / (float)levelContain[_level - 1].star);
        if(_star >= levelContain[_level - 1].star)
        {
            _level++;
            _star = 0;
            txtLevel.text = _level.ToString();

            for (int i = 0; i < ArrayLand.Length; i++)
            {
                if(_level >= ArrayLand[i].GetComponent<Land>().levelUnlock)
                {
                    ArrayLand[i].GetComponent<Land>().GetComponent<SpriteRenderer>().sprite = sprOdat;
                }
            }

            for (int i = 0; i < ArrayChuong.Length; i++)
            {
                if(_level >= ArrayChuong[i].GetComponent<Chuong>().levelUnlock)
                {
                    ArrayChuong[i].GetComponent<Chuong>().effect.SetActive(true);
                }
            }

            for (int i = 0; i < ArrayLandFactory.Length; i++)
            {
                if(_level >= ArrayLandFactory[i].GetComponent<LandFactory>().levelUnlock)
                {
                    ArrayLandFactory[i].GetComponent<LandFactory>().effect.SetActive(true);
                }
            }
            UIManager.instance.ShowPanelHouse();
        }
        img_slide.fillAmount = ((float)_star / (float)levelContain[_level - 1].star);
    }

    public void AddOil(int oil)
    {
        _oil += oil;
        txtOil.text = _oil.ToString();
    }

    public void SubOil(int oil)
    {
        _oil -= oil;
    }

    public int GetOil()
    {
        return _oil;
    }

    public void AddLevelHouse()
    {
        _levelHouse++;
    }

    public int GetLevelHouse()
    {
        return _levelHouse;
    }

    public void AddLevel()
    {
        _level++;
        txtLevel.text = _level.ToString();
    }

    public int GetLevel()
    {
        return _level;
    }

    public void AddGold(int gold)
    {
        _gold += gold;
        txtGold.text = _gold.ToString();
    }

    public void SubGold(int gold)
    {
        _gold -= gold;
        txtGold.text = _gold.ToString();
    }

    public int GetGold()
    {
        return _gold;
    }

    public void AddWood(int wood)
    {
        _wood += wood;
        txtWood.text = _wood.ToString();
    }

    public void SubWood(int wood)
    {
        _wood -= wood;
        txtWood.text = _wood.ToString();
    }

    public int GetWood()
    {
        return _wood;
    }

    public void AddWater(int water)
    {
        _water += water;
        txtWater.text = _water.ToString();
    }

    public void SubWater(int water)
    {
        _water -= water;
        txtWater.text = _water.ToString();
    }

    public int GetWater()
    {
        return _water;
    }

    public void AddStone(int stone)
    {
        _stone += stone;
        txtStone.text = _stone.ToString();
    }

    public void SubStone(int stone)
    {
        _stone -= stone;
        txtStone.text = _stone.ToString();
    }

    public int GetStone()
    {
        return _stone;
    }
}

[System.Serializable]
public class LevelContain
{
    public int level;
    public int star;
    public int wood;
    public int stone;
    public int oil;
    public int water;
}

[System.Serializable]
public struct ContentOfHouse
{
    public string ct1;
    public string ct1VN;
    public string ct2;
    public string ct2VN;
    public string ct3;
    public string ct3VN;
    public string ct4;
    public string ct4VN;
}