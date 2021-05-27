using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int id;
    public bool Cothepha;
    public Sprite sprHo;
    public Sprite anhGoc;

    public GameObject effect;

    public int currentTime;

    private void Start()
    {
        currentTime = PlayerPrefs.GetInt("TimeStone" + id);
        if(currentTime > 0)
        {
            Cothepha = false;
        } else
        {
            Cothepha = true;
        }
        if (!Cothepha)
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
                Destroy(ef, 3);
                DataGlobal.instance.SubGold(10);
                DataGlobal.instance.AddStone(2);
                DataGlobal.instance.AddStar(2);
                Cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sprHo;
                currentTime = 100;
                PlayerPrefs.SetInt("TimeStone" + id, currentTime);
                StartCoroutine(HoiSinh());
            }
        }
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Can mot bien thoi gian o ngoai, moi s giam 1 don vi
    // Khi no bang 0 thi se chuyen ve anh goc
    // Khi bi pha se chuyen ve anh binh thuong

    IEnumerator HoiSinh()
    {
        
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            if (currentTime <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = anhGoc;
                Cothepha = true;
            }
            else
            {
                Cothepha = false;
                GetComponent<SpriteRenderer>().sprite = sprHo;
            }
            PlayerPrefs.SetInt("TimeStone" + id, currentTime);
        }
    }
}
