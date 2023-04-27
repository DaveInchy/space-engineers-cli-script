using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        Scheduler Task;
        Scheduler Task2;

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;

            try
            {
                this.Blocks = new Grid(GridTerminalSystem as IMyGridTerminalSystem);

                DoorController Doors = new DoorController(this.Blocks);
                LightController Lights = new LightController(this.Blocks);

                Task = new Scheduler(this, Lights.Sequence(), true);
                Task2 = new Scheduler(this, Doors.Sequence(), true);

                this.Controllers.Add(new Controller<Object>(controller: Doors, execute: Task2.Run));
                this.Controllers.Add(new Controller<Object>(controller: Lights, execute: Task.Run));
            }
            catch (System.Exception e)
            {
                string msg = $"\nError: Caught Exception:\n > {e.ToString()}";
                logError(msg);
            }
        }

        public void Save()
        {
            string msg = "\nError: Saving is not Implemented";
            logError(msg);
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
                            string msg = "\nError: No command specified";
                            logError(msg);
                        }
                        else if (Commands.TryGetValue(initiator, out init))
                        {
                            init();
                            ExecutionCounter++;
                        }
                        else
                        {
                            string msg = $"\nError: Unknown command {initiator}";
                            logError(msg);
                        }
                    }
                }
                else if ((updateSource & BlockUpdate) != 0)
                { 
                    string cmd = "";
                    for (int n=0; n < args.Count(); n++)
                    {
                        cmd += $"{args[n]} ";
                    }
                    var compute = (Runtime.CurrentInstructionCount / Runtime.MaxInstructionCount) * 100;
                    Echo(
                          $"[CommandLineActions] Statistics:"
                        + $"\nITER :: {UpdateCounter.ToString()}x"
                        + $"\nLOAD :: {compute}%"
                        + $"\n----------------------"
                        + $"\nNUM COMMANDS :: {ExecutionCounter.ToString()}x"
                        + $"\nCOMMAND ARGS :: {args.Count()}x"
                        + $"\nLAST COMMAND :: '{cmd}'"
                        + $"\n\nLOG:\n[CommandLineActions]\t{errLog}"
                    );
                }

                List<Controller<Object>> controllers = this.Controllers;

                // ----------- Manipulate Controller Metadata ----------- //

                for (int n = 0; n < controllers.ToArray().Length; n++)
                {
                    // Run
                    controllers.ToArray()[n].Run();
                }

                // ----------- Manipulate Controller Metadata ----------- //

                this.Controllers = controllers;

            }
            catch (System.Exception e)
            {
                string msg = $"\nError: Caught Exception => {e.ToString()}";
                logError(msg);
            }

            if (!Task.Running) Task.Start();
            UpdateCounter++;

            return;
        }
    }
}
