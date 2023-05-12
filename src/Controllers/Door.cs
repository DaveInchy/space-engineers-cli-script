using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;

/* ---------------------------------------------------------------------------------------------------------- *
 * Door Controller :: Runs Sequence that closes each door on the grid after 2.5-3 seconds. 
 * if you want to exclude a door to remove this effect use [exclude] in the block title name thingy.
 * ---------------------------------------------------------------------------------------------------------- *
 * @TODO
 * - Create command actions for doors.
 * ---------------------------------------------------------------------------------------------------------- */

namespace IngameScript
{
    partial class Program
    {
        public class DoorController
        {
            private Grid Blocks;

            public DoorController(Grid Blocks)
            {
                this.Blocks = Blocks;
                return;
            }

            // This is a looped method
            public IEnumerable<double> Sequence()
            {
                // Automatically close doors after a few ticks
                yield return 2.5;
                closeDoors();
                yield return 0;
            }

            private void closeDoors()
            {
                IMyDoor[] Doors = this.Blocks.Doors.All.ToArray();
                for (int n = 0; n < Doors.Length; n++)
                {
                    if (Doors[n] != null)
                    {

                        if (Doors[n].Status == DoorStatus.Open && !Doors[n].CustomName.Contains("[exclude]"))
                        {
                            Doors[n].ApplyAction("Open_Off");
                        }
                    }
                }
            }
        }
    }
}
