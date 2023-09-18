using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private SpriteRenderer doorButtonSpriteRenderer;
    [SerializeField] private Sprite openButton;
    [SerializeField] private BoxCollider2D doorCollider;
    #endregion

    #region Unity Metods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                OpenDoor();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Core Metods
    private void OpenDoor()
    {
        doorSpriteRenderer.sprite = openedDoor;
        doorButtonSpriteRenderer.sprite = openButton;
        doorCollider.enabled = false;
    }
    #endregion
}
