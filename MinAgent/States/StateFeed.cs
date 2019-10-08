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
    class StateFeed : State
    {
        //Random rnd;

        public override IAction Execute(MinAgent agent)
        {
           // rnd = new Random();

            if (agent.plants.Count > 0)
            {
                agent.targetPlant = (Plant)agent.plants[0];
            }
            else
            {
                agent.targetPlant = null;
                return new Move(new AIVector(agent.moveX, agent.moveY));
            }

            if (agent.alliedAgents.Count > 0)
            {
                foreach (var allied in agent.alliedAgents)
                {
                    MinAgent alliedAgent = (MinAgent)allied;
                    if (allied.Hunger > agent.Hunger || (allied.Health < agent.Health && allied.Hunger > allied.Endurance))
                    {
                        return new Move(new AIVector(agent.moveX, agent.moveY));
                    }
                }
            }

            if (agent.targetPlant != null && AIVector.Distance(agent.Position, agent.targetPlant.Position) > AIModifiers.maxFeedingRange) //if agent is too far away from a plant to feed, move closer to it
            {
                AIVector vector = new AIVector(agent.targetPlant.Position.X - agent.Position.X, agent.targetPlant.Position.Y - agent.Position.Y);
                agent.moveX = vector.X;
                agent.moveY = vector.Y;
                return new Move(new AIVector(agent.moveX, agent.moveY));
            }
            else //eat focused plant
            {
                return new Feed(agent.targetPlant);
            }
        }
    }
}
