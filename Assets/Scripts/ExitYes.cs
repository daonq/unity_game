using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitYes : MonoBehaviour
{
    public GameObject panel_Thankyou;
    public Text textThankyou;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            textThankyou.text = DataGlobal.instance.tiengviet ? "Cảm ơn!" : "Thank you!";
            panel_Thankyou.SetActive(true);
            StartCoroutine(QuitApp());
        });
    }

    IEnumerator QuitApp()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
