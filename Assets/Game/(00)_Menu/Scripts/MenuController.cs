using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    #region Properties 
    public Button playButton;
    public UnityAction playButtonClicked;
    public string scoreBase = "Max Score: ";
    public GameObject panel;
    ///
    private bool lockScreen = true;
    #endregion

    #region Zenject
    [Inject]
    private TransitionController transitionController;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        panel.transform.localScale = Vector3.zero;
        transitionController.FadeOut(() =>
        {
            panel.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);
            lockScreen = false;
        });
    }
    public void Start()
    {
        SetUpEvents();
    }
    #endregion

    #region Core Methods
    /// <summary>
    /// Set up ui events
    /// </summary>
    private void SetUpEvents()
    {
        playButtonClicked = () =>
        {
            if (lockScreen)
                return;

            lockScreen = true;

            panel.transform.DOScale(0, 0.6f).SetEase(Ease.OutBounce);

            UnityAction onFadeIn = () =>
            {
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
            };

            UnityAction onTransitionFinished = () =>
            {
                SceneManager.UnloadSceneAsync("Menu");
            };

            transitionController.LoadScene(onFadeIn, onTransitionFinished);

        };

        playButton.onClick.AddListener(playButtonClicked);
    }
    #endregion
}
