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
    public void FadeOut(UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(Fade(onTransitionFinished, 0));
    }
    public void FadeIn(UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(Fade(onTransitionFinished, 1));
    }
    public void LoadScene(UnityAction onFadeIn, UnityAction onTransitionFinished)
    {
        asyncProcessor.StartCoroutine(MakeTransition(onFadeIn, onTransitionFinished));
    }
    private IEnumerator MakeTransition(UnityAction onFadeIn, UnityAction onTransitionFinished)
    {
        yield return fadeImg.DOFade(1, 0.4f).WaitForCompletion();
        onFadeIn.Invoke();
        yield return fadeImg.DOFade(0, 0.4f).WaitForCompletion();
        onTransitionFinished.Invoke();
    }
    private IEnumerator Fade(UnityAction onTransitionFinished, int finalFalue)
    {
        yield return fadeImg.DOFade(finalFalue, 0.4f).WaitForCompletion();
        onTransitionFinished.Invoke();
    }
    #endregion
}

