using UnityEngine;
using System;
using System.Collections.Generic;

public class AsteroidsSpawner : MonoBehaviour
{
  private Camera camera;

  [Header("Waves")]
  [SerializeField] private int _waveNum = 0;
  [SerializeField] private int _asteroidsCount = 0;

  [Space(10), Header("Asteroids")]
  [SerializeField] private Transform _asteroidsPool;
  [SerializeField] private Asteroid _asteroid;

  [Space(10), Header("Window")]
  [SerializeField] private GameUIWindow _gameWindow;

  private int AsteroidsVaweCount => 10 + _waveNum * UnityEngine.Random.Range(2, 5);
  private List<Asteroid> _allAsteroids = new List<Asteroid>();

  public event Action OnSpawnAsteroids;
  public event Action OnWaveClear;
  public event Action<int> SendWaveCount;

  public void Initialize()
  {
    foreach (var asteroid in _allAsteroids)
      if(asteroid != null)
        Destroy(asteroid.gameObject);

    _allAsteroids.Clear();

    _waveNum = 0;
    _asteroidsCount = 0;

    camera = Camera.main;
    OnSpawnAsteroids += SpawnAsteroids;
    OnWaveClear += CheckWaveValidate;
    SendWaveCount += _gameWindow.WavesCount.ChangeValue;

    OnWaveClear?.Invoke();
  }

  private void SpawnWave()
  {
    _waveNum++;

    for (int i=0; i < AsteroidsVaweCount; i++)
      SpawnAsteroids();
  }

  private void CheckWaveValidate()
  {
    if (_asteroidsCount > 0)
      return;

    SpawnWave();
    SendWaveCount?.Invoke(_waveNum);
  }

  private void SpawnAsteroids()
  {
    _asteroidsCount++;
    var asteroid = Instantiate(_asteroid, GetPosition(), Quaternion.identity);
    asteroid.Initialize();
    asteroid.OnDestroy += OnDestroyAsteroid;
    asteroid.transform.SetParent(_asteroidsPool);

    _allAsteroids.Add(asteroid);
  }

  private void OnDestroyAsteroid()
  {
    if(_asteroidsCount > 0)
      _asteroidsCount--;

    OnWaveClear?.Invoke();
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
