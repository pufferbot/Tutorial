using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;

    public enum GameState{
        PauseMenu, TabMenu, Running, Dialogue
    }

    private GameState state;

    public void SetGameState(GameState state){
        this.state = state;
        if (state == GameState.Running) { inputManager.SwitchActionMap(InputManager.NewActionMap.GroundMovement); }
        else if (state == GameState.Dialogue) { inputManager.SwitchActionMap(InputManager.NewActionMap.Dialogue); }
        else if (state == GameState.TabMenu) { inputManager.SwitchActionMap(InputManager.NewActionMap.TabMenu); }
        else if (state == GameState.PauseMenu) { inputManager.SwitchActionMap(InputManager.NewActionMap.PauseMenu); }
    }
    public GameState GetGameState(){
        return state;
    }

    public bool LoadingCharacter;

    public void SetCharacterPosition(GameObject character, Vector3 newPosition, Vector3 newRotation){
        character.transform.position = newPosition;
        character.transform.eulerAngles = newRotation;
    }

}
