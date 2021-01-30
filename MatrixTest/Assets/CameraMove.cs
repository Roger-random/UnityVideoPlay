using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public GameObject lookTarget;
    public InputActionAsset playerControls;
    public InputAction moveAction;
    public float moveStep;

    private Vector3 moveTarget;
    private Vector3 currentVelocity = new Vector3(0,0,0);

    private void Awake()
    {
        InputActionMap inputActionMap = playerControls.FindActionMap("Player");

        moveAction = inputActionMap.FindAction("Move");
        moveAction.performed += MoveAction_performed;
    }

    private void MoveAction_performed(InputAction.CallbackContext obj)
    {
        Vector2 tilt = obj.ReadValue<Vector2>();
        float xDelta;
        float yDelta;

        if (tilt.x < -0.5)
        {
            xDelta = -moveStep;
        }
        else if (tilt.x > 0.5)
        {
            xDelta = moveStep;
        }
        else
        {
            xDelta = 0;
        }

        if (tilt.y < -0.5)
        {
            yDelta = -moveStep;
        }
        else if (tilt.y > 0.5)
        {
            yDelta = moveStep;
        }
        else
        {
            yDelta = 0;
        }

        moveTarget += new Vector3(xDelta, yDelta, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveTarget = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, moveTarget, ref currentVelocity, 0.1f);
        gameObject.transform.LookAt(lookTarget.transform.position);
    }
}
