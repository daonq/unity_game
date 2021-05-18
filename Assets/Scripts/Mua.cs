using UnityEngine;

public class Mua : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            UIManager.instance.ShowPS();
        }
    }
}
