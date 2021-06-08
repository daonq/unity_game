using UnityEngine;
using UnityEngine.UI;

public class AddExp : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            DataGlobal.instance.AddStar(20);
            DataGlobal.instance.AddOil(1000);
        });
    }
}
