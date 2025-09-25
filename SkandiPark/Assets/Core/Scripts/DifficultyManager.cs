using JetBrains.Annotations;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIManagerGameOne _ui;
    [SerializeField] private SealController _sealController;

    [Header("Round Settings")]
    [SerializeField] private float difficultIncreaseInterval = 15f;

    [Header("Timings")]
    [SerializeField] private float newShowDuration;
    [SerializeField] private float newStayDuration;
    [SerializeField] private Vector2 newDelayRange;

    [Header("Holes")]
    [SerializeField] private GameObject[] holes;
    [SerializeField] private GameObject[] holesAtLevel0;
    [SerializeField] private GameObject[] holesAtLevel3;
    [SerializeField] private GameObject[] holesAtLevel4;

    private int roundSeconds;
    private int currentLevel = -1;

    private void Awake()
    {
        if(_ui == null)
        {
            _ui = FindFirstObjectByType<UIManagerGameOne>();
        }

        if(_sealController == null)
        {
            _sealController = GetComponentInChildren<SealController>(true);
        }

        foreach (GameObject obj in holes)
        {
            obj.SetActive(false);
        }

        roundSeconds = _ui.timeLimitSeconds;
    }

    private void Start()
    {
        _sealController.SetTimings(newShowDuration, newStayDuration, newDelayRange);
        currentLevel = 0;

        foreach(GameObject obj in holesAtLevel0)
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        if(_ui != null && _ui.IsPaused == false && _ui.IsEnded == false)
        {
            if(currentLevel < 1 && _ui.Elapsed >= difficultIncreaseInterval)
            {
                _sealController.SetTimings(newShowDuration * 0.75f, newStayDuration * 0.75f, newDelayRange);
                currentLevel = 1;
            }

            if(currentLevel < 2 && _ui.Elapsed >= difficultIncreaseInterval * 2)
            {
                _sealController.SetTimings(newShowDuration * 0.5f, newStayDuration * 0.5f, newDelayRange);
                currentLevel = 2;
            }
            

            if(currentLevel < 3 && _ui.Elapsed >= difficultIncreaseInterval * 3)
            {
                foreach (GameObject obj in holesAtLevel3)
                {
                    obj.SetActive(true);
                }

                currentLevel = 3;
            }

            if(currentLevel < 4 && _ui.Elapsed >= difficultIncreaseInterval * 4)
            {
                foreach (GameObject obj in holesAtLevel4)
                {
                    obj.SetActive(true);
                }

                currentLevel = 4;
            }
        }
    }
}
