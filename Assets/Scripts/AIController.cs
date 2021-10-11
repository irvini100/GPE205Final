using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {
    public enum AIStates { Idle, Chase, StopAndShoot, Flee};
    public AIStates currentState;
    public float howFarIsClose = 1;
    public float timeEnteredCurrentState;

    public void ChangeState (AIStates newState)
    {
        //Actually change the state.
        currentState = newState;
        //Save the time that I changed states.
        timeEnteredCurrentState = Time.time;
    }

    public void DoIdle()
    {

    }

    public void DoChase()
    {
        TurnTowardsPlayer();
        Pawn.mover.MoveForward();
    }

    public void DoFlee()
    {
        TurnTowardsPlayer();
        Pawn.mover.Back();
    }

    public void TurnTowards(Vector3 position)
    {
        //Find the vector that points from this object to the position.
        Vector3 vectorToTarget = position - Pawn.transform.position;
        //Find the rotation instructions that will cause me to look down that vector.
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //Turn a little bit towards the goal.  (Just enough that when we run this every frame, we move at pawn.turnspeed degrees per second.)
        Pawn.transform.rotation = Quaternion.RotateTowards(Pawn.transform.rotation, targetRotation, Pawn.turnSpeed * Time.deltaTime);
    }

    public void TurnTowards (Transform transform)
    {
        TurnTowards(transform.position);
        }

    public void TurnTowardsPlayer()
    {
        //TurnTowards(GameManager.instance.players[0].Pawn.transform);
        Debug.Log(GameManager.Instance.players[0]);
        
    }

    public void TurnTowards(GameObject targetObject)
    {
        TurnTowards(targetObject.transform);
    }

    public bool IsTankClose (TankData tankToCheck)
    {
        if (Vector3.Distance(Pawn.transform.position, tankToCheck.transform.position) <= howFarIsClose)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool TimeInCurrentStateIsGreaterThan ( float time)
    {
        //If the current time is > time
        if (Time.time > timeEnteredCurrentState + time)
        {
            return true;
        }
        else{
            return false;
        }
    }
    }

