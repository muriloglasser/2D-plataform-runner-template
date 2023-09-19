using UnityEngine;

public class Barrel : MonoBehaviour
{
    #region Properties
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particle;
    #endregion

    #region Unity Metods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bullet":
                DestroyMe();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                DestroyMe();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Core Metods
    private void DestroyMe()
    {
        particle.SetActive(true);
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
    }
    #endregion
}
