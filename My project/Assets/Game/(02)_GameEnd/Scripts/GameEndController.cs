using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class GameEndController : MonoBehaviour
{
    #region Properties 
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject panel;
    [SerializeField] private CanvasGroup canvasGroup;
    ///
    private UnityAction menuButtonClicked;
    private UnityAction playAgainButtonClicked;
    private string scoreBase = "Score: ";
    private bool lockScreen = true;
    #endregion

    #region Zenject
    [Inject]
    private TransitionController transitionController;

    #endregion

    #region Unity Metods
    public void Start()
    {
        InitializePanel();
        SetUpPoints();
        SetUpEvents();
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Initialize panel animations
    /// </summary>
    private void InitializePanel()
    {
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 1).OnComplete(delegate { lockScreen = false; });
    }
    /// <summary>
    /// Set up ui events
    /// </summary>
    private void SetUpEvents()
    {
        playAgainButtonClicked = () =>
        {
            if (lockScreen)
                return;

            panel.transform.DOScale(0, 0.6f).SetEase(Ease.OutBounce);

            lockScreen = true;

            UnityAction onFadeIn = () =>
            {
                SceneManager.UnloadSceneAsync("Gameplay");
                canvasGroup.gameObject.SetActive(false);
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
            };

            UnityAction onTransitionFinished = () =>
            {
                SceneManager.UnloadSceneAsync("GameEnd");
            };

            transitionController.LoadScene(onFadeIn, onTransitionFinished);
        };

        playAgainButton.onClick.AddListener(playAgainButtonClicked);


        menuButtonClicked = () =>
        {
            if (lockScreen)
                return;

            panel.transform.DOScale(0, 0.6f).SetEase(Ease.OutBounce);

            lockScreen = true;

            transitionController.FadeIn(() =>
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);

            });
        };

        menuButton.onClick.AddListener(menuButtonClicked);
    }
    /// <summary>
    /// Set up player points
    /// </summary>
    private void SetUpPoints()
    {
        scoreText.text = string.Concat(scoreBase, PlayerPrefs.GetFloat("Score").ToString());
    }
    #endregion
}
