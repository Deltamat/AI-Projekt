using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;

namespace Agent0047
{
    class StateProcreate : State
    {
        Agent0047 lover;
        public override IAction Execute(Agent0047 agent)
        {
            lover = null;
            foreach (Agent0047 allied in agent.alliedAgents)
            {
                if (allied.ProcreationCountDown == 0)
                {
                    lover = allied;
                }
            }

            if (lover != null)
            {
                if (agent.alliedAgents.Count() > 1 && agent.alliedAgents[0].ProcreationCountDown == 0 && AIVector.Distance(agent.Position, agent.alliedAgents[0].Position) < AIModifiers.maxProcreateRange)
                {
                    return new Procreate(agent.alliedAgents[0]);
                }
                else if (agent.alliedAgents.Count() > 1 && agent.alliedAgents[0].ProcreationCountDown == 0 && AIVector.Distance(agent.Position, agent.alliedAgents[0].Position) > AIModifiers.maxProcreateRange)
                {
                    AIVector vector = new AIVector(agent.alliedAgents[0].Position.X - agent.Position.X, agent.alliedAgents[0].Position.Y - agent.Position.Y);
                    agent.moveX = vector.X;
                    agent.moveY = vector.Y;
                    return new Move(new AIVector(agent.moveX, agent.moveY));
                }
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
