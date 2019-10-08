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
        Random rnd = new Random();
        public override IAction Execute(MinAgent agent)
        {
            if (agent.closeEnemyAgents.Count > 0)
            {
                AIVector vector = agent.Position - agent.closeEnemyAgents[0].Position;
                vector.Normalize();
                AIVector rotatedVector = RotateVector(vector, rnd.Next(50, 90));
                //agent.moveX = vector.X;
                //agent.moveY = vector.Y;
                agent.moveX = rotatedVector.X;
                agent.moveY = rotatedVector.Y;
                //return new Move(vector);
                return new Move(rotatedVector);
            }
            return new Move(new AIVector(agent.moveX, agent.moveY));
            
        }

        private AIVector RotateVector(AIVector vector, double angle)
        {
            angle = -angle * (Math.PI / 180);
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new AIVector((float)cos * vector.X - (float)sin * vector.Y, (float)sin * vector.X + (float)cos * vector.Y); 
        }
    }
}
