using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    public PatrollingState patrollingState; //property for the patrol state

    public void Initialize()
    {
        //setup default state
        patrollingState = new PatrollingState();
        ChangeState(patrollingState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState != null and make sure that we are not running any state when changing enemy state
        if (activeState != null)
        {
            activeState.Exit(); //if we are running a state, run cleanup on activeState
        }
        activeState = newState; //change to a new state

        //fail-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            activeState.stateMachine = this; //setup new state
            activeState.enemy = GetComponent<Enemy>(); //assign state enemy class
            activeState.Enter();
        }
    }
}
