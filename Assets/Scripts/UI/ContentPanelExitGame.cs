using UnityEngine;
using TMPro;

public class ContentPanelExitGame : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = DataGlobal.instance.tiengviet ? "Bạn có muốn rời trò chơi không?" : "Do you want exit the game?";
    }
}
