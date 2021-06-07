using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Su dung cho nhung cay co 2 trang thai sprite thoi
public class cr2 : MonoBehaviour
{
    public int id;
    public bool cothepha;
    public Sprite sp1;
    public Sprite sp2;

    public GameObject effect;
    public Material mat;

    public int time;

    public GameObject cua; // Cái cưa ý nhé :v

    public GameObject animationCua;

    private string nameDown;

    private void Start()
    {
        time = PlayerPrefs.GetInt("Timecr2" + id);
        if(time > 0)
        {
            cothepha = false;
        } else
        {
            cothepha = true;
        }

        if (!cothepha)
        {
            StartCoroutine(HoiSinh());
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
                    Handler();
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
                Handler();
            }
        }
#endif
        }
    }

    private void OnMouseDown()
    {
        if(DataGlobal.instance.AllowMouseDown && !Tutorial.instance.modeTutorial)
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
        if (cothepha && DataGlobal.instance.ArrayHaveOwnedItem[0] > 0)
        {
            cua.SetActive(true);
            StartCoroutine(HideCua());
        }
        else if(cothepha && DataGlobal.instance.ArrayHaveOwnedItem[0] <= 0)
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không có cưa để chặt cây.\nVui lòng mua nó ở cửa hàng!" : "You don't have item.\nLet's buy it on market!");
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

        DataGlobal.instance.ArrayHaveOwnedItem[0] -= 1;
        DataGlobal.instance.AddWood(2);
        DataGlobal.instance.AddStar(2);

        cothepha = false;
        GetComponent<SpriteRenderer>().sprite = sp1;
        time = 180;
        PlayerPrefs.SetInt("Timecr2" + id, time);
        animationCua.SetActive(false);

        StartCoroutine(HoiSinh());
    }


    IEnumerator HoiSinh()
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            if(time <= 0)
            {
                cothepha = true;
                GetComponent<SpriteRenderer>().sprite = sp2;
                GetComponent<Animator>().enabled = true;
            } else
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp1;
            }
            PlayerPrefs.SetInt("Timecr2" + id, time);
        }
    }
}
