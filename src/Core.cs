using System;
using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{

    partial class Program
    {
        Grid Blocks;

        const UpdateType CommandUpdate = UpdateType.Trigger | UpdateType.Terminal;
        const UpdateType BlockUpdate = UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100 | UpdateType.Trigger;

        int UpdateCounter = 0;
        int ExecutionCounter = 0;

        List<Controller<Object>> Controllers = new List<Controller<Object>>();
        MyCommandLine CommandLine = new MyCommandLine();
        Dictionary<string, Action> Commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
        List<string> args = new List<string>();

        public string errLog = "";

        public static Random Rand = new Random();

    }
}
