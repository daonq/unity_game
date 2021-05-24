using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// Quả nhé không phải quà!
public class Qua : MonoBehaviour
{
    public Sprite qua1;
    public Sprite qua2;
    public Sprite qua3;

    public bool Cothepha;

    public GameObject gio;
    public Text textSoluong;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = qua3;
        textSoluong.text = DataGlobal.instance.ArrayHaveOwnedItem[2].ToString();
        //vongtron.transform.DOScale(Vector3.zero, 1);
        gio.SetActive(false);
    }

    private void OnMouseDown()
    {
        textSoluong.text = DataGlobal.instance.ArrayHaveOwnedItem[2].ToString();
        if (DataGlobal.instance.AllowMouseDown && Cothepha)
        {
            //vongtron.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1);
            gio.SetActive(true);
            StartCoroutine(Hide());
        }
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(1);
        gio.SetActive(false);
        //vongtron.transform.DOScale(Vector3.zero, 1);
    }
}
