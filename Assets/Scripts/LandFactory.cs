using UnityEngine;
using System.Collections;
using TMPro;

public class LandFactory : MonoBehaviour
{
    public int id;
    public int levelUnlock;

    public enum StateLandFactory { NONE, WAITING, BUILD}
    public StateLandFactory stateLandFactory;

    private int idFactory;
    private DetailFactory factory;

    public GameObject icon_thu_hoach;

    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textCount;

    public bool active;

    public GameObject effect;

    public GameObject hoinuoc;

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            effect.SetActive(true);
        } else
        {
            effect.SetActive(false);
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
                if(stateLandFactory == StateLandFactory.NONE)
                {
                    UIManager.instance.OnClickToFactory(id);
                } else if(stateLandFactory == StateLandFactory.WAITING)
                {
                    UIManager.instance.OnClickToWaiting();
                } else if(stateLandFactory == StateLandFactory.BUILD)
                {
                    active = true;
                    UIManager.instance.ShowBuildFactory();
                }
            } else
            {
                Debug.Log("You need to level " + levelUnlock +
                    " to be able to open this land!");
            }
        }
    }

    public void OnWaiting(DetailFactory factory)
    {
        effect.SetActive(false);
        idFactory = factory.id;
        this.factory = factory;
        stateLandFactory = StateLandFactory.WAITING;
        GetComponent<SpriteRenderer>().sprite = factory.sp0;
    }

    public void OnBuild()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD;
        GetComponent<SpriteRenderer>().sprite = factory.sp1;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild1()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD;
        GetComponent<SpriteRenderer>().sprite = factory.sp2;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild2()
    {
        if (factory.id == 0)
        {
            GameObject hn = Instantiate(hoinuoc, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        stateLandFactory = StateLandFactory.BUILD;
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

        if (active)
        {
            textCount.text = count + "/" + countMax;
            textTime.text = time + "/" + timeMax;
        }

        while (count < countMax)
        {
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
}
