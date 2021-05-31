using UnityEngine;
using UnityEngine.EventSystems;

public class OnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject lua;
    [SerializeField] private GameObject PanelSeed;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        PanelSeed.SetActive(false);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        lua = Instantiate(DataGlobal.instance.SeedObject, pos, Quaternion.identity);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }
}
