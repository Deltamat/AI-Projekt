using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using System.Diagnostics;

namespace ExampleAI
{
    public class RandomAgent : Agent
    {
        Random rnd;
        public List<Agent> closeEnemyAgents;
        //Only for randomization of movement
        float moveX = 0;
        float moveY = 0;

        public RandomAgent(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 30;
            Strength= 50;
            Health= 60;
            Eyesight= 70;
            Endurance= 40;
            Dodge= 0;


            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
            string ddd = this.GetType().FullName;
        }



        public override IAction GetNextAction(List<IEntity> otherEntities)
        {


            List<Agent> agents = otherEntities.FindAll(a=>a is Agent).ConvertAll<Agent>(a=>(Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a=>a is Plant);
            closeEnemyAgents = agents.FindAll(a => !(a is RandomAgent));
            closeEnemyAgents.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));

            Agent rndAgent = null;
            rndAgent = agents[rnd.Next(agents.Count)];

            //if (closeEnemyAgents.Count > 0 && AIVector.Distance(Position, closeEnemyAgents[0].Position) <= AIModifiers.maxMeleeAttackRange)
            //{
            //    return new Attack(closeEnemyAgents[0]);
            //}
            if (closeEnemyAgents.Count > 0)
            {
                if (AIVector.Distance(Position, closeEnemyAgents[0].Position) > AIModifiers.maxMeleeAttackRange)
                {
                    var vectorToEnemyAgentPosition = closeEnemyAgents[0].Position - Position;
                    return new Move(vectorToEnemyAgentPosition);
                }
                else //attack
                {
                    return new Attack(closeEnemyAgents[0]);
                }
            }
            

            switch (rnd.Next(5))
            {
                case 1: //Procreate
                    if (rndAgent != null && rndAgent.GetType() == typeof(RandomAgent))
                    {
                        return new Procreate(rndAgent);
                    }
                    break;

                case 2: //Attack Melee
                    if (rndAgent != null && rndAgent.GetType() != typeof(RandomAgent))
                    {
                        return new Attack(rndAgent);
                    }
                    break;
                case 3: //Feed
                    if (plants.Count > 0)
                    {
                        return new Feed((Plant)plants[rnd.Next(plants.Count)]);
                    }
                    break;
                case 4: //Move
                    return new Move(new AIVector(moveX, moveY));
                default:
                    return new Defend();
            }

            return new Move(new AIVector(moveX, moveY));

        }


        
        public override void ActionResultCallback(bool success)
        {
           //Do nothing - AI dont take success of an action into account
        }
    }
}
