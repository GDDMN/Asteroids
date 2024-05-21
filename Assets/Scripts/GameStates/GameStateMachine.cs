using UnityEngine;
using System.Collections.Generic;

public class GameStateMachine : MonoBehaviour
{
  [SerializeField] private GameStates currentGameState;
  private Dictionary<GameStates, BaseState> allStates;
  private BaseState activeState;

  [SerializeField] private MainMenuWindow windowMenu;
  [SerializeField] private GameUIWindow windowGame;
  [SerializeField] private LoseUiWindow windowLose;

  [Space(10)]
  public PlayerSpawner playerSpawner;
  public AsteroidsSpawner asteroidsSpawner;

  private Dictionary<GameStates, BaseWindow> windows;

  public Dictionary<GameStates, BaseWindow> Windows => windows;

  public void Awake()
  {
    allStates = new Dictionary<GameStates, BaseState>()
    {
      [GameStates.MENU] = new MenuState(this),
      [GameStates.GAME] = new GameState(this),
      [GameStates.LOSE] = new LoseState(this)
    };

    windows = new Dictionary<GameStates, BaseWindow>()
    {
      [GameStates.MENU] = windowMenu,
      [GameStates.GAME] = windowGame,
      [GameStates.LOSE] = windowLose
    };

    activeState = allStates[currentGameState];
    activeState.Enter();
  }

  public void SetGameState(GameStates state)
  {
    activeState.Exit();
    currentGameState = state;
    activeState = allStates[currentGameState];
    activeState.Enter();
  }

  public void SetGameplayState()
  {
    activeState.Exit();
    currentGameState = GameStates.GAME;
    activeState = allStates[currentGameState];
    activeState.Enter();
  }

  public void SetMenuState()
  {
    activeState.Exit();
    currentGameState = GameStates.MENU;
    activeState = allStates[currentGameState];
    activeState.Enter();
  }

  public void SetLoseState()
  {
    activeState.Exit();
    currentGameState = GameStates.LOSE;
    activeState = allStates[currentGameState];
    activeState.Enter();
  }
}