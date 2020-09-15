using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public float moveSpeed;
    public float rotationSpeed;

    [SerializeField] private Transform groundCheckPoint;

    [SerializeField] private CharacterController characterController;

    private Camera camera;
    private Vector3 playerVelocity;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(variableJoystick.Horizontal, 0f) || Mathf.Approximately(variableJoystick.Vertical, 0f))
        {
            return;
        }
        
        // var _isGrounded = Physics.CheckSphere(transform.position, 1f, ~LayerMask.NameToLayer("Ground"), QueryTriggerInteraction.Ignore);
        RaycastHit hitInfo;
        bool _isGrounded = Physics.Linecast(groundCheckPoint.position, groundCheckPoint.position + Vector3.down, out hitInfo);
        

        print(_isGrounded ? "grounded" : "not grounded");
        if (_isGrounded)
        {
            playerVelocity.y = 0f;
            
            print(hitInfo.collider.gameObject.name);
            PathBlock pathBlock = hitInfo.collider.GetComponent<PathBlock>();
            if (pathBlock != null)
            {
                pathBlock.SetTouched();
            }
        }
        else
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;    
        }
        

        float xAxis = variableJoystick.Horizontal;
        float yAxis = variableJoystick.Vertical;
        
        Vector3 moveVector = new Vector3(xAxis, 0, yAxis);
        Vector3 relativeMoveVector = Camera.main.transform.TransformVector(moveVector);
        relativeMoveVector.y = 0;


        characterController.Move(relativeMoveVector * Time.deltaTime * moveSpeed);

        if (transform.rotation != Quaternion.LookRotation (relativeMoveVector))
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (relativeMoveVector), Time.deltaTime * rotationSpeed);    
        }
        
        
        characterController.Move(playerVelocity * Time.deltaTime);

        
        // Vector3 forward = camera.transform.forward;
        // Vector3 right = camera.transform.right;
        // forward.y = 0f;
        // right.y = 0f;
        // forward.Normalize();
        // right.Normalize();

        // var desiredMoveDirection = forward * yAxis + right * xAxis;
        // transform.Translate(desiredMoveDirection * speed * Time.deltaTime);
    }
    
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // PathBlock pathBlock = hit.collider.GetComponent<PathBlock>();
        //
        // if (pathBlock != null)
        // {
        //     pathBlock.SetTouched();
        // }
    }
}
