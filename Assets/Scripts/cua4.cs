using UnityEngine;

public class cua4 : MonoBehaviour
{
    public GameObject cay;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            cay.GetComponent<cr4>().cuanhe();
            gameObject.SetActive(false);
        }
    }
}
