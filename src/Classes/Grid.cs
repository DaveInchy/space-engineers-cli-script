using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;

namespace IngameScript
{
    partial class Program
    {

        public class Grid : GridBlocks
        {
            public List<IMyButtonPanel> Buttons = new List<IMyButtonPanel>();
            public List<IMySensorBlock> Sensors = new List<IMySensorBlock>();
            public List<IMyTimerBlock> Timers = new List<IMyTimerBlock>();
            public List<IMyTextPanel> LCDs = new List<IMyTextPanel>();

            public Grid(IMyGridTerminalSystem Grid)
            {
                this.Grid = Grid;
                if (this.Grid == null) return;

                // Functional
                Grid.GetBlocksOfType<IMyButtonPanel>(this.Buttons);
                Grid.GetBlocksOfType<IMySensorBlock>(this.Sensors);
                Grid.GetBlocksOfType<IMyTimerBlock>(this.Timers);
                Grid.GetBlocksOfType<IMyTextPanel>(this.LCDs);

                // Doors
                this.Doors = new DoorBlocks();

                Grid.GetBlocksOfType<IMyDoor>(this.Doors.All);
                Grid.GetBlocksOfType<IMyAdvancedDoor>(this.Doors.Advanced);
                Grid.GetBlocksOfType<IMyAirtightDoorBase>(this.Doors.AirTight);
                Grid.GetBlocksOfType<IMyAirtightSlideDoor>(this.Doors.Sliding);
                Grid.GetBlocksOfType<IMyAirtightHangarDoor>(this.Doors.Hangar);

                // Lights
                this.Lights = new LightBlocks();

                Grid.GetBlocksOfType<IMyLightingBlock>(this.Lights.All);
                Grid.GetBlocksOfType<IMyInteriorLight>(this.Lights.Interior);
                Grid.GetBlocksOfType<IMyReflectorLight>(this.Lights.Spot);

                return;
            }

        }

        public class GridBlocks
        {

            public IMyGridTerminalSystem Grid;
            public DoorBlocks Doors;
            public LightBlocks Lights;

            public class LightBlocks
            {
                public List<IMyLightingBlock> All = new List<IMyLightingBlock>();
                public List<IMyInteriorLight> Interior = new List<IMyInteriorLight>();
                public List<IMyReflectorLight> Spot = new List<IMyReflectorLight>();
            }

            public class DoorBlocks
            {
                public List<IMyDoor> All = new List<IMyDoor>();
                public List<IMyAdvancedDoor> Advanced = new List<IMyAdvancedDoor>();
                public List<IMyAirtightDoorBase> AirTight = new List<IMyAirtightDoorBase>();
                public List<IMyAirtightSlideDoor> Sliding = new List<IMyAirtightSlideDoor>();
                public List<IMyAirtightHangarDoor> Hangar = new List<IMyAirtightHangarDoor>();
            }

        }

    }
}
