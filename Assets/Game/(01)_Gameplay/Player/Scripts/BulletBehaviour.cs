using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private float bulletForce;
    [SerializeField] private Rigidbody2D rigidBody;
    #endregion

    #region Unity Metods
    private void Start()
    {
        AddForce();
        Destroy(gameObject, 1f);

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
            default:
                break;
        }
    }

    #endregion

    #region Core Metods
    public void AddForce()
    {
        var direction = new Vector3(bulletForce, 0, 0);
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    #endregion
}
