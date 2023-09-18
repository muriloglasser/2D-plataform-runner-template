using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameplayInstaller : MonoInstaller<GameplayInstaller>
{
    #region Properties
    [Header("Coroutine processor")]
    public AsyncProcessor asyncProcessor;

    [Header("Hud properties")]
    public TMPro.TMP_Text pointsTxt;
    public TMPro.TMP_Text countDownTxt;

    [Header("Mobile input")]
    [SerializeField] private bool enableMobileGameplay = false;
    [SerializeField] private EventTrigger jumpButton;
    [SerializeField] private EventTrigger slideButton;
    [SerializeField] private EventTrigger shootButton;

    [Header("Camera properties")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraM;
    [SerializeField] private float cameraSpeed;
    #endregion

    #region Zenject
    /// <summary>
    /// Install Zenject bindings
    /// </summary>
    public override void InstallBindings()
    {
        SetUpHudBehaviour();
        SetGameManager();
        SetCameraBehaviour();
        SetMobileInputBehaviour();
    }
    /// <summary>
    /// Make binding with TMP_Text to hud behaviour
    /// </summary>
    public void SetUpHudBehaviour()
    {
        Container.Bind<IGameManager>().To<HudBehaviour>().AsSingle().WithArguments(pointsTxt, countDownTxt, asyncProcessor);
    }
    /// <summary>
    /// Make binding to GameManager
    /// </summary>
    public void SetGameManager()
    {
        Container.Bind<GameManager>().AsSingle().WithArguments(asyncProcessor);
    }
    /// <summary>
    /// Make binding to CameraBehaviour
    /// </summary>
    public void SetCameraBehaviour()
    {
        Container.Bind<IGameManager>().To<CameraBehaviour>().AsSingle().WithArguments(cameraOffset, cameraM, player, cameraSpeed, asyncProcessor);
    }
    /// <summary>
    /// Set mobile input behaviour
    /// </summary>
    public void SetMobileInputBehaviour()
    {
        Container.Bind<InputBehaviour>().AsSingle().WithArguments(asyncProcessor, jumpButton, slideButton, shootButton, enableMobileGameplay);
    }
    #endregion
}
