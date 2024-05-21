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
