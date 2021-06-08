using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Land : MonoBehaviour
{
    public int id;
    public int levelUnlock;
    public GameObject Seed;
    public enum StateLand { NONE, SEEDED, DONE };
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
    public int stt = 0;

    private void Start()
    {
        if (DataGlobal.instance.GetLevel() < levelUnlock)
        {
            GetComponent<SpriteRenderer>().sprite = spriteLock;
        }
        //loadData();
        LoadDataOnGame();
    }

    // Luu khi thoat game
    // Lay ra khi vao lai game
    // Luu id cua hat dang trong tren o dat y
    // Luu thoi gian cua hat do
    // 

    void loadData()
    {
        // stt = 1: None, stt = 2: seeded, stt = 3: done
        stt = PlayerPrefs.GetInt("statusOdat");
        if (stt == 1)
        {
            stateLand = StateLand.NONE;
        }
        else if (stt == 2)
        {
            stateLand = StateLand.SEEDED;
        }
        else if (stt == 3)
        {
            stateLand = StateLand.DONE;
        }

        _idSeed = PlayerPrefs.GetInt("idhatgiong");


        //chua co cay gi
        //co cay va duoc thu hoach
        //da co cay va cay chua lon
        timeOut = CurrencyManager.Offline(PlayerPrefs.GetString("timethucodat" + id));
        if (timeOut >= PlayerPrefs.GetInt("timeodat" + id))
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

    private string nameDown;

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
        } else if(DataGlobal.instance.AllowMouseDown && id == 22 && DataGlobal.instance.GetFirstGame() == 0)
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
        } else if(DataGlobal.instance.AllowMouseDown && id == 22 && DataGlobal.instance.GetFirstGame() == 0)
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
        if (DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            if (stateLand == StateLand.NONE)
            {
                UIManager.instance.OnClickToLand(id);
            }
            else if (stateLand == StateLand.SEEDED)
            {
                objectTime.SetActive(true);
                StartCoroutine(HideTime());
            }
            else if (stateLand == StateLand.DONE)
            {
                OnDone(_idSeed);
            }
        }
        else
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Vùng đất sẽ mở khóa khi bạn đạt cấp độ " + levelUnlock : "Land will unlock when you reach level " + levelUnlock);
        }
    }

    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(5);
        objectTime.SetActive(false);
    }


    private int _exp;
    private Sprite _sp4;
    public GameObject roiSeed;
    public void Seeded(int id, int time, Sprite sp1, Sprite sp2, Sprite sp3, int exp, Sprite sp4, Material mat)
    {
        _sp4 = sp4;
        _exp = exp;
        _idSeed = id;
        this.mat = mat;
        GameObject rs = Instantiate(roiSeed, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        rs.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.listSeedsLucGieoHat[id];
        rs.transform.DOMoveY(transform.position.y, 0.9f);

        Destroy(rs, 1);
        StartCoroutine(OnSeeded(time, sp1, sp2, sp3));
    }

    IEnumerator OnSeeded(int time, Sprite sp1, Sprite sp2, Sprite sp3)
    {
        stateLand = StateLand.SEEDED;
        yield return new WaitForSeconds(1);
        Seed.GetComponent<SpriteRenderer>().sprite = sp1;
        int timeDefaut = time;
        textTime.text = formatTime(time);
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
            if (time == 1)
            {
                stateLand = StateLand.DONE;
                Seed.GetComponent<SpriteRenderer>().sprite = sp3;
            }

            if (stateLand == StateLand.NONE)
            {
                stt = 1;
            }
            else if (stateLand == StateLand.SEEDED)
            {
                stt = 2;
            }
            else if (stateLand == StateLand.DONE)
            {
                stt = 3;
            }
            // stt = 1: None, stt = 2: seeded, stt = 3: done
            PlayerPrefs.SetInt("statusOdat" + id, stt);

            textTime.text = formatTime(time);
        }
    }

    public string formatTime(int t)
    {
        int phut = t / 60;
        int s = t % 60;
        if (phut == 0)
        {
            return s + "s";
        }
        return phut + "m" + s + "s";
    }

    public void OnDone(int idSeed)
    {
        Seed.GetComponent<SpriteRenderer>().sprite = null;
        stateLand = StateLand.NONE;
        PlayerPrefs.SetInt("statusOdat" + id, 1);
        DataGlobal.instance.ArrayAmount[idSeed] += 5;
        DataGlobal.instance.UpdateDataAmount();
        DataGlobal.instance.AddStar(_exp);
        GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
        ef.transform.Rotate(new Vector3(-90, 0, 0));
        ef.GetComponent<ParticleSystemRenderer>().material = mat;
        Destroy(ef, 5);
    }

    // Day la o dat
    // No se co 3 trang thai: NONE > SEEDED > DONE.
    // Dau tien khi vao game minh se load ra trang thai cua o dat truoc do.
    // Neu NONE thi khong lam gi ca.
    // Neu SEEDED thi lay ra id cua seed sau do chen sprite cua Seed vao.
    // Sau do lay time cua seed tru di time thoat game, neu co the thu hoach thi chuyen state sang DONE
    // Se co mot ham nhan vao idSeed, trang thai o dat.

    public void LoadDataOnGame()
    {
        DetailSeed seed = null;
        // Lay trang thai o dat dau tien
        stt = PlayerPrefs.GetInt("statusOdat" + id);
        if (stt == 1)
        {
            stateLand = StateLand.NONE;
        }
        else if (stt == 2)
        {
            timeOut = CurrencyManager.Offline(PlayerPrefs.GetString("timethucodat" + id));
            _idSeed = PlayerPrefs.GetInt("idhatgiong" + id);

            for (int i = 0; i < DataGlobal.instance.listSeed.Count; i++)
            {
                if (_idSeed == DataGlobal.instance.listSeed[i].id)
                {
                    seed = DataGlobal.instance.listSeed[i];
                    break;
                }
            }

            if (timeOut >= seed.time)
            {
                stateLand = StateLand.DONE;
                Seed.GetComponent<SpriteRenderer>().sprite = seed.spr3;
            }
            else
            {
                currentTime = PlayerPrefs.GetInt("timeodat" + id) - timeOut;
                stateLand = StateLand.SEEDED;
                StartCoroutine(CountTime(currentTime, seed));
            }
            mat = seed.mat;
            _exp = seed.exp;

        }
        else if (stt == 3)
        {
            _idSeed = PlayerPrefs.GetInt("idhatgiong" + id);
            for (int i = 0; i < DataGlobal.instance.listSeed.Count; i++)
            {
                if (_idSeed == DataGlobal.instance.listSeed[i].id)
                {
                    seed = DataGlobal.instance.listSeed[i];
                    break;
                }
            }
            mat = seed.mat;
            _exp = seed.exp;
            Seed.GetComponent<SpriteRenderer>().sprite = seed.spr3;
            stateLand = StateLand.DONE;
        }
    }

    IEnumerator CountTime(int currentTime, DetailSeed seed)
    {
        textTime.text = formatTime(currentTime);
        if (currentTime > seed.time / 2 && currentTime <= seed.time)
        {
            Seed.GetComponent<SpriteRenderer>().sprite = seed.spr1;
        }
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;

            PlayerPrefs.SetInt("idhatgiong" + id, _idSeed);
            PlayerPrefs.SetString("timethucodat" + id, DateTime.Now.ToString());

            if (stateLand == StateLand.NONE)
            {
                stt = 1;
            }
            else if (stateLand == StateLand.SEEDED)
            {
                stt = 2;
            }
            else if (stateLand == StateLand.DONE)
            {
                stt = 3;
            }
            // stt = 1: None, stt = 2: seeded, stt = 3: done
            PlayerPrefs.SetInt("statusOdat" + id, stt);

            if (currentTime <= seed.time / 2 && currentTime > 1)
            {
                Seed.GetComponent<SpriteRenderer>().sprite = seed.spr2;
            }
            if (currentTime <= 1)
            {
                stateLand = StateLand.DONE;
                Seed.GetComponent<SpriteRenderer>().sprite = seed.spr3;
            }
            textTime.text = formatTime(currentTime);
        }
    }
}
