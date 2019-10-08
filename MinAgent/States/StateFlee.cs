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
        float longestSpace;
        AIVector halfwayDistance;

        Random rnd = new Random();
        public override IAction Execute(Agent0047 agent)
        {
            //Resets variables
            longestSpace = 0f;
            halfwayDistance = null;
           
            if (agent.closeEnemyAgents.Count == 1) //Checks if there is only one enemy, then reverses direction
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
            else
            {
                //Goes through all nearby enemies
                foreach (Agent enemyAgent in agent.closeEnemyAgents)
                {
                    foreach (Agent otherEnemyAgent in agent.closeEnemyAgents)
                    {
                        //Finds the longest distance between two enemies, from all nearby enemies
                        if (AIVector.Distance(enemyAgent.Position, otherEnemyAgent.Position) > longestSpace && otherEnemyAgent != enemyAgent)
                        {
                            longestSpace = AIVector.Distance(enemyAgent.Position, otherEnemyAgent.Position); //Finds the distance
                            halfwayDistance = (enemyAgent.Position - otherEnemyAgent.Position) * 0.5f; //Finds the halfway point between the enemies furthest away from each other
                        }
                    }
                }
            }

            if (halfwayDistance != null)
            {
                //Returns a direction vector from the agents position and the halfway point found above
                return new Move(new AIVector(halfwayDistance.X - agent.Position.X, halfwayDistance.Y - agent.Position.Y));
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
