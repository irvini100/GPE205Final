using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
private float lastStateChangeTime;
public GameObject target;
public enum AIStates { Idle, Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost, RestartPatrol };
public float fleeDistance;
public Transform[] waypoints;
public float waypointStopDistance;
private int currentWaypoint = 0;
public AIStates currentState;
    // Start is called before the first frame update
    public override void Start()
    {
        //Run the parent base start.
        base.Start();
        ChangeState(AIStates.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
         MakeDecisions();
        //Run the parent base update.
        base.Update();
        
    }

    protected virtual void DoIdleState()
    {
        //Do Nothing
    }
    
    public void Seek(Vector3 targetPosition)
    {
      //RotateTorwards the function.
      pawn.RotateTowards(targetPosition);
      //Move Forward.
      pawn.MoveForward();
    }

    public void DoSeekState()
    {
        //Seek our target.
        Seek(target);
    }

    protected virtual void DoChaseState()
    {
        Seek(target);
    }

    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);
        //Move Forward
        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        //Seek the postion of our target transform.
        Seek(targetTransform.position);
    }

    public void Seek(Pawn targetPawn)
    {
        //Seek the pawn's transform.
        Seek(targetPawn.transform);
    }


    public virtual void ChangeState(AIStates newState)
    {
        //Change the current state
        currentState = newState;
        //Save the time when we changed states.
        lastStateChangeTime = Time.time;
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIStates.Idle:
            //Do work
            DoIdleState();
            //Check for transitions
            if (IsDistanceLessThan(target, 10)){
                ChangeState(AIStates.Chase);
            }
            break;
            case AIStates.Chase:
            //Do work
            DoChaseState();
            //Check for transitions
            if (!IsDistanceLessThan(target, 10))
            {
              ChangeState(AIStates.Idle);
            }
            break;
        }
    }

    public void Shoot()
    {
        //Tell the pawn to shoot.
        pawn.Shoot();
    }

    protected virtual void DoAttackState()
    {
        //Chase
        Seek(target);
        //Shoot
        Shoot();
    }

    protected void Flee()
    {
        //Find the vector to our target.
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        //Find the vector away from our target by multiplying by -1.
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        //Find the vector we will travel down in order to flee.
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;
        //Seek the point that is "fleeVector" away from our current position.
        Seek(pawn.transform.position + fleeVector);
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOffFleeDistance = targetDistance / fleeDistance;
        float flippedPercentOffFleeDistance = 1 - percentOffFleeDistance;
    }

    protected void Patrol()
    {
        //If we have enouogh waypoints in our list to move to a current waypoint.
        if (waypoints.Length > currentWaypoint)
        {
            //Then seek that waypoint.
            Seek(waypoints[currentWaypoint]);
            //If we are close enough, then increment to the next waypoint.
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }

            else
            {
                RestartPatrol();
            }
        }
        }

        protected void RestartPatrol()
        {
            //Set the index to 0.
            currentWaypoint = 0;
        }

        public void TargetPlayerOne()
        {
            //If the GameManager exists
            if (GameManager.instance != null)
            {
                //And the array of players exists
                if (GameManager.instance.players != null)
                {
                    //And there are players in it
                    if (GameManager.instance.players.Count > 0)
                    {
                        //Then target the gameObject of the pawn of the first player controller in the list.
                        target = GameManager.instance.players[0].pawn.gameObject;
                    }
                }
            }
        }

        protected bool IsHasTarget()
        {
            //Return true if we have a target, false if we don't.
            return (target != null);
        }
    

    protected void TargetNearestTank()
    {
        //Get a list of all the tanks (pawns).
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        //Assume that the first tank is closest.
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //Iterate through them one at a time.
        foreach (Pawn tank in allTanks)
        {
            //If this one is closer than the closest.
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                //It is the closest.
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        //Target the closest tank.
        target = closestTank.gameObject;
    }

    public bool CanHear(GameObject target)
    {
        //Get the target's noisemaker.
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        //If they don't have one, they can't make noise, so return false.
        if (noiseMaker == null)
        {
            return false;
        }

        //If they are making a noise, add the volumeDistance in the noiseMaker to the hearingDistance of this AI.
        float totalDistance = noiseMaker.voluemeDistance + hearingDistance;
        //If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            //...then we can hear the target.
            return true;
        }
        else
        {
            //Otherwise, we are too far away to hear them.
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        //Find the vector from the agent to the target.
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        //Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        //If that angle is less than our field of view.
        if (angleToTarget < fieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
