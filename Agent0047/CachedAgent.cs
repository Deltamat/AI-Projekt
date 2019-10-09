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
    public class CachedAgent : Agent
    {
        public Type OriginalType { get; private set; }
        public AIVector CachedPos { get; private set; }
        public Guid CachedId { get; private set; }

        public CachedAgent(IPropertyStorage propertyStorage, Agent agent) : base(propertyStorage)
        {
            CachedId = agent.Id; // Sets the id so that if you meet the agent again you can recognise it
            OriginalType = agent.GetType(); // Saves the type to be able to tell friend from foe
            UpdateInformation(agent); // Updates the stats to match the Agent
        }

        public override void ActionResultCallback(bool success)
        {
            throw new NotImplementedException();
        }

        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the information of a CachedAgent
        /// </summary>
        /// <param name="agent">The real agent that the CachedAgent is a mirror of</param>
        public void UpdateInformation(Agent agent)
        {
            // Stats
            MovementSpeed = agent.MovementSpeed;
            Strength = agent.Strength;
            Health = agent.Health;
            Eyesight = agent.Eyesight;
            Endurance = agent.Endurance;
            Dodge = agent.Dodge;

            // Position
            CachedPos = agent.Position;
        }
    }
}
