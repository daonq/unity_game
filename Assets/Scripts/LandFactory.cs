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

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            if(DataGlobal.instance.GetLevel() >= levelUnlock)
            {
                if(stateLandFactory == StateLandFactory.NONE)
                {
                    UIManager.instance.OnClickToFactory(id);
                } else if(stateLandFactory == StateLandFactory.WAITING)
                {
                    UIManager.instance.OnClickToWaiting();
                } else if(stateLandFactory == StateLandFactory.BUILD)
                {
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
        idFactory = factory.id;
        this.factory = factory;
        stateLandFactory = StateLandFactory.WAITING;
        GetComponent<SpriteRenderer>().sprite = factory.sp0;
    }

    public void OnBuild()
    {
        stateLandFactory = StateLandFactory.BUILD;
        GetComponent<SpriteRenderer>().sprite = factory.sp1;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild1()
    {
        stateLandFactory = StateLandFactory.BUILD;
        GetComponent<SpriteRenderer>().sprite = factory.sp2;
        StartCoroutine(FactoryWork());
    }

    public void OnBuild2()
    {
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
        textCount.text = count + "/" + countMax;
        textTime.text = time + "/" + timeMax;
        while (count < countMax)
        {
            yield return new WaitForSeconds(1);
            if(time < timeMax)
            {
                time++;
                textTime.text = time + "/" + timeMax;
            } else
            {
                time = 0;
                count++;
                textCount.text = count + "/" + countMax;
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
        textCount.text = count + "/" + countMax;
    }
}
