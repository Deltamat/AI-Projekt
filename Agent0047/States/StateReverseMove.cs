using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;

namespace Agent0047.States
{
    class StateReverseMove : State
    {
        public override IAction Execute(Agent0047 agent)
        {
            //if ((agent.Position.X < agent.Eyesight || agent.Position.X + agent.Eyesight > Agent0047.window.Width) && agent.delay > 400)
            //{
            //    agent.moveX *= -1;
            //    agent.delay = 0;
            //}
            //else if ((agent.Position.Y < agent.Eyesight || agent.Position.Y + agent.Eyesight > Agent0047.window.Height - 20) && agent.delay > 400)
            //{
            //    agent.moveY *= -1;
            //    agent.delay = 0;
            //}

            if (agent.Position.X < agent.Eyesight)
            {
                agent.moveX = 1;
            }
            else if (agent.Position.X + agent.Eyesight > Agent0047.window.Width)
            {
                agent.moveX = -1;
            }
            if (agent.Position.Y < agent.Eyesight)
            {
                agent.moveY = 1;
            }
            else if (agent.Position.Y + agent.Eyesight > Agent0047.window.Height - 20)
            {
                agent.moveY = -1;
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
