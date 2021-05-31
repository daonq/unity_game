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

    private void OnMouseUp()
    {
        Destroy(gameObject);
        DataGlobal.instance.AllowMouseDown = true;
        MainCamera.instance.camLock = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
