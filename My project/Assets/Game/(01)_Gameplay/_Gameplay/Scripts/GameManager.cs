using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    /// <summary>
    /// Start gameplay
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Call game over
    /// </summary>
    /// <param name="points"></param>
    public void OnPlayerDied(float points)
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        PlayerPrefs.SetFloat("Score", points);
    }
    /// <summary>
    /// Call stage end
    /// </summary>
    public void OnStageEnded(float points)
    {
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Additive);
        PlayerPrefs.SetFloat("Score", points);
    }
    /// <summary>
    /// Return gameplay state
    /// </summary>
    /// <returns></returns>
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
    public void OnStageEnded();
}
