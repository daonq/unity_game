using UnityEngine;
using UnityEngine.UI;

public class AddWood : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            GetComponent<Button>().onClick.AddListener(delegate
            {
                DataGlobal.instance.AddWood(20);
            });
        });
    }
}
