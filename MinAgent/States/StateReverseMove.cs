﻿using System;
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
            if ((agent.Position.X < agent.Eyesight || agent.Position.X + agent.Eyesight > MinAgent.window.Width) && agent.delay > 300)
            {
                agent.moveX *= -1;
                agent.delay = 0;
            }
            else if ((agent.Position.Y < agent.Eyesight || agent.Position.Y + agent.Eyesight > MinAgent.window.Height - 20) && agent.delay > 300)
            {
                agent.moveY *= -1;
                agent.delay = 0;
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
