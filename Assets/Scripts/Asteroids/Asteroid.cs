using System.Collections;
using System.Collections.Generic;
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

public class Asteroid : MonoBehaviour
{
  [SerializeField] private AsteroidData _data;
  [SerializeField] private Rigidbody2D _rigidbody;

  public void Initialize()
  {
    _data.Direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), 
                                  UnityEngine.Random.Range(-1f, 1f)).normalized;

    _data.Speed = UnityEngine.Random.Range(_data.MinSpeed,
                                           _data.MaxSpeed);

    _rigidbody.velocity = _data.Direction * _data.Speed;
  }


}
