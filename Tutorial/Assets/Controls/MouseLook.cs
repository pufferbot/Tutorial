using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] float sensitivityX = 80; //Make slider in menu for this 1-10, and multiply by 10 before passing in
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0;

    private void Update()
    {
        if(gameManager.GetGameState() != GameManager.GameState.Running)
            return;
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }

    public void ToggleMouseLock(){
        if(Cursor.lockState == CursorLockMode.Locked){
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
