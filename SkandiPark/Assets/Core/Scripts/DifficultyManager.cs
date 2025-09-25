using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIManagerGameOne _ui;
    [SerializeField] private SealController _sealController;

    [Header("Round Settings")]
    [SerializeField] private float difficultIncreaseInterval = 15f;

    [Header("Timings")]
    [SerializeField] private float newShowDuration = 1f;
    [SerializeField] private float newStayDuration = 2f;
    [SerializeField] private Vector2 newDelayRange = new Vector2(0, 2);

    private int roundSeconds;
    private int level = -1;

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

        roundSeconds = _ui.timeLimitSeconds;
    }

    private void Start()
    {
        _sealController.SetTimings(newShowDuration, newStayDuration, newDelayRange);
        level = 0;
    }

    private void Update()
    {
        if(_ui != null && _ui.IsPaused == false && _ui.IsEnded == false)
        {
            if(level < 1 && _ui.Elapsed >= difficultIncreaseInterval)
            {
                _sealController.SetTimings(newShowDuration * 0.75f, newStayDuration * 0.75f, newDelayRange);
                level = 1;
            }

            if(level < 2 && _ui.Elapsed >= difficultIncreaseInterval * 2)
            {
                _sealController.SetTimings(newShowDuration * 0.5f, newStayDuration * 0.5f, newDelayRange);
                level = 2;
            }

            if(level < 3 && _ui.Elapsed >= difficultIncreaseInterval * 3)
            {
                //Aktivere SealHole(5)
                level = 3;
            }

            if(level < 4 && _ui.Elapsed >= difficultIncreaseInterval * 4)
            {
                //Aktivere SealHole(1)
                //Aktivere SealHole(2)
                level = 4;
            }
        }
    }
}
