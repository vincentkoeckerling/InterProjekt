using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float MinYaw = -360;
    public float MaxYaw = 360;
    public float MinPitch = -60;
    public float MaxPitch = 60;
    public float LookSensitivity = 3;

    public float MoveSpeed = 3;
    public float SprintSpeed = 7;

    private CharacterController movementController;

    private float yaw;
    private float pitch;

    private Vector3 forward => new Vector3(transform.forward.x, 0, transform.forward.z);

    private void Start()
    {

        movementController = GetComponent<CharacterController>();   //  Character Controller

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;
    }

    private void Update()
    {

        Vector3 direction = Vector3.zero;
        direction += forward * Input.GetAxisRaw("Vertical");
        direction += transform.right * Input.GetAxisRaw("Horizontal");

        direction.Normalize();

        float speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {  // Player can sprint by holding "Left Shit" keyboard button
            speed = SprintSpeed;
        }
        else
        {
            speed = MoveSpeed;
        }

        movementController.Move(direction * Time.deltaTime * speed);

        // Camera Look
        yaw += Input.GetAxisRaw("Mouse X") * LookSensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * LookSensitivity;

        yaw = ClampAngle(yaw, MinYaw, MaxYaw);
        pitch = ClampAngle(pitch, MinPitch, MaxPitch);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    protected float ClampAngle(float angle)
    {
        return ClampAngle(angle, 0, 360);
    }

    protected float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}