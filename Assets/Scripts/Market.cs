using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Market : MonoBehaviour
{
    public GameObject vongtron;

    private string nameDown;

    private void Start()
    {
        vongtron.transform.localScale = Vector3.zero;
    }

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown && !Tutorial.instance.modeTutorial)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#else
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#endif
        }
        else if (DataGlobal.instance.AllowMouseDown && DataGlobal.instance.GetFirstGame() == 4)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#else
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                nameDown = hit.collider?.name;
            }
#endif
        }
    }

    private void OnMouseUp()
    {
        if (DataGlobal.instance.AllowMouseDown && !Tutorial.instance.modeTutorial)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1, 1, 1);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                if (hit.collider?.name == nameDown)
                {
                    Handler();
                }
            }
#else
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider?.name == nameDown)
            {
                Handler();
            }
        }
#endif
        }
        else if (DataGlobal.instance.AllowMouseDown && DataGlobal.instance.GetFirstGame() == 4)
        {
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                transform.localScale = new Vector3(1, 1, 1);
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                if (hit.collider?.name == nameDown)
                {
                    Handler();
                }
            }
#else
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider?.name == nameDown)
            {
                Handler();
            }
        }
#endif
        }
    }

    private void Handler()
    {
        nameDown = "";
        vongtron.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        Tutorial.instance.caitay.SetActive(false);
        if (DataGlobal.instance.GetFirstGame() != 4)
        {
            StartCoroutine(Hide());
        }

        if (DataGlobal.instance.GetFirstGame() == 4)
        {
            DataGlobal.instance.SetFirstGame(5);
            Tutorial.instance.TutorialMuaCua1();
        }
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(5);
        vongtron.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
    }
}
