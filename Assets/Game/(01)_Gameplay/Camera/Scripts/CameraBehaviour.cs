using System.Collections;
using UnityEngine;

public class CameraBehaviour : IGameManager
{
    #region Properties
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private AsyncProcessor asyncProcessor;
    ///
    private Coroutine cameraMovement;
    #endregion

    #region Constructor
    public CameraBehaviour(Vector3 cameraOffset, Transform cameraM, Transform player, float cameraSpeed, AsyncProcessor asyncProcessor)
    {
        this.cameraOffset = cameraOffset;
        this.camera = cameraM;
        this.player = player;
        this.cameraSpeed = cameraSpeed;
        this.asyncProcessor = asyncProcessor;
    }
    #endregion

    #region Zenject   
    public void AddPoint(int point)
    {
        // throw new System.NotImplementedException();
    }
    public void BeginCountdown()
    {
        //  throw new System.NotImplementedException();
    }
    public void OnStageEnded()
    {
        asyncProcessor.StopCoroutine(cameraMovement);
    }
    public void PlayerDied()
    {
        asyncProcessor.StopCoroutine(cameraMovement);
    }
    public void StartGameplay()
    {
       cameraMovement = asyncProcessor.StartCoroutine(MoveCamera());
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Camera movement
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCamera()
    {
        while (true)
        {
            yield return null;
            camera.position = Vector3.Lerp(camera.position, player.position + cameraOffset, cameraSpeed * Time.deltaTime);
        }

    }
    #endregion

}
