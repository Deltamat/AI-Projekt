using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;

namespace MinAgent.States
{
    class StateReverseMove : State
    {
        public override IAction Execute(MinAgent agent)
        {
            if ((agent.Position.X < agent.Eyesight - 10 || agent.Position.X + agent.Eyesight - 10 > MinAgent.window.Width) && agent.delay > 100)
            {
                agent.moveX *= -1;
                agent.delay = 0;
            }
            else if ((agent.Position.Y < agent.Eyesight || agent.Position.Y + agent.Eyesight - 10 > MinAgent.window.Height - agent.Eyesight - 10) && agent.delay > 100)
            {
                agent.moveY *= -1;
                agent.delay = 0;
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
