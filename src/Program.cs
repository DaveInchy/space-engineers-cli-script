using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        List<Scheduler> Task = new List<Scheduler>();

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;

            try
            {
                this.Blocks = new Grid(GridTerminalSystem as IMyGridTerminalSystem);

                var id = 0;
                LightController Lights = new LightController(this.Blocks);
                Task.Insert(id, new Scheduler(this, Lights.Sequence(), true));
                this.Controllers.Add(new Controller<Object>(controller: Lights, execute: Task.ToArray()[id].Run));

                id++;
                DoorController Doors = new DoorController(this.Blocks);
                Task.Insert(id, new Scheduler(this, Doors.Sequence(), true));
                this.Controllers.Add(new Controller<Object>(controller: Doors, execute: Task.ToArray()[id].Run));
            }
            catch (System.Exception e)
            {
                string msg = $"\nError: Caught Exception:\n > {e}";
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
                    Commands["/lcd"] = LCDState;
                    Commands["/door"] = DoorState;
                    Commands["/light"] = LightState;
                    Commands["/block"] = BlockState;

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
                    for (int n = 0; n < args.Count(); n++)
                    {
                        cmd += $"{args[n]} ";
                    }
                    var compute = (Runtime.CurrentInstructionCount / Runtime.MaxInstructionCount) * 100;
                    Echo(
                          $"--[CommandLineActions]--"
                        + $"\nA CLI for ScriptKids"
                        + $"\n\nPERFORMANCE STATS:"
                        + $"\n----------------------"
                        + $"\nTICK :: {UpdateCounter}x"
                        + $"\nLOAD :: {compute}%"
                        + $"\n----------------------"
                        + $"\nCOMMAND LINE ARG :: {cmd}"
                        + $"\nCOMMAND ARGS NUM :: {args.Count()}x"
                        + $"\nCOMMAND EXEC NUM :: {ExecutionCounter}x"
                        + $"\n\nLOG:\n\t{errLog}"
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
                string msg = $"\nError: Caught Exception => {e}";
                logError(msg);
            }

            foreach (Scheduler task in Task)
            {
                if (!task.Running) task.Start();
            }

            UpdateCounter++;

            return;
        }
    }
}
