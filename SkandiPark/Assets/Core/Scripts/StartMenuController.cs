using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenu; //StartMenu panel.
    public GameObject settingsMenu; //SettingsMenu panel.

    void Start()
    {
        //For at når scenen loader, at spillet stopper og start menuen vises.
        Time.timeScale = 0f;
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OnPlayPressed()
    {
        Time.timeScale = 1; //Spillet starter.
        startMenu.SetActive(false);
    }

    public void OnSettingsPressed()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true); //Til visning af settings menuen.
    }

    public void OnBackFromSettings()
    {
        settingsMenu.SetActive(false);
        startMenu.SetActive(true); //Tilbage til startmenu.
    }

    public void OnBackPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene"); //Tilbage til kortet.
    }
}
