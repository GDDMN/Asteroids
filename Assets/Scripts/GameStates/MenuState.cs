public class MenuState : BaseState
{
  private GameStateMachine _gsm;
  public MenuState(GameStateMachine gsm)
  {
    _gsm = gsm;
  }

  public void Enter()
  {
    foreach (var window in _gsm.Windows)
      window.Value.gameObject.SetActive(false);

    _gsm.Windows[GameStates.MENU].gameObject.SetActive(true);
  }

  public void Exit()
  {
  }
}
