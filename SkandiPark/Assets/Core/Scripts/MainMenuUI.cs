using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Animal Buttons")]
    public Button PolarBearButton;
    public Button HavørnButton;
    public Button WolfButton;
    public Button BrownBearButton;

    [Header("Start Button")]
    public Button Button_Play;

    public static string selectedAnimal; //Så det husker hvilket dyr som blev valgt, ift. scene skift.

    private Button selectedButton;

    private void Start()
    {
        Button_Play.interactable = false; //Låsning af start knappen inden man vælger et af dyrene.

        //Lytter efter klik på knapperne med dyrene.
        PolarBearButton.onClick.AddListener(() => SelectAnimal(PolarBearButton, "PolarBear"));
        HavørnButton.onClick.AddListener(() => SelectAnimal(HavørnButton, "Havørn"));
        WolfButton.onClick.AddListener(() => SelectAnimal(WolfButton, "Wolf"));
        BrownBearButton.onClick.AddListener(() => SelectAnimal(BrownBearButton, "BrownBear"));

        //Lytter efter knappen af start.
        Button_Play.onClick.AddListener(StartGame);
    }

    private void SelectAnimal(Button chosenButton, string animalName)
    {
        //Nulstilling af farverne på knapperne.
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
        Button[] all = { PolarBearButton, HavørnButton, WolfButton, BrownBearButton };
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
            SceneManager.LoadScene("SampleScene");
        }
    }
}
