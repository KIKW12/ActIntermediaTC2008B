using UnityEngine;

[System.Serializable] 
public class SpiralShotSettings
{
    [Header("Bullet Settings")]
    public int NumberOfBullets = 6;
    public float CoolDownAfterShot = 0.1f;
    public float BulletSpeed = 8f;

    [Header("Spiral Settings")]
    [Range(5f, 45f)] public float RotationPerShot = 15f; // Grados que rota por cada disparo
    [Range(1f, 5f)] public float SpiralArms = 2f; // Número de brazos de la espiral
    public bool ClockwiseRotation = true;
    
    [Header("Offset Settings")]
    [Range(-180f, 180f)] public float AngleOffset = 0f;
}
