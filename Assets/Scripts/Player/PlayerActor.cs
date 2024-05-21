using UnityEngine;
using System;

public class PlayerActor : MonoBehaviour, IHurtable
{
  [SerializeField] private PlayerData _data;
  [SerializeField] private Rigidbody2D _rigidbody;
  [SerializeField] private ProjectileController _projectile;

  public PlayerData Data => _data;

  public event Action OnDestroy;

  public void Rotate()
  {
    _data.Angle = (_data.Angle * _data.RotateSpeed) * Time.deltaTime;
    transform.Rotate(-Vector3.forward, _data.Angle);
  }

  public void SetDirectionForce(float yForce)
  {
    _data.Direction.y = yForce;
  }

  public void SetAngle(float rotateDirection)
  {
    _data.Angle = rotateDirection;
  }

  public void ForwardMove(Vector2 direction)
  {
    Accelerate(direction);
    _rigidbody.velocity = _data.Acceleration;
  }

  public void Accelerate(Vector2 direction)
  {
    _data.Acceleration += direction * (_data.AccelerationPreSecs * Time.deltaTime);
    _data.Acceleration = Vector2.ClampMagnitude(_data.Acceleration, _data.MaxSpeed);
  }

  public void SlowDown(float secondsToStop)
  {
    _data.Acceleration += -_data.Acceleration * (Time.deltaTime / secondsToStop);
  }

  public void Shot()
  {
    var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
    projectile.Initialize(transform.up);
  }

  public void Destroy()
  {
    OnDestroy?.Invoke();
    Destroy(this.gameObject);
  }

  public void OnTriggerEnter2D(Collider2D collision)
  {
    var hurtable = collision.GetComponent<IHurtable>();

    if (hurtable != null)
    {
      collision.GetComponent<IHurtable>().Destroy();
      Destroy();
    }
  }
}
