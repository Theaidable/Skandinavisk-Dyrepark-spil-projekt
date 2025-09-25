using JetBrains.Annotations;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIManagerGameOne _ui;
    [SerializeField] private SealController[] _sealControllers;

    [Header("Round Settings")]
    [SerializeField] private float difficultIncreaseInterval = 15f;

    [Header("Base Timings")]
    [SerializeField] private float baseShowDuration;
    [SerializeField] private float baseStayDuration;
    [SerializeField] private Vector2 baseDelayRange;

    [Header("Runtime Timings")]
    [SerializeField] private float currentShowDuration;
    [SerializeField] private float currentStayDuration;
    [SerializeField] private Vector2 currentDelayRange;

    [Header("Holes")]
    [SerializeField] private GameObject[] holes;
    [SerializeField] private GameObject[] holesAtLevel0;
    [SerializeField] private GameObject[] holesAtLevel3;
    [SerializeField] private GameObject[] holesAtLevel4;

    private int currentLevel = -1;

    private void Awake()
    {
        if(_ui == null)
        {
            _ui = FindFirstObjectByType<UIManagerGameOne>();
        }

        if(_sealControllers == null)
        {
            _sealControllers = GetComponent<SealController[]>();
        }

        foreach (GameObject obj in holes)
        {
            obj.SetActive(false);
        }

        currentShowDuration = baseShowDuration;
        currentStayDuration = baseStayDuration;
        currentDelayRange = baseDelayRange;

        foreach(var sealController in _sealControllers)
        {
            sealController.SetTimings(currentShowDuration, currentStayDuration, currentDelayRange);
        }
    }

    private void Start()
    {
        foreach (GameObject obj in holesAtLevel0)
        {
            obj.SetActive(true);
        }

        currentLevel = 0;
    }

    private void Update()
    {
        if(_ui != null && _ui.IsPaused == false && _ui.IsEnded == false)
        {
            if(currentLevel < 1 && _ui.Elapsed >= difficultIncreaseInterval)
            {
                currentShowDuration = baseShowDuration * 0.75f;
                currentStayDuration = baseStayDuration * 0.75f;
                currentDelayRange = baseDelayRange;

                foreach (var sealController in _sealControllers)
                {
                    sealController.SetTimings(currentShowDuration, currentStayDuration, currentDelayRange);
                }

                currentLevel = 1;
            }

            if(currentLevel < 2 && _ui.Elapsed >= difficultIncreaseInterval * 2)
            {
                currentShowDuration = baseShowDuration * 0.5f;
                currentStayDuration = baseStayDuration * 0.5f;
                currentDelayRange = baseDelayRange;

                foreach (var sealController in _sealControllers)
                {
                    sealController.SetTimings(currentShowDuration, currentStayDuration, currentDelayRange);
                }

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
