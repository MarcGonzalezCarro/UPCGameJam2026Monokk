using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Dirección basada en la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * vertical + camRight * horizontal;

        // Mover solo la posición (sin rotar)
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Gravedad
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
