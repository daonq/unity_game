using UnityEngine;
using UnityEngine.UI;

public class LevelFactory : MonoBehaviour
{
    public int levelChoice;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                UIManager.instance.OnClickToBuild(levelChoice);
            }
        });

        if (toggle.isOn)
        {
            UIManager.instance.OnClickToBuild(levelChoice);
        }
    }
}
