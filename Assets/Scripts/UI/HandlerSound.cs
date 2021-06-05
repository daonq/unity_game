using UnityEngine;
using UnityEngine.UI;

public class HandlerSound : MonoBehaviour
{
    [SerializeField] private Image icon_sound;
    [SerializeField] private Sprite sound_on;
    [SerializeField] private Sprite sound_off;

    private void Awake()
    {
        icon_sound.sprite = DataGlobal.instance.sound == 0 ? sound_on : sound_off;

        GetComponent<ButtonSound>().OnSound += delegate
        {
            icon_sound.sprite = DataGlobal.instance.sound == 0 ? sound_off : sound_on;
            DataGlobal.instance.sound = DataGlobal.instance.sound == 0 ? 1 : 0;
        };
    }
}
