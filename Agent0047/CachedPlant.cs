using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Agent0047
{
    public class CachedPlant : Plant
    {
        public AIVector CachedPos { get; private set; }
        public Guid CachedId { get; private set; }

        public CachedPlant(IEntity plant)
        {
            CachedId = plant.Id;
            UpdateInformation(plant);
        }

        public void UpdateInformation(IEntity plant)
        {
            CachedPos = plant.Position;
        }
    }
}
