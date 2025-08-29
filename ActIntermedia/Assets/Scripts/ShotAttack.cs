using UnityEngine;

public static class ShotAttack
{
    private static float _spiralCurrentRotation = 0f; // Para mantener la rotación acumulativa
    private static float _linearCurrentRotation = 0f; // Para la rotación del patrón lineal

    public static void SimpleShot (Vector2 origin, Vector2 velocity)
    {
        // Implement shooting logic here
        Bullet bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = origin;
        bullet.Velocity = velocity;
    }

    public static void RadialShot
        (Vector2 origin, Vector2 aimDirection, RadialShotSettings settings)
    {
        float angleBetween = 360f / settings.NumberOfBullets;
        if (settings.AngleOffset != 0f || settings.PhaseOffset != 0f)
        {
            aimDirection = aimDirection
                .Rotate(settings.AngleOffset + (settings.PhaseOffset * angleBetween));
        }
        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float angle = angleBetween * i;
            Vector2 BulletDirection = aimDirection.Rotate(angle);
            SimpleShot(origin, BulletDirection * settings.BulletSpeed);
        }
    }

    public static void SpiralShot(Vector2 origin, Vector2 aimDirection, SpiralShotSettings settings)
    {
        // Aplicar rotación acumulativa usando Quaternion.Euler
        float rotationDirection = settings.ClockwiseRotation ? 1f : -1f;
        _spiralCurrentRotation += settings.RotationPerShot * rotationDirection;

        // Usar Quaternion.Euler para rotar la dirección base
        Quaternion baseRotation = Quaternion.Euler(0, 0, _spiralCurrentRotation + settings.AngleOffset);
        Vector2 rotatedDirection = baseRotation * aimDirection;

        // Crear múltiples brazos de la espiral
        float anglePerArm = 360f / settings.SpiralArms;
        
        for (int arm = 0; arm < settings.SpiralArms; arm++)
        {
            // Rotar cada brazo usando Quaternion.Euler
            Quaternion armRotation = Quaternion.Euler(0, 0, anglePerArm * arm);
            Vector2 armDirection = armRotation * rotatedDirection;
            
            // Disparar las balas en cada brazo
            for (int i = 0; i < settings.NumberOfBullets; i++)
            {
                Vector2 bulletDirection = armDirection;
                SimpleShot(origin, bulletDirection * settings.BulletSpeed);
            }
        }
    }

    public static void LinearShot(Vector2 origin, Vector2 aimDirection, LinearShotSettings settings)
    {
        // Rotar todo el patrón usando Quaternion.Euler
        _linearCurrentRotation += settings.RotationSpeed;
        Quaternion patternRotation = Quaternion.Euler(0, 0, _linearCurrentRotation + settings.AngleOffset);
        Vector2 baseDirection = patternRotation * aimDirection;

        // Crear múltiples líneas paralelas
        for (int line = 0; line < settings.NumberOfLines; line++)
        {
            // Calcular el ángulo para esta línea usando Quaternion.Euler
            float lineAngle = (line - (settings.NumberOfLines - 1) * 0.5f) * settings.AngleBetweenLines;
            Quaternion lineRotation = Quaternion.Euler(0, 0, lineAngle);
            Vector2 lineDirection = lineRotation * baseDirection;

            // Disparar balas en esta línea con espaciado
            for (int bullet = 0; bullet < settings.BulletsPerLine; bullet++)
            {
                Vector2 bulletOrigin = origin;
                
                // Aplicar espaciado perpendicular a la dirección de disparo
                if (settings.BulletsPerLine > 1)
                {
                    Vector2 perpendicular = new Vector2(-lineDirection.y, lineDirection.x);
                    float offset = (bullet - (settings.BulletsPerLine - 1) * 0.5f) * settings.BulletSpacing;
                    bulletOrigin += perpendicular * offset;
                }

                SimpleShot(bulletOrigin, lineDirection * settings.BulletSpeed);
            }
        }
    }
}
