using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Zenject;

public class TransitionController
{
    #region Properties
    public CanvasGroup fadeImg;
    public AsyncProcessor asyncProcessor;
    #endregion

    #region Zenject 
    [Inject]
    public void SetUp(CanvasGroup fadeImg, AsyncProcessor asyncProcessor)
    {
        this.fadeImg = fadeImg;
        this.asyncProcessor = asyncProcessor;
    }
    #endregion

    #region Core Metods      
    /// <summary>
    /// Start fade out coroutine
    /// </summary>
    /// <param name="onTransitionFinished"> Action to execute after fade out </param>
    public void FadeOut(UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(Fade(onTransitionFinished, 0));
    }
    /// <summary>
    /// Start fade in coroutine
    /// </summary>
    /// <param name="onTransitionFinished"> Action to execute after fadein </param>
    public void FadeIn(UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(Fade(onTransitionFinished, 1));
    }
    /// <summary>
    /// Fade and execute action
    /// </summary>
    /// <param name="onTransitionFinished"> Action to excute</param>
    /// <param name="finalFalue"> Value to fade </param>
    /// <returns></returns>
    private IEnumerator Fade(UnityAction onTransitionFinished, int finalFalue)
    {
        yield return fadeImg.DOFade(finalFalue, 0.4f).WaitForCompletion();
        onTransitionFinished.Invoke();
    }
    /// <summary>
    /// Start coroutine tha makes a fade in and out
    /// </summary>
    /// <param name="onFadeIn"> Action to execute after fadein </param>
    /// <param name="onTransitionFinished"> Action to execute after fadeout  </param>
    public void LoadScene(UnityAction onFadeIn, UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(MakeTransition(onFadeIn, onTransitionFinished));
    }
    /// <summary>
    /// Fade in and out and execute actions
    /// </summary>
    /// <param name="onFadeIn"> Action to execute after fadein </param>
    /// <param name="onTransitionFinished"> Action to execute after fadeout </param>
    /// <returns></returns>
    private IEnumerator MakeTransition(UnityAction onFadeIn, UnityAction onTransitionFinished)
    {
        yield return fadeImg.DOFade(1, 0.4f).WaitForCompletion();
        onFadeIn.Invoke();
        yield return fadeImg.DOFade(0, 0.4f).WaitForCompletion();
        onTransitionFinished.Invoke();
    }
    #endregion
}

