using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float runSpeed;

    [Header("Mouse Settings")] 
    [SerializeField] protected float horizontalSensitivity = 1.0f;
    [SerializeField] protected float verticalSensitivity = 1.0f;
    
    protected void Start()
    {
        LockCursor();
    }

    protected virtual void Update()
    {
        LockCursor();
    
        UpdatePosition();
        UpdateRotation();
    }

    protected virtual void UpdatePosition()
    {
        Vector2 inputAxis = 
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Vector3 forward = transform.forward * inputAxis.y;
        Vector3 right = transform.right * inputAxis.x;

        AppleyMoveVector(forward + right);
    }

    protected void AppleyMoveVector(Vector3 direction)
    {
        Vector3 moveVector = direction.normalized;
        float applySpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        transform.position += moveVector * applySpeed * Time.deltaTime;
    }
    

    protected virtual void UpdateRotation()
    {
        
    }
    
    protected void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}