using UnityEngine;

public class DragSeed : MonoBehaviour
{
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    private void OnMouseExit()
    {
        Destroy(gameObject);
    }
}
