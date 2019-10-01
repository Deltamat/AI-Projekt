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


        //Only for randomization of movement
        float moveX = 0;
        float moveY = 0;

        public MinAgent(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 20;
            Strength = 50;
            Health = 100;
            Eyesight = 40;
            Endurance = 0;
            Dodge = 40;


            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
        }



        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);
            //Checks if any non-allied agents are nearby and puts them in a list
            var closeEnemyAgents = agents.FindAll(a => !(a is MinAgent));
            //Checks if allied agents are nearby and puts them in a list
            var alliedAgents = agents.FindAll(a => a is MinAgent);

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
            return new Move(new AIVector(moveX, moveY));
            
        }



        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
        }
    }
}
