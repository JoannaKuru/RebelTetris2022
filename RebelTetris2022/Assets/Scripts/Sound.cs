using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Button btn;
    private bool isOn;

    // Stores the image from Unity BtnSound source image
    void Start()
    {
        soundOnIcon = btn.image.sprite;
    }

    // Changes the image and sets the audio mute or on
    public void ButtonClicked()
    {
        if (isOn)
        {
            btn.image.sprite = soundOffIcon;
            isOn = false;
            AudioListener.volume = 0f;
        }
        else
        {
            btn.image.sprite = soundOnIcon;
            isOn = true;
            AudioListener.volume = 1f;
        }
    }
}