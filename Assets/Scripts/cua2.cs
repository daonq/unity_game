using UnityEngine;

public class cua2 : MonoBehaviour
{
    public GameObject cay;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            cay.GetComponent<cr2>().cuanhe();
            gameObject.SetActive(false);
        }
    }
}
