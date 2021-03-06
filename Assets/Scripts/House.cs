using UnityEngine;
using UnityEngine.EventSystems;

public class House : MonoBehaviour
{
    private string nameDown;

    [SerializeField] Sprite[] sprites;

    private void Start()
    {
        if(DataGlobal.instance.GetLevelHouse() == 1)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        } else if(DataGlobal.instance.GetLevelHouse() == 2)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        } else if(DataGlobal.instance.GetLevelHouse() == 3)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown && !Tutorial.instance.modeTutorial)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#else
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#endif
        }
    }

    private void OnMouseUp()
    {
        if (DataGlobal.instance.AllowMouseDown && !Tutorial.instance.modeTutorial)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1, 1, 1);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                if (nameDown == hit.collider?.name)
                {
                    UIManager.instance.ShowPanelHouse();
                    nameDown = "";
                }
            }
#else
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (nameDown == hit.collider?.name)
            {
                UIManager.instance.ShowPanelHouse();
                nameDown = "";
            }
        }
#endif
        }
    }

    public void NangCapNha(Sprite imgHouse)
    {
        GetComponent<SpriteRenderer>().sprite = imgHouse;
        DataGlobal.instance.AddLevelHouse();
    }
}
