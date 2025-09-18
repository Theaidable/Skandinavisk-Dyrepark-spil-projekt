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
    [Header("Components")]
    //Her kan jeg tilføje PlayerInputController components samt andre hvis nødvendigt.

    [Header("Graphics")]
    [SerializeField] private Sprite standardSeal;
    [SerializeField] private Sprite standardSealHit;
    [SerializeField] private Sprite polarbear;
    [SerializeField] private Sprite polarBearHit;

    [Header("Positions")]
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [Header("Timings")]
    [SerializeField] private float showDuration; //Tid for at komme op og ned
    [SerializeField] private float duration; //Tiden som sælerne bliver oppe
    [SerializeField] private Vector2 nextDelayRange; //Random vente tid før næste pop op

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
        if(hittable == false)
        {
            return;
        }

        hitpoints = Mathf.Max(0, hitpoints - 1);
        _spriteRenderer.sprite = _spawnType == SpawnType.Polarbear ? polarBearHit : standardSealHit;

        if(hitpoints == 0)
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
            _spriteRenderer.sprite = polarbear;
            hitpoints = 1;
        }
        else
        {
            _spawnType = SpawnType.Standard;
            _spriteRenderer.sprite = standardSeal;
            hitpoints = 1;
        }

        hittable = true;
    }
}
