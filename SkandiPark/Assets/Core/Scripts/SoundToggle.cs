using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Sprite soundOnIcon; //Billedet af lyden er slået til.
    public Sprite soundOffIcon; //Billedet af lyden er slået fra.
    public Image buttonImage;

    private bool isSoundOn = true;

    void Start()
    {
        //Vi sørger for at det starter ud korrekt.
        UpdateIcon();
        AudioManager.Instance?.SetMuted(!isSoundOn);
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioManager.Instance?.SetMuted(!isSoundOn);
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
