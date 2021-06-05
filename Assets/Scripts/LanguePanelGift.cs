using UnityEngine;
using TMPro;

public class LanguePanelGift : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = DataGlobal.instance.tiengviet ? "Phần thưởng" : "Reward";
    }
}
