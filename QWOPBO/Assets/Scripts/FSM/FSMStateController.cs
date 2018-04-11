using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStateController : MonoBehaviour {

    public FSMState currentState;
    public FSMEnemyStats enemyStats;
    public Transform eyes;
    public FSMState remainState;


    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public EnemyPunching enemyPunching;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    void Awake () 
    {
        enemyPunching = GetComponent<EnemyPunching> ();
        navMeshAgent = GetComponent<NavMeshAgent> ();
    }

    public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
    {
        wayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromTankManager;
        if (aiActive) 
        {
            navMeshAgent.enabled = true;
        } else 
        {
            navMeshAgent.enabled = false;
        }
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState (this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null) 
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
        }
    }

    public void TransitionToState(FSMState nextState)
    {
        if (nextState != remainState) 
        {
            currentState = nextState;
            OnExitState ();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}