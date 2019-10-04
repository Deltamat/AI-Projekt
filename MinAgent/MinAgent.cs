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
        int lastUpdateHealth;
        bool underAttack = false;
        State currentState = new StateFeed();

        //Only for randomization of movement
        public float moveX = 0;
        public float moveY = 0;
        public int delay;
        public Plant targetPlant;
        public List<IEntity> plants;

        public MinAgent(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 0;
            Strength = 0;
            Health = 10;
            Eyesight = 190;
            Endurance = 50;
            Dodge = 0;


            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
        }
        
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            plants = otherEntities.FindAll(a => a is Plant);
            plants.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            //Checks if any non-allied agents are nearby and puts them in a list
            var closeEnemyAgents = agents.FindAll(a => !(a is MinAgent));
            //Checks if allied agents are nearby and puts them in a list
            var alliedAgents = agents.FindAll(a => a is MinAgent);

            delay++;

            //Agent rndAgent = null;
            //rndAgent = agents[rnd.Next(agents.Count)];

            //if (rndAgent != null && rndAgent.GetType() == typeof(MinAgent) && ProcreationCountDown == 0)
            //{
            //    return new Procreate(rndAgent);
            //}
            foreach (var item in closeEnemyAgents)
            {
                if (AIVector.Distance(Position, item.Position) <= AIModifiers.maxMeleeAttackRange)
                {
                    underAttack = true;
                }
            }
            //if (lastUpdateHealth > Health /*&& Hunger < AIModifiers.maxHungerBeforeHitpointsDamage*/)
            //{
            //    underAttack = true;
            //}
            ////Mangler gameTime for at det kan virke ordenligt
            ////else if (lastUpdateHealth > Health + AIModifiers.hungerHitpointsDamagePerSecond && Hunger > AIModifiers.maxHungerBeforeHitpointsDamage)
            ////{
            ////    underAttack = true;
            ////}
            //else
            //{
            //    underAttack = false;
            //}
            
            lastUpdateHealth = Health;

            if (plants.Count > 0 && Hunger > 20f)
            {
                currentState = new StateFeed();
            }
            else if (plants.Count == 0 && Hunger > 20f && delay > 300) //choose new direction if there are no nearby plants
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);

                delay = 0;
            }

            //targetPlant = null;
            
            return currentState.Execute(this);
        }
           
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
        }
    }
}
