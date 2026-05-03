using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaController : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float rotationSpeed = 700.0f;
    public InputActionReference moveAction; 
    private Animator anim;
    private float currentAnimSpeed = 0f;

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        bool isMoving = movement.magnitude > 0.1f;
        bool isRunning = Keyboard.current != null && Keyboard.current.shiftKey.isPressed;
        float targetAnimSpeed = 0f;
        float currentMoveSpeed = 0f;

        if (isMoving)
        {
            if (isRunning)
            {
                targetAnimSpeed = 1f;
                currentMoveSpeed = runSpeed;
            }
            else
            {
                targetAnimSpeed = 0.5f;
                currentMoveSpeed = walkSpeed;
            }
        }
        else
        {
            targetAnimSpeed = 0f;
        }

        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, Time.deltaTime * 10f);
        anim.SetFloat("Speed", currentAnimSpeed);

        if (isMoving) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime, Space.Self);
        }
    }
}