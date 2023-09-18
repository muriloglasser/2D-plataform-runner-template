using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerBehaviour : MonoBehaviour
{
    #region Properties 
    [Header("Player properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Vector2 throwBackForce;
    [SerializeField] private bool overlapping = true;
    [Header("Components")]
    [SerializeField] private Transform overlapPoint;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    ///
    private bool died = false;
    private bool started = false;
    private bool reload = false;
    private bool slide = false;
    ///
    private int points = 0;
    private PlayerAnimationState playerAnimationState = PlayerAnimationState.Idle;
    private List<IGameManager> IGameManager;
    private GameManager gameManager;
    #endregion

    #region Zenject
    [Inject]
    public void SetUp(List<IGameManager> IGameManager, GameManager gameManager)
    {
        this.IGameManager = IGameManager;
        this.gameManager = gameManager;
    }
    #endregion

    #region Unity Metods
    private void Start()
    {
        Idle();
    }
    private void FixedUpdate()
    {
        if (!gameManager.IsGameStarted())
            return;
        else
        {
            if (!started)
            {
                started = true;
                playerAnimationState = PlayerAnimationState.Walk;
                animator.SetBool("IsWalking", true);
            }
        }

        overlapping = Physics2D.OverlapCircle(overlapPoint.position, 0.1f, groundLayer);

        Walk();
        Jump();
        Slide();
        Shoot();
    }
    private void Update()
    {
        AnimationCycle();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Box":
                AddPoints(1000);
                break;
            case "CenaryObstacle":
                Die();
                break;
            case "Door":
                Die();
                break;
            case "Saw":
                Die();
                break;
            case "StageEnd":
                gameManager.OnStageEnded(points);

                for (int i = 0; i < IGameManager.Count; i++)
                {
                    IGameManager[i].OnStageEnded();
                }
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "GreenBarrel":
                Die();
                break;
            case "RedBarrel":
                Die();
                break;
            case "Stop":
                speedMultiplier = 0;
                break;
            default:
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Stop":
                speedMultiplier = 1000;
                break;
            default:
                break;
        }
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Add points to player
    /// </summary>
    /// <param name="points"> points to add </param>
    public void AddPoints(int points)
    {
        this.points += points;
        for (int i = 0; i < IGameManager.Count; i++)
        {
            IGameManager[i].AddPoint(this.points);

        }
    }
    /// <summary>
    /// Set player idle 
    /// </summary>
    public void Idle()
    {
        if (playerAnimationState == PlayerAnimationState.Idle)
            return;

        animator.Play("Idle");
    }
    /// <summary>
    /// Player movement
    public void Walk()
    {
        if (died)
            return;

        var direction = new Vector3(speedMultiplier * Time.fixedDeltaTime, rigidBody.velocity.y, 0);
        rigidBody.velocity = direction;
    }
    /// <summary>
    /// Player jump
    public void Jump()
    {
        if (died)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            if (overlapping)
            {
                var direction = new Vector3(0, jumpForce, 0);
                rigidBody.AddForce(direction, ForceMode2D.Impulse);
            }
        }
    }
    /// <summary>
    /// Player slide
    /// </summary>
    public void Slide()
    {
        if (died)
            return;

        if (Input.GetKey(KeyCode.S) && !slide)
        {
            if (overlapping)
            {
                animator.SetBool("IsSliding", true);
                animator.SetBool("IsWalking", false);

                StartCoroutine(ResetSlide());
            }
        }
    }
    /// <summary>
    /// Reset player slide
    /// </summary>
    /// <returns></returns>
    public IEnumerator ResetSlide()
    {
        slide = true;
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("IsSliding", false);
        animator.SetBool("IsWalking", true);

        slide = false;
    }
    /// <summary>
    /// Player shoot
    public void Shoot()
    {
        if (died || !overlapping || slide)
            return;

        if (Input.GetMouseButton(0) && !reload)
        {
            animator.SetBool("IsShooting", true);
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
            StartCoroutine(Reload());
        }
    }
    /// <summary>
    /// Reload player shoot
    /// </summary>
    /// <returns></returns>
    public IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(0.8f);
        reload = false;
    }
    /// <summary>
    /// Trigger player death
    /// </summary>
    public void Die()
    {
        if (died)
            return;


        if (playerAnimationState != PlayerAnimationState.Dead)
        {
            playerAnimationState = PlayerAnimationState.Dead;
            animator.SetBool("IsDead", true);
        }

        rigidBody.velocity = Vector2.zero;
        var direction = new Vector3(throwBackForce.x, throwBackForce.y, 0);
        rigidBody.AddForce(direction, ForceMode2D.Force);

        died = true;

        gameManager.OnPlayerDied(points);

        for (int i = 0; i < IGameManager.Count; i++)
        {
            IGameManager[i].PlayerDied();
        }


    }
    /// <summary>
    /// Controls player animation
    /// </summary>
    public void AnimationCycle()
    {

        if (overlapping && playerAnimationState == PlayerAnimationState.Jump)
        {
            playerAnimationState = PlayerAnimationState.Walk;
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsSliding", false);
        }
        else if (!overlapping && playerAnimationState == PlayerAnimationState.Walk)
        {
            playerAnimationState = PlayerAnimationState.Jump;
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsSliding", false);
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsShooting", false);
        }

    }
    #endregion
}


public enum PlayerAnimationState
{
    None,
    Idle,
    Walk,
    Jump,
    Dead,
    Shoot

}



