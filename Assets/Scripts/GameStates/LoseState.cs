public class LoseState : BaseState
{
  private GameStateMachine _gsm;
  public LoseState(GameStateMachine gsm)
  {
    _gsm = gsm;
  }

  public void Enter()
  {
    foreach (var window in _gsm.Windows)
      window.Value.gameObject.SetActive(false);

    _gsm.Windows[GameStates.LOSE].gameObject.SetActive(true);

  }

  public void Exit()
  {
  }
}
