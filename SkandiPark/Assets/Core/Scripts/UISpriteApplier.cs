using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public enum GfxKey
{
    ScoreFramer, 
    TimerFramer, 
    PauseButton,
    ResumeButton, 
    MainMenuButton, 
    PauseSoundButton,
    TryAgainButton,
    BackToMainMenuButton,
    PlayButton, 
    SettingsButton, 
    BackButton,
    SettingsSoundButton, 
    SettingsBackButton
}

[ExecuteAlways]
public class UISpriteApplier : MonoBehaviour
{
    [SerializeField] private GraphicsBank bank;
    [SerializeField] private GfxKey key;
    [SerializeField] private bool applyToButtonImage = true;

    private void Awake()
    {
        if (bank == null)
        {
            Debug.LogWarning($"{name}: GraphicsBank not assigned", this);
            return;
        }

        var sprite = Lookup(bank, key);

        if (sprite == null)
        {
            return;
        }

        var img = GetComponent<Image>();

        if (img != null)
        {
            img.sprite = sprite;
        }

        if (applyToButtonImage == true)
        {
            var button = GetComponent<Button>();

            if (button && button.image != null)
            {
                button.image.sprite = sprite;
            }
        }
    }

    void OnEnable()
    {
        Apply();

        #if UNITY_EDITOR

        GraphicsBank.Changed += OnBankChanged;

        #endif
    }

    void OnDisable()
    {
        #if UNITY_EDITOR

        GraphicsBank.Changed -= OnBankChanged;

        #endif
    }

    #if UNITY_EDITOR

    private void OnValidate()
    {
        Apply();
    }

    void OnBankChanged(GraphicsBank b)
    {
        if (b == bank) Apply();
    }

    #endif

    public void Apply()
    {
        if (bank == null)
        {
            return;
        }

        var sprite = Lookup(bank, key);

        if (sprite == null)
        {
            return;
        }

        var img = GetComponent<Image>();

        if (img != null)
        {
            img.sprite = sprite;
        }

        if (applyToButtonImage)
        {
            var button = GetComponent<Button>();
            if (button != null && button.image != null)
            {
                button.image.sprite = sprite;
            }
        }

        #if UNITY_EDITOR

        if (img != null)
        {
            UnityEditor.EditorUtility.SetDirty(img);
        }

        #endif
    }

    static Sprite Lookup(GraphicsBank bank, GfxKey key)
    {
        switch (key)
        {
            case GfxKey.ScoreFramer: return bank.scoreFramer;
            case GfxKey.TimerFramer: return bank.timerFramer;
            case GfxKey.PauseButton: return bank.pauseButton;

            case GfxKey.ResumeButton: return bank.resumeButton;
            case GfxKey.MainMenuButton: return bank.mainMenuButton;
            case GfxKey.PauseSoundButton: return bank.pauseSoundButton;

            case GfxKey.TryAgainButton: return bank.tryAgainButton;
            case GfxKey.BackToMainMenuButton: return bank.backToMainMenuButton;

            case GfxKey.PlayButton: return bank.playButton;
            case GfxKey.SettingsButton: return bank.settingsButton;
            case GfxKey.BackButton: return bank.backButton;

            case GfxKey.SettingsSoundButton: return bank.settingsSoundButton;
            case GfxKey.SettingsBackButton: return bank.settingsBackButton;
            default: return null;
        }
    }

}
