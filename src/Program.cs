﻿using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
            this.Blocks = new GridBlocks(GridTerminalSystem as IMyGridTerminalSystem);
            this.InitGridBlocks(this.Blocks);
        }

        public void Save()
        {
            string msg = "\nError: Saving is not Implemented";
            errLog += msg;

            Echo(msg);
            // throw new Exception(msg);
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
                            errLog += msg;
                            Echo(msg);
                        }
                        else if (Commands.TryGetValue(initiator, out init))
                        {
                            init();
                            ExecutionCounter++;
                        }
                        else
                        {
                            string msg = $"\nError: Unknown command {initiator}";
                            errLog += msg;
                            Echo(msg);
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
                    Echo(
                          $"[CommandLineActions] Statistics:"
                        + $"\nUpdated {UpdateCounter.ToString()} times"
                        + $"\n----------------------"
                        + $"\nExecuted Command {ExecutionCounter.ToString()} times"
                        + $"\nNum. Arguments: {args.Count()}"
                        + $"\nLast Command: \n\t{cmd}"
                        + $"\n\nLOG:\n[CommandLineActions]{errLog}"
                    );
                }

                List<Controller<Object, Action>> controllers = this.Controllers;

                // Handle Controllers

                this.Controllers = controllers;

                for (int n = 0; n < controllers.ToArray().Length; n++)
                {
                    controllers.ToArray()[n].execute();
                }

            }
            catch (System.Exception e)
            {
                string msg = $"\nError: Caught Exception => {e.ToString()}";
                errLog += msg;
                Echo(msg);
            }

            UpdateCounter++;

            return;
        }
    }
}
