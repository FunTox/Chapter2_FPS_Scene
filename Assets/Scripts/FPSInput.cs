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
    private float _acceleration;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _originalStepOffstep = _characterController.stepOffset;

    }
    
    void Update()
    {
        _acceleration = Input.GetButton("Fire1")? runSpeed: speed;
        float deltaX = Input.GetAxis("Horizontal") * _acceleration;
        float deltaZ = Input.GetAxis("Vertical") * _acceleration;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _acceleration);
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
