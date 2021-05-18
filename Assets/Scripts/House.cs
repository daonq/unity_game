using UnityEngine;

public class House : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            UIManager.instance.ShowPanelHouse();
        }
    }

    public void NangCapNha(Sprite imgHouse)
    {
        GetComponent<SpriteRenderer>().sprite = imgHouse;
        DataGlobal.instance.AddLevelHouse();
    }
}
