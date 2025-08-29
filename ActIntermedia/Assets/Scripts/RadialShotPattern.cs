using UnityEngine;

[CreateAssetMenu(menuName = "Bullet System/Radial Shot Pattern")]
public class RadialShotPattern : ScriptableObject
{
    public int RepetitionCount;
    public float StartWait = 0f;
    public float EndWait = 0f;
    public RadialShotSettings[] PatternSettings;
}
