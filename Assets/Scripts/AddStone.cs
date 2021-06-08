using UnityEngine;
using UnityEngine.UI;

public class AddStone : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            DataGlobal.instance.AddStone(20);
        });
    }
}
