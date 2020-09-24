using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class PlayerController : SingletonComponent<PlayerController>
{
    public event Action OnNewBlockTouched = () => { };
    
    public VariableJoystick variableJoystick;
    public float moveSpeed;
    public float rotationSpeed;
    public float coyoteTime = .2f;
    

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Animator characterAnimator;

    [SerializeField] private CharacterController characterController;

    private new Camera camera;
    private Vector3 playerVelocity;
    private static readonly int InAir = Animator.StringToHash("InAir");

    private float currentCoyoteTime;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }
    
    void Update()
    {
        RaycastHit hitInfo;
        bool isGrounded = Physics.Linecast(groundCheckPoint.position, groundCheckPoint.position + Vector3.down, out hitInfo);
        
        
        if (isGrounded)
        {
            currentCoyoteTime = coyoteTime;   
            playerVelocity.y = 0f;
            
            PathBlock pathBlock = hitInfo.collider.GetComponent<PathBlock>();
            if (pathBlock != null && pathBlock.CanTouch)
            {
                OnNewBlockTouched();
                pathBlock.SetTouched();
            }
            characterAnimator.SetBool(InAir, false);
        }
        else if (0 <= currentCoyoteTime)
        {
            currentCoyoteTime -= Time.deltaTime;
        }
        else
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            characterAnimator.SetBool(InAir, true);
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
    }


    public void ResetPlayer(Transform startPoint)
    {
        characterController.enabled = false;
        playerVelocity = Vector3.zero;
        Transform transformCache = transform;
        transformCache.position = startPoint.position;
        transformCache.rotation = startPoint.rotation;
        characterController.enabled = true;
    }
}
