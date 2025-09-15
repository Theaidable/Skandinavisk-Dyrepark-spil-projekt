using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Animal Buttons")]
    public Button PolarBearButton;
    public Button Hav�rnButton;
    public Button WolfButton;
    public Button BrownBearButton;

    [Header("Start Button")]
    public Button Button_Play;

    private Button selectedButton;

    private void Start()
    {
        Button_Play.interactable = false; //L�sning af start knappen inden man v�lger et af dyrene.

        //Lytter efter klik p� knapperne med dyrene.
        PolarBearButton.onClick.AddListener(() => SelectAnimal(PolarBearButton));
        Hav�rnButton.onClick.AddListener(() => SelectAnimal(Hav�rnButton));
        WolfButton.onClick.AddListener(() => SelectAnimal(WolfButton));
        BrownBearButton.onClick.AddListener(() => SelectAnimal(BrownBearButton));

        //Lytter efter knappen af start.
        Button_Play.onClick.AddListener(StartGame);
    }

    private void SelectAnimal(Button chosenButton)
    {
        //Nulstilling af farverne p� knapperne.
        ResetColors();

        //Markering af den valgte knap.
        selectedButton = chosenButton;
        ColorBlock cb = selectedButton.colors;
        cb.normalColor = Color.yellow; //Highlight.
        selectedButton.colors = cb;

        //Aktivering af start knappen.
        Button_Play.interactable = true;
    }

    private void ResetColors()
    {
        Button[] all = { PolarBearButton, Hav�rnButton, WolfButton, BrownBearButton };
        foreach (Button b in all)
        {
            ColorBlock cb = b.colors;
            cb.normalColor = Color.white;
            b.colors = cb;
        }
    }

    private void StartGame()
    {
        if (selectedButton != null)
        {
            Debug.Log("Starter spil med: " + selectedButton.name);
            SceneManager.LoadScene("GameScene");
        }
    }
}
