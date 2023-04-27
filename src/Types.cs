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
        internal struct LightingAnimation
        {
            public IMyLightingBlock block { get; set; }
            public int progress { get; set; }
            public int progressMax { get; set; }
            public int progressMin { get; set; }
            public int progressNum { get; set; }
            public int animationId { get; set; } // 0 = None, 1 = Danger, 2 = Warning, 3 = Flicker
            public string status { get; set; } // "enabled" => "active" | "finished"
        };
    }
}
