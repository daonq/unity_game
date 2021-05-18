using System.Collections;
using UnityEngine;

public class CayRung : MonoBehaviour
{
    public bool Cothepha;
    public Sprite sprite1;
    public Sprite sprite2;
    public int timeHs;

    private void Start()
    {
        if (!Cothepha)
        {
            StartCoroutine(HoiSinh());
        }
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown && Cothepha)
        {
            if(DataGlobal.instance.GetGold() >= 10)
            {
                DataGlobal.instance.SubGold(10);
                DataGlobal.instance.AddWood(2);
                Cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sprite1;
                StartCoroutine(HoiSinh());
            } else
            {
                Debug.Log("Ban khong du tien!");
            }
        } else
        {
            Debug.Log("Hien tai khong the pha huy!");
        }
    }

    IEnumerator HoiSinh()
    {
        yield return new WaitForSeconds(timeHs);
        GetComponent<SpriteRenderer>().sprite = sprite2;
        Cothepha = true;
    }
}
