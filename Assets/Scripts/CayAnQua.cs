using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CayAnQua : MonoBehaviour
{
    public Sprite spriteTao1;
    public Sprite spriteTao2;
    public Sprite spriteTao3;

    public Sprite spriteDao1;
    public Sprite spriteDao2;
    public Sprite spriteDao3;

    public Sprite spriteCam1;
    public Sprite spriteCam2;
    public Sprite spriteCam3;

    public bool Cothepha;
    public enum TypeCay { CAM, DAO, TAO}
    public TypeCay type;

    public GameObject vongtron;
    public Text textSoluong;

    private void Start()
    {
        switch (type)
        {
            case TypeCay.CAM:
                GetComponent<SpriteRenderer>().sprite = spriteCam3;
                break;
            case TypeCay.DAO:
                GetComponent<SpriteRenderer>().sprite = spriteDao3;
                break;
            case TypeCay.TAO:
                GetComponent<SpriteRenderer>().sprite = spriteTao3;
                break;
        }  
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown && Cothepha)
        {
            if(DataGlobal.instance.GetGold() >= 10)
            {
                DataGlobal.instance.SubGold(10);
                switch (type)
                {
                    case TypeCay.CAM:
                        DataGlobal.instance.ArrayAmount[12] += 5;
                        DataGlobal.instance.AddStar(2);
                        GetComponent<SpriteRenderer>().sprite = spriteTao1;
                        break;
                    case TypeCay.DAO:
                        DataGlobal.instance.ArrayAmount[10] += 5;
                        DataGlobal.instance.AddStar(2);
                        GetComponent<SpriteRenderer>().sprite = spriteDao1;
                        break;
                    case TypeCay.TAO:
                        DataGlobal.instance.ArrayAmount[11] += 5;
                        DataGlobal.instance.AddStar(2);
                        GetComponent<SpriteRenderer>().sprite = spriteTao1;
                        break;
                }
                Cothepha = false;
                StartCoroutine(HoiSinh());
            }
        }
    }

    IEnumerator HoiSinh()
    {
        switch (type)
        {
            case TypeCay.CAM:
                GetComponent<SpriteRenderer>().sprite = spriteCam1;
                yield return new WaitForSeconds(25);
                GetComponent<SpriteRenderer>().sprite = spriteCam2;
                yield return new WaitForSeconds(50);
                GetComponent<SpriteRenderer>().sprite = spriteCam3;
                break;
            case TypeCay.DAO:
                GetComponent<SpriteRenderer>().sprite = spriteDao1;
                yield return new WaitForSeconds(25);
                GetComponent<SpriteRenderer>().sprite = spriteDao2;
                yield return new WaitForSeconds(50);
                GetComponent<SpriteRenderer>().sprite = spriteDao3;
                break;
            case TypeCay.TAO:
                GetComponent<SpriteRenderer>().sprite = spriteTao1;
                yield return new WaitForSeconds(25);
                GetComponent<SpriteRenderer>().sprite = spriteTao2;
                yield return new WaitForSeconds(50);
                GetComponent<SpriteRenderer>().sprite = spriteTao3;
                break;
        }
        Cothepha = true;
    }
}
