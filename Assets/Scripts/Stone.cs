using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stone : MonoBehaviour
{
    public int id;
    public bool Cothepha;
    public Sprite sprHo;
    public Sprite anhGoc;

    public GameObject effect;

    public int currentTime;

    public GameObject phao; // Pháo :))))

    public GameObject tungDa;
    public Material mat;

    private string nameDown;

    private void Start()
    {
        currentTime = PlayerPrefs.GetInt("TimeStone" + id);
        if(currentTime > 0)
        {
            Cothepha = false;
        } else
        {
            Cothepha = true;
        }
        if (!Cothepha)
        {
            StartCoroutine(HoiSinh());
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

    public void Handler()
    {
        nameDown = "";
        if (DataGlobal.instance.ArrayHaveOwnedItem[1] > 0 && Cothepha)
        {
            phao.SetActive(true);
            StartCoroutine(HidePhao());
        }
        else if (DataGlobal.instance.ArrayHaveOwnedItem[1] == 0 && Cothepha)
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không có thuốc nổ để phá đá.\nVui lòng mua nó ở cửa hàng!" : "You don't have item.\nLet's buy it on market!");
        }
    }

    IEnumerator HidePhao()
    {
        yield return new WaitForSeconds(3);
        if (phao.activeSelf)
        {
            phao.SetActive(false);
        }
    }

    public void no()
    {
        GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(ef, 3);

        GameObject ef2 = Instantiate(tungDa, transform.position, Quaternion.identity);
        ef2.transform.Rotate(new Vector3(-90, 0, 0));
        ef2.GetComponent<ParticleSystemRenderer>().material = mat;
        Destroy(ef2, 3);

        //DataGlobal.instance.SubGold(10);
        DataGlobal.instance.ArrayHaveOwnedItem[1] -= 1;
        DataGlobal.instance.AddStone(2);
        DataGlobal.instance.AddStar(2);
        Cothepha = false;
        GetComponent<SpriteRenderer>().sprite = sprHo;
        currentTime = 100;
        PlayerPrefs.SetInt("TimeStone" + id, currentTime);
        StartCoroutine(HideHo());
        StartCoroutine(HoiSinh());
    }

    IEnumerator HideHo()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().sprite = null;
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
                if (nameDown == hit.collider?.name) Handler();
            }
#else
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (nameDown == hit.collider?.name) Handler();
        }
#endif
        }
    }

    // Can mot bien thoi gian o ngoai, moi s giam 1 don vi
    // Khi no bang 0 thi se chuyen ve anh goc
    // Khi bi pha se chuyen ve anh binh thuong

    IEnumerator HoiSinh()
    {
        
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            if (currentTime <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = anhGoc;
                Cothepha = true;
            }
            //else
            //{
            //    Cothepha = false;
            //    GetComponent<SpriteRenderer>().sprite = sprHo;
            //}
            PlayerPrefs.SetInt("TimeStone" + id, currentTime);
        }
    }
}
