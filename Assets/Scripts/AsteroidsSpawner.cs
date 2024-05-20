using UnityEngine;
using System;

public class AsteroidsSpawner : MonoBehaviour
{
  private Camera camera;
  [SerializeField] private int _asteroidsCount = 0;
  [SerializeField] private Transform _asteroidsPool;

  [SerializeField] private Asteroid _asteroid;

  public event Action OnSpawnAsteroids;

  private void Awake()
  {
    camera = Camera.main;
    OnSpawnAsteroids += SpawnAsteroids;

    for (int i = 0; i <= 4; i++)
      SpawnAsteroids();
  }

  private void SpawnAsteroids()
  {
    _asteroidsCount++;
    var asteroid = Instantiate(_asteroid, GetPosition(), Quaternion.identity);
    asteroid.Initialize();

    asteroid.transform.SetParent(_asteroidsPool);
  }

  private Vector2 GetPosition()
  {
    float offset = UnityEngine.Random.Range(0f, 1f);
    Vector2 viewportSpawnPos = Vector2.zero;

    int edge = UnityEngine.Random.Range(0, 4);

    if (edge == 0)
      viewportSpawnPos = new Vector2(offset, 0);
    else if (edge == 1)
      viewportSpawnPos = new Vector2(offset, 1);
    else if (edge == 2)
      viewportSpawnPos = new Vector2(0, offset);
    else if (edge == 3)
      viewportSpawnPos = new Vector2(1, offset);

    Vector2 worldPointSpawn = camera.ViewportToWorldPoint(viewportSpawnPos);
    return worldPointSpawn;
  }
}
