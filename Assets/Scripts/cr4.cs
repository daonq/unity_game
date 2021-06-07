using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class cr4 : MonoBehaviour
{
    public int id;
    public bool cothepha;
    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;
    public Sprite sp4;
    public int time;

    public GameObject effect;
    public Material mat;

    public GameObject cua; // Cái cưa ý nhé :v

    public GameObject animationCua;

    private string nameDown;

    private void Start()
    {
        time = PlayerPrefs.GetInt("Timecr4" + id);
        if (time > 0)
        {
            cothepha = false;
        }
        else
        {
            cothepha = true;
        }

        if (!cothepha)
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
            else nameDown = "";
#endif
        }
        else if(DataGlobal.instance.AllowMouseDown && id == 65)
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
            else nameDown = "";
#endif
        }
    }

    public void Handler()
    {
        nameDown = "";
        if (cothepha && DataGlobal.instance.ArrayHaveOwnedItem[0] > 0)
        {
            cua.SetActive(true);
            StartCoroutine(HideCua());
        }
        else
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không có cưa để chặt cây.\nVui lòng mua nó ở cửa hàng!" : "You don't have item. Let's buy it on market!");
        }
        if (DataGlobal.instance.firstGame == 6)
        {
            DataGlobal.instance.firstGame = 7;
            Tutorial.instance.caitay.GetComponent<RectTransform>().localPosition = new Vector3(-350, 75, 0);
        }
    }

    IEnumerator HideCua()
    {
        yield return new WaitForSeconds(3);
        if (cua.activeSelf)
        {
            cua.SetActive(false);
        }
    }

    public void cuanhe()
    {
        Tutorial.instance.caitay.SetActive(false);
        Tutorial.instance.EndTutorial();
        GetComponent<Animator>().enabled = false;
        animationCua.SetActive(true);
        StartCoroutine(HieuUng());
    }

    IEnumerator HieuUng()
    {
        yield return new WaitForSeconds(3);
        GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
        ef.transform.Rotate(new Vector3(-90, 0, 0));
        ef.GetComponent<ParticleSystemRenderer>().material = mat;
        Destroy(ef, 3);

        //DataGlobal.instance.SubGold(10);
        DataGlobal.instance.ArrayHaveOwnedItem[0] -= 1;
        DataGlobal.instance.AddWood(2);
        DataGlobal.instance.AddStar(2);

        cothepha = false;
        GetComponent<SpriteRenderer>().sprite = sp1;
        time = 180;
        PlayerPrefs.SetInt("Timecr4" + id, time);
        animationCua.SetActive(false);

        StartCoroutine(HoiSinh());
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (nameDown == hit.collider?.name) Handler();
    }

    IEnumerator HoiSinh()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {
                cothepha = true;
                GetComponent<SpriteRenderer>().sprite = sp4;
                GetComponent<Animator>().enabled = true;
            }
            else if (time > 0 && time <= 60)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp3;
            }
            else if (time > 60 && time <= 120)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp2;
            }
            else if (time > 120 && time <= 180)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp1;
            }
            PlayerPrefs.SetInt("Timecr4" + id, time);
        }
    }
}
