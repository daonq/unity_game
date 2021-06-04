using UnityEngine;

public class Ban : MonoBehaviour
{
    public GameObject market;
    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            market.GetComponent<Market>().vongtron.transform.localScale = new Vector3(0, 0, 0);
            UIManager.instance.ShowPB();
        }
    }
}
