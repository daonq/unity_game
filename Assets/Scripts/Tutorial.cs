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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(DataGlobal.instance.firstGame == 0)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 2, -10);
            muiten.SetActive(true);
            DataGlobal.instance.firstGame = 1;
        }
    }

    public void TutorialFactory()
    {
        if (DataGlobal.instance.firstGame == 1)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, -0.5f, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-550, -138, 0);
            DataGlobal.instance.firstGame = 2;
        }
    }

    public void TutorialWaiting()
    {
        if(DataGlobal.instance.firstGame == 2)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, -0.5f, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-550, -138, 0);
            DataGlobal.instance.firstGame = 3;
        }
    }

    public void TutorialAnimals()
    {
        if(DataGlobal.instance.firstGame == 3)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-410, -120, 0);
            DataGlobal.instance.firstGame = 4;
        }
    }

    public void TutorialMuaCua()
    {
        if(DataGlobal.instance.firstGame == 4)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-10, -100, 0);
            DataGlobal.instance.firstGame = 5;
        }
    }

    public void TutorialMuaCua1()
    {
        if(DataGlobal.instance.firstGame == 5)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = true;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-160, -150, 0);
            DataGlobal.instance.firstGame = 6;
        }
    }

    public void TutorialChatCay()
    {
        if(DataGlobal.instance.firstGame == 6)
        {
            modeTutorial = true;
            MainCamera.instance.camLock = false;
            MainCamera.instance.mcam.GetComponent<Transform>().position = new Vector3(-2, 3, -10);
            caitay.SetActive(true);
            caitay.GetComponent<RectTransform>().localPosition = new Vector3(-350, -10, 0);
            DataGlobal.instance.firstGame = 7;
        }
    }

    public void EndTutorial()
    {
        modeTutorial = false;
    }
}
