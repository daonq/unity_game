using System.Collections;
using UnityEngine;

public class cr4 : MonoBehaviour
{
    public bool cothepha;
    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;
    public Sprite sp4;
    public int time;

    public GameObject effect;
    public Material mat;

    private void Start()
    {
        if (!cothepha)
        {
            StartCoroutine(HoiSinh());
        }
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if(cothepha && DataGlobal.instance.GetGold() >= 10)
            {
                GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
                ef.transform.Rotate(new Vector3(-90, 0, 0));
                ef.GetComponent<ParticleSystemRenderer>().material = mat;
                Destroy(ef, 3);

                DataGlobal.instance.SubGold(10);
                DataGlobal.instance.AddWood(2);
                DataGlobal.instance.AddStar(2);

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
        GetComponent<SpriteRenderer>().sprite = sp1;
        yield return new WaitForSeconds(time / 4);
        GetComponent<SpriteRenderer>().sprite = sp2;
        yield return new WaitForSeconds(time / 2);
        GetComponent<SpriteRenderer>().sprite = sp3;
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().sprite = sp4;
        cothepha = true;
    }
}
