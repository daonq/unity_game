using UnityEngine;
using UnityEngine.EventSystems;

public class EffectQuaCam : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject test;

    public void OnDrag(PointerEventData eventData)
    {
        MainCamera.instance.camLock = true;
        gameObject.SetActive(false);
        GameObject t = Instantiate(test, transform.position, Quaternion.identity);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }
}
