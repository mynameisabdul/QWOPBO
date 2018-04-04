using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scripts/FSM/FSMDecisions/Look")]
public class FSMLookDecision : FSMDecision {

    public override bool Decide(FSMStateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(FSMStateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag ("Player")) {
            controller.chaseTarget = hit.transform;
            return true;
        } else 
        {
            return false;
        }
    }
}