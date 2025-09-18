using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Sprite soundOnIcon; //Billedet af lyden er sl�et til.
    public Sprite soundOffIcon; //Billedet af lyden er sl�et fra.
    public Image buttonImage;

    private bool isSoundOn = true;

    void Start()
    {
        //Vi s�rger for at det starter ud korrekt.
        UpdateIcon();
        AudioListener.pause = !isSoundOn;
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.pause = !isSoundOn; //S� vi kan sl� alt lyd fra/til.
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (isSoundOn)
            buttonImage.sprite = soundOnIcon;
        else
            buttonImage.sprite = soundOffIcon;
    }
}
