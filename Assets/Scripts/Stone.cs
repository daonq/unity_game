using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool Cothepha;
    public Sprite sprHo;
    public Sprite anhGoc;

    public GameObject effect;

    private void Start()
    {
        if(!Cothepha)
        {
            StartCoroutine(HoiSinh());
        }
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if (DataGlobal.instance.GetGold() >= 10 && Cothepha)
            {
                GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
                //ef.transform.Rotate(new Vector3(-90, 0, 0));
                //ef.GetComponent<ParticleSystemRenderer>().material = mat;
                Destroy(ef, 3);

                DataGlobal.instance.SubGold(10);
                DataGlobal.instance.AddStone(2);
                DataGlobal.instance.AddStar(2);
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

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator HoiSinh()
    {
        yield return new WaitForSeconds(100);
        GetComponent<SpriteRenderer>().sprite = anhGoc;
        Cothepha = true;
    }
}
