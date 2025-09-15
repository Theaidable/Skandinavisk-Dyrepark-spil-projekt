using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum SpawnType
{
    Standard,
    Polarbear
}

public class Seal : MonoBehaviour
{
    [Header("Components")]
    //Her kan jeg tilføje PlayerInputController components samt andre hvis nødvendigt.

    [Header("Graphics")]
    [SerializeField] private Sprite sealStandard;
    [SerializeField] private Sprite sealStandardHit;
    [SerializeField] private Sprite polarbear;
    [SerializeField] private Sprite polarBearHit;

    [Header("Position")]
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [Header("Visability")]
    [SerializeField] private float showDuration;
    [SerializeField] private float duration;

    [Header("GameStats")]
    [SerializeField] private float sealStandardSpawnRate;
    [SerializeField] private float polarbearSpawnRate;

    private SpriteRenderer _spriteRenderer;

    //Seal parametre
    private bool hittable;
    private SpawnType spawnType;

    private void Awake()
    {
        //References
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        //Start at "start"
        transform.localPosition = start;

        float elapsed = 0f;

        while(elapsed < showDuration)
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
    }

    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);

        if(random < polarbearSpawnRate)
        {
            spawnType = SpawnType.Polarbear;
        }
        else
        {
            random = Random.Range(0f, 1f);

            if (random < sealStandardSpawnRate)
            {
                spawnType = SpawnType.Standard;
                _spriteRenderer.sprite = sealStandard;
            }
        }

        hittable = true;
    }
}
