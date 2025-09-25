using UnityEngine;

[CreateAssetMenu(menuName = "WhackASeal/Graphics Bank")]
public class GraphicsBank : ScriptableObject
{
    [Header("Seal")]
    public Sprite standardSeal;
    public Sprite standardSealHit;

    [Header("Polar bear")]
    public Sprite polarBear;
    public Sprite polarBearHit;

    [Header("InGameOverLay")]
    public Sprite scoreFramer;
    public Sprite timerFramer;
    public Sprite pauseButton;

    [Header("PauseMenu")]
    public Sprite resumeButton;
    public Sprite mainMenuButton;
    public Sprite pauseSoundButton;

    [Header("TryAgainMenu")]
    public Sprite tryAgainButton;

    [Header("StartMenu")]
    public Sprite playButton;
    public Sprite settingsButton;
    public Sprite backButton;

    [Header("SettingsMenu")]
    public Sprite settingsSoundButton;
    public Sprite settingsBackButton;

    [Header("Background")]
    public Texture2D backgroundTexture;

#if UNITY_EDITOR
    public static event System.Action<GraphicsBank> Changed;
    private void OnValidate()
    {
        // Fyr et event når nogen ændrer noget i asset’et i Inspector
        Changed?.Invoke(this);
    }
#endif
}