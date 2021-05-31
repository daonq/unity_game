using UnityEngine;

public class thuhoachvatnuoi : MonoBehaviour
{
    public GameObject chuong;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            chuong.GetComponent<Chuong>().thuhoach();
        }
    }
}
