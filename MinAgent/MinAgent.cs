using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using System.Diagnostics;

namespace MinAgent
{
    public class MinAgent : Agent
    {
        Random rnd;

        //Only for randomization of movement
        float moveX = 0;
        float moveY = 0;
        int delay;
        Plant closePlant;

        public MinAgent(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 140;
            Strength = 0;
            Health = 10;
            Eyesight = 50;
            Endurance = 50;
            Dodge = 0;


            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
            string ddd = this.GetType().FullName;
        }
        
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);
            //Checks if any non-allied agents are nearby and puts them in a list
            var closeEnemyAgents = agents.FindAll(a => !(a is MinAgent));
            //Checks if allied agents are nearby and puts them in a list
            var alliedAgents = agents.FindAll(a => a is MinAgent);
            
            delay++;

            Agent rndAgent = null;
            rndAgent = agents[rnd.Next(agents.Count)];

            if (rndAgent != null && rndAgent.GetType() == typeof(MinAgent) && ProcreationCountDown == 0)
            {
                return new Procreate(rndAgent);
            }

            if (plants.Count > 0 && Hunger > 20f)
            {
                if (closePlant == null) //if there are no focused plants
                {
                    closePlant = (Plant)plants[rnd.Next(plants.Count)]; //focuses on a nearby plant
                }

                if (AIVector.Distance(Position, closePlant.Position) >= AIModifiers.maxFeedingRange) //if agent is too far away from a plant to feed, move closer to it
                {
                    AIVector vector = new AIVector(closePlant.Position.X - Position.X, closePlant.Position.Y - Position.Y);
                    moveX = vector.Normalize().X;
                    moveY = vector.Normalize().Y;
                    return new Move(new AIVector(moveX, moveY));
                }
                else //eat focused plant
                {
                    return new Feed(closePlant);
                }
            }
            else if (plants.Count == 0 && Hunger > 20f && delay > 300) //choose new direction if there are no nearby plants
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);

                delay = 0;
            }

            closePlant = null;

            return new Move(new AIVector(moveX, moveY));

        }
        
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
        }
    }
}
