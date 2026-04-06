using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float deltaX;
    private PlayerInputController playerInputConroller;

    private void Awake()
    {
        playerInputConroller = GetComponent<PlayerInputController>();
    }

    private void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        deltaX = playerInputConroller.movementInputVector.x * speed * Time.deltaTime;
        if (deltaX < 0 && gameObject.transform.position.x <= 5f)
        {
            return;
        }
        else if (deltaX > 0 && gameObject.transform.position.x >= 29f)
        {
            return;
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + deltaX, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
