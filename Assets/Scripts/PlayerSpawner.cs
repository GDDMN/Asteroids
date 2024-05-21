using UnityEngine;
using System;

public class PlayerSpawner : MonoBehaviour
{
  [SerializeField] private PlayerController _player;
  [SerializeField] private Vector2 SpawnPosition;
  [SerializeField] private GameUIWindow _gameWindow;

  private int _lives;
  private PlayerController playerController;

  public event Action OnPlayerSpawn;
  public event Action OnGameOver;
  public event Action<int> OnChangeLivesValue;

  public void Initialize()
  {
    _lives = 3;
    OnPlayerSpawn += SpawnPlayer;
    OnGameOver += GameOver;
    OnChangeLivesValue += _gameWindow.LivesCounter.ChangeValue;
    
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
    OnChangeLivesValue?.Invoke(_lives);
    playerController = Instantiate(_player, SpawnPosition, Quaternion.identity);
    playerController.transform.SetParent(this.gameObject.transform);
    playerController.Actor.OnDestroy += SpawnPlayer;
  }
  
  private void GameOver()
  {
    OnPlayerSpawn -= SpawnPlayer;
    OnChangeLivesValue -= _gameWindow.LivesCounter.ChangeValue;
    OnGameOver -= GameOver;
  }
}
