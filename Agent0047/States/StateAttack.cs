using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework.Actions;
using AIFramework;

namespace MinAgent
{
    class StateAttack : State
    {
        public override IAction Execute(Agent0047 agent)
        {
            if (agent.closeEnemyAgents.Count > 0 && agent.Strength > 0)
            {
                //move closer if out of range
                if (AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) > AIModifiers.maxMeleeAttackRange)
                {
                    AIVector vectorToEnemyAgentPosition = agent.closeEnemyAgents[0].Position - agent.Position;
                    return new Move(vectorToEnemyAgentPosition);
                }
                else //attack
                {
                    return new Attack(agent.closeEnemyAgents[0]);
                }
            }
            else
            {
                return new StateFlee().Execute(agent);
            }
        }
    }
}
