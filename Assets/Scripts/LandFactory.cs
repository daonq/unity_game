using UnityEngine;
using System.Collections;
using TMPro;

public class LandFactory : MonoBehaviour
{
    public int id;
    public int levelUnlock;

    public int idLevelFactory;

    public enum StateLandFactory { NONE, WAITING, BUILD, BUILD1, BUILD2}
    public StateLandFactory stateLandFactory;

    private int idFactory;
    private DetailFactory factory;

    public GameObject icon_thu_hoach;

    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textCount;

    public bool active;

    public GameObject effect;

    public GameObject hoinuoc;

    public bool ELSE = false;

    public int stt; // 0: None, 2: Waiting, 3: Build;

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            effect.SetActive(true);
        } else
        {
            effect.SetActive(false);
        }
        LoadDataOnGame();
    }


    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
        if (ELSE)
        {
            ELSE = false;
            //UIManager.instance.AnThongBao();
        }
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if (DataGlobal.instance.GetLevel() >= levelUnlock)
            {
                if(stateLandFactory == StateLandFactory.NONE)
                {
                    UIManager.instance.OnClickToFactory(id);
                } else if(stateLandFactory == StateLandFactory.WAITING)
                {
                    UIManager.instance.OnClickToWaiting(factory);
                } else if(stateLandFactory == StateLandFactory.BUILD || stateLandFactory == StateLandFactory.BUILD1 || stateLandFactory == StateLandFactory.BUILD2)
                {
                    active = true;
                    UIManager.instance.ShowBuildFactory(factory);
                }
            } else
            {
                ELSE = true;
                //UIManager.instance.Hienthongbao("You need to level " + levelUnlock + " to be able to open this land!");
            }
        }
    }

    // State 0: None, 2:Waiting, 3:Build;
    public void OnWaiting(DetailFactory factory)
    {
        effect.SetActive(false);
        idFactory = factory.id;
        this.factory = factory;
        stateLandFactory = StateLandFactory.WAITING;
        PlayerPrefs.SetInt("idFactory" + id, idFactory);
        PlayerPrefs.SetInt("stateLandFactory" + id, 2);
        GetComponent<SpriteRenderer>().sprite = factory.sp0;
    }

    public void OnBuild()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD;
        PlayerPrefs.SetInt("stateLandFactory" + id, 3);
        GetComponent<SpriteRenderer>().sprite = factory.sp1;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild1()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD1;
        PlayerPrefs.SetInt("stateLandFactory" + id, 4);
        GetComponent<SpriteRenderer>().sprite = factory.sp2;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild2()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD2;
        PlayerPrefs.SetInt("stateLandFactory" + id, 5);
        GetComponent<SpriteRenderer>().sprite = factory.sp3;
        StartCoroutine(FactoryWork());
    }

    int time, timeMax, count, countMax;

    IEnumerator FactoryWork()
    {
        if(DataGlobal.instance.levelCurrentOfFactory[id] == 0)
        {
            timeMax = factory.time1;
            countMax = factory.count1;
        } else if(DataGlobal.instance.levelCurrentOfFactory[id] == 1)
        {
            timeMax = factory.time2;
            countMax = factory.count2;
        } else if(DataGlobal.instance.levelCurrentOfFactory[id] == 2)
        {
            timeMax = factory.time3;
            countMax = factory.count3;
        }

        time = 0;
        count = 0;

        

        while (count < countMax)
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
                if (active)
                {
                    textCount.text = count + "/" + countMax;
                }
            }
        }
    }

    public void OnThuHoach(int idFactory)
    {
        if(idFactory == 0)
        {
            DataGlobal.instance.AddWater(count);
        } else if(idFactory == 1)
        {
            DataGlobal.instance.AddStone(count);
        } else if(idFactory == 2)
        {
            DataGlobal.instance.AddWood(count);
        } else if(idFactory == 3)
        {
            DataGlobal.instance.AddGold(count);
        } else if(idFactory == 4)
        {
            DataGlobal.instance.AddOil(count);
        }
        count = 0;
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
            idFactory = PlayerPrefs.GetInt("idFactory" + id);

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
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD;
            idLevelFactory = 0;
            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }
            GetComponent<SpriteRenderer>().sprite = factory.sp1;
            StartCoroutine(FactoryWork());
        } else if(stt == 4)
        {
            idLevelFactory = 1;
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD1;

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }
            GetComponent<SpriteRenderer>().sprite = factory.sp2;
            StartCoroutine(FactoryWork());
        } else if(stt == 5)
        {
            idLevelFactory = 2;
            idFactory = PlayerPrefs.GetInt("idFactory" + id);
            if (idFactory == 0)
            {
                GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            }
            stateLandFactory = StateLandFactory.BUILD2;

            for (int i = 0; i < DataGlobal.instance.listFactory.Count; i++)
            {
                if (idFactory == DataGlobal.instance.listFactory[i].id)
                {
                    factory = DataGlobal.instance.listFactory[i];
                    break;
                }
            }
            GetComponent<SpriteRenderer>().sprite = factory.sp3;
            StartCoroutine(FactoryWork());
        }
    }
}
