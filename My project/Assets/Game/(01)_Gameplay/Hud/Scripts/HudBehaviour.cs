using System.Collections;
using TMPro;
using UnityEngine;

public class HudBehaviour : IGameManager
{
    #region Properties
    private TMP_Text pointsText;
    private TMP_Text countDownText;
    private AsyncProcessor asyncProcessor;
    #endregion

    #region Constructor
    public HudBehaviour(TMP_Text pointsTxt, TMP_Text countdownTxt, AsyncProcessor asyncProcessor)
    {
        pointsText = pointsTxt;
        countDownText = countdownTxt;
        this.asyncProcessor = asyncProcessor;
    }
    #endregion

    #region Core Metods  
    /// <summary>
    /// Start game by game manager
    /// </summary>
    public void BeginCountdown()
    {
        asyncProcessor.StartCoroutine(StartCountDown());
    }
    /// <summary>
    /// Start gameplay after countdown
    /// </summary>
    public void StartGameplay()
    {
        countDownText.gameObject.SetActive(false);
    }
    /// <summary>
    /// Add point to hud
    /// </summary>
    /// <param name="point"></param>
    public void AddPoint(int point)
    {
        Debug.Log("Points added :" + point);
        pointsText.text = string.Concat("POINTS: ", point.ToString());
    }
    /// <summary>
    /// On Player died
    /// </summary>
    public void PlayerDied()
    {
        pointsText.gameObject.SetActive(false);
    }
    /// <summary>
    /// Start game countdown
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCountDown()
    {
        var count = 3;
        while (count >= 0)
        {
            if (count != 0)
                countDownText.text = count.ToString();
            else
                countDownText.text = "GO!";

            yield return new WaitForSeconds(1);
            count--;
        }
    }


    #endregion
}
