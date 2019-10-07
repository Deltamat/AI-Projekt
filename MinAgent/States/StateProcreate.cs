using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;

namespace MinAgent
{
    class StateProcreate : State
    {
        public override IAction Execute(MinAgent agent)
        {
            if (agent.alliedAgents.Count() > 1 && agent.alliedAgents[1].ProcreationCountDown == 0 && AIVector.Distance(agent.Position, agent.alliedAgents[1].Position) < AIModifiers.maxProcreateRange)
            {
                return new Procreate(agent.alliedAgents[1]);
            }
            else if (agent.alliedAgents.Count() > 1 && agent.alliedAgents[1].ProcreationCountDown == 0 && AIVector.Distance(agent.Position, agent.alliedAgents[1].Position) > AIModifiers.maxProcreateRange)
            {
                AIVector vector = new AIVector(agent.alliedAgents[1].Position.X - agent.Position.X, agent.alliedAgents[1].Position.Y - agent.Position.Y);
                agent.moveX = vector.Normalize().X;
                agent.moveY = vector.Normalize().Y;
                return new Move(new AIVector(agent.moveX, agent.moveY));
            }
            else
            {
                return new Move(new AIVector(agent.moveX, agent.moveY));
            }
        }
    }
}
