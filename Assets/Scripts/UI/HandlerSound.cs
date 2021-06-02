using UnityEngine;
using UnityEngine.UI;

public class HandlerSound : MonoBehaviour
{
    [SerializeField] private Image icon_sound;
    [SerializeField] private Sprite sound_on;
    [SerializeField] private Sprite sound_off;

    private void Awake()
    {
        GetComponent<ButtonSound>().OnSound += delegate
        {
            if(DataGlobal.instance.sound == 0)
            {
                icon_sound.GetComponent<Image>().sprite = sound_on;
                DataGlobal.instance.sound = 1;
            } else
            {
                icon_sound.GetComponent<Image>().sprite = sound_off;
                DataGlobal.instance.sound = 0;
            }
        };
    }
}
