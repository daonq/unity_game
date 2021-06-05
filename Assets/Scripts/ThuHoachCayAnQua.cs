using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThuHoachCayAnQua : MonoBehaviour
{
    public int id;
    public int indexAmount;
    public GameObject qua;
    public Material mat;

    public Sprite status1;
    public Sprite status2;
    public Sprite status3;

    public bool cothepha;

    public int currentTime;

    public GameObject gio;

    private string nameDown;

    private void Start()
    {
        currentTime = PlayerPrefs.GetInt("TimeCayAnQua" + id);
        if(currentTime > 0)
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

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
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
        if (cothepha && DataGlobal.instance.ArrayHaveOwnedItem[2] > 0)
        {
            gio.SetActive(true);
            StartCoroutine(HideGio());
        }
        else
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Bạn không có giỏ cây để hái quả.\nVui lòng mua nó ở cửa hàng!" : "You don't have item. Let's buy it on market!");
        }
    }

    IEnumerator HideGio()
    {
        yield return new WaitForSeconds(3);
        if (gio.activeSelf)
        {
            gio.SetActive(false);
        }
    }

    public void thuhoach()
    {
        DataGlobal.instance.ArrayHaveOwnedItem[2] -= 1;
        DataGlobal.instance.AddStar(2);
        DataGlobal.instance.ArrayAmount[indexAmount] += 5;
        DataGlobal.instance.UpdateDataAmount();

        GameObject ef = Instantiate(qua, transform.position, Quaternion.identity);
        ef.transform.Rotate(new Vector3(-90, 0, 0));
        ef.GetComponent<ParticleSystemRenderer>().material = mat;
        Destroy(ef, 3);
        cothepha = false;
        GetComponent<SpriteRenderer>().sprite = status1;
        currentTime = 200;
        PlayerPrefs.SetInt("TimeCayAnQua" + id, currentTime);
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
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            if(currentTime <= 0)
            {
                cothepha = true;
                GetComponent<SpriteRenderer>().sprite = status3;
            } else if(currentTime > 0 && currentTime <= 100)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = status2;
            } else if(currentTime > 100 && currentTime <= 200)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = status1;
            }
            PlayerPrefs.SetInt("TimeCayAnQua" + id, currentTime);
        }
    }
}
