using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Velocity;
    private const float MaxLife = 3f;
    private float _lifeTime = 0f;

    // Update is called once per frame
    private void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
        _lifeTime += Time.deltaTime;
        if (_lifeTime > MaxLife)
        {
            Disable();
        }
    }

    private void Disable()
    {
        _lifeTime = 0f;
        gameObject.SetActive(false);
    }
}
