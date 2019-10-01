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

        public MinAgent(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 20;
            Strength = 50;
            Health = 100;
            Eyesight = 30;
            Endurance = 0;
            Dodge = 50;


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

            Agent rndAgent = null;
            rndAgent = agents[rnd.Next(agents.Count)];

            if (rndAgent != null && rndAgent.GetType() == typeof(MinAgent) && ProcreationCountDown == 0)
            {
                return new Procreate(rndAgent);
            }

            if (plants.Count > 0 && Hunger > 50f)
            {
                return new Feed((Plant)plants[rnd.Next(plants.Count)]);
            }

            return new Move(new AIVector(moveX, moveY));

        }



        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
        }
    }
}
