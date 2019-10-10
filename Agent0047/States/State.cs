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
    public abstract class State
    {
        public abstract IAction Execute(Agent0047 agent);
    }
}
