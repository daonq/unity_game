using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // 1. Chi mui ten vao o dat
    // 2. Nguoi choi chi co the nhan vao o dat do ma thoi
    // 3. Khi nhan vao thi hien panel mua hat
    // 4. Ban tay chi vao cay lua
    // 5. Nguoi choi nhan vao cay lua
    // 6. Ban tay chuyen sang nut Buy
    // 7. Nguoi choi nhan vao nut Buy
    // 8. Sau do thu hoach thi cai mui ten chi vao cay lua
    // 9. Ban tay chi vao cho
    // 10. Nguoi choi nhan vao
    // 11. Chi vao nut ban
    // 12. Nhan vao nut ban
    // 13. Hien len panel ban
    // 14. Chon vao cay lua
    // 15. Nhan vao nut Sell
    // 16. Chon vao LandFactory
    // 17. Chon vao nha may
    // 18. Nhan nut mua
    // 19. Het!

    public static Tutorial instance;

    public GameObject muiten;

    public GameObject caitay;

    public bool modeTutorial;

    public Button btnGift;
    public Button btnSetting;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Debug.Log("First Game: " + DataGlobal.instance.GetFirstGame());
        switch (DataGlobal.instance.GetFirstGame())
        {
            case 0:
                TutorialGieoHat();
                break;
            case 1:
                TutorialFactory();
                break;
            case 2:
                TutorialWaiting();
                break;
            case 3:
                TutorialAnimals();
                break;
            case 4:
                TutorialMuaCua();
                break;
            case 5:
                TutorialMuaCua1();
                break;
            case 6:
                TutorialChatCay();
                break;
            case 7:
                EndTutorial();
                break;
        }
    }

    public void TutorialGieoHat()
    {
        if (DataGlobal.instance.GetFirstGame() == 0)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 2, -10);
            muiten.SetActive(true);
            //DataGlobal.instance.SetFirstGame(1);
        }
    }

    public void TutorialFactory()
    {
        if (DataGlobal.instance.GetFirstGame() == 1)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, -0.5f, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-470, -100, 0);
            //DataGlobal.instance.SetFirstGame(2);
        }
    }

    public void TutorialWaiting()
    {
        if (DataGlobal.instance.GetFirstGame() == 2)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, -0.5f, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-470, -100, 0);
            //DataGlobal.instance.SetFirstGame(3);
        }
    }

    public void TutorialAnimals()
    {
        if (DataGlobal.instance.GetFirstGame() == 3)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-330, -60, 0);
            //DataGlobal.instance.SetFirstGame(4);
        }
    }

    public void TutorialMuaCua()
    {
        if (DataGlobal.instance.GetFirstGame() == 4)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(90, -60, 0);
            //DataGlobal.instance.SetFirstGame(5);
        }
    }

    public void TutorialMuaCua1()
    {
        if (DataGlobal.instance.GetFirstGame() == 5)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-20, -120, 0);
            //DataGlobal.instance.SetFirstGame(6);
        }
    }

    public void TutorialChatCay()
    {
        if (DataGlobal.instance.GetFirstGame() == 6)
        {
            btnGift.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-225, 64, 0);
            caitay.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //DataGlobal.instance.SetFirstGame(7);
        }
    }

    public void EndTutorial()
    {
        if (DataGlobal.instance.GetFirstGame() == 7)
        {
            modeTutorial = false;
            caitay.SetActive(false);
            btnGift.gameObject.SetActive(true);
            btnSetting.gameObject.SetActive(true);
            MainCamera.instance.camLock = false;
            DataGlobal.instance.SetFirstGame(8);

            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Chúc mừng bạn đã hoàn thành phần hướng dẫn!\nBạn nhận được 1000 Vàng, 200 Nước, 200 Đá, 200 Gỗ, 50 Dầu." : "Congratulations on completing the tutorial!\nYou get 1000 Gold, 200 Water, 200 Stone, 200 Wood, 50 Oil.");
            DataGlobal.instance.AddGold(1000);
            DataGlobal.instance.AddWater(200);
            DataGlobal.instance.AddStone(200);
            DataGlobal.instance.AddWood(200);
            DataGlobal.instance.AddOil(50);
        }
    }
}
