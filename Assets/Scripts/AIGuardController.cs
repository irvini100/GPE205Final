using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuardController : AIController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//If I am in the idle state.
        if (currentState == AIStates.Idle)
        {
            DoIdle();

            //Check for transitions.
            if (IsTankClose(GameManager.Instance.players[0].Pawn))
            {
                //ChangeState
                ChangeState(AIStates.Chase);
            }
        }
        else if (currentState == AIStates.Chase){
            //Do the chase action
            DoChase();

            //Check for transitions
            if (TimeInCurrentStateIsGreaterThan( 3))
            {
                //TODO:  ChanngeState back to idle.
                ChangeState(AIStates.Idle);
            }
        }
	}
}
