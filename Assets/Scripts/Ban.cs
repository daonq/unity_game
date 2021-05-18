using UnityEngine;

public class Ban : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            UIManager.instance.ShowPB();
        }
    }
}
