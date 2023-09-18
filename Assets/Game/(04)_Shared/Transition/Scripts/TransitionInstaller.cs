
using UnityEngine;
using Zenject;

public class TransitionInstaller : MonoInstaller
{
    #region Properties
    [Header("Transition properties")]
    public CanvasGroup fadeImg;
    public AsyncProcessor asyncProcessor;
    #endregion

    #region Zenject
    /// <summary>
    /// Install transition behaviour
    /// </summary>
    public override void InstallBindings()
    {
        Container.Bind<TransitionController>().AsSingle().WithArguments(fadeImg, asyncProcessor);
    }
    #endregion
}
