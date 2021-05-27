using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using UnityEngine.UI;

public class Chuong : MonoBehaviour
{
    public int id;
    public int levelUnlock;
    public GameObject chuong2;
    public GameObject bien;
    public enum StateChuong { NONE, WAITING, DONE};
    public StateChuong state;

    public Transform[] vitri;

    public GameObject ga;
    public GameObject bo;
    public GameObject lon;
    public GameObject cuu;

    public List<GameObject> listVatNuoi = new List<GameObject>();

    public GameObject thu_hoach;
    public GameObject icon_thu_hoach;

    public Sprite[] listIconThuHoach;

    public GameObject effect;

    public GameObject effectThuHoach;
    public Material[] listMat;

    private DetailVatnuoi _vatnuoi;
    private int sl;

    public int currentTime;
    public int maxTime;
    public int timeOut;
    public int stt = 0;
    public int idVatnuoi;

    public Text textTime;
    public GameObject objectTime;

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            effect.SetActive(true);
        }
        LoadDataOnGame();
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
                if (state == StateChuong.NONE)
                {
                    UIManager.instance.ShowPanelChuong(id);
                } else if(state == StateChuong.WAITING)
                {
                    objectTime.SetActive(true);
                    StartCoroutine(HideTime());
                } else if(state == StateChuong.DONE)
                {
                    if(idVatnuoi == 13)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[0];
                        Destroy(ef, 3);
                    } else if(idVatnuoi == 14)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[1];
                        Destroy(ef, 3);
                    } else if(idVatnuoi == 15)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[2];
                        Destroy(ef, 3);
                    } else if(idVatnuoi == 16)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[3];
                        Destroy(ef, 3);
                    }

                    state = StateChuong.NONE;
                    PlayerPrefs.SetInt("statusChuong" + id, 0);

                    for (int i = 0; i < listVatNuoi.Count; i++)
                    {
                        Destroy(listVatNuoi[i]);
                    }
                    listVatNuoi.Clear();
                    DataGlobal.instance.ArrayAmount[idVatnuoi] += sl;
                    int reward = 0;
                    for (int i = 0; i < DataGlobal.instance.listVatNuoi.Count; i++)
                    {
                        if (idVatnuoi == DataGlobal.instance.listVatNuoi[i].id)
                        {
                            reward = DataGlobal.instance.listVatNuoi[i].rewards;
                            break;
                        }
                    }
                    DataGlobal.instance.AddStar(sl*reward);
                    thu_hoach.SetActive(false);
                }
            }
        }
    }

    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(5);
        objectTime.SetActive(false);
    }

    public void Build(DetailVatnuoi vatnuoi, int soluong)
    {
        state = StateChuong.WAITING;
        bien.SetActive(false);
        effect.SetActive(false);
        sl = soluong;
        PlayerPrefs.SetInt("soluongvatnuoi" + id, sl);
        PlayerPrefs.SetInt("idvatnuoi" + id, vatnuoi.id);
        _vatnuoi = vatnuoi;

        GetComponent<SpriteRenderer>().sprite = vatnuoi.imageChuong1;
        chuong2.GetComponent<SpriteRenderer>().sprite = vatnuoi.imageChuong2;
        StartCoroutine(VatNuoiBatDau());

        listVatNuoi.Clear();
        for (int i = 0; i < soluong; i++)
        {
            if(_vatnuoi.id == 13)
            {
                GameObject vn = Instantiate(ga, vitri[i].position, Quaternion.identity);
                listVatNuoi.Add(vn);
            } else if(_vatnuoi.id == 14)
            {
                GameObject vn = Instantiate(lon, vitri[i].position, Quaternion.identity);
                listVatNuoi.Add(vn);
            } else if(_vatnuoi.id == 15)
            {
                GameObject vn = Instantiate(bo, vitri[i].position, Quaternion.identity);
                listVatNuoi.Add(vn);
            } else if(_vatnuoi.id == 16)
            {
                GameObject vn = Instantiate(cuu, vitri[i].position, Quaternion.identity);
                listVatNuoi.Add(vn);
            }
        }

        for (int i = 0; i < listVatNuoi.Count; i++)
        {
            listVatNuoi[i].GetComponent<SkeletonAnimation>().AnimationName = "an";
        }

        if(_vatnuoi.id == 13)
        {
            icon_thu_hoach.GetComponent<SpriteRenderer>().sprite = listIconThuHoach[0];
        } else if(_vatnuoi.id == 14)
        {
            icon_thu_hoach.GetComponent<SpriteRenderer>().sprite = listIconThuHoach[1];
        } else if(_vatnuoi.id == 15)
        {
            icon_thu_hoach.GetComponent<SpriteRenderer>().sprite = listIconThuHoach[2];
        } else if(_vatnuoi.id == 16)
        {
            icon_thu_hoach.GetComponent<SpriteRenderer>().sprite = listIconThuHoach[3];
        }
    }

    IEnumerator VatNuoiBatDau()
    {
        /*
        state = StateChuong.WAITING;
        yield return new WaitForSeconds(_vatnuoi.time);
        for (int i = 0; i < listVatNuoi.Count; i++)
        {
            listVatNuoi[i].GetComponent<SkeletonAnimation>().AnimationName = "lo";
        }
        thu_hoach.SetActive(true);
        state = StateChuong.DONE;*/

        int timeMax = _vatnuoi.time;
        textTime.text = timeMax + "s";
        while (timeMax > 0)
        {
            yield return new WaitForSeconds(1);
            timeMax--;
            if(timeMax <= 0)
            {
                state = StateChuong.DONE;
                for (int i = 0; i < listVatNuoi.Count; i++)
                {
                    listVatNuoi[i].GetComponent<SkeletonAnimation>().AnimationName = "lo";
                }
                PlayerPrefs.SetInt("statusChuong" + id, 3);
            } else
            {
                state = StateChuong.WAITING;
                PlayerPrefs.SetInt("statusChuong" + id, 2);
            }
            textTime.text = timeMax + "s";
            PlayerPrefs.SetInt("timechuong" + id, timeMax);
            PlayerPrefs.SetString("timethuchuong" + id, DateTime.Now.ToString());
        }
    }

    public void LoadDataOnGame()
    {
        DetailVatnuoi _vatnuoi = null;
        stt = PlayerPrefs.GetInt("statusChuong" + id);
        if(stt == 0)
        {
            state = StateChuong.NONE;
        } else if(stt == 2)
        {
            timeOut = CurrencyManager.Offline(PlayerPrefs.GetString("timethuchuong" + id));
            idVatnuoi = PlayerPrefs.GetInt("idvatnuoi" + id);
            sl = PlayerPrefs.GetInt("soluongvatnuoi" + id);

            listVatNuoi.Clear();
            for (int i = 0; i < sl; i++)
            {
                if (idVatnuoi == 13)
                {
                    GameObject vn = Instantiate(ga, vitri[i].position, Quaternion.identity);
                    listVatNuoi.Add(vn);
                }
                else if (idVatnuoi == 14)
                {
                    GameObject vn = Instantiate(lon, vitri[i].position, Quaternion.identity);
                    listVatNuoi.Add(vn);
                }
                else if (idVatnuoi == 15)
                {
                    GameObject vn = Instantiate(bo, vitri[i].position, Quaternion.identity);
                    listVatNuoi.Add(vn);
                }
                else if (idVatnuoi == 16)
                {
                    GameObject vn = Instantiate(cuu, vitri[i].position, Quaternion.identity);
                    listVatNuoi.Add(vn);
                }
            }

            for (int i = 0; i < DataGlobal.instance.listVatNuoi.Count; i++)
            {
                if(idVatnuoi == DataGlobal.instance.listVatNuoi[i].id)
                {
                    _vatnuoi = DataGlobal.instance.listVatNuoi[i];
                    break;
                }
            }

            GetComponent<SpriteRenderer>().sprite = _vatnuoi.imageChuong1;
            chuong2.GetComponent<SpriteRenderer>().sprite = _vatnuoi.imageChuong2;

            Debug.Log("Time out: " + timeOut);
            Debug.Log("Vat nuoi time: " + _vatnuoi.time);
            if (timeOut > _vatnuoi.time)
            {
                state = StateChuong.DONE;
            }
            else
            {
                currentTime = Math.Abs(PlayerPrefs.GetInt("timechuong" + id) - timeOut);
                state = StateChuong.WAITING;
                StartCoroutine(CountTime(currentTime, _vatnuoi));
            }
        } else if(stt == 3)
        {
            state = StateChuong.DONE;
            for (int i = 0; i < DataGlobal.instance.listVatNuoi.Count; i++)
            {
                if (idVatnuoi == DataGlobal.instance.listVatNuoi[i].id)
                {
                    _vatnuoi = DataGlobal.instance.listVatNuoi[i];
                    break;
                }
            }
        }
    }

    IEnumerator CountTime(int currentTime, DetailVatnuoi vatnuoi)
    {
        textTime.text = currentTime + "s";
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            textTime.text = currentTime + "s";
            PlayerPrefs.SetInt("idvatnuoi" + id, vatnuoi.id);
            PlayerPrefs.SetString("timethuchuong" + id, DateTime.Now.ToString());

            if(state == StateChuong.NONE)
            {
                stt = 1;
            } else if(state == StateChuong.WAITING)
            {
                stt = 2;
            } else if(state == StateChuong.DONE)
            {
                stt = 3;
            }
            // stt = 1: NONE, stt = 2: WAITING, stt = 3: DONE;
            PlayerPrefs.SetInt("statusChuong" + id, stt);

            if(currentTime <= 1)
            {
                state = StateChuong.DONE;
            }
        }

    }
    // Luu vat nuoi va so luong vat nuoi
    // trang thai va thoi gian

    // Lay ra trang thai
    // Neu trong thi khong lam gi ca
    // Neu co vat nuoi thi xem thoi gian cua no ntn
    // Neu chua thu hoach duoc thi chay tiep
    // Neu thu hoach duoc roi thi thui
}
