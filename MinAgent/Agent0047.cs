using System;
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
    public class Agent0047 : Agent
    {
        Random rnd;
        int lastUpdateHealth;
        bool underAttack = false; //gammel bool
        public State currentState = new StateMoveToCenter();
        public static Rectangle window = Application.OpenForms[0].Bounds;
        double deltaTime;
        double prevTime;
        public int maxHealth;

        //Only for randomization of movement
        public float moveX = 0;
        public float moveY = 0;

        public int delay;
        public Plant targetPlant;
        public List<IEntity> plants;
        public List<Agent> closeEnemyAgents;
        public List<Agent> alliedAgents;

        public Agent0047(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 50;
            Strength = 80;
            Health = 40;
            Eyesight = 50;
            Endurance = 30;
            Dodge = 0;

            maxHealth = Health;
            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
        }
        
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            deltaTime = DateTime.Now.TimeOfDay.TotalMilliseconds - prevTime;

            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            plants = otherEntities.FindAll(a => a is Plant);
            plants.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            
            //Checks if any non-allied agents are nearby and puts them in a list
            closeEnemyAgents = agents.FindAll(a => !(a is Agent0047));
            closeEnemyAgents.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            //Checks if allied agents are nearby and puts them in a list
            alliedAgents = agents.FindAll(a => a is Agent0047 && a != this);
            alliedAgents.Sort((x, y) => AIVector.Distance(Position, x.Position).CompareTo(AIVector.Distance(Position, y.Position)));
            
            delay++;

            foreach (var item in alliedAgents)
            {
                Agent0047 agent = (Agent0047)item;
                if (agent.currentState is StateAttack)
                {
                    // move towards the friendly and assists with the fight
                }
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

            if (underAttack && closeEnemyAgents.Count == 0)
            {
                moveX = rnd.Next(-1, 2);
                moveY = rnd.Next(-1, 2);
                delay = 0;
                underAttack = false;
            }


            lastUpdateHealth = Health;

           
            if (ProcreationCountDown == 0 && alliedAgents.Count == 0)
            {
                currentState = new StateMoveToCenter();
            }
            else if (ProcreationCountDown == 0)
            {
                currentState = new StateProcreate();
            }

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
            //If either in melee attack range of an enemy agent or hunger below 40 while it sees and enemy agent, the agent will attack/move closer.
            if (closeEnemyAgents.Count > 0 && (closeEnemyAgents[0].Strength < Strength && Hunger < 40 || AIVector.Distance(this.Position, closeEnemyAgents[0].Position) <= AIModifiers.maxMeleeAttackRange))
            {
                currentState = new StateAttack();
            }
            //Stops agents from standing still
            if (moveX == 0 && moveY == 0)
            {
                moveX = rnd.Next(-100, 101)*0.01f;
                moveY = rnd.Next(-100, 101)*0.01f;
            }

            prevTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
            return currentState.Execute(this);
        }
           
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account yet
        }
    }
}