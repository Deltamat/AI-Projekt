using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Entities;
using System.Diagnostics;
using System.Reflection;

namespace MinAgent
{
    public class Agent0047Factory : AgentFactory
    {
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new Agent0047(propertyStorage);
        }

        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            return new Agent0047(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(Agent0047); }
        }

        public override string Creators
        {
            get { return "Gucci gang"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}
