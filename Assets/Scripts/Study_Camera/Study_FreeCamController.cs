using UnityEngine;
using UnityEngine.Serialization;

public class Study_FreeCamController : MonoBehaviour
{
    // WASD로 이동 
    // W는 Forward (바라보는 방향), S는 BacK
    // A는 Left, D는 Right
    
    [Header("Movement Settings")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;

    [Header("Mouse Settings")] 
    [SerializeField] private float horizontalSensitivity = 1.0f;
    [SerializeField] private float verticalSensitivity = 1.0f;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    
        UpdatePosition();
        UpdateRotation();
    }
    
    private void UpdatePosition()
    {
       Vector3 inputAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
       
       Vector3 right = transform.right * inputAxis.x;
       Vector3 forward = transform.forward * inputAxis.z;
       
       // 상승과 하강까지 처리함
       inputAxis.y += Input.GetKey(KeyCode.Q) ? 1 : 0; 
       inputAxis.y += Input.GetKey(KeyCode.E) ? -1 : 0;
       Vector3 up = transform.up * inputAxis.y;
       
       Vector3 moveVector = (forward + right + up).normalized;
       float applySpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
       transform.position += moveVector * applySpeed * Time.deltaTime;
    }

    private float angleY = 0.0f;
    private float angleX = 0.0f;
    
    [SerializeField] private float maxXAngle = 90;
    [SerializeField] private float minXAngle = -90;
    
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        
        angleY += mouseX;
        angleX -= mouseY;
        
        angleX = Mathf.Clamp(angleX, minXAngle, maxXAngle);
        
        transform.localRotation = Quaternion.Euler(angleX, angleY, 0);
    }
}