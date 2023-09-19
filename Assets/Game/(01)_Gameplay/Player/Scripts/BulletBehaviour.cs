using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private float bulletForce;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameObject bulletExplosion;
    #endregion

    #region Unity Metods
    private void Start()
    {
        AddForce();
        StartCoroutine(DestroyMeByTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "RedBarrel":
                DestroyMe();
                break;
            case "GreenBarrel":
                DestroyMe();
                break;
            case "Enemie":
                DestroyMe();
                break;
            default:
                break;
        }
    }

    #endregion

    #region Core Metods
    private void AddForce()
    {
        var direction = new Vector3(bulletForce, 0, 0);
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }
    private void DestroyMe()
    {
        bulletExplosion.transform.SetParent(null);
        bulletExplosion.SetActive(true);
        Destroy(bulletExplosion, 2f);
        Destroy(gameObject);
    }
    private IEnumerator DestroyMeByTime()
    {
        yield return new WaitForSeconds(0.5f);
        DestroyMe();
    }
    #endregion
}
