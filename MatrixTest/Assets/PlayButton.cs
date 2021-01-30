using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayButton : MonoBehaviour
{
    public PlaybackLogic playbackLogic;
    public InputActionAsset playerControls;
    public InputAction fireAction;
    public float buttonSpring = 0.5f;

    private Collider buttonCollider;
    private Vector3 initialPosition;
    private Vector3 pressing = new Vector3(0, -0.2f, 0);

    private void Awake()
    {
        InputActionMap inputActionMap = playerControls.FindActionMap("Player");

        fireAction = inputActionMap.FindAction("Fire");
        fireAction.performed += FireAction_performed;

        buttonCollider = GetComponent<Collider>();
    }

    private void FireAction_performed(InputAction.CallbackContext obj)
    {
        Vector2Control mousev2c = Mouse.current.position;
        Vector3 mousePosition = new Vector3(mousev2c.x.ReadValue(), mousev2c.y.ReadValue(), 0);
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if(buttonCollider.Raycast(mouseRay, out hit, 100f))
        {
            playbackLogic.PlayRandom();
            gameObject.transform.position = initialPosition + pressing;
        }
    }

    private void OnEnable()
    {
        fireAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, initialPosition, buttonSpring * Time.deltaTime);
    }
}
