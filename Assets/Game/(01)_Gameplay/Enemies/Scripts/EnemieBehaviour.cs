using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemieBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkDistance = 7;
    ///
    private int life = 2;
    private Coroutine walkCoroutine;
    #endregion

    #region Unity Metods
    private void Start()
    {
        walkCoroutine = StartCoroutine(Walk());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bullet":
                Die();
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        if (walkCoroutine != null)
            StopCoroutine(walkCoroutine);
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Walk update
    /// </summary>
    /// <returns></returns>
    private IEnumerator Walk()
    {
        while (true)
        {
            float leftWalk = walkDistance;

            while (leftWalk > 0)
            {
                yield return null;
                leftWalk -= Time.deltaTime;
                Move(false);
            }
            transform.localScale = new Vector2(1, 1);

            float rightWalk = walkDistance;

            while (rightWalk > 0)
            {
                yield return null;
                rightWalk -= Time.deltaTime;
                Move(true);
            }
            transform.localScale = new Vector2(-1, 1);

        }

    }
    /// <summary>
    /// Move enemie right and left
    /// </summary>
    /// <param name="right"></param>
    private void Move(bool right)
    {
        var direction = new Vector2((right ? 1 : -1) * walkSpeed * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = direction;
    }
    /// <summary>
    /// Zombie death
    /// </summary>
    private void Die()
    {
        life--;
        spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(delegate
        {
            spriteRenderer.DOColor(Color.white, 0.1f);
        });

        if (life != 0)
            return;
       
        rb.velocity = Vector2.zero;
        StopCoroutine(walkCoroutine);
        boxCollider.enabled = false;
        animator.SetTrigger("Dead");
        rb.isKinematic = true;
    }
    #endregion
}
