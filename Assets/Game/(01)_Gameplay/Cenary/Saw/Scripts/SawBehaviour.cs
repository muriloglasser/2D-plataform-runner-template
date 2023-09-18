using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private float sawRotationSpeed = 500;
    #endregion

    #region Unity Metods
    void Update()
    {
        transform.Rotate(0, 0, sawRotationSpeed * Time.deltaTime);
    }
    #endregion
}

