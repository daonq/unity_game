﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThuHoachCayAnQua : MonoBehaviour
{
    public int id;
    public int indexAmount;
    public GameObject qua;
    public Material mat;

    public Sprite status1;
    public Sprite status2;
    public Sprite status3;

    public bool cothepha;

    public int currentTime;

    private void Start()
    {
        currentTime = PlayerPrefs.GetInt("TimeCayAnQua" + id);
        if(currentTime > 0)
        {
            cothepha = false;
        } else
        {
            cothepha = true;
        }
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
            if (cothepha && DataGlobal.instance.GetGold() >= 10)
            {
                DataGlobal.instance.SubGold(10);
                DataGlobal.instance.AddStar(2);
                DataGlobal.instance.ArrayAmount[indexAmount] += 5;

                GameObject ef = Instantiate(qua, transform.position, Quaternion.identity);
                ef.transform.Rotate(new Vector3(-90, 0, 0));
                ef.GetComponent<ParticleSystemRenderer>().material = mat;
                Destroy(ef, 3);
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = status1;
                currentTime = 200;
                PlayerPrefs.SetInt("TimeCayAnQua" + id, currentTime);
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
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            if(currentTime <= 0)
            {
                cothepha = true;
                GetComponent<SpriteRenderer>().sprite = status3;
            } else if(currentTime > 0 && currentTime <= 100)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = status2;
            } else if(currentTime > 100 && currentTime <= 200)
            {
                cothepha = false;
                GetComponent<SpriteRenderer>().sprite = status1;
            }
            PlayerPrefs.SetInt("TimeCayAnQua" + id, currentTime);
        }
    }
}
