using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class DoorController
        {
            private GridBlocks Blocks;

            public DoorController(GridBlocks Blocks)
            {

                try
                {
                    this.Blocks = Blocks;
                }
                catch (System.Exception e)
                {
                    string msg = $"\nError: Caught Exception:\n > {e.ToString()}";
                }

                return;
            }

            public void execute()
            {
                // Close Doors after a few ticks
                IMyDoor[] DoorsNormal = this.Blocks.Doors.Normal.ToArray();
                for (int n = 0; n < DoorsNormal.Length; n++)
                {
                    if (DoorsNormal[n] != null && DoorsNormal[n].Status.Equals(DoorStatus.Open))
                    {
                        DoorsNormal[n].ToggleDoor();
                    }
                }

                IMyAirtightSlideDoor[] DoorsSlider = this.Blocks.Doors.Sliding.ToArray();
                for (int n = 0; n < DoorsSlider.Length; n++)
                {
                    if (DoorsSlider[n] != null && DoorsSlider[n].Status.Equals(DoorStatus.Open))
                    {
                        DoorsSlider[n].ToggleDoor();
                    }
                }
                return;
            }
        }
    }
}
