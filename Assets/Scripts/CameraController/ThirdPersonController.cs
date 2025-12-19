using UnityEngine;

public class ThirdPersonController : CameraController
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform headPivotTransform;
    
    [SerializeField] protected float maxAngleX = 90;
    [SerializeField] protected float minAngleX = -90;
    
    private float angleX = 0.0f;
    private float angleY = 0.0f;

    protected override void Update()
    {
        base.Update();
        UpdateCameraRotation();
    }

    protected override void UpdateRotation()
    {
        Vector2 mouseInput = new Vector2(
            Input.GetAxis("Mouse X") * horizontalSensitivity,
            Input.GetAxis("Mouse Y") * verticalSensitivity);
        
        angleX -= mouseInput.y;
        angleX = Mathf.Clamp(angleX, minAngleX, maxAngleX);
        
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            angleY += mouseInput.x;
        }
        else
        {
            transform.Rotate(Vector3.up, mouseInput.x);
            angleY = 0.0f;
        }

        headPivotTransform.localRotation = Quaternion.Euler(angleX, angleY, 0);

    }
    
    private void UpdateCameraRotation()
    {
        cameraTransform.LookAt(cameraTarget);
    }
}