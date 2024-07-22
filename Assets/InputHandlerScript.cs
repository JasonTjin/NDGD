using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandlerScript : MonoBehaviour
{
    public GameObject narrativeController;
    public NarrativeControllerScript narrativeControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        narrativeControllerScript = narrativeController.GetComponent<NarrativeControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            narrativeControllerScript.LeftArrowSelected();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            narrativeControllerScript.RightArrowSelected();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            narrativeControllerScript.UpArrowSelected();
        }
    }

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;


        switch(rayHit.collider.gameObject.name)
        {
            case "LeftArrow":
            narrativeControllerScript.LeftArrowSelected();
            break;

            case "RightArrow":
            narrativeControllerScript.RightArrowSelected();
            break;

            case "UpArrow":
            narrativeControllerScript.UpArrowSelected();
            break;

            default:
            break;
        }
    }
}
