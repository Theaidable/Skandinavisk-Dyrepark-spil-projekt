using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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

    public static event Action OnPaused;
    public static event Action OnResumed;
    public static event Action OnEnded;

    private static bool _inputLocked;
    public static bool InputLocked => _inputLocked;

    public bool IsPaused => paused;
    public bool IsEnded => ended;

    public float Elapsed => timeLimitSeconds - timeLeft;

    private void Awake()
    {
        _inputLocked = false;
        OnPaused = null;
        OnResumed = null;
        OnEnded = null;
    }

    void Start()
    {
        timeLeft = timeLimitSeconds;

        AudioManager.Instance?.PlayBackgroundMusic();

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
        _inputLocked = true;
        Time.timeScale = 0f;
        OnPaused?.Invoke();
        if (pauseMenu) pauseMenu.SetActive(true);
    }

    public void ResumeGame() //Pause/Resume.
    {
        paused = false;
        _inputLocked = false;
        Time.timeScale = 1f;
        OnResumed?.Invoke();
        if (pauseMenu) pauseMenu.SetActive(false);
    }

    public void ShowTryAgainMenu() //Runden er slut.
    {
        ended = true;
        _inputLocked = true;
        Time.timeScale = 0f;
        OnEnded?.Invoke();
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
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("MapScene");
    }
}
