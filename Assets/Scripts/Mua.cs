using UnityEngine;

public class Mua : MonoBehaviour
{
    public GameObject market;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            market.GetComponent<Market>().vongtron.transform.localScale = Vector3.zero;
            Tutorial.instance.caitay.SetActive(false);
            UIManager.instance.ShowPS();
            //DataGlobal.instance.SetFirstGame(5);
            //Tutorial.instance.TutorialMuaCua1();
        }
    }
}
