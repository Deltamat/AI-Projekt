using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace MinAgent
{
    class StateFlee : State
    {

        public override IAction Execute(MinAgent agent)
        {
            if (agent.closeEnemyAgents.Count > 0)
            {
                AIVector vector = agent.Position - agent.closeEnemyAgents[0].Position;
                vector.Normalize();
                agent.moveX = vector.X;
                agent.moveY = vector.Y;
                return new Move(vector);
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
            
        }
    }
}
