using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Seal : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [Header("Visable")]
    [SerializeField] private float showDuration;
    [SerializeField] private float duration;

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

    private void Start()
    {
        StartCoroutine(ShowHide(startPosition, endPosition));
    }
}
