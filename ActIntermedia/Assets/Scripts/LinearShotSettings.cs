using UnityEngine;

[System.Serializable] 
public class LinearShotSettings
{
    [Header("Bullet Settings")]
    public int BulletsPerLine = 3;
    public float CoolDownAfterShot = 0.15f;
    public float BulletSpeed = 12f;

    [Header("Linear Pattern Settings")]
    [Range(1, 8)] public int NumberOfLines = 4; // Cuántas líneas paralelas
    [Range(10f, 90f)] public float AngleBetweenLines = 45f; // Ángulo entre cada línea
    [Range(0f, 10f)] public float RotationSpeed = 3f; // Velocidad de rotación del patrón completo
    
    [Header("Offset Settings")]
    [Range(-180f, 180f)] public float AngleOffset = 0f;
    [Range(0.5f, 3f)] public float BulletSpacing = 1f; // Espaciado entre balas en la misma línea
}
