using UnityEngine;

public class ProjectileController : MonoBehaviour, IProjectile
{
  [SerializeField] private ProjectileData _data;
  [SerializeField] private Rigidbody2D _rigidbody;

  public void Initialize(Vector2 direction)
  {
    _data.Direction = direction;
    _rigidbody.velocity = _data.Direction * _data.Speed;
    Destroy(this.gameObject, _data.LifeTime);
  }
}
