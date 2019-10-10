using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework.Actions;
using AIFramework;

namespace Agent0047
{
    class StateFollow : State
    {
        public override IAction Execute(Agent0047 agent)
        {
            AIVector moveVector = agent.agentToFollow.Position - agent.Position;
            return new Move(moveVector);
        }
    }
}
