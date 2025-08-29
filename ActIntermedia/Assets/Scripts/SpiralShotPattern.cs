using UnityEngine;

[CreateAssetMenu(menuName = "Bullet System/Spiral Shot Pattern")]
public class SpiralShotPattern : ScriptableObject
{
    public int RepetitionCount;
    public float StartWait = 0f;
    public float EndWait = 0f;
    public SpiralShotSettings[] PatternSettings;
}
