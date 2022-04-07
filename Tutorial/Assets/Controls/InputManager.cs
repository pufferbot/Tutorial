using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public GameManager gameManager;
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] PlayerUI playerUI;
    [SerializeField] Interact interact;
    [SerializeField] WeaponControls weaponControls;
    [SerializeField] DialogueManager dialogueManager;

    Vector2 horizontalInput;
    Vector2 mouseInput;
    bool isSprinting = false;

    private PlayerControls playerControls;
    private InputActionMap groundMovement;
    private InputActionMap pauseMenu;
    private InputActionMap tabMenu;
    private InputActionMap dialogue;

    private void Awake()
    {
        playerControls = new PlayerControls();
        groundMovement = playerControls.GroundMovement;
        pauseMenu = playerControls.PauseMenu;
        tabMenu = playerControls.TabMenu;
        dialogue = playerControls.Dialogue;

        //playerControls.[actionMap].[action].performed += context => do something

        //GroundMovement
        playerControls.GroundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        playerControls.GroundMovement.Jump.performed += _ => movement.OnJumpPressed();
        playerControls.GroundMovement.Sprint.started += _ => isSprinting = true;
        playerControls.GroundMovement.Sprint.canceled += _ => isSprinting = false;
        playerControls.GroundMovement.Crouch.started += _ => movement.OnCrouchPressed();
        playerControls.GroundMovement.Crouch.canceled += _ => movement.OnCrouchPressed();

        playerControls.GroundMovement.Pause.performed += _ => playerUI.TogglePauseMenu();
        playerControls.GroundMovement.Menu.performed += _ => playerUI.ToggleMenu();

        playerControls.GroundMovement.Interact.performed += _ => interact.OnInteract();
        playerControls.GroundMovement.Hold.started += _ => interact.OnHold();
        playerControls.GroundMovement.Hold.canceled += _ => interact.Release();

        playerControls.GroundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerControls.GroundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        
        playerControls.GroundMovement.Attack.performed += _ => weaponControls.OnAttack();

        //PauseMenu
        playerControls.PauseMenu.Resume.performed += _ => playerUI.TogglePauseMenu();

        //TabMenu
        playerControls.TabMenu.Resume.performed += _ => playerUI.ToggleMenu();

        //Dialogue
        playerControls.Dialogue.Skip.performed += _ => dialogueManager.skipSpeak();

    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput, isSprinting);
        mouseLook.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public enum NewActionMap
    {
        GroundMovement, PauseMenu, TabMenu, Dialogue
    };
    public void SwitchActionMap(NewActionMap newActionMap)
    {
        groundMovement.Disable();
        pauseMenu.Disable();
        tabMenu.Disable();
        dialogue.Disable();

        if (newActionMap == NewActionMap.GroundMovement)
        {
            horizontalInput = new Vector2(0, 0);
            groundMovement.Enable();
        }
        else if (newActionMap == NewActionMap.PauseMenu)
        {
            pauseMenu.Enable();
        }
        else if (newActionMap == NewActionMap.TabMenu)
        {
            tabMenu.Enable();
        }
        else if (newActionMap == NewActionMap.Dialogue)
        {
            dialogue.Enable();
        }

    }

}