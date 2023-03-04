using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;

namespace IngameScript
{
    partial class Program
    {

        public class GridBlocks
        {
            public List<IMyUserControllableGun> Guns = new List<IMyUserControllableGun>();
            public List<IMyLightingBlock> Lights = new List<IMyLightingBlock>();
            public List<IMyButtonPanel> Buttons = new List<IMyButtonPanel>();
            public List<IMySensorBlock> Sensors = new List<IMySensorBlock>();
            public List<IMyTimerBlock> Timers = new List<IMyTimerBlock>();
            public List<IMyTextPanel> LCDs = new List<IMyTextPanel>();
        }

    }
}
