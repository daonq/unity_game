using UnityEngine;
using UnityEngine.UI;

public class ExitGameNow : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.Quit();
        });
    }
}
