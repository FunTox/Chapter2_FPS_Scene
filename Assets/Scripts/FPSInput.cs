using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPSInput")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float runSpeed = 12.0f;
    private CharacterController _characterController;
    
    public float jumpSpeed;
    private float _ySpeed;
    private float _originalStepOffstep;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _originalStepOffstep = _characterController.stepOffset;

    }
    
    void Update()
    {
        float deltaX;
        float deltaZ;
        Vector3 movement;
        
        if (Input.GetButton("Fire1"))
        {
            deltaX = Input.GetAxis("Horizontal") * runSpeed;
            deltaZ = Input.GetAxis("Vertical") * runSpeed;
            movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, runSpeed);
        }
        else
        {
            deltaX = Input.GetAxis("Horizontal") * speed;
            deltaZ = Input.GetAxis("Vertical") * speed;
            movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);

        }

        _ySpeed += Physics.gravity.y * Time.deltaTime;
        if (_characterController.isGrounded)
        {
            _characterController.stepOffset = _originalStepOffstep;
            _ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                _ySpeed = jumpSpeed;
            }
            else
            {
                _characterController.stepOffset = 0;
            }
        }
        movement.y = _ySpeed;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement * Time.deltaTime);
    }
}
