using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Land : MonoBehaviour
{
    public int id;
    public int levelUnlock;
    public GameObject Seed;
    public enum StateLand { NONE, SEEDED, DONE};
    public StateLand stateLand;

    private int _idSeed;

    public Text textTime;
    public GameObject objectTime;

    public Sprite spriteLock;

    public GameObject effect;

    private Material mat;

    public int currentTime;
    public int maxTime;
    public int timeOut;

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() < levelUnlock)
        {
            GetComponent<SpriteRenderer>().sprite = spriteLock;
        }
        loadData();
    }
    void loadData()
    {
        //chua co cay gi
        //co cay va duoc thu hoach
        //da co cay va cay chua lon
        timeOut = CurrencyManager.Offline(PlayerPrefs.GetString("timethucodat" + id));
        if(timeOut >= PlayerPrefs.GetInt("timeodat" + id))
        {
            //cay da lon va duoc thu hoach
        }
        else
        {
            //cay chua lon va tinh time con lai
            currentTime = PlayerPrefs.GetInt("timeodat" + id) - timeOut;
            //.....
        }
    }
    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if (DataGlobal.instance.GetLevel() >= levelUnlock)
            {
                if (stateLand == StateLand.NONE)
                {
                    UIManager.instance.OnClickToLand(id);
                } else if(stateLand == StateLand.SEEDED)
                {
                    objectTime.SetActive(true);
                    StartCoroutine(HideTime());
                } else if(stateLand == StateLand.DONE)
                {
                    OnDone(_idSeed);
                }
            } else
            {
                UIManager.instance.Hienthongbao("The plot of land is opened at the level " + levelUnlock);
            }
        }
    }

    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(5);
        objectTime.SetActive(false);
    }

    private int _exp;
    private Sprite _sp4;
    public void Seeded(int id, int time, Sprite sp1, Sprite sp2, Sprite sp3, int exp, Sprite sp4, Material mat)
    {
        _sp4 = sp4;
        _exp = exp;
        _idSeed = id;
        this.mat = mat;
        StartCoroutine(OnSeeded(time, sp1, sp2, sp3));
    }

    IEnumerator OnSeeded(int time, Sprite sp1, Sprite sp2, Sprite sp3)
    {
        stateLand = StateLand.SEEDED;
        Seed.GetComponent<SpriteRenderer>().sprite = sp1;
        int timeDefaut = time;
        textTime.text = time + "s";
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;

            PlayerPrefs.SetInt("idhatgiong" + id, _idSeed);
            PlayerPrefs.SetInt("timeodat" + id, time);
            PlayerPrefs.SetString("timethucodat" + id, DateTime.Now.ToString());

            if (time == timeDefaut / 2)
            {
                Seed.GetComponent<SpriteRenderer>().sprite = sp2;
            }
            if(time == 1)
            {
                stateLand = StateLand.DONE;
                Seed.GetComponent<SpriteRenderer>().sprite = sp3;
            }
            textTime.text = time + "s";
        }
    }

    public void OnDone(int idSeed)
    {
        Seed.GetComponent<SpriteRenderer>().sprite = null;
        stateLand = StateLand.NONE;
        DataGlobal.instance.ArrayAmount[idSeed] += 5;
        DataGlobal.instance.AddStar(_exp);
        GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
        ef.transform.Rotate(new Vector3(-90, 0, 0));
        ef.GetComponent<ParticleSystemRenderer>().material = mat;
        Destroy(ef, 5);
    }
}
