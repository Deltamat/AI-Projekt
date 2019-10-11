using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework.Actions;
using AIFramework;
using AIFramework.Entities;

namespace Agent0047
{
    class StateAttack : State
    {
        public override IAction Execute(Agent0047 agent)
        {
            // if no enemies just EAT 
            if (agent.closeEnemyAgents.Count == 0)
            {
                return new StateFeed().Execute(agent);
            }

            // if in range of enemy: attack
            if (AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) <= AIModifiers.maxMeleeAttackRange && !agent.closeEnemyAgents[0].Defending)
            {
                return new Attack(agent.closeEnemyAgents[0]);
            }

            float enemyDPS = 0;
            float ownDPS = agent.Strength * 0.5f;
            int combinedEnemyHealth = 0;
            int combinedAllyHealth = agent.Health;
            foreach (var enemy in agent.closeEnemyAgents)
            {
                combinedEnemyHealth += enemy.Health;
                enemyDPS += enemy.Strength * 0.5f;
            }
            foreach (var ally in agent.alliedAgents)
            {
                combinedAllyHealth += ally.Health;
                ownDPS += ally.Strength * 0.5f;
            }
            float timeToKillEnemy = combinedEnemyHealth / ownDPS;
            float timeToKillAllied = combinedAllyHealth / enemyDPS;

            if (timeToKillAllied > timeToKillEnemy)
            {
                Agent targetEnemy = agent.closeEnemyAgents[0];
                foreach (var enemy in agent.closeEnemyAgents)
                {
                    if (targetEnemy.Health > enemy.Health && !enemy.Defending)
                    {
                        targetEnemy = enemy;
                    }
                }

                if (agent.closeEnemyAgents.Count > 0 && AIVector.Distance(agent.Position, targetEnemy.Position) <= AIModifiers.maxMeleeAttackRange)
                {
                    //attack
                    return new Attack(agent.closeEnemyAgents[0]);
                }
                else if (agent.closeEnemyAgents.Count > 0)
                {
                    //move closer if out of range
                    AIVector vectorToEnemyAgentPosition = targetEnemy.Position - agent.Position;
                    return new Move(vectorToEnemyAgentPosition);
                }
            }
            else if (AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) < agent.Eyesight * 0.5f)
            {
                AIVector moveVector = agent.closeEnemyAgents[0].Position - agent.Position;
                agent.moveX = moveVector.X;
                agent.moveY = moveVector.Y;
                return new StateFlee().Execute(agent);
            }
            return new StateFeed().Execute(agent);
            //return new StateFlee().Execute(agent);

            //if outnumbered flee
            /*if(agent.closeEnemyAgents.Count > agent.alliedAgents.Count + 1) 
            {   
                return new StateFlee().Execute(agent);
            }
            else */
            //if (agent.closeEnemyAgents.Count > 0 && AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) <= AIModifiers.maxMeleeAttackRange)
            //{
            //    //attack
            //    return new Attack(agent.closeEnemyAgents[0]);
            //}
            //else if (agent.closeEnemyAgents.Count > 0)
            //{
            //    //move closer if out of range
            //    AIVector vectorToEnemyAgentPosition = agent.closeEnemyAgents[0].Position - agent.Position;
            //    return new Move(vectorToEnemyAgentPosition);
            //}
            //else
            //{
            //    //if there are no closeEnemyAgents they are either dead/gone somewhere out of vision range.
            //    return new StateMoveToCenter().Execute(agent);
            //}

        }
    }
}