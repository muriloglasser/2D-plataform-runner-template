using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
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

    #region Zenject
    public void BeginCountdown()
    {
        asyncProcessor.StartCoroutine(StartCountDown());
    }
    public void StartGameplay()
    {
        countDownText.gameObject.SetActive(false);
    }
    public void AddPoint(int point)
    {
        pointsText.gameObject.transform.DOScale(0.15f, 0.2f).OnComplete(delegate
        {
            pointsText.gameObject.transform.DOScale(1f, 0.3f).OnComplete(delegate { }).SetEase(Ease.OutBounce); 


        }); 
        pointsText.text = string.Concat("POINTS: ", point.ToString());
    }
    public void PlayerDied()
    {
        pointsText.transform.parent.gameObject.SetActive(false);
    }
    public void OnStageEnded()
    {
        pointsText.transform.parent.gameObject.SetActive(false);
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
            if (count != 0)
                countDownText.text = count.ToString();
            else
                countDownText.text = "GO!";

            countDownText.transform.localScale = Vector3.zero;
            countDownText.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(1);
            count--;
        }
    }

    #endregion
}
