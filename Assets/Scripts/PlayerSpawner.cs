using UnityEngine;
using System;

public class PlayerSpawner : MonoBehaviour
{
  [SerializeField] private PlayerController _player;
  [SerializeField] private Vector2 SpawnPosition;

  private int _lives = 3;
  private PlayerController playerController;

  public event Action OnPlayerSpawn;
  public event Action OnGameOver;

  private void Awake()
  {
    OnPlayerSpawn += SpawnPlayer;
    OnGameOver += GameOver;

    SpawnPlayer();
  }

  private void SpawnPlayer()
  {
    if(_lives <= 0 )
    {
      OnGameOver?.Invoke();
      return;
    }

    _lives--;
    playerController = Instantiate(_player, SpawnPosition, Quaternion.identity);
    playerController.transform.SetParent(this.gameObject.transform);
    playerController.OnDestroy += SpawnPlayer;
  }

  private void GameOver()
  {

  }
}
