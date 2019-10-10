using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework.Actions;
using AIFramework;

namespace Agent0047
{
    class StateAttack : State
    {
        public override IAction Execute(Agent0047 agent)
        {
            /*if(agent.closeEnemyAgents.Count > agent.alliedAgents.Count)
            {   //if outnumbered flee
                return new StateFlee().Execute(agent);
            }
            else */if (agent.closeEnemyAgents.Count > 0 && AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) <= AIModifiers.maxMeleeAttackRange)
            {
                //attack
                return new Attack(agent.closeEnemyAgents[0]);
            }
            else if(agent.closeEnemyAgents.Count > 0)
            {
                //move closer if out of range
                AIVector vectorToEnemyAgentPosition = agent.closeEnemyAgents[0].Position - agent.Position;
                return new Move(vectorToEnemyAgentPosition);
            }
            else
            {
                //if there are no closeEnemyAgents they are either dead/gone somewhere out of vision range.
                return new StateMoveToCenter().Execute(agent);
            }
        }
    }
}