using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThuHoachCayAnQua : MonoBehaviour
{
    public int indexAmount;
    public GameObject qua;
    public Material mat;

    public Sprite status1;
    public Sprite status2;
    public Sprite status3;

    public bool cothepha;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if (cothepha)
            {
                DataGlobal.instance.ArrayHaveOwnedItem[2] -= 1;
                DataGlobal.instance.AddStar(2);
                DataGlobal.instance.ArrayAmount[indexAmount] += 5;
                GameObject ef = Instantiate(qua, transform.position, Quaternion.identity);
                ef.transform.Rotate(new Vector3(-90, 0, 0));
                ef.GetComponent<ParticleSystemRenderer>().material = mat;
                Destroy(ef, 3);
                StartCoroutine(HoiSinh());
            }
        }
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator HoiSinh()
    {
        cothepha = false;
        GetComponent<SpriteRenderer>().sprite = status1;
        yield return new WaitForSeconds(60);
        GetComponent<SpriteRenderer>().sprite = status2;
        yield return new WaitForSeconds(60);
        GetComponent<SpriteRenderer>().sprite = status3;
        cothepha = true;
    }
}
