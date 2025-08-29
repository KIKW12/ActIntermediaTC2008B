using System.Collections;
using UnityEngine;

public class LinearShotWeapon : MonoBehaviour
{
    [SerializeField] private LinearShotPattern _linearShotPattern;
    private bool _onShotPattern = false;

    private void OnEnable()
    {
        if (_linearShotPattern != null && !_onShotPattern)
        {
            StartCoroutine(ShotPatternCoroutine(_linearShotPattern));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _onShotPattern = false;
    }

    private IEnumerator ShotPatternCoroutine(LinearShotPattern pattern)
    {
        _onShotPattern = true;
        Vector2 aimDirection = transform.up;

        yield return new WaitForSeconds(pattern.StartWait);

        // Bucle continuo mientras el componente esté activo
        while (enabled)
        {
            Vector2 center = transform.position; // Actualizar posición en tiempo real
            
            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                if (!enabled) break; // Salir si se desactiva
                
                ShotAttack.LinearShot(center, aimDirection, pattern.PatternSettings[i]);
                yield return new WaitForSeconds(pattern.PatternSettings[i].CoolDownAfterShot);
            }
            
            // Pequeña pausa antes de repetir el patrón
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(pattern.EndWait);
        _onShotPattern = false;
    }
}
