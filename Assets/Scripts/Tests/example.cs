using UnityEngine;
using System.Collections;
using Knotgames.Network;

public class example : MonoBehaviour
{
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    NetObject netObject;
    private void Start()
    {
        netObject = GetComponent<NetObject>();
    }

    void Update()
    {
        if (netObject.IsMine)
        {
            CharacterController controller = GetComponent<CharacterController>();
            // is the controller on the ground?
            if (controller.isGrounded)
            {
                //Feed moveDirection with input.
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                //Multiply it by speed.
                moveDirection *= speed;
                //Jumping
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            //Applying gravity to the controller
            moveDirection.y -= gravity * Time.deltaTime;
            //Making the character move
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}