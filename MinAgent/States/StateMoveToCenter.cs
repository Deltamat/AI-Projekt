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
        //Random rnd = new Random();
        public override IAction Execute(MinAgent agent)
        {
            Random rnd = new Random();
            AIVector vector = new AIVector(MinAgent.window.Width * 0.5f - agent.Position.X, MinAgent.window.Height * 0.5f - agent.Position.Y);
            agent.moveX = vector.X;
            agent.moveY = vector.Y;
            if (AIVector.Distance(agent.Position, vector) <= 5)
            {
                agent.moveX = rnd.Next(-1, 2);
                agent.moveY = rnd.Next(-1, 2);
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
        }
    }
}
