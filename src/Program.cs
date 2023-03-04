using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        GridBlocks Blocks;

        const UpdateType CommandUpdate = UpdateType.Trigger | UpdateType.Terminal;
        const UpdateType BlockUpdate = UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100 | UpdateType.Trigger;

        int UpdateCounter = 0;
        int ExecutionCounter = 0;

        MyCommandLine CommandLine = new MyCommandLine();
        Dictionary<string, Action> Commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
        List<string> args = new List<string>();

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;

            Blocks = new GridBlocks();
            this.InitGridBlocks(Blocks);
        }

        public void Save()
        {
            string msg = "Saving is not Implemented";

            Echo(msg);
            throw new Exception(msg);
        }

        public void Main(string argument, UpdateType updateSource)
        {
            try
            {
                // Check what kinda execution is ran.
                if ((updateSource & CommandUpdate) != 0)
                {

                    // Prepare the command execution
                    Action init;

                    // Register command execution methods
                    Commands["/help"] = Help;
                    Commands["/lcd"] = TextPanel;

                    if (CommandLine.TryParse(argument))
                    {

                        string initiator = CommandLine.Argument(0);

                        // Store current Argument list
                        args = new List<string>();
                        args.AddRange(CommandLine.Items);

                        if (initiator == null)
                        {
                            Echo("[CommandLineActions]\nError: No command specified");
                        }
                        else if (Commands.TryGetValue(initiator, out init))
                        {
                            init();
                            ExecutionCounter++;
                        }
                        else
                        {
                            Echo($"[CommandLineActions]\nError: Unknown command {initiator}");
                        }
                    }
                }
                else if ((updateSource & BlockUpdate) != 0)
                {
                    string cmd = "";
                    for (int n=0; n <= args.Count(); n++)
                    {
                        cmd += $"{args[n]} ";
                    }
                    Echo(
                          $"[CommandLineActions] Statistics:"
                        + $"\nUpdated {UpdateCounter.ToString()} times"
                        + $"\n----------------------"
                        + $"\nExecuted Commands {ExecutionCounter.ToString()} times"
                        + $"\nRecent Command:\n\t=> {cmd}"
                        + $"\nAmount Args: {args.Count()}"
                    );
                }
            }
            catch (Exception e)
            {
                Echo($"[CommandLineActions]\nError: Caught Exception => {e.ToString()}");
            }

            UpdateCounter++;

            return;
        }
    }
}
