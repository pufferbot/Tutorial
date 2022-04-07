using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] CharacterController controller;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject cameraObject;

    public float speed = 10f;
    Vector2 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    [SerializeField] float gravity = -32.2f; //-32.2 ft/s^2 is gravitational force downwards on Earth. Equal to -9.81 m/s^2
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] bool grounded;

    [SerializeField] float sprintMultiplier = 1.5f;
    bool isSprinting;

    [SerializeField] float crouchTime;
    [SerializeField] float standHeight;
    [SerializeField] float crouchHeight;
    bool isCrouching;

    private void LateUpdate()
    {
        if(gameManager.LoadingCharacter || gameManager.GetGameState() != GameManager.GameState.Running) return;
        grounded = controller.isGrounded;
        if (controller.isGrounded)
        {
            verticalVelocity.y = -1;
        }
        else
        {
            jump = false;
        }

        float totalSpeed = speed;

        if (playerStats.overEncumbered)
        {
            totalSpeed /= 2;
            isSprinting = false;
            jump = false;
        }

        //v = sqr(-2 * jmpHeight * gravity)
        if (jump && controller.isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            jump = false;
        }

        if (isSprinting && playerStats.currentStamina > 0 && horizontalInput != new Vector2(0,0))
        {
            playerStats.DamageStamina((15 - playerStats.dexterity.GetValue()) * Time.deltaTime); //Dexterity affects how much stamina sprinting uses
            totalSpeed = speed * sprintMultiplier; 
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * totalSpeed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

    }

    public void ReceiveInput(Vector2 _horizontalInput, bool _isSprinting)
    {
        horizontalInput = _horizontalInput;
        isSprinting = _isSprinting;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
    
    public void OnCrouchPressed()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            StartCoroutine(Crouch());
        }
        else
        {
            StartCoroutine(Stand());
        }

    }

    IEnumerator Crouch()
    {
        float timeElapsed = 0;

        while (timeElapsed < crouchTime)
        {
            if(!isCrouching)
            {
                yield return null;
            }
            controller.height = Mathf.Lerp(standHeight, crouchHeight, timeElapsed / crouchTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        controller.height = crouchHeight;
    }

    IEnumerator Stand()
    {
        float timeElapsed = 0;

        while (timeElapsed < crouchTime)
        {
            if(!isCrouching)
            {
                yield return null;
            }
            controller.height = Mathf.Lerp(crouchHeight, standHeight, timeElapsed / crouchTime);
            controller.Move(Vector3.up / ((standHeight - crouchHeight) / crouchTime));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        controller.height = standHeight;
    }

}
