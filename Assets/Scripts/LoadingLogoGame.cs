using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingLogoGame : MonoBehaviour
{
    public Image img_fill;
    public Text textFill;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        int max = 100;
        int start = 0;
        textFill.text = start + "%";
        while (start < max)
        {
            yield return new WaitForSeconds(0.01f);
            start++;
            img_fill.fillAmount = (float) start / (float) max;
            textFill.text = start + "%";
        }
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
