using UnityEngine;

public class KeoLua : MonoBehaviour
{
    Vector3 pos;
    private void OnMouseDrag()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 10;
        transform.position = pos;
    }
}
