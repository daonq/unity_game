using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Market : MonoBehaviour
{
    //public GameObject vongtron;

    //private void Start()
    //{
    //    vongtron.transform.localScale = Vector3.zero;
    //}

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            //vongtron.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1);
            //StartCoroutine(Hide());
            UIManager.instance.ShowPB();
        }
    }

    //IEnumerator Hide()
    //{
    //    yield return new WaitForSeconds(5);
    //    vongtron.transform.DOScale(new Vector3(0, 0, 0), 1);
    //}
}
