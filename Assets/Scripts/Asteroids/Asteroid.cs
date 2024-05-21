using UnityEngine;
using System;

[Serializable]
public struct AsteroidData
{
  public Vector2 Direction;
  public float MinSpeed;
  public float MaxSpeed;
  public float Speed;
}

public class Asteroid : MonoBehaviour, IHurtable
{
  [SerializeField] private AsteroidData _data;
  [SerializeField] private Rigidbody2D _rigidbody;

  public event Action OnDestroy;

  public void Initialize()
  {
    _data.Direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), 
                                  UnityEngine.Random.Range(-1f, 1f)).normalized;

    _data.Speed = UnityEngine.Random.Range(_data.MinSpeed,
                                           _data.MaxSpeed);

    _rigidbody.velocity = _data.Direction * _data.Speed;
  }

  public void Destroy()
  {
    OnDestroy?.Invoke();
    Destroy(this.gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var projectile = collision.GetComponent<IProjectile>();

    if (projectile != null)
    {
      Destroy(collision.gameObject);
      Destroy();
    }
  }
}
