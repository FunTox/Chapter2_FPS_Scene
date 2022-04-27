using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPSInput")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _characterController;
    
    public float jumpSpeed;
    private float ySpeed;
    private float originalStepOffstep;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        originalStepOffstep = _characterController.stepOffset;

    }
    
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (_characterController.isGrounded)
        {
            _characterController.stepOffset = originalStepOffstep;
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
            else
            {
                _characterController.stepOffset = 0;
            }
        }
        movement.y = ySpeed;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement * Time.deltaTime);
    }
}
