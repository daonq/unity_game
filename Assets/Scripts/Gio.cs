using UnityEngine;

public class Gio : MonoBehaviour
{
    public GameObject cay;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            cay.GetComponent<ThuHoachCayAnQua>().thuhoach();
            gameObject.SetActive(false);
        }
    }
}
