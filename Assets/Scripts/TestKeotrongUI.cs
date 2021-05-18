using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeotrongUI : MonoBehaviour
{
    public GameObject btn;

    private void OnMouseDown()
    {
        if (btn.activeSelf)
        {
            btn.SetActive(false);
        } else
        {
            btn.SetActive(true);
        }
    }
}
