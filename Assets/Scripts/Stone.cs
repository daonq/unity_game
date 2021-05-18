using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool Cothepha;
    public Sprite sprHo;
    public Sprite anhGoc;

    private void Start()
    {
        if(!Cothepha)
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
                DataGlobal.instance.AddStone(2);
                Cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sprHo;
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
        yield return new WaitForSeconds(100);
        GetComponent<SpriteRenderer>().sprite = anhGoc;
        Cothepha = true;
    }
}
