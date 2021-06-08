using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

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
    public TextMeshProUGUI textButtonBuySeed;
    public Button btnBuy;
    public Button btnExit;
    public Image image;
    private int idLand;
    private DetailSeed seedClick;

    public GameObject effectBuyHat;

    public Toggle toggleGieoHat;

    public Toggle[] arrayToggleSeed;

    public ScrollRect scrollRect;

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
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ tiền để mua hạt giống này!" : "You don't have enough gold to buy this seed!");
            }
            else if (DataGlobal.instance.GetLevel() < seedClick.levelUnlock)
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ cấp độ để mở khóa hạt giống này!" : "You don't have enough level to buy this seed!");
            }

            if (DataGlobal.instance.GetFirstGame() == 0)
            {
                effectBuyHat.SetActive(false);
                for (int i = 0; i < arrayToggleSeed.Length; i++)
                {
                    arrayToggleSeed[i].interactable = true;
                }
                btnExit.interactable = true;
                scrollRect.horizontal = true;
                DataGlobal.instance.SetFirstGame(1);
                Tutorial.instance.TutorialFactory();
            }
        });
    }

    public void OnClickToLand(int id)
    {
        idLand = id;
        if(DataGlobal.instance.GetFirstGame() == 0)
        {
            Tutorial.instance.muiten.SetActive(false);
            effectBuyHat.SetActive(true);
            for (int i = 1; i < arrayToggleSeed.Length; i++)
            {
                arrayToggleSeed[i].interactable = false;
            }
            btnExit.interactable = false;
            scrollRect.horizontal = false;
        }
        textButtonBuySeed.text = DataGlobal.instance.tiengviet ? "Mua" : "Buy";
        PanelSeeds.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        toggleGieoHat.isOn = true;
        OnSelectSeed(DataGlobal.instance.listSeed[0]);
    }

    public void OnSelectSeed(DetailSeed seed)
    {
        seedClick = seed;
        title.text = DataGlobal.instance.tiengviet ? seed.titleVN : seed.title;
        exp.text = DataGlobal.instance.tiengviet ? "+ Kinh nghiệm: " + seed.exp : "+ Experience: " + seed.exp;
        time.text = DataGlobal.instance.tiengviet ? "+ Thời gian: " + formatTime(seed.time) : "+ Time: " + formatTime(seed.time);
        product.text = DataGlobal.instance.tiengviet ? "+ Sản phẩm: 5 " + seed.nameProductVN : "+ Product: 5 " + seed.nameProduct;
        levelUL.text = DataGlobal.instance.tiengviet ? "+ Cấp độ mở khóa: " + seed.levelUnlock : "+ Level unlock: " + seed.levelUnlock;
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
    public TextMeshProUGUI levelUnlockF1;
    public TextMeshProUGUI textButtonBuyFactory;
    public Button btnBuyF1;
    public Button btnExitF1;
    public Image imageF1;
    private int idFactory; // Vi tri nha may
    private DetailFactory _factory1;

    public GameObject effectBuyNhaMay;

    public Toggle toggleMuanhamay;
    public Toggle[] arrayToggleFactory;

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
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không thể xây nhà máy này ở đây được!" : "You can't build this factory in here!");
            }
            else if (DataGlobal.instance.GetGold() >= _factory1.gold && DataGlobal.instance.GetLevel() >= _factory1.levelUnlock)
            {
                PanelFactory1.SetActive(false);
                DataGlobal.instance.AllowMouseDown = true;
                MainCamera.instance.camLock = false;
                DataGlobal.instance.SubGold(_factory1.gold);
                DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnWaiting(_factory1);
            }
            else if (DataGlobal.instance.GetGold() < _factory1.gold && DataGlobal.instance.GetLevel() >= _factory1.levelUnlock)
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ tiền để mua nhà máy này!" : "You don't have enough gold to buy this factory!");
            }
            else if (DataGlobal.instance.GetGold() >= _factory1.gold && DataGlobal.instance.GetLevel() < _factory1.levelUnlock)
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ cấp độ để mở khóa nhà máy này!" : "You don't have reach level to buy this factory!");
            }
            else
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ cấp độ và tiền để mua nhà máy này!" : "You don't have enough gold and level to buy this factory!");
            }

            if (DataGlobal.instance.GetFirstGame() == 1)
            {
                effectBuyNhaMay.SetActive(false);
                for (int i = 0; i < arrayToggleFactory.Length; i++)
                {
                    arrayToggleFactory[i].interactable = true;
                }
                btnExitF1.interactable = true;
                DataGlobal.instance.SetFirstGame(2);
                Tutorial.instance.TutorialWaiting();
            }
        });
    }

    public void OnClickToFactory(int id)
    {
        idFactory = id;
        if(DataGlobal.instance.GetFirstGame() == 1)
        {
            Tutorial.instance.caitay.SetActive(false);
            effectBuyNhaMay.SetActive(true);
            for (int i = 1; i < arrayToggleFactory.Length; i++)
            {
                arrayToggleFactory[i].interactable = false;
            }
            btnExitF1.interactable = false;
        }
        textButtonBuyFactory.text = DataGlobal.instance.tiengviet ? "Mua" : "Buy";
        PanelFactory1.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;

        toggleMuanhamay.isOn = true;
        OnSelectFactory(DataGlobal.instance.listFactory[0]);
    }

    public void OnSelectFactory(DetailFactory factory)
    {
        _factory1 = factory;
        titleF1.text = DataGlobal.instance.tiengviet ? factory.titleVN : factory.title;
        goldF1.text = "" + factory.gold;
        rawMatF1.text = DataGlobal.instance.tiengviet ? "+ Nguyên liệu: " + factory.rawMatVN : "+ Raw materials: " + factory.rawMat;
        timeF1.text = DataGlobal.instance.tiengviet ? "+ Thời gian: " + formatTime(factory.time1) + "/" + factory.nameProduct : "+ Time: " + formatTime(factory.time1) + "s/" + factory.nameProduct;
        productF1.text = DataGlobal.instance.tiengviet ? "+ Sản phẩm: " + factory.nameProductVN : "+ Product: " + factory.nameProduct;
        levelUnlockF1.text = DataGlobal.instance.tiengviet ? "+ Cấp độ yêu cầu: " + factory.levelUnlock : "+ Level unlock: " + factory.levelUnlock;
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

    public GameObject effectWaitingNhaMay;

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
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không có đủ tài nguyên để nâng cấp nhà máy này!" : "You don't have enough resources to upgrade this factory!");
            }
            if (DataGlobal.instance.GetFirstGame() == 2)
            {
                effectWaitingNhaMay.SetActive(false);
                btnExitPW.interactable = true;
                DataGlobal.instance.SetFirstGame(3);
                Tutorial.instance.TutorialAnimals();
            }
        });
    }

    public void OnClickToWaiting(DetailFactory factoryWating, int idFac)
    {
        if(DataGlobal.instance.GetFirstGame() == 2)
        {
            Tutorial.instance.caitay.SetActive(false);
            effectWaitingNhaMay.SetActive(true);
            btnExitPW.interactable = false;
        }
        idFactory = idFac;
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

    public Toggle toggleBuild;

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
                && DataGlobal.instance.GetLevelHouse() >= 2 && DataGlobal.instance.levelCurrentOfFactory[idFactory] == 0)
                {
                    DataGlobal.instance.levelCurrentOfFactory[idFactory] = 1;
                    DataGlobal.instance.SubGold(_factory1.gold2);
                    PanelBuild.SetActive(false);
                    DataGlobal.instance.AllowMouseDown = true;
                    MainCamera.instance.camLock = false;
                    DataGlobal.instance.ArrayLandFactory[_factory1.id].GetComponent<LandFactory>().OnBuild1();
                }
                else
                {
                    PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không thể nâng cấp!" : "You can't upgrade!");
                }
            } else if(_levelChoice == 3)
            {
                if (DataGlobal.instance.GetGold() >= _factory1.gold3 && DataGlobal.instance.GetLevel() >= _factory1.level3
                && DataGlobal.instance.GetLevelHouse() >= 3 && DataGlobal.instance.levelCurrentOfFactory[idFactory] == 1)
                {
                    DataGlobal.instance.levelCurrentOfFactory[idFactory] = 2;
                    DataGlobal.instance.SubGold(_factory1.gold3);
                    PanelBuild.SetActive(false);
                    DataGlobal.instance.AllowMouseDown = true;
                    MainCamera.instance.camLock = false;
                    DataGlobal.instance.ArrayLandFactory[idFactory].GetComponent<LandFactory>().OnBuild2();
                }
                else
                {
                    PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không thể nâng cấp!" : "You can't upgrade!");
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

    public void ShowBuildFactory(DetailFactory factoryBuild, int idNhamay)
    {
        _factory1 = factoryBuild;
        idFactory = idNhamay;
        titlePB1.text = DataGlobal.instance.tiengviet ? _factory1.titleVN : _factory1.title;
        titlePB2.text = DataGlobal.instance.tiengviet ? "Thông tin" : "Detail";
        DataGlobal.instance.AllowMouseDown = false;
        PanelBuild.SetActive(true);
        MainCamera.instance.camLock = true;
        imageLevel1.sprite = factoryBuild.sp1;
        imageLevel2.sprite = factoryBuild.sp2;
        imageLevel3.sprite = factoryBuild.sp3;
        toggleBuild.isOn = true;
        OnClickToBuild(1);
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
            iconHarvest.sprite = DataGlobal.instance.iconThuHoachFactory[_factory1.id];
        } else if(levelChoice == 2)
        {
            imagePB.sprite = _factory1.sp2;
            LockPB.SetActive(true);
            unLockPb.SetActive(false);
            levelPB.text = DataGlobal.instance.tiengviet ? "+ Cấp độ: " + _factory1.level2 : "+ Level: " + _factory1.level2;
            mainHousePB.text = DataGlobal.instance.tiengviet ? "+ Nhà chính: " + _factory1.mainhouse2 : "+ Main house level: " + _factory1.mainhouse2;
            goldPB.text = DataGlobal.instance.tiengviet ? "+ Vàng: " + _factory1.gold2 : "+ Gold: " + _factory1.gold2;

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
            levelPB.text = DataGlobal.instance.tiengviet ? "+ Cấp độ: " + _factory1.level3 : "+ Level: " + _factory1.level3;
            mainHousePB.text = DataGlobal.instance.tiengviet ? "+ Nhà chính: " + _factory1.mainhouse3 : "+ Main house level: " + _factory1.mainhouse3;
            goldPB.text = DataGlobal.instance.tiengviet ? "+ Vàng: " + _factory1.gold3 : "+ Gold: " + _factory1.gold3;

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
    public TextMeshProUGUI textButtonStore;
    public TextMeshProUGUI textTitleStore;
    public Button btnBuyPS;
    public Button btnExitPS;
    public TextMeshProUGUI unitPrice;
    public TextMeshProUGUI des;
    public Image iconPS;
    public TextMeshProUGUI haveOwned;
    private DetailItem _item;

    public GameObject effectBuyItem;

    public Toggle toggleStore;
    public Toggle[] arrayToggleStore;

    public Text[] AmountItem;

    public void AddListnerForPS()
    {
        btnExitPS.onClick.AddListener(delegate
        {
            PanelStore.SetActive(false);
            DataGlobal.instance.AllowMouseDown = true;
            MainCamera.instance.camLock = false;

            if (DataGlobal.instance.GetFirstGame() == 5)
            {
                effectBuyItem.SetActive(false);
                DataGlobal.instance.SetFirstGame(6);
                Tutorial.instance.TutorialChatCay();
                btnBuyPS.interactable = true;
            }

            for (int i = 0; i < arrayToggleStore.Length; i++)
            {
                arrayToggleStore[i].interactable = true;
            }
        });

        btnBuyPS.onClick.AddListener(delegate
        {
            if(DataGlobal.instance.GetGold() >= _item.unitPrice)
            {
                DataGlobal.instance.ArrayHaveOwnedItem[_item.id] += 1;
                DataGlobal.instance.SubGold(10);
                haveOwned.text = DataGlobal.instance.tiengviet ? "Đang có: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id] : "Have owned: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id];
                for (int i = 0; i < 3; i++)
                {
                    AmountItem[i].text = DataGlobal.instance.ArrayHaveOwnedItem[i].ToString();
                }
            }
            else
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ tiền để mua vật phẩm này!" : "You don't have enough gold to buy this item!");
            }
            if (DataGlobal.instance.GetFirstGame() == 5)
            {
                effectBuyItem.GetComponent<RectTransform>().localPosition = new Vector3(200, 140, 0);
                btnBuyPS.interactable = false;
                btnExitPS.interactable = true;
            }
        });
    }

    public void ShowPS()
    {
        if (DataGlobal.instance.GetFirstGame() == 5)
        {
            effectBuyItem.SetActive(true);
            for (int i = 1; i < arrayToggleStore.Length; i++)
            {
                arrayToggleStore[i].interactable = false;
            }
            btnExitPS.interactable = false;
        }
        PanelStore.SetActive(true);
        textButtonStore.text = DataGlobal.instance.tiengviet ? "Mua" : "Buy";
        textTitleStore.text = DataGlobal.instance.tiengviet ? "Cửa hàng" : "Store";
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        toggleStore.isOn = true;
        OnSelectItem(toggleStore.GetComponent<Item>().item);
    }

    public void OnSelectItem(DetailItem item)
    {
        _item = item;
        unitPrice.text = "" + _item.unitPrice;
        des.text = DataGlobal.instance.tiengviet ? _item.desVN : _item.des;
        iconPS.sprite = _item.icon;
        haveOwned.text = DataGlobal.instance.tiengviet ? "Đang có: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id] : "Have owned: " + DataGlobal.instance.ArrayHaveOwnedItem[_item.id];
        for (int i = 0; i < 3; i++)
        {
            AmountItem[i].text = DataGlobal.instance.ArrayHaveOwnedItem[i].ToString();
        }
    }

    public GameObject PanelBan;
    public TextMeshProUGUI textButtonSell;
    public Button btnExitPB;
    public Button btnSell;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI unityPrice;
    public TextMeshProUGUI TotalMoney;
    public TextMeshProUGUI titleBan;

    public TextMeshProUGUI textOrder;
    public TextMeshProUGUI textUnitPrice;
    public TextMeshProUGUI textTotolMoney;
    public TextMeshProUGUI textAmount;

    public Image iconNongsan1;
    public Image iconNongSan2;

    public Toggle toggleBan;
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
            int totalDue = DataGlobal.instance.ArrayAmount[_nongsan.id] * _nongsan.unitPrice;
            DataGlobal.instance.AddGold(totalDue);
            DataGlobal.instance.ArrayAmount[_nongsan.id] = 0;
            DataGlobal.instance.UpdateDataAmount();
            TotalMoney.text = "0";
            amount.text = "0";
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn nhận được " + totalDue + " vàng" : "You have received " + totalDue + " Gold");
        });
    }

    public void ShowPB()
    {
        textButtonSell.text = DataGlobal.instance.tiengviet ? "Bán" : "Sell";
        textOrder.text = DataGlobal.instance.tiengviet ? "Hóa đơn" : "Order";
        textUnitPrice.text = DataGlobal.instance.tiengviet ? "+ Đơn vị giá:" : "+ Unit price:";
        textTotolMoney.text = DataGlobal.instance.tiengviet ? "+ Tổng tiền:" : "+ Total money:";
        textAmount.text = DataGlobal.instance.tiengviet ? "+ Số lượng:" : "+ Amount:";
        PanelBan.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        toggleBan.isOn = true;
        OnClickNongSan(toggleBan.GetComponent<NongSan>().nongSan);
    }

    public void OnClickNongSan(DetailNongSan nongSan)
    {
        _nongsan = nongSan;
        titleBan.text = DataGlobal.instance.tiengviet ? _nongsan.titleVN : _nongsan.title;
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
    public TextMeshProUGUI levelUnlockChuong;
    public TextMeshProUGUI textButtonMuaVatNuoi;
    public Image imageChuong;
    public Image imageVatnuoi1;
    public Image imageVatnuoi2;
    public Image imageVatnuoi3;
    public Image imageVatnuoi4;
    public Button BuyVatNuoi;
    public Button ExitChuong;
    public Button Muathem;
    public Button MuaGiam;

    public Toggle toggleChuong;

    public Toggle[] arrayToggleChuong;

    private DetailVatnuoi _vatnuoi = null;
    private int _idChuong;
    private int soluongVatnuoi;

    public GameObject effectBuyAnimals;

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
            if(DataGlobal.instance.GetGold() >= (_vatnuoi.gold * soluongVatnuoi) && DataGlobal.instance.GetLevel() >= _vatnuoi.levelUnlock)
            {
                DataGlobal.instance.SubGold(_vatnuoi.gold * soluongVatnuoi);
                DataGlobal.instance.ArrayChuong[_idChuong].GetComponent<Chuong>().Build(_vatnuoi, soluongVatnuoi);
                DataGlobal.instance.AllowMouseDown = true;
                PanelChuong.SetActive(false);
                MainCamera.instance.camLock = false;
            }
            else if(DataGlobal.instance.GetGold() < (_vatnuoi.gold * soluongVatnuoi) && DataGlobal.instance.GetLevel() >= _vatnuoi.levelUnlock)
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ vàng để mua con vật này!" : "You don't have enough gold to buy this animals!");
            }
            else if(DataGlobal.instance.GetGold() >= (_vatnuoi.gold * soluongVatnuoi) && DataGlobal.instance.GetLevel() < _vatnuoi.levelUnlock)
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ cấp độ để mở khóa con vật này!" : "You don't have reach level to buy this animals!");
            }
            else
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không đủ vàng và cấp độ để mua con vật này!" : "You don't have enough gold and level to buy this animals!");
            }
            if (DataGlobal.instance.GetFirstGame() == 3)
            {
                effectBuyAnimals.SetActive(false);
                ExitChuong.interactable = true;
                Muathem.interactable = true;
                MuaGiam.interactable = true;
                for (int i = 0; i < arrayToggleChuong.Length; i++)
                {
                    arrayToggleChuong[i].interactable = true;
                }
                DataGlobal.instance.SetFirstGame(4);
                Tutorial.instance.TutorialMuaCua();
            }
        });
    }

    public void ShowPanelChuong(int idChuong)
    {
        if(DataGlobal.instance.GetFirstGame() == 3)
        {
            effectBuyAnimals.SetActive(true);
            Tutorial.instance.caitay.SetActive(false);
            ExitChuong.interactable = false;
            MuaGiam.interactable = false;
            Muathem.interactable = false;
            for (int i = 1; i < arrayToggleChuong.Length; i++)
            {
                arrayToggleChuong[i].interactable = false;
            }
        }
        _idChuong = idChuong;
        textButtonMuaVatNuoi.text = DataGlobal.instance.tiengviet ? "Mua" : "Buy";
        PanelChuong.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        toggleChuong.isOn = true;
        OnClickVatnuoi(DataGlobal.instance.listVatNuoi[0]);
    }

    public void OnClickVatnuoi(DetailVatnuoi vatnuoi)
    {
        _vatnuoi = vatnuoi;
        soluongVatnuoi = 1;
        imageChuong.sprite = _vatnuoi.imageChuong1;
        titleChuong.text = DataGlobal.instance.tiengviet ? _vatnuoi.titleVN : _vatnuoi.title;
        harvestChuong.text = DataGlobal.instance.tiengviet ? "+ Thu hoạch: " + _vatnuoi.harvestVN : "+ Harvest: " + _vatnuoi.harvest;
        timeChuong.text = DataGlobal.instance.tiengviet ? "+ Thời gian: " + formatTime(_vatnuoi.time) : "+ Time: " + formatTime(_vatnuoi.time);
        levelUnlockChuong.text = DataGlobal.instance.tiengviet ? "+ Cấp độ mở khóa: " + _vatnuoi.levelUnlock : "+ Level unlock: " + _vatnuoi.levelUnlock;
        CapnhatSoluongvatnuoi();
    }

    public void CapnhatSoluongvatnuoi()
    {
        imageVatnuoi1.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi2.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi3.sprite = _vatnuoi.imageVatnuoi;
        imageVatnuoi4.sprite = _vatnuoi.imageVatnuoi;
        goldChuong.text = "" + _vatnuoi.gold * soluongVatnuoi;
        rewardsChuong.text = DataGlobal.instance.tiengviet ? "+ Phần thưởng: +" + _vatnuoi.rewards * soluongVatnuoi + " sao" : "+ Rewards: +" + _vatnuoi.rewards * soluongVatnuoi + " exp";
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
    public TextMeshProUGUI titleHouseHehe;
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

    public Toggle toggleHouse;

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
            } 
            else
            {
                PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không thể nâng cấp!" : "You are not eligible to upgrade!");
            }
        });
    }

    public void ShowPanelHouse()
    {
        PanelHouse.SetActive(true);
        DataGlobal.instance.AllowMouseDown = false;
        MainCamera.instance.camLock = true;
        titleHouseHehe.text = DataGlobal.instance.tiengviet ? "Nhà chính" : "Main house";
        toggleHouse.isOn = true;
        OnClickItemHouse(toggleHouse.GetComponent<ItemHouse>().house);
    }

    public void OnClickItemHouse(DetailHouse house)
    {
        _house = house;
        if(_house.id <= (DataGlobal.instance.GetLevelHouse() - 1))
        {
            Contain.SetActive(true);
            Upgrade.SetActive(false);
            title2.text = DataGlobal.instance.tiengviet ? "Cấp độ: " + DataGlobal.instance.GetLevel() : "Level: " + DataGlobal.instance.GetLevel();
            containOil.text   = DataGlobal.instance.tiengviet ? "Sức chứa: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].oil   : "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].oil;
            containWater.text = DataGlobal.instance.tiengviet ? "Sức chứa: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].water : "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].water;
            containStone.text = DataGlobal.instance.tiengviet ? "Sức chứa: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].stone : "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].stone;
            containWood.text  = DataGlobal.instance.tiengviet ? "Sức chứa: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].wood  : "Contain: " + DataGlobal.instance.levelContain[DataGlobal.instance.GetLevel() - 1].wood;
            content1.text = DataGlobal.instance.tiengviet ? DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct1VN : DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct1;
            content2.text = DataGlobal.instance.tiengviet ? DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct2VN : DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct2;
            content3.text = DataGlobal.instance.tiengviet ? DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct3VN : DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct3;
            content4.text = DataGlobal.instance.tiengviet ? DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct4VN : DataGlobal.instance.listContentOFHOUSE[DataGlobal.instance.GetLevel() - 1].ct4;
        }
        else
        {
            Contain.SetActive(false);
            Upgrade.SetActive(true);
            title2.text = DataGlobal.instance.tiengviet ? "Nâng cấp" : "Upgrade";

            levelRequire.text = DataGlobal.instance.tiengviet ? "+ Cấp độ: " + _house.levelRQ : "+ Level: " + _house.levelRQ;
            if(_house.levelRQ <= DataGlobal.instance.GetLevel())
            {
                thieu1.SetActive(false);
                du1.SetActive(true);
            } else
            {
                thieu1.SetActive(true);
                du1.SetActive(false);
            }

            goldRequire.text = DataGlobal.instance.tiengviet ? "+ Vàng: " + _house.goldRQ : "+ Gold: " + _house.goldRQ;
            if(_house.goldRQ <= DataGlobal.instance.GetGold())
            {
                thieu2.SetActive(false);
                du2.SetActive(true);
            } else
            {
                thieu2.SetActive(true);
                du2.SetActive(false);
            }

            oilRequire.text = DataGlobal.instance.tiengviet ? "+ Dầu: " + _house.oilRQ : "+ Oil: " + _house.oilRQ;
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

    public string formatTime(int t)
    {
        int phut = t / 60;
        int giay = t % 60;
        if (phut == 0)
        {
            return giay + "s";
        }
        if(giay == 0 && phut > 0)
        {
            return phut + "m";
        }
        return phut + "m" + giay + "s";
    }
}
