using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerGameOne : MonoBehaviour
{
    [Header("Overlay refs")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Button pauseButton;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject tryAgainMenu;
    public TextMeshProUGUI finalScoreText;

    [Header("Round Settings")]
    [Range(15, 180)] public int timeLimitSeconds = 60;

    int score = 0;
    float timeLeft;
    bool paused = false;
    bool ended = false;

    void Start()
    {
        timeLeft = timeLimitSeconds;

        if (pauseButton) pauseButton.onClick.AddListener(PauseGame);

        if (pauseMenu) pauseMenu.SetActive(false);
        if (tryAgainMenu) tryAgainMenu.SetActive(false);

        scoreText.text = "Score 0";
        timerText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    void Update()
    {
        if (ended || paused) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;

        timerText.text = Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0 && !ended)
            ShowTryAgainMenu();
    }

    public void UpdateScore(int delta) //Public så gameplayed kan få kaldt det.
    {
        if (ended) return;
        score += delta;
        scoreText.text = "Score " + score;
    }

    public void PauseGame() //Pause/Resume.
    {
        if (ended) return;
        paused = true;
        Time.timeScale = 0f;
        if (pauseMenu) pauseMenu.SetActive(true);
    }

    public void ResumeGame() //Pause/Resume.
    {
        paused = false;
        Time.timeScale = 1f;
        if (pauseMenu) pauseMenu.SetActive(false);
    }

    public void ShowTryAgainMenu() //Runden er slut.
    {
        ended = true;
        Time.timeScale = 0f;
        if (tryAgainMenu) tryAgainMenu.SetActive(true);
        if (finalScoreText) finalScoreText.text = "Score: " + score;
    }

    public void Restart() //Knapper.
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }
}
