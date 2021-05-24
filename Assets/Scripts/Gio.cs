using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Gio : MonoBehaviour
{
    public int idAmount; // 12: Cam, 11: Tao, 10: Dao
    public GameObject cay;
    public GameObject qua;
    public Material mat;

    private void OnMouseDown()
    {
        if(DataGlobal.instance.ArrayHaveOwnedItem[2] > 0)
        {
            DataGlobal.instance.ArrayHaveOwnedItem[2] -= 1;
            DataGlobal.instance.AddStar(2);
            DataGlobal.instance.ArrayAmount[idAmount] += 5;
            cay.GetComponent<Qua>().textSoluong.text = DataGlobal.instance.ArrayHaveOwnedItem[2].ToString();
            cay.GetComponent<Qua>().Cothepha = false;
            //cay.GetComponent<Qua>().vongtron.transform.DOScale(Vector3.zero, 1);
            GameObject ef = Instantiate(qua, cay.transform.position, Quaternion.identity);
            ef.transform.Rotate(new Vector3(-90, 0, 0));
            ef.GetComponent<ParticleSystemRenderer>().material = mat;
            Destroy(ef, 3);
            StartCoroutine(HoiSinh());
        }
        else
        {
            Debug.Log("Ban khong co gio cay!");
        }
    }

    IEnumerator HoiSinh()
    {
        cay.GetComponent<SpriteRenderer>().sprite = cay.GetComponent<Qua>().qua1;
        yield return new WaitForSeconds(30);
        cay.GetComponent<SpriteRenderer>().sprite = cay.GetComponent<Qua>().qua2;
        yield return new WaitForSeconds(60);
        cay.GetComponent<SpriteRenderer>().sprite = cay.GetComponent<Qua>().qua3;
        cay.GetComponent<Qua>().Cothepha = true;
    }
}
