using UnityEngine;
using UnityEngine.EventSystems;

public class OnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject lua;
    [SerializeField] private GameObject PanelSeed;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        lua = Instantiate(DataGlobal.instance.SeedObject);
        PanelSeed.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; 
    }
}
