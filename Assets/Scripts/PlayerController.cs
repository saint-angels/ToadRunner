using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public float moveSpeed;

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
        
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        

        float xAxis = variableJoystick.Horizontal;
        float yAxis = variableJoystick.Vertical;
        
        Vector3 moveVector = new Vector3(xAxis, 0, yAxis);
        Vector3 relativeMoveVector = Camera.main.transform.TransformVector(moveVector);
        relativeMoveVector.y = 0;
        print($"{moveVector}: {relativeMoveVector}");
        
        
        characterController.Move(moveVector * Time.deltaTime * moveSpeed);

        if (moveVector != Vector3.zero)
        {
            gameObject.transform.forward = moveVector;
        }
        
        playerVelocity.y += -9.8f * Time.deltaTime;
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
}
