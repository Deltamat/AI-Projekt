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
            MovementSpeed = 75;
            Strength = 5;
            Health = 80;
            Eyesight = 40;
            Endurance = 45;
            Dodge = 5;
            
            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
            string ddd = this.GetType().FullName;
        }
        
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);

            

            delay++;

            Agent rndAgent = null;
            rndAgent = agents[rnd.Next(agents.Count)];

            if (rndAgent != null && rndAgent.GetType() == typeof(MinAgent) && ProcreationCountDown == 0)
            {
                return new Procreate(rndAgent);
            }

            if (plants.Count > 0 && Hunger > 20f)
            {
                if (closePlant == null)
                {
                    closePlant = (Plant)plants[rnd.Next(plants.Count)];
                }

                if (AIVector.Distance(Position, closePlant.Position) > AIModifiers.maxFeedingRange)
                {
                    moveX = closePlant.Position.Normalize().X - Position.Normalize().X;
                    moveY = closePlant.Position.Normalize().Y - Position.Normalize().Y;
                    return new Move(new AIVector(moveX, moveY));
                }
                else
                {
                    return new Feed(closePlant);
                }
            }
            else if (Hunger > 20f && delay > 1000)
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
