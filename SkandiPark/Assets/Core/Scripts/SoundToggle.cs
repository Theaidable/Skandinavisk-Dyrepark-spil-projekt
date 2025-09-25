using UnityEngine;
using UnityEngine.UI;
public enum IconSet { PauseMenu, SettingsMenu }

public class SoundToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GraphicsBank bank;
    [SerializeField] private IconSet iconSet = IconSet.PauseMenu;

    private Image buttonImage;
    private Sprite soundOnIcon;
    private Sprite soundOffIcon;
    private bool isSoundOn = true;

    private void Awake()
    {
        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        PullIconsFromBank();
        ApplyIcon();
    }

    private void OnEnable()
    {
        #if UNITY_EDITOR
        GraphicsBank.Changed += OnBankChanged;  // live-opdater i editor
        #endif

        // sync lydsystemet
        AudioManager.Instance?.SetMuted(!isSoundOn);
        ApplyIcon();
    }

    void OnDisable()
    {
        #if UNITY_EDITOR
        GraphicsBank.Changed -= OnBankChanged;
        #endif
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioManager.Instance?.SetMuted(!isSoundOn);
        ApplyIcon();
    }

    private void ApplyIcon()
    {
        if (buttonImage == null)
        {
            return;
        }
        buttonImage.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }

    private void PullIconsFromBank()
    {
        if (bank == null)
        {
            return;
        }

        switch (iconSet)
        {
            case IconSet.PauseMenu:
                soundOnIcon = bank.pauseSoundButton;
                soundOffIcon = bank.pauseSoundButtonOff;
                break;

            case IconSet.SettingsMenu:
                soundOnIcon = bank.settingsSoundButton;
                soundOffIcon = bank.settingsSoundButtonOff;
                break;
        }
    }

    #if UNITY_EDITOR
    private void OnBankChanged(GraphicsBank b)
    {
        if (b != bank)
        {
            return;
        }

        PullIconsFromBank();
        ApplyIcon();
    }

    private void OnValidate()
    {
        // hjælper når du skifter IconSet i Inspector
        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        PullIconsFromBank();
        ApplyIcon();
    }
    #endif
}
