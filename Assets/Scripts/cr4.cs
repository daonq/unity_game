using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        time = PlayerPrefs.GetInt("Timecr4" + id);
        if(time > 0)
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
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if(cothepha && DataGlobal.instance.ArrayHaveOwnedItem[0] > 0)
            {
                cua.SetActive(true);
                StartCoroutine(HideCua());
            }
            else
            {
                DataGlobal.instance.ClickObject = true;
                PanelNotify.instance.ShowContent("You don't have item. Let's buy it on market!");
            }
            if(DataGlobal.instance.firstGame == 6)
            {
                DataGlobal.instance.firstGame = 7;
                Tutorial.instance.caitay.GetComponent<RectTransform>().localPosition = new Vector3(-350, 75, 0);
            }
            //else if(DataGlobal.instance.GetGold() < 10)
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
        Tutorial.instance.caitay.SetActive(false);
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
                GetComponent<SpriteRenderer>().sprite = sp4;
                GetComponent<Animator>().enabled = true;
            } else if(time > 0 && time <= 60)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp3;
            } else if(time > 60 && time <= 120)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp2;
            } else if(time > 120 && time <= 180)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sp1;
            }
            PlayerPrefs.SetInt("Timecr4" + id, time);
        }
    }
}
