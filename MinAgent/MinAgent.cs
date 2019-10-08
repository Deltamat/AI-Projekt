﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using MinAgent.States;

namespace MinAgent
{
    public class MinAgent : Agent
    {
        Random rnd;
        int lastUpdateHealth;
        bool underAttack = false; //gammel bool
        State currentState = new StateMoveToCenter();
        public static Rectangle window = Application.OpenForms[0].Bounds;

        //Only for randomization of movement
        public float moveX = 0;
        public float moveY = 0;
        public int delay;
        public Plant targetPlant;
        public List<IEntity> plants;
        public List<Agent> closeEnemyAgents;
        public List<Agent> alliedAgents;

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
        }
        
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            plants = otherEntities.FindAll(a => a is Plant);
            plants.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            
            
            //foreach (var plant in plants.OrderBy(c => AIVector.Distance(Position, c.Position))) ;
            
            //Checks if any non-allied agents are nearby and puts them in a list
            closeEnemyAgents = agents.FindAll(a => !(a is MinAgent));
            closeEnemyAgents.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            //Checks if allied agents are nearby and puts them in a list
            alliedAgents = agents.FindAll(a => a is MinAgent && a != this);
            alliedAgents.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            
            delay++;
            
            
            //Agent rndAgent = null;
            //rndAgent = agents[rnd.Next(agents.Count)];

            if (ProcreationCountDown == 0 && alliedAgents.Count == 0)
            {
                currentState = new StateMoveToCenter();
            }
            else if (ProcreationCountDown == 0)
            {
                currentState = new StateProcreate();
            }

            foreach (var enemy in closeEnemyAgents)
            {
                if (AIVector.Distance(Position, enemy.Position) <= AIModifiers.maxMeleeAttackRange * 2)
                {
                    underAttack = true;
                    currentState = new StateFlee();
                    currentState.Execute(this);
                }
            }

            if (underAttack && delay < 100)
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);
                delay = 0;
                underAttack = false;
            }
            //if (lastUpdateHealth > Health && Hunger < AIModifiers.maxHungerBeforeHitpointsDamage)
            //{
            //    underAttack = true;
            //}
            //Mangler gameTime for at det kan virke ordenligt
            //else if (lastUpdateHealth > Health + AIModifiers.hungerHitpointsDamagePerSecond && Hunger > AIModifiers.maxHungerBeforeHitpointsDamage)
            //{
            //    underAttack = true;
            //}
            //else
            //{
            //    underAttack = false;
            //}
            
            lastUpdateHealth = Health;

            if ((Position.X < Eyesight - 10 ||
                Position.X + Eyesight - 10 > window.Width ||
                Position.Y < Eyesight ||
                Position.Y + Eyesight - 10 > window.Height - Eyesight - 10) &&
                delay > 60 && plants.Count == 0)
            {
                currentState = new StateReverseMove();
            }
            else if (Hunger > 20f)
            {
                currentState = new StateFeed();
            }
            else if (plants.Count == 0 && Hunger > 20f && delay > 300) //choose new direction if there are no nearby plants
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);

                delay = 0;
            }

            //Stops agents from standing still
            if (moveX == 0 && moveY == 0)
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);
            }
            

            return currentState.Execute(this);
        }
           
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account yet
        }
    }
}
