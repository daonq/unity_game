using UnityEngine;
using UnityEngine.EventSystems;

public class EffectQuaCam : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject test;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        GameObject t = Instantiate(test, transform.position, Quaternion.identity);
    }
}
