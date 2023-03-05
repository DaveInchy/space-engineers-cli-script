using System;
using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{

    partial class Program
    {

        GridBlocks Blocks;

        const UpdateType CommandUpdate = UpdateType.Trigger | UpdateType.Terminal;
        const UpdateType BlockUpdate = UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100 | UpdateType.Trigger;

        int UpdateCounter = 0;
        int ExecutionCounter = 0;

        MyCommandLine CommandLine = new MyCommandLine();
        Dictionary<string, Action> Commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
        List<string> args = new List<string>();

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
