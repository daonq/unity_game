using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Text thongbao;

    public void Hienthongbao(string mes)
    {
        thongbao.text = mes;
        thongbao.gameObject.SetActive(true);
        thongbao.GetComponent<RectTransform>().DOMove(thongbao.transform.position + new Vector3(0, 300, 0), 1f);
        //StartCoroutine(thongbaoText());
    }

    public void AnThongBao()
    {
        StartCoroutine(ANTHONGBAO());
    }

    IEnumerator ANTHONGBAO()
    {
        yield return new WaitForSeconds(1);
        thongbao.text = "";
        thongbao.gameObject.SetActive(false);
        thongbao.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    /*
    IEnumerator thongbaoText()
    {
        thongbao.gameObject.SetActive(true);
        thongbao.GetComponent<RectTransform>().DOMove(thongbao.transform.position + new Vector3(0, 300, 0), 2f);
        yield return new WaitForSeconds(2f);
        thongbao.gameObject.SetActive(false);
        thongbao.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }*/

    private void Start()
    {
        AddListenForPanelSeeds();
        AddListnerForPS();
        AddListenerForPanelBan();
        AddListenForPanelFactory1();
        AddListenForPanelWaiting();
        AddListenerToPanelBuild();
        AddListenerForPanelChuong();
        AddEventListnerForPanelHouse();
    }

    #region PANEL SEED

    public GameObject PanelSeeds;
    public TextMeshProUGUI title;
    public TextMeshProUGUI exp;
    public TextMeshProUGUI time;
    public TextMeshProUGUI product;
    public TextMeshProUGUI levelUL;
    public TextMeshProUGUI gold;
    public Button btnBuy;
    public Button btnExit;
    public Image image;
    private int idLand;
    private DetailSeed seedClick;

    public void AddListenForPanelSeeds()
    {
        btnExit.onClick.AddListener(delegate
        {
            PanelSeeds.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        btnBuy.onClick.AddListener(delegate
        {
            if (DataGlobal.instance.GetLevel() >= seedClick.levelUnlock && DataGlobal.instance.GetGold() >= seedClick.gold)
            {
                PanelSeeds.SetActive(false);
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
                DataGlobal.instance.SubGold(seedClick.gold);
                DataGlobal.instance.ArrayLand[idLand].GetComponent<Land>().Seeded(seedClick.id, seedClick.time, seedClick.spr1, seedClick.spr2, seedClick.spr3, seedClick.exp, seedClick.spr4, seedClick.mat);
            }
            else if (DataGlobal.instance.GetLevel() >= seedClick.levelUnlock && DataGlobal.instance.GetGold() < seedClick.gold)
            {
                Debug.Log("Ban chua du Gold de mua seed nay!");
            }
            else if (DataGlobal.instance.GetLevel() < seedClick.levelUnlock)
            {
                Debug.Log("Ban chua du Level de mua seed nay!");
            }
        });
    }

    public void OnClickToLand(int id)
    {
        idLand = id;
        PanelSeeds.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
    }

    public void OnSelectSeed(DetailSeed seed)
    {
        seedClick = seed;
        title.text = seed.title;
        exp.text = "Experience: " + seed.exp;
        time.text = "Time: " + seed.time;
        product.text = "Product: 5 " + seed.nameProduct;
        levelUL.text = "Level unlock: " + seed.levelUnlock;
        gold.text = "" + seed.gold;
        image.sprite = seed.spr3;
    }

    #endregion


    #region PANEL FACTORY 1

    public GameObject PanelFactory1;
    public TextMeshProUGUI titleF1;
    public TextMeshProUGUI goldF1;
    public TextMeshProUGUI rawMatF1;
    public TextMeshProUGUI timeF1;
    public TextMeshProUGUI productF1;
    public Button btnBuyF1;
    public Button btnExitF1;
    public Image imageF1;
    private int idFactory; // Vi tri nha may
    private DetailFactory _factory1;

    public void AddListenForPanelFactory1()
    {
        btnExitF1.onClick.AddListener(delegate
        {
            PanelFactory1.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        btnBuyF1.onClick.AddListener(delegate
        {
            if ((_factory1.id == 4 && !(idFactory == 5 || idFactory == 6 || idFactory == 7)) ||
                (_factory1.id != 4 && (idFactory == 5 || idFactory == 6 || idFactory == 7)))
            {

            }
            else if (DataGlobal.instance.GetGold() >= _factory1.gold)
            {
                PanelFactory1.SetActive(false);
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
                DataGlobal.instance.SubGold(_factory1.gold);
                DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnWaiting(_factory1);
            }
            else if (DataGlobal.instance.GetGold() < _factory1.gold)
            {
                Debug.Log("Ban khong du GOLD de xay nha may nay!");
            }
        });
    }

    public void OnClickToFactory(int id)
    {
        idFactory = id;
        PanelFactory1.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
    }

    public void OnSelectFactory(DetailFactory factory)
    {
        _factory1 = factory;
        titleF1.text = factory.title;
        goldF1.text = "" + factory.gold;
        rawMatF1.text = "Raw materials: " + factory.rawMat;
        timeF1.text = "Time: " + factory.time1 + "s/" + factory.nameProduct;
        productF1.text = "Product: " + factory.nameProduct;
        imageF1.sprite = factory.sp3;
    }

    #endregion


    #region PANEL WAITING
    public GameObject PanelWaiting;
    public TextMeshProUGUI textWater;
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textWood;
    public Button btnBuild;
    public Button btnExitPW;

    public Image du1W;
    public Image du2W;
    public Image du3W;

    public void AddListenForPanelWaiting()
    {
        btnExitPW.onClick.AddListener(delegate
        {
            PanelWaiting.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        btnBuild.onClick.AddListener(delegate
        {
            if (DataGlobal.instance.GetWater() >= _factory1.water && DataGlobal.instance.GetStone() >= _factory1.stone && DataGlobal.instance.GetWood() >= _factory1.wood)
            {
                PanelWaiting.SetActive(false);
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
                DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnBuild();
                DataGlobal.instance.SubWater(_factory1.water);
                DataGlobal.instance.SubStone(_factory1.stone);
                DataGlobal.instance.SubWood(_factory1.wood);
            }
            else
            {
                Debug.Log("Ban khong du tai nguyen de nang cap!");
            }
        });
    }

    public void OnClickToWaiting(DetailFactory factoryWating)
    {
        _factory1 = factoryWating;
        PanelWaiting.SetActive(true);
        textWater.text = "" + factoryWating.water;
        textStone.text = "" + factoryWating.stone;
        textWood.text = "" + factoryWating.water;
        if(DataGlobal.instance.GetWater() >= factoryWating.water)
        {
            du1W.gameObject.SetActive(true);
        }
        else
        {
            du1W.gameObject.SetActive(false);
        }
        if(DataGlobal.instance.GetStone() >= factoryWating.stone)
        {
            du2W.gameObject.SetActive(true);
        } else
        {
            du2W.gameObject.SetActive(false);
        }
        if(DataGlobal.instance.GetWood() >= factoryWating.wood)
        {
            du3W.gameObject.SetActive(true);
        }
        else
        {
            du3W.gameObject.SetActive(false);
        }
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
    }

    #endregion

    #region PANEL BUILD

    public GameObject PanelBuild;
    public TextMeshProUGUI titlePB1;
    public TextMeshProUGUI titlePB2;
    public TextMeshProUGUI levelPB;
    public TextMeshProUGUI mainHousePB;
    public TextMeshProUGUI goldPB;
    public Image imagePB;
    public Image imageLevel1;
    public Image imageLevel2;
    public Image imageLevel3;
    public Image iconHarvest;

    public GameObject LockPB;
    public GameObject unLockPb;

    public Image not1;
    public Image tick1;
    public Image not2;
    public Image tick2;
    public Image not3;
    public Image tick3;

    public Button btn_ThuHoach;
    public Button btn_NangCap;
    public Button btn_ExitPB;

    public void AddListenerToPanelBuild()
    {
        btn_ThuHoach.onClick.AddListener(delegate
        {
            DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnThuHoach(_factory1.id);
            DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().setActive();
            PanelBuild.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        btn_NangCap.onClick.AddListener(delegate
        {
            if(_levelChoice == 2)
            {
                if(DataGlobal.instance.GetGold() >= _factory1.gold2 && DataGlobal.instance.GetLevel() >= _factory1.level2
                && DataGlobal.instance.GetLevelHouse() == 2 && DataGlobal.instance.levelCurrentOfFactory[_factory1.id] == 0)
                {
                    DataGlobal.instance.levelCurrentOfFactory[_factory1.id] = 1;
                    DataGlobal.instance.SubGold(_factory1.gold2);
                    DataGlobal.instance.ArrayLandFactory[_factory1.id].GetComponent<LandFactory>().OnBuild1();
                }
            } else if(_levelChoice == 3)
            {
                if (DataGlobal.instance.GetGold() >= _factory1.gold3 && DataGlobal.instance.GetLevel() >= _factory1.level3
                && DataGlobal.instance.GetLevelHouse() == 3 && DataGlobal.instance.levelCurrentOfFactory[_factory1.id] == 1)
                {
                    DataGlobal.instance.levelCurrentOfFactory[_factory1.id] = 2;
                    DataGlobal.instance.SubGold(_factory1.gold3);
                    DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnBuild2();
                }
            }
        });

        btn_ExitPB.onClick.AddListener(delegate
        {
            DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().setActive();
            PanelBuild.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });
    }

    public void ShowBuildFactory(DetailFactory factoryBuild)
    {
        _factory1 = factoryBuild;
        DataGlobal.instance.AllowMouseDown = false;
        PanelBuild.SetActive(true);
        MainCamera.instance.camLock = true;
        imageLevel1.sprite = factoryBuild.sp1;
        imageLevel2.sprite = factoryBuild.sp2;
        imageLevel3.sprite = factoryBuild.sp3;
    }
    private int _levelChoice;
    public void OnClickToBuild(int levelChoice)
    {
        _levelChoice = levelChoice;
        if(levelChoice - 1 <= DataGlobal.instance.levelCurrentOfFactory[idFactory])
        {
            LockPB.SetActive(false);
            unLockPb.SetActive(true);
            imagePB.sprite = _factory1.sp1;
        } else if(levelChoice == 2)
        {
            imagePB.sprite = _factory1.sp2;
            LockPB.SetActive(true);
            unLockPb.SetActive(false);
            levelPB.text = "+ Level: " + _factory1.level2;
            mainHousePB.text = "+ Main house level: " + _factory1.mainhouse2; 
            goldPB.text = "+ Gold: " + _factory1.gold2;

            if (DataGlobal.instance.GetLevel() >= _factory1.level2)
            {
                not1.gameObject.SetActive(false);
                tick1.gameObject.SetActive(true);
            }
            else
            {
                not1.gameObject.SetActive(true);
                tick1.gameObject.SetActive(false);
            }

            if (DataGlobal.instance.GetLevelHouse() >= _factory1.mainhouse2)
            {
                not2.gameObject.SetActive(false);
                tick2.gameObject.SetActive(true);
            }
            else
            {
                not2.gameObject.SetActive(true);
                tick2.gameObject.SetActive(false);
            }

            if (DataGlobal.instance.GetGold() >= _factory1.gold2)
            {
                not3.gameObject.SetActive(false);
                tick3.gameObject.SetActive(true);
            }
            else
            {
                not3.gameObject.SetActive(true);
                tick3.gameObject.SetActive(false);
            }

        } else if(levelChoice == 3)
        {
            imagePB.sprite = _factory1.sp3;
            LockPB.SetActive(true);
            unLockPb.SetActive(false);
            levelPB.text = "+ Level: " + _factory1.level3;
            mainHousePB.text = "+ Main house level: " + _factory1.mainhouse3;
            goldPB.text = "+ Gold: " + _factory1.gold3;

            if (DataGlobal.instance.GetLevel() >= _factory1.level3)
            {
                not1.gameObject.SetActive(false);
                tick1.gameObject.SetActive(true);
            }
            else
            {
                not1.gameObject.SetActive(true);
                tick1.gameObject.SetActive(false);
            }

            if (DataGlobal.instance.GetLevelHouse() >= _factory1.mainhouse3)
            {
                not2.gameObject.SetActive(false);
                tick2.gameObject.SetActive(true);
            }
            else
            {
                not2.gameObject.SetActive(true);
                tick2.gameObject.SetActive(false);
            }

            if (DataGlobal.instance.GetGold() >= _factory1.gold3)
            {
                not3.gameObject.SetActive(false);
                tick3.gameObject.SetActive(true);
            }
            else
            {
                not3.gameObject.SetActive(true);
                tick3.gameObject.SetActive(false);
            }
        }
    }

    #endregion


    public GameObject PanelStore;
    public Button btnBuyPS;
    public Button btnExitPS;
    public TextMeshProUGUI unitPrice;
    public TextMeshProUGUI des;
    public Image iconPS;
    public TextMeshProUGUI haveOwned;
    private DetailItem _item;

    public void AddListnerForPS()
    {
        btnExitPS.onClick.AddListener(delegate
        {
            PanelStore.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        btnBuyPS.onClick.AddListener(delegate
        {
            if(DataGlobal.instance.GetGold() >= _item.unitPrice)
            {
                DataGlobal.instance.ArrayHaveOwnedItem[_item.id] += 1;
                DataGlobal.instance.SubGold(10);
                haveOwned.text = "Have owned: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id];
            }
        });
    }

    public void ShowPS()
    {
        PanelStore.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
    }

    public void OnSelectItem(DetailItem item)
    {
        _item = item;
        unitPrice.text = "" + _item.unitPrice;
        des.text = _item.des;
        iconPS.sprite = _item.icon;
        haveOwned.text = "Have owned: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id];
    }


    public GameObject PanelBan;
    public Button btnExitPB;
    public Button btnSell;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI unityPrice;
    public TextMeshProUGUI TotalMoney;
    public TextMeshProUGUI titleBan;
    public Image iconNongsan1;
    public Image iconNongSan2;
    private DetailNongSan _nongsan;

    public void AddListenerForPanelBan()
    {
        btnExitPB.onClick.AddListener(delegate
        {
            DataGlobal.instance.AllowMouseDown = true;
            PanelBan.SetActive(false);
            MainCamera.instance.camLock = false;
        });

        btnSell.onClick.AddListener(delegate
        {
            DataGlobal.instance.AddGold(DataGlobal.instance.ArrayAmount[_nongsan.id] * _nongsan.unitPrice);
            DataGlobal.instance.ArrayAmount[_nongsan.id] = 0;
            TotalMoney.text = "0";
            amount.text = "0";
        });
    }

    public void ShowPB()
    {
        DataGlobal.instance.AllowMouseDown = false;
        PanelBan.SetActive(true);
        MainCamera.instance.camLock = true;
    }

    public void OnClickNongSan(DetailNongSan nongSan)
    {
        _nongsan = nongSan;
        titleBan.text = _nongsan.title;
        amount.text = "" + DataGlobal.instance.ArrayAmount[_nongsan.id];
        unityPrice.text = "" + _nongsan.unitPrice;
        TotalMoney.text = "" + (_nongsan.unitPrice * DataGlobal.instance.ArrayAmount[_nongsan.id]);
        iconNongsan1.sprite = _nongsan.icon;
        iconNongSan2.sprite = _nongsan.icon;
    }


    public GameObject PanelChuong;
    public TextMeshProUGUI titleChuong;
    public TextMeshProUGUI harvestChuong;
    public TextMeshProUGUI timeChuong;
    public TextMeshProUGUI rewardsChuong;
    public TextMeshProUGUI goldChuong;
    public Image imageChuong;
    public Image imageVatnuoi1;
    public Image imageVatnuoi2;
    public Image imageVatnuoi3;
    public Image imageVatnuoi4;
    public Button BuyVatNuoi;
    public Button ExitChuong;
    public Button Muathem;
    public Button MuaGiam;

    private DetailVatnuoi _vatnuoi = null;
    private int _idChuong;
    private int soluongVatnuoi;

    public void AddListenerForPanelChuong()
    {
        ExitChuong.onClick.AddListener(delegate
        {
            DataGlobal.instance.AllowMouseDown = true;
            PanelChuong.SetActive(false);
            MainCamera.instance.camLock = false;
        });

        Muathem.onClick.AddListener(delegate
        {
            if(soluongVatnuoi >= 1 && soluongVatnuoi < 4)
            {
                soluongVatnuoi++;
                CapnhatSoluongvatnuoi();
            }
        });

        MuaGiam.onClick.AddListener(delegate
        {
            if(soluongVatnuoi > 1 && soluongVatnuoi <= 4)
            {
                soluongVatnuoi--;
                CapnhatSoluongvatnuoi();
            }
        });

        BuyVatNuoi.onClick.AddListener(delegate
        {
            if(_vatnuoi == null)
            {
                _vatnuoi = DataGlobal.instance.listVatNuoi[0];
                soluongVatnuoi = 1;
            }
            if(DataGlobal.instance.GetGold() >= (_vatnuoi.gold * soluongVatnuoi))
            {
                DataGlobal.instance.SubGold(_vatnuoi.gold * soluongVatnuoi);
                DataGlobal.instance.ArrayChuong[_idChuong].GetComponent<Chuong>().Build(_vatnuoi, soluongVatnuoi);
                DataGlobal.instance.AllowMouseDown = true;
                PanelChuong.SetActive(false);
                MainCamera.instance.camLock = false;
            }
        });
    }

    public void ShowPanelChuong(int idChuong)
    {
        _idChuong = idChuong;
        DataGlobal.instance.AllowMouseDown = false;
        PanelChuong.SetActive(true);
        MainCamera.instance.camLock = true;
    }

    public void OnClickVatnuoi(DetailVatnuoi vatnuoi)
    {
        _vatnuoi = vatnuoi;
        soluongVatnuoi = 1;
        imageChuong.sprite = _vatnuoi.imageChuong1;
        titleChuong.text = _vatnuoi.title;
        harvestChuong.text = "Harvest: " + _vatnuoi.harvest;
        timeChuong.text = "Time: " + _vatnuoi.time;
        CapnhatSoluongvatnuoi();
    }

    public void CapnhatSoluongvatnuoi()
    {
        imageVatnuoi1.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi2.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi3.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi4.sprite = _vatnuoi.imageVatnuoi;
        goldChuong.text = "" + _vatnuoi.gold * soluongVatnuoi;
        rewardsChuong.text = "Rewards: +" + _vatnuoi.rewards * soluongVatnuoi + " exp";
        if (soluongVatnuoi == 1)
        {
            imageVatnuoi2.gameObject.SetActive(false);
            imageVatnuoi3.gameObject.SetActive(false);
            imageVatnuoi4.gameObject.SetActive(false);
        }
        else if (soluongVatnuoi == 2)
        {
            imageVatnuoi2.gameObject.SetActive(true);
            imageVatnuoi3.gameObject.SetActive(false);
            imageVatnuoi4.gameObject.SetActive(false);
        }
        else if (soluongVatnuoi == 3)
        {
            imageVatnuoi2.gameObject.SetActive(true);
            imageVatnuoi3.gameObject.SetActive(true);
            imageVatnuoi4.gameObject.SetActive(false);
        }
        else if (soluongVatnuoi == 4)
        {
            imageVatnuoi2.gameObject.SetActive(true);
            imageVatnuoi3.gameObject.SetActive(true);
            imageVatnuoi4.gameObject.SetActive(true);
        }
    }

    public GameObject PanelHouse;
    public TextMeshProUGUI containWater;
    public TextMeshProUGUI containStone;
    public TextMeshProUGUI containWood;
    public TextMeshProUGUI containOil;
    public TextMeshProUGUI title2;
    public GameObject Contain;
    public TextMeshProUGUI content1;
    public TextMeshProUGUI content2;
    public TextMeshProUGUI content3;
    public TextMeshProUGUI content4;
    public GameObject Upgrade;
    public TextMeshProUGUI levelRequire;
    public GameObject thieu1;
    public GameObject du1;
    public TextMeshProUGUI goldRequire;
    public GameObject thieu2;
    public GameObject du2;
    public TextMeshProUGUI oilRequire;
    public GameObject thieu3;
    public GameObject du3;
    public Button exitMainHouse;
    public Button NangCapHouse;
    public Image imageHouse;
    public GameObject WhereHouse;

    private DetailHouse _house;

    public void AddEventListnerForPanelHouse()
    {
        exitMainHouse.onClick.AddListener(delegate
        {
            PanelHouse.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;
        });

        NangCapHouse.onClick.AddListener(delegate
        {
            if(_house.levelRQ <= DataGlobal.instance.GetLevel() && _house.oilRQ <= DataGlobal.instance.GetOil() && 
               _house.goldRQ <= DataGlobal.instance.GetGold() && _house.id == DataGlobal.instance.GetLevelHouse())
            {
                WhereHouse.GetComponent<House>().NangCapNha(_house.imgHouse);
                PanelHouse.SetActive(false);
                MainCamera.instance.camLock = false;
                DataGlobal.instance.AllowMouseDown = true;
            } else
            {
                Debug.Log("Ban chua du dieu kien de nang cap!");
            }
        });
    }

    public void ShowPanelHouse()
    {
        PanelHouse.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
    }

    public void OnClickItemHouse(DetailHouse house)
    {
        _house = house;
        if(_house.id <= (DataGlobal.instance.GetLevelHouse() - 1))
        {
            Contain.SetActive(true);
            Upgrade.SetActive(false);
            title2.text = "Farm level: " + DataGlobal.instance.GetLevel();
            title2.alignment = TextAlignmentOptions.Left;
            containOil.text = "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].oil;
            containWater.text = "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].water;
            containStone.text = "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].stone;
            containWood.text = "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].wood;
        } else
        {
            Contain.SetActive(false);
            Upgrade.SetActive(true);
            title2.text = "House upgrade";
            title2.alignment = TextAlignmentOptions.Center;

            levelRequire.text = "Level: " + _house.levelRQ;
            if(_house.levelRQ <= DataGlobal.instance.GetLevel())
            {
                thieu1.SetActive(false);
                du1.SetActive(true);
            } else
            {
                thieu1.SetActive(true);
                du1.SetActive(false);
            }

            goldRequire.text = "Gold: " + _house.goldRQ;
            if(_house.goldRQ <= DataGlobal.instance.GetGold())
            {
                thieu2.SetActive(false);
                du2.SetActive(true);
            } else
            {
                thieu2.SetActive(true);
                du2.SetActive(false);
            }

            oilRequire.text = "Oil: " + _house.goldRQ;
            if(_house.oilRQ <= DataGlobal.instance.GetOil())
            {
                thieu3.SetActive(false);
                du3.SetActive(true);
            } else
            {
                thieu3.SetActive(true);
                du3.SetActive(false);
            }

        }
        imageHouse.sprite = _house.imgHouse;
    }
   
}
