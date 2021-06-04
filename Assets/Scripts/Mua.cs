using UnityEngine;

public class Mua : MonoBehaviour
{
    public GameObject market;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            market.GetComponent<Market>().vongtron.transform.localScale = new Vector3(0, 0, 0);
            UIManager.instance.ShowPS();
            Tutorial.instance.caitay.SetActive(false);
        }
    }
}
