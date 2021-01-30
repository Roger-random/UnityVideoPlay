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

    private Collider buttonCollider;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
