using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public string vn;
    public string en;

    private void Start()
    {
        GetComponent<Text>().text = DataGlobal.instance.tiengviet ? vn : en;
    }
}
