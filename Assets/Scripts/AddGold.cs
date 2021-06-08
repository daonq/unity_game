using UnityEngine;
using UnityEngine.UI;

public class AddGold : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            DataGlobal.instance.AddGold(20);
        });
    }
}
