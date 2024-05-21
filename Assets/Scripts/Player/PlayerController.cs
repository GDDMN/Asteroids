using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerData
{
  public float MaxSpeed;
  public Vector2 Acceleration;
  public Vector2 Direction;
  public float AccelerationPreSecs;
  public float SecondsToStop;

  public float Angle;
  public float RotateSpeed;
}

public class PlayerController : MonoBehaviour, IHurtable
{
  private InputController _inputController;
  private Transform _transform;

  [SerializeField] private PlayerData _data;
  [Space(10)]

  [SerializeField] private Rigidbody2D _rigidbody;
  [SerializeField] private ProjectileController _projectile;

  public event Action OnDestroy;

  private void Awake()
  {
    _inputController = new InputController();
    _transform = GetComponent<Transform>();
  }

  private void OnEnable()
  {
    _inputController.Enable();
    _inputController.Player.Shot.performed += context => Shot();
  }

  private void OnDisable()
  {
    _inputController.Player.Shot.performed -= context => Shot();
    _inputController.Disable();
  }

  public void Update()
  {

    _data.Direction.y = _inputController.Player.Move.ReadValue<Vector2>().y;
    _data.Angle = _inputController.Player.Move.ReadValue<Vector2>().x;

    Vector2 rawDirection = (transform.right * _data.Direction.x) + (transform.up * _data.Direction.y);

    if (_data.Direction.y == 0)
      SlowDown(_data.SecondsToStop);

    ForwardMove(rawDirection);
    Rotate();
  }

  private void Rotate()
  {
    _data.Angle = (_data.Angle * _data.RotateSpeed) * Time.deltaTime;
    _transform.Rotate(-Vector3.forward, _data.Angle);
  }

  private void ForwardMove(Vector2 direction)
  {
    Accelerate(direction);
    _rigidbody.velocity = _data.Acceleration;
  }

  private void Accelerate(Vector2 direction)
  {
    _data.Acceleration += direction * (_data.AccelerationPreSecs * Time.deltaTime);
    _data.Acceleration = Vector2.ClampMagnitude(_data.Acceleration, _data.MaxSpeed);
  }

  private void SlowDown(float secondsToStop)
  {
    _data.Acceleration += -_data.Acceleration * (Time.deltaTime / secondsToStop);
  }

  private void Shot()
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
