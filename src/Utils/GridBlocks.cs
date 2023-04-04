using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;

namespace IngameScript
{
    partial class Program
    {

        public class GridBlocks
        {
            private IMyGridTerminalSystem Grid;

            public List<IMyLightingBlock> Lights = new List<IMyLightingBlock>();
            public List<IMyButtonPanel> Buttons = new List<IMyButtonPanel>();
            public List<IMySensorBlock> Sensors = new List<IMySensorBlock>();
            public List<IMyTimerBlock> Timers = new List<IMyTimerBlock>();
            public List<IMyTextPanel> LCDs = new List<IMyTextPanel>();

            public DoorBlocks Doors;

            public GridBlocks(IMyGridTerminalSystem Grid)
            {
                this.Grid = Grid;
                if (this.Grid == null) return;

                // Search the grid for blocks

                // Functional
                Grid.GetBlocksOfType<IMyLightingBlock>(this.Lights);
                Grid.GetBlocksOfType<IMyButtonPanel>(this.Buttons);
                Grid.GetBlocksOfType<IMySensorBlock>(this.Sensors);
                Grid.GetBlocksOfType<IMyTimerBlock>(this.Timers);
                Grid.GetBlocksOfType<IMyTextPanel>(this.LCDs);

                // Doors
                this.Doors = new DoorBlocks();

                Grid.GetBlocksOfType<IMyDoor>(this.Doors.Normal);
                Grid.GetBlocksOfType<IMyAdvancedDoor>(this.Doors.Advanced);
                Grid.GetBlocksOfType<IMyAirtightDoorBase>(this.Doors.AirTight);
                Grid.GetBlocksOfType<IMyAirtightSlideDoor>(this.Doors.Sliding);
                Grid.GetBlocksOfType<IMyAirtightHangarDoor>(this.Doors.Hangar);

                return;
            }

            public class DoorBlocks
            {
                public List<IMyDoor> Normal = new List<IMyDoor>();
                public List<IMyAdvancedDoor> Advanced = new List<IMyAdvancedDoor>();
                public List<IMyAirtightDoorBase> AirTight = new List<IMyAirtightDoorBase>();
                public List<IMyAirtightSlideDoor> Sliding = new List<IMyAirtightSlideDoor>();
                public List<IMyAirtightHangarDoor> Hangar = new List<IMyAirtightHangarDoor>();
            }

        }

    }
}
