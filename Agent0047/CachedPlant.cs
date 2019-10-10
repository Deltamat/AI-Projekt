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
    class CachedPlant : Plant
    {
        public AIVector CachedPos { get; private set; }
        public Guid CachedId { get; private set; }

        public CachedPlant(Plant plant)
        {
            CachedId = plant.Id;
            UpdateInformation(plant);
        }

        public void UpdateInformation(Plant plant)
        {
            CachedPos = plant.Position;
        }
    }
}
