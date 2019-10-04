using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIFramework;
using AIFramework.Actions;

namespace MinAgent
{
    class StateMoveToCenter : State
    {
        public override IAction Execute(MinAgent agent)
        {
            AIVector vector = new AIVector(MinAgent.window.Width * 0.5f - agent.Position.X, MinAgent.window.Height * 0.5f - agent.Position.Y);
            agent.moveX = vector.Normalize().X;
            agent.moveY = vector.Normalize().Y;
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
