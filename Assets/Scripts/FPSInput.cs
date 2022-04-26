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

    //private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //_rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _characterController.Move(new Vector3(0, 10, 0));
            //_rigidbody.AddForce(new Vector3(0,10f,0) * 10, ForceMode.Impulse);
        }


    }
}
