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
        public override IAction Execute(MinAgent agent)
        {
            if (agent.closeEnemyAgents.Count > 0 && agent.Strength > 0)
            {
                if (AIVector.Distance(agent.Position, agent.closeEnemyAgents[0].Position) > AIModifiers.maxMeleeAttackRange) // move closer
                {
                    var enVector = agent.closeEnemyAgents[0].Position - agent.Position;
                    return new Move(enVector);
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
