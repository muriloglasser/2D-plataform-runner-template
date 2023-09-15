using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private Animator animator;
    #endregion

    #region Unity Metods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                StartCoroutine(DestroyMe());
                break;
            default:
                break;
        }
    }
    #endregion

    #region Core Metods
    /// <summary>
    /// Set box gotten animation
    /// </summary>
    /// <returns></returns>
    public IEnumerator DestroyMe()
    {
        animator.SetTrigger("Gotten");
        yield return new WaitForSeconds(0.36f);
        Destroy(gameObject);
    }
    #endregion
}
