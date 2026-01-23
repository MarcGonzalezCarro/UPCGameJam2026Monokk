using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;          
    public float mouseSensitivity = 100f;
    public float distance = 5f;        
    public float height = 2f;          

    public float minY = -30f;          
    public float maxY = 60f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        //rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = player.position + offset;

        transform.LookAt(player.position + Vector3.up * height);
    }
}
