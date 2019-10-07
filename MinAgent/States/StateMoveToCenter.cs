using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace MinAgent
{
    class StateMoveToCenter : State
    {
        Random rnd = new Random();
        public override IAction Execute(MinAgent agent)
        {
            if (agent.delay > 100)
            {
                //AIVector vector = new AIVector(MinAgent.window.Width * 0.5f - agent.Position.X, MinAgent.window.Height * 0.5f - agent.Position.Y);
                AIVector vector = new AIVector(rnd.Next(0, MinAgent.window.Width + 1) - agent.Position.X, rnd.Next(0, MinAgent.window.Height + 1) - agent.Position.Y);
                agent.moveX = vector.Normalize().X;
                agent.moveY = vector.Normalize().Y;
                return new Move(new AIVector(agent.moveX, agent.moveY));
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
