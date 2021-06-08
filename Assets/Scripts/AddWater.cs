using UnityEngine;
using UnityEngine.UI;

public class AddWater : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            DataGlobal.instance.AddWater(20);
        });
    }
}
