using System.Collections;
using UnityEngine;

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

    private void OnMouseDown()
    {
        if(DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if(cothepha && DataGlobal.instance.ArrayHaveOwnedItem[0] > 0)
            {
                cua.SetActive(true);
                StartCoroutine(HideCua());
            }
            else
            {
                PanelNotify.instance.ShowContent("You don't have item. Let's buy it on market!");
            }
            //else if (DataGlobal.instance.GetGold() < 10)
            //{
            //    DataGlobal.instance.goldUI.GetComponent<Animator>().SetBool("hetTien", true);
            //    StartCoroutine(HetTienEnd());
            //}
        }
    }

    //IEnumerator HetTienEnd()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    DataGlobal.instance.goldUI.GetComponent<Animator>().SetBool("hetTien", false);
    //}

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

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
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
