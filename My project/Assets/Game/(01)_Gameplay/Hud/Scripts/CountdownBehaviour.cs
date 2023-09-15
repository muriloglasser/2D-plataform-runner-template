using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class CountdownBehaviour : MonoBehaviour
{
    #region Properties
    public TMP_Text countDownText;
    [SerializeField] private IStartGame _IStartGame;
    #endregion

    #region Zenject
    [Inject]
    public void SetUp(IStartGame IStartGame)
    {
        _IStartGame = IStartGame;
    }
    #endregion

    #region Unity Metods
    private void Start()
    {
        StartCoroutine(StartCountDown());   
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Start game countdown
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCountDown()
    {
        var count = 3;
        while (count >= 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countDownText.gameObject.SetActive(false);
        _IStartGame.StartGame();
    }
    #endregion
}
/// <summary>
/// Interface that starts game
/// </summary>
public interface IStartGame
{
    public void StartGame();
}
