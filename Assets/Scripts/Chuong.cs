using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

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

    private void Start()
    {
        if(DataGlobal.instance.GetLevel() >= levelUnlock)
        {
            effect.SetActive(true);
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
                if (state == StateChuong.NONE)
                {
                    UIManager.instance.ShowPanelChuong(id);
                } else if(state == StateChuong.WAITING)
                {
                    Debug.Log("Dang phat trien");
                } else if(state == StateChuong.DONE)
                {
                    if(_vatnuoi.id == 13)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[0];
                        Destroy(ef, 3);
                    } else if(_vatnuoi.id == 14)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[1];
                        Destroy(ef, 3);
                    } else if(_vatnuoi.id == 15)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[2];
                        Destroy(ef, 3);
                    } else if(_vatnuoi.id == 16)
                    {
                        GameObject ef = Instantiate(effectThuHoach, transform.position, Quaternion.identity);
                        ef.transform.Rotate(new Vector3(-90, 0, 0));
                        ef.GetComponent<ParticleSystemRenderer>().material = listMat[3];
                        Destroy(ef, 3);
                    }

                    state = StateChuong.NONE;
                    for (int i = 0; i < listVatNuoi.Count; i++)
                    {
                        Destroy(listVatNuoi[i]);
                    }
                    listVatNuoi.Clear();
                    DataGlobal.instance.ArrayAmount[_vatnuoi.id] += sl;
                    DataGlobal.instance.AddStar(sl*_vatnuoi.rewards);
                    thu_hoach.SetActive(false);
                }
            } else
            {
                Debug.Log("Ban chua du level de mo khoa!");
            }
        }
    }

    private DetailVatnuoi _vatnuoi;
    private int sl;
    public void Build(DetailVatnuoi vatnuoi, int soluong)
    {
        bien.SetActive(false);
        effect.SetActive(false);
        sl = soluong;
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
        state = StateChuong.WAITING;
        yield return new WaitForSeconds(_vatnuoi.time);
        for (int i = 0; i < listVatNuoi.Count; i++)
        {
            listVatNuoi[i].GetComponent<SkeletonAnimation>().AnimationName = "lo";
        }
        thu_hoach.SetActive(true);
        state = StateChuong.DONE;
    }
}
