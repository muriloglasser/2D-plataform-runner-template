using Zenject;
using UnityEngine;

public class GameplayInstaller : MonoInstaller<GameplayInstaller>
{
    #region Properties
    [Header("Coroutine processor")]
    public AsyncProcessor asyncProcessor;

    [Header("Hud properties")]
    public TMPro.TMP_Text pointsTxt;
    public TMPro.TMP_Text countDownTxt;

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
    #endregion
}
