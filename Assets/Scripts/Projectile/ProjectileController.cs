using UnityEngine;
using System;

[Serializable]
public struct ProjectileData
{
  public Vector2 Direction;
  public float Speed;
  public float LifeTime;
}


public class ProjectileController : MonoBehaviour
{
  [SerializeField] private ProjectileData _data;
  [SerializeField] private Rigidbody2D _rigidbody;

  public void Initialize(Vector2 direction)
  {
    _data.Direction = direction;
    _rigidbody.velocity = _data.Direction * (_data.Speed * Time.deltaTime);
    Destroy(this.gameObject, _data.LifeTime);
  }
}
