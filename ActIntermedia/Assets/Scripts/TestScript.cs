using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private float _shootCoolDown;
    [SerializeField] private float _bulletSpeed;

    private float _shootCoolDownTimer = 0f;

    private void Update()
    {
        _shootCoolDownTimer -= Time.deltaTime;
        if (_shootCoolDownTimer <= 0f){
            Shot(transform.position, transform.up * _bulletSpeed);
            _shootCoolDownTimer += _shootCoolDown;
        }
    }

    private void Shot (Vector2 origin, Vector2 velocity)
    {
        // Implement shooting logic here
        Bullet bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = origin;
        bullet.Velocity = velocity;
    }
}