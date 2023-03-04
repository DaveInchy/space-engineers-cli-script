using System;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

namespace IngameScript
{

    partial class Program
    {

        public void InitGridBlocks(GridBlocks Blocks)
        {
            try
            {
                GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>(Blocks.Lights, Light => Light != null);
                GridTerminalSystem.GetBlocksOfType<IMyButtonPanel>(Blocks.Buttons, Button => Button != null);
                GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(Blocks.Sensors, Sensor => Sensor != null);
                GridTerminalSystem.GetBlocksOfType<IMyTimerBlock>(Blocks.Timers, Timer => Timer != null);
                GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(Blocks.LCDs, LCD => LCD != null);
            }
            catch (Exception e)
            {
                Echo($"[CommandLineActions]\nError: Caught Exception:\n > {e.ToString()}");
            }

            return;
        }
        
    }
}
