using UnityEngine;
using TMPro;

public class ChangeTMP : MonoBehaviour
{
    [SerializeField] private string vn;
    [SerializeField] private string en;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = DataGlobal.instance.tiengviet ? vn : en;
    }
}
