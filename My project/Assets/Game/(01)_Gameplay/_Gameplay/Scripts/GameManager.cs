using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager
{
    #region Properties
    private List<IGameManager> IGameManager;
    private AsyncProcessor asyncProcessor;
    private bool gameStarted = false;
    #endregion

    #region Zenject
    [Inject]
    public void SetUp(List<IGameManager> IGameManager, AsyncProcessor asyncProcessor)
    {
        this.IGameManager = IGameManager;
        this.asyncProcessor = asyncProcessor;
        asyncProcessor.StartCoroutine(StartGame());
    }
    #endregion

    #region Core Metods
    private IEnumerator StartGame()
    {
        for (int i = 0; i < IGameManager.Count; i++)
        {
            IGameManager[i].BeginCountdown();
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < IGameManager.Count; i++)
        {
            IGameManager[i].StartGameplay();
        }

        gameStarted = true;
    }
    public bool IsGameStarted()
    {
        return gameStarted;
    }
    #endregion
}

/// <summary>
/// Game manager control interface
/// </summary>
public interface IGameManager
{
    public void AddPoint(int point);
    public void BeginCountdown();
    public void StartGameplay();
    public void PlayerDied();
}
