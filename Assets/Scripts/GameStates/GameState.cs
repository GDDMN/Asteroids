public class GameState : BaseState
{
  private GameStateMachine _gsm;
  public GameState(GameStateMachine gsm)
  {
    _gsm = gsm;
  }

  public void Enter()
  {
    _gsm.playerSpawner.Initialize();
    _gsm.asteroidsSpawner.Initialize();

    _gsm.playerSpawner.OnGameOver += _gsm.SetLoseState;

    foreach (var window in _gsm.Windows)
      window.Value.gameObject.SetActive(false);

    _gsm.Windows[GameStates.GAME].gameObject.SetActive(true);
  }

  public void Exit()
  {
  }
}
