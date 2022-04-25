using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //check for attack range
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            
            
            //potential behaviors (circling, taunting)
            if (enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }

            if(enemyManager.currentRecoveryTime <=0 && distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return attackState;            //if within attack range, go to attack State
            }
            else if (distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return pursueTargetState;            //if player is out of attack range, return to pursue state
            }
            else
            {
                return this;             //if in recovery, return to this state, contiune behaviors
            }
        }
    }
}
