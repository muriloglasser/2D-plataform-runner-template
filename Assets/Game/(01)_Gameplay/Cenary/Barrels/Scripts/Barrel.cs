using UnityEngine;

public class Barrel : MonoBehaviour
{
    #region Properties
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
    #endregion

    #region Core Metods
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    #endregion
}
