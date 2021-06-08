using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LandFactory : MonoBehaviour
{
    public int id;
    public int levelUnlock;

    public int idLevelFactory;

    public enum StateLandFactory { NONE, WAITING, BUILD, BUILD1, BUILD2 }
    public StateLandFactory stateLandFactory;

    public int idFactory;
    private DetailFactory factory;

    public GameObject icon_thu_hoach;

    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textCount;

    public bool active;

    public GameObject effect;

    public GameObject hoinuoc;

    public int stt; // 0: None, 2: Waiting, 3: Build;

    public GameObject thuhoach;
    public GameObject icon_thuHoach;

    public GameObject effectBanTungToe;
    [HideInInspector] public Material matEffectTren;

    public GameObject bien;
    public Text textLevelUnlock;

    private string nameDown;

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            effect.SetActive(true);
        } else
        {
            effect.SetActive(false);
        }
        textLevelUnlock.text = DataGlobal.instance.tiengviet ? "Cấp độ " + levelUnlock : "Level " + levelUnlock;
        LoadDataOnGame();
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
        else if (DataGlobal.instance.AllowMouseDown && id == 0 && (DataGlobal.instance.GetFirstGame() == 1 || DataGlobal.instance.GetFirstGame() == 2))
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
        }
        else if(DataGlobal.instance.AllowMouseDown && id == 0 && (DataGlobal.instance.GetFirstGame() == 1 || DataGlobal.instance.GetFirstGame() == 2))
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
            if (stateLandFactory == StateLandFactory.NONE)
            {
                UIManager.instance.OnClickToFactory(id);
            }
            else if (stateLandFactory == StateLandFactory.WAITING)
            {
                UIManager.instance.OnClickToWaiting(factory);
            }
            else if (stateLandFactory == StateLandFactory.BUILD || stateLandFactory == StateLandFactory.BUILD1 || stateLandFactory == StateLandFactory.BUILD2)
            {
                active = true;
                UIManager.instance.ShowBuildFactory(factory, id);
            }
        }
        else
        {
            DataGlobal.instance.ClickObject = true;
            PanelNotify.instance.ShowContent(DataGlobal.instance.tiengviet ? "Vùng đất sẽ mở khóa khi bạn đạt cấp độ " + levelUnlock : "Land will unlock when you reach level " + levelUnlock);
        }
    }

    // State 0: None, 2:Waiting, 3:Build;
    public void OnWaiting(DetailFactory factory)
    {
        effect.SetActive(false);
        bien.SetActive(false);
        idFactory = factory.id;
        icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];
        this.factory = factory;
        stateLandFactory = StateLandFactory.WAITING;
        PlayerPrefs.SetInt("idFactory" + id, idFactory);
        PlayerPrefs.SetInt("stateLandFactory" + id, 2);
        GetComponent<SpriteRenderer>().sprite = factory.sp0;
    }

    public void OnBuild()
    {
        effect.SetActive(false);
        bien.SetActive(false);
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD;
        PlayerPrefs.SetInt("stateLandFactory" + id, 3);
        GetComponent<SpriteRenderer>().sprite = factory.sp1;
        GetTimeAndCount();
        StartCoroutine(FactoryWork());
    }

    public void OnBuild1()
    {
        effect.SetActive(false);
        bien.SetActive(false);
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD1;
        PlayerPrefs.SetInt("stateLandFactory" + id, 4);
        GetComponent<SpriteRenderer>().sprite = factory.sp2;
        GetTimeAndCount();
        StopCoroutine(FactoryWork());
        StartCoroutine(FactoryWork());
    }

    public void OnBuild2()
    {
        effect.SetActive(false);
        bien.SetActive(false);
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD2;
        PlayerPrefs.SetInt("stateLandFactory" + id, 5);
        GetComponent<SpriteRenderer>().sprite = factory.sp3;
        GetTimeAndCount();
        StopCoroutine(FactoryWork());
        StartCoroutine(FactoryWork());
    }

    int time, timeMax, count, countMax;
    int timeOutThoatGame, countDem;

    IEnumerator FactoryWork()
    {
        while (true)
        {
            if (active)
            {
                textCount.text = count + "/" + countMax;
                textTime.text = time + "/" + timeMax;
            }
            yield return new WaitForSeconds(1);
            if(time < timeMax)
            {
                time++;
                if (active)
                {
                    textTime.text = time + "/" + timeMax;
                }
            } else
            {
                time = 0;
                count++;
                thuhoach.SetActive(true);
                if (active)
                {
                    textCount.text = count + "/" + countMax;
                }
            }
            if(count >= countMax)
            {
                count = countMax;
            }
            PlayerPrefs.SetInt("countNhamay" + id, count);
        }
        //PlayerPrefs.SetString("timethucNhamay" + id, DateTime.Now.ToString());
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetString("timethucNhamay" + id, DateTime.Now.ToString());
    }

    public void UpdateIconThuHoach()
    {
        icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];
    }

    public void OnThuHoach(int idFactory)
    {
        thuhoach.SetActive(false);
        if(idFactory == 0)
        {
            DataGlobal.instance.AddWater(count);
            matEffectTren = DataGlobal.instance.listMatFactory[0];
        } else if(idFactory == 1)
        {
            DataGlobal.instance.AddStone(count);
            matEffectTren = DataGlobal.instance.listMatFactory[1];
        } else if(idFactory == 2)
        {
            DataGlobal.instance.AddWood(count);
            matEffectTren = DataGlobal.instance.listMatFactory[2];
        } else if(idFactory == 3)
        {
            DataGlobal.instance.AddGold(count);
            matEffectTren = DataGlobal.instance.listMatFactory[3];
        } else if(idFactory == 4)
        {
            DataGlobal.instance.AddOil(count);
            matEffectTren = DataGlobal.instance.listMatFactory[4];
        }
        GameObject efNe = Instantiate(effectBanTungToe, thuhoach.transform.position, Quaternion.identity);
        efNe.transform.Rotate(new Vector3(-90, 0, 0));
        efNe.GetComponent<ParticleSystemRenderer>().material = matEffectTren;
        Destroy(efNe, 3);

        /*
        GameObject starMove = Instantiate(DataGlobal.instance.ToMoveStar, thuhoach.transform.position, Quaternion.identity);
        starMove.GetComponent<MoveStar>().Move();
        //Destroy(starMove, 1);
        */
        DataGlobal.instance.AddStar(count);

        count = 0;
        PlayerPrefs.SetInt("countNhamay" + id, count);
        if (active)
        {
            textCount.text = count + "/" + countMax;
        }
    }

    public void setActive()
    {
        active = false;
    }

    public void LoadDataOnGame()
    {
        stt = PlayerPrefs.GetInt("stateLandFactory" + id);

        if(stt == 0)
        {
            stateLandFactory = StateLandFactory.NONE;
        } else if(stt == 2)
        {
            stateLandFactory = StateLandFactory.WAITING;
            effect.SetActive(false);
            bien.SetActive(false);
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if(idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }
            GetComponent<SpriteRenderer>().sprite = factory.sp0;
        } else if(stt == 3)
        {
            effect.SetActive(false);
            bien.SetActive(false);
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            idLevelFactory = 0;
            icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }

            countDem = PlayerPrefs.GetInt("countNhamay" + id);
            timeOutThoatGame = CurrencyManager.Offline(PlayerPrefs.GetString("timethucNhamay" + id));

            GetTimeAndCount();
            count = timeOutThoatGame / timeMax;
            count += countDem;
            if(count >= countMax)
            {
                count = countMax;
            }
            if(count > 0)
            {
                thuhoach.SetActive(true);
            }

            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD;
            GetComponent<SpriteRenderer>().sprite = factory.sp1;
            StartCoroutine(FactoryWork());
        } else if(stt == 4)
        {
            effect.SetActive(false);
            bien.SetActive(false);
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            idLevelFactory = 1;
            icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }

            countDem = PlayerPrefs.GetInt("countNhamay" + id);
            timeOutThoatGame = CurrencyManager.Offline(PlayerPrefs.GetString("timethucNhamay" + id));

            GetTimeAndCount();
            count = timeOutThoatGame / timeMax;
            count += countDem;
            if (count >= countMax)
            {
                count = countMax;
            }
            if (count > 0)
            {
                thuhoach.SetActive(true);
            }

            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD1;

            GetComponent<SpriteRenderer>().sprite = factory.sp2;
            StartCoroutine(FactoryWork());
        } else if(stt == 5)
        {
            effect.SetActive(false);
            bien.SetActive(false);
            idLevelFactory = 2;
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            icon_thuHoach.GetComponent<SpriteRenderer>().sprite = DataGlobal.instance.iconThuHoachFactory[idFactory];

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }

            countDem = PlayerPrefs.GetInt("countNhamay" + id);
            timeOutThoatGame = CurrencyManager.Offline(PlayerPrefs.GetString("timethucNhamay" + id));

            GetTimeAndCount();
            count = timeOutThoatGame / timeMax;
            count += countDem;
            if (count >= countMax)
            {
                count = countMax;
            }
            if (count > 0)
            {
                thuhoach.SetActive(true);
            }

            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD2;

            GetComponent<SpriteRenderer>().sprite = factory.sp3;
            StartCoroutine(FactoryWork());
        }
    }

    public void GetTimeAndCount()
    {
        if (DataGlobal.instance.levelCurrentOfFactory[id] == 0)
        {
            timeMax = factory.time1;
            countMax = factory.count1;
        }
        else if (DataGlobal.instance.levelCurrentOfFactory[id] == 1)
        {
            timeMax = factory.time2;
            countMax = factory.count2;
        }
        else if (DataGlobal.instance.levelCurrentOfFactory[id] == 2)
        {
            timeMax = factory.time3;
            countMax = factory.count3;
        }
    }
}
