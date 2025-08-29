using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Pattern Configurations")]
    [SerializeField] private RadialShotSettings _radialShotSettings;
    [SerializeField] private SpiralShotSettings _spiralShotSettings;
    [SerializeField] private LinearShotSettings _linearShotSettings;

    [Header("Timing Settings")]
    [SerializeField] private float _patternDuration = 10f; // Duración de cada patrón
    [SerializeField] private float _shootCoolDown = 0.2f; // Velocidad de disparo

    public enum BossState
    {
        Inactive,
        RadialPhase,
        SpiralPhase,
        LinearPhase,
        Completed
    }

    private BossState _currentState = BossState.Inactive;
    private int _currentPatternIndex = 0;
    private float _patternTimer = 0f;
    private float _shootCoolDownTimer = 0f;
    private bool _isActive = false;

    private void Start()
    {
        StartBossSequence();
    }

    private void Update()
    {
        if (!_isActive) return;

        _shootCoolDownTimer -= Time.deltaTime;
        _patternTimer += Time.deltaTime;

        // Cambiar patrón cada duración especificada
        if (_patternTimer >= _patternDuration)
        {
            _currentPatternIndex = (_currentPatternIndex + 1) % 3;
            _patternTimer = 0f;
            
            // Actualizar estado
            _currentState = (BossState)(_currentPatternIndex + 1);
            
            // Completar después de 3 ciclos (30 segundos)
            if (_currentPatternIndex == 0 && _patternTimer == 0f && _currentState != BossState.RadialPhase)
            {
                CompleteBossSequence();
                return;
            }
        }

        // Disparar según el patrón actual
        if (_shootCoolDownTimer <= 0f)
        {
            FireCurrentPattern();
            _shootCoolDownTimer = _shootCoolDown;
        }
    }

    public void StartBossSequence()
    {
        _isActive = true;
        _currentPatternIndex = 0;
        _currentState = BossState.RadialPhase;
        _patternTimer = 0f;
        _shootCoolDownTimer = 0f;
    }

    private void CompleteBossSequence()
    {
        _isActive = false;
        _currentState = BossState.Completed;
    }

    private void FireCurrentPattern()
    {
        Vector2 center = transform.position;
        Vector2 aimDirection = transform.up;

        switch (_currentPatternIndex)
        {
            case 0: // Radial
                if (_radialShotSettings != null)
                {
                    ShotAttack.RadialShot(center, aimDirection, _radialShotSettings);
                }
                break;
            case 1: // Spiral
                if (_spiralShotSettings != null)
                {
                    ShotAttack.SpiralShot(center, aimDirection, _spiralShotSettings);
                }
                break;
            case 2: // Linear
                if (_linearShotSettings != null)
                {
                    ShotAttack.LinearShot(center, aimDirection, _linearShotSettings);
                }
                break;
        }
    }

    // Métodos públicos para obtener información del estado actual
    public BossState GetCurrentState()
    {
        return _currentState;
    }

    public string GetCurrentPhaseName()
    {
        switch (_currentState)
        {
            case BossState.RadialPhase: return "Radial Attack";
            case BossState.SpiralPhase: return "Spiral Attack";
            case BossState.LinearPhase: return "Linear Attack";
            case BossState.Completed: return "Battle Complete";
            default: return "Inactive";
        }
    }

    public float GetPatternTimeRemaining()
    {
        return _patternDuration - _patternTimer;
    }

    // Método para reiniciar la secuencia
    [ContextMenu("Restart Boss Sequence")]
    public void RestartBossSequence()
    {
        StartBossSequence();
    }
}
