using UnityEngine;

[CreateAssetMenu(menuName = "WhackASeal/Graphics Bank")]
public class GraphicsBank : ScriptableObject
{
    [Header("Standard Seal")]
    public Sprite standardSeal;
    public Sprite standardSealHit;

    [Header("Polar bear")]
    public Sprite polarBear;
    public Sprite polarBearHit;
}
