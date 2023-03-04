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
        private void Help()
        {
            Echo($"[CommandLineActions] use '/help [<SubCommand>]'."
                + $"\n"
                + $"\nSubCommand's:"
                + $"\n----------------------"
                + $"\n/help => Get help for each command or in general."
                + $"\n/lcd => Change LCD states, Using pre-programmed sprites and text / info."
                + $"\n----------------------"
                + $"\n"
                + $"\nPage: 1 of 1 | use '/help [<Index>]'"
            );
        }
    }
}
