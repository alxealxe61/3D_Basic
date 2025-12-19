using UnityEngine;

public class FreeCamController : CameraController
{
    [SerializeField] protected float maxAngleX = 90;
    [SerializeField] protected float minAngleX = -90;
    
    private float angleX = 0.0f;
    private float angleY = 0.0f;

    
    protected override void UpdatePosition()
    {
        Vector2 inputAxis = 
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Vector3 right = transform.right * inputAxis.x;
        Vector3 forward = transform.forward * inputAxis.y;
        
        inputAxis.y += Input.GetKey(KeyCode.Q) ? 1 : 0; 
        inputAxis.y += Input.GetKey(KeyCode.E) ? -1 : 0;
        Vector3 up = transform.up * inputAxis.y;
        
        AppleyMoveVector(right + forward + up);
    }
    
    protected override void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        
        angleY += mouseX;
        angleX -= mouseY;
        
        angleX = Mathf.Clamp(angleX, minAngleX, maxAngleX);
        
        transform.localRotation = Quaternion.Euler(angleX, angleY, 0);
    }
}