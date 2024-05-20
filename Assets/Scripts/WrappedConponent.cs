using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappedConponent : MonoBehaviour
{
  private Camera camera;

  private void Awake()
  {
    camera = Camera.main;
  }

  private void Update()
  {
    Vector2 moveAdjustment = Vector2.zero;
    Vector2 viewportPosition = Vector2.zero;

    if (transform != null)
      viewportPosition = camera.WorldToViewportPoint(transform.position);

    SetAdjustment(ref moveAdjustment, ref viewportPosition);

    if (transform != null)
      SetObjectPosition(transform, moveAdjustment, viewportPosition);
  }

  private void SetAdjustment(ref Vector2 adjustment, ref Vector2 viewportPos)
  {
    if (viewportPos.x < 0)
      adjustment.x += 1;

    else if (viewportPos.x > 1)
      adjustment.x -= 1;

    else if (viewportPos.y < 0)
      adjustment.y += 1;

    else if (viewportPos.y > 1)
      adjustment.y -= 1;
  }

  private void SetObjectPosition(Transform transform, Vector2 adjustment, Vector2 viewportPos)
  {
    Vector2 position = camera.ViewportToWorldPoint(viewportPos + adjustment);
    transform.position = position;
  }
}
