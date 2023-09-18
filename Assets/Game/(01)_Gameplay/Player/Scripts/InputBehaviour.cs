using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InputBehaviour
{
    #region Properties
    [SerializeField] private AsyncProcessor asyncProcessor;
    [SerializeField] private EventTrigger jumpButton;
    [SerializeField] private EventTrigger slideButton;
    [SerializeField] private EventTrigger shootButton;
    [SerializeField] private bool enableMobileGameplay = false;
    ///
    public bool sliding = false;
    public bool jumping = false;
    public bool shooting = false;
    private Coroutine inputCoroutine;
    #endregion

    #region Zenject
    [Inject]
    private GameManager gameManger;
    [Inject]
    public void SetUp(AsyncProcessor asyncProcessor, EventTrigger jumpButton, EventTrigger slideButton, EventTrigger shootButton, bool enableMobileGameplay)
    {
        this.asyncProcessor = asyncProcessor;
        this.jumpButton = jumpButton;
        this.slideButton = slideButton;
        this.shootButton = shootButton;
        this.enableMobileGameplay = enableMobileGameplay;


#if !UNITY_EDITOR
        inputCoroutine = asyncProcessor.StartCoroutine(MobileInput());
#else
        if (enableMobileGameplay)
        {
            jumpButton.gameObject.SetActive(true);
            slideButton.gameObject.SetActive(true);
            shootButton.gameObject.SetActive(true);
            inputCoroutine = asyncProcessor.StartCoroutine(MobileInput());
        }
        else
        {
            jumpButton.gameObject.SetActive(false);
            slideButton.gameObject.SetActive(false);
            shootButton.gameObject.SetActive(false);
            inputCoroutine = asyncProcessor.StartCoroutine(PcInput());
        }
#endif
        asyncProcessor.StartCoroutine(AnimateButtons(true));
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Set mobile inputs
    /// </summary>
    public IEnumerator MobileInput()
    {
        SetPressed(jumpButton, () =>
        {
            jumping = true;
        });

        SetReleased(jumpButton, () =>
        {
            jumping = false;
        });

        SetPressed(slideButton, () =>
        {
            sliding = true;
        });

        SetReleased(slideButton, () =>
        {
            sliding = false;
        });

        SetPressed(shootButton, () =>
        {
            shooting = true;
        });

        SetReleased(shootButton, () =>
        {
            shooting = false;
        });

        while (true)
        {
            if (gameManger.IsGameFinished())
            {
                asyncProcessor.StartCoroutine(AnimateButtons(false));
                asyncProcessor.StopCoroutine(inputCoroutine);
            }

            yield return null;
        }
    }
    /// <summary>
    /// Set pc inputs
    /// </summary>
    /// <returns></returns>
    public IEnumerator PcInput()
    {
        while (true)
        {
            if (gameManger.IsGameFinished())
            {
                asyncProcessor.StopCoroutine(inputCoroutine);
            }

            yield return null;

            if (Input.GetKey(KeyCode.W))
            {
                jumping = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                jumping = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                sliding = true;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                sliding = false;
            }

            if (Input.GetMouseButton(0))
            {
                shooting = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                shooting = false;
            }

        }
    }
    /// <summary>
    /// Set event trigger pressed event
    /// </summary>
    /// <param name="trigger"> selectedevent trigger </param>
    /// <param name="action"> action to trigger </param>
    public void SetPressed(EventTrigger trigger, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) =>
        {
            action.Invoke();
        });
        trigger.triggers.Add(entry);
    }
    /// <summary>
    /// Set event trigger released event
    /// </summary>
    /// <param name="trigger"> selectedevent trigger </param>
    /// <param name="action"> action to trigger </param>
    public void SetReleased(EventTrigger trigger, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) =>
        {
            action.Invoke();
        });
        trigger.triggers.Add(entry);
    }
    /// <summary>
    /// Animate buttons before gameplay starts and when game play is finished
    /// </summary>
    /// <param name="inAnimation">Condition to start or finish</param>
    public IEnumerator AnimateButtons(bool inAnimation)
    {
        if (inAnimation)
        {
            jumpButton.transform.localScale = Vector3.zero;
            slideButton.transform.localScale = Vector3.zero;
            shootButton.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(0.2f);
            jumpButton.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);
            slideButton.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);
            shootButton.transform.DOScale(1, 0.6f).SetEase(Ease.OutBounce);

        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            jumpButton.transform.DOScale(0, 0.15f).SetEase(Ease.Linear);
            slideButton.transform.DOScale(0, 0.15f).SetEase(Ease.Linear);
            shootButton.transform.DOScale(0, 0.15f).SetEase(Ease.Linear);
        }
    }
    #endregion
}
