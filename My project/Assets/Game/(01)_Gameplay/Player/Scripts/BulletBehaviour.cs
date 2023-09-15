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
        var direction = new Vector3(bulletForce,0,0);
        rigidBody.AddForce(direction,ForceMode2D.Impulse);
    }
    #endregion
}
