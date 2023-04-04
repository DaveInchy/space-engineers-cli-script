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
        List<Controller<Object, Action>> Controllers = new List<Controller<Object, Action>>();

        const UpdateType CommandUpdate = UpdateType.Trigger | UpdateType.Terminal;
        const UpdateType BlockUpdate = UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100 | UpdateType.Trigger;

        int UpdateCounter = 0;
        int ExecutionCounter = 0;

        MyCommandLine CommandLine = new MyCommandLine();
        Dictionary<string, Action> Commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
        List<string> args = new List<string>();

        public string errLog = "";

        public void InitGridBlocks(GridBlocks Blocks)
        {
            try
            {
                var Door = new DoorController(Blocks);

                var ControllerClass = new DoorController(this.Blocks);
                var DoorController = new Controller<Object, Action>(ControllerClass, ControllerClass.execute);
                this.Controllers.Add(DoorController);
            }
            catch (System.Exception e)
            {
                string msg = $"\nError: Caught Exception:\n > {e.ToString()}";
                errLog += msg;
                Echo(msg);
            }

            return;
        }
        
    }
}
