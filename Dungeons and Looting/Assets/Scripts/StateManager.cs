using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static StateManager instance;
    public enum GameState
    {
        HasEquipped1, 
        HasEquipped2
    }

    public GameState CurrentState;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ChangeState(GameState.HasEquipped1);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        ExitState(CurrentState);

        CurrentState = newState;

        EnterState(CurrentState);
    }

    void EnterState(GameState newState)
    {
        switch (newState)
        {
            case GameState.HasEquipped1:
                Debug.Log("Weapon 1 Equipped, ID 0");
                break;
        }
    }

    void ExitState(GameState state)
    {
        switch (state)
        {
            case GameState.HasEquipped1:
                break;
        }
    }
}
