using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum SpawnType
{
    Standard,
    Polarbear
}

public class SealController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GraphicsBank gfx;

    [Header("Positions")]
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [Header("Timings")]
    private Vector2 nextDelayRange; //Random vente tid før næste pop op
    private float showDuration; //Tid for at komme op og ned
    private float duration; //Tiden som sælerne bliver oppe

    [Header("Spawn Rates")]
    [SerializeField] private float standardSealSpawnRate;
    [SerializeField] private float polarbearSpawnRate;

    private Coroutine _loop;
    private SpriteRenderer _spriteRenderer;
    private SpawnType _spawnType;
    private bool hittable;
    private int hitpoints;

    private UIManagerGameOne uiManager;

    private void Awake()
    {
        //References
        _spriteRenderer = GetComponent<SpriteRenderer>();

        uiManager = FindFirstObjectByType<UIManagerGameOne>();
    }

    private void OnEnable()
    {
        _loop = StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private void OnDisable()
    {
        if(_loop != null)
        {
            StopCoroutine(_loop);
        }
    }

    public void TryHit()
    {
        if(hittable == true && UIManagerGameOne.InputLocked == false)
        {
            hitpoints = Mathf.Max(0, hitpoints - 1);
            _spriteRenderer.sprite = _spawnType == SpawnType.Polarbear ? gfx.polarBearHit : gfx.standardSealHit;

            if(_spawnType == SpawnType.Standard)
            {
                AudioManager.Instance?.PlaySealHit();
            }
            else
            {
                AudioManager.Instance?.PlayPolarbearHit();
            }

            if (hitpoints == 0)
            {
                hittable = false;

                if (_spawnType == SpawnType.Standard)
                {
                    uiManager.UpdateScore(10); //10 points for en sæl.
                }
                else if (_spawnType == SpawnType.Polarbear)
                {
                    uiManager.UpdateScore(-5); //-5 points for en isbjørn.
                }
            }
        }
    }

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        while (true) //Skal ændres GameOver == false
        {
            float delay = Random.Range(nextDelayRange.x, nextDelayRange.y);

            while(delay > 0)
            {
                delay -= Time.deltaTime;
                yield return null;
            }

            CreateNext();
            AudioManager.Instance?.PlayPopUp();

            //Start ved "start"
            transform.localPosition = start;

            float elapsed = 0f;

            //Show the seal
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = end;

            //Wait for duration to pass
            yield return new WaitForSeconds(duration);

            //Hide the seal
            elapsed = 0f;

            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = start;

            delay = Random.Range(nextDelayRange.x,nextDelayRange.y);
        }
    }

    private void CreateNext()
    {
        float total = Mathf.Max(0.0001f, standardSealSpawnRate + polarbearSpawnRate);
        float random = Random.value * total;

        if(random < polarbearSpawnRate)
        {
            _spawnType = SpawnType.Polarbear;
            _spriteRenderer.sprite = gfx.polarBear;
            hitpoints = 1;
        }
        else
        {
            _spawnType = SpawnType.Standard;
            _spriteRenderer.sprite = gfx.standardSeal;
            hitpoints = 1;
        }

        hittable = true;
    }

    public void SetTimings(float newShowDuration, float newDuration, Vector2 newDelayRange)
    {
        showDuration = Mathf.Max(0.05f, newShowDuration);
        duration = Mathf.Max(0.05f, newDuration);
        nextDelayRange = newDelayRange;
    }
}
