using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private InputController _inputController;
  private Transform _transform;

  [SerializeField] private PlayerActor _actor;

  public PlayerActor Actor => _actor;

  private void Awake()
  {
    _inputController = new InputController();
    _transform = GetComponent<Transform>();
  }

  private void OnEnable()
  {
    _inputController.Enable();
    _inputController.Player.Shot.performed += context => _actor.Shot();
  }

  private void OnDisable()
  {
    _inputController.Player.Shot.performed -= context => _actor.Shot();
    _inputController.Disable();
  }

  public void Update()
  {
    _actor.SetDirectionForce(_inputController.Player.Move.ReadValue<Vector2>().y);
    _actor.SetAngle(_inputController.Player.Move.ReadValue<Vector2>().x);

    Vector2 rawDirection = (transform.right * _actor.Data.Direction.x) + (transform.up * _actor.Data.Direction.y);

    if (_actor.Data.Direction.y == 0)
      _actor.SlowDown(_actor.Data.SecondsToStop);

    _actor.ForwardMove(rawDirection);
    _actor.Rotate();
  }
}