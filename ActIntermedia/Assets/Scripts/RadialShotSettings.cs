using UnityEngine;

[System.Serializable] 
public class RadialShotSettings
{
    [Header("Bullet Settings")]
    public int NumberOfBullets = 5;
    public float CoolDownAfterShot;
    public float BulletSpeed = 10f;

    [Header("Offset Settings")]
    [Range(-180f, 180f)] public float AngleOffset = 0f;
    [Range(-1f, 1f)] public float PhaseOffset = 0f;
}
