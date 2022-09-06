using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed = 15f;
    public float gravity = -9.81f;

    public Transform playerGroundedCheck;
    public float floorSphere = 0.4f;
    public LayerMask floorMask;

    Vector3 velocity;
    bool grounded;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        grounded = Physics.CheckSphere(playerGroundedCheck.position, floorSphere, floorMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * walkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}