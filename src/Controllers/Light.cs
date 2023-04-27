using Microsoft.JScript;
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

/* ---------------------------------------------------------------------------------------------------------- *
 * Lighting Controller :: Runs Sequence that makes some lights flicker to make your base / ship more lively,
 * altho this indicates theres phantom forces going on. (not really)
 * ---------------------------------------------------------------------------------------------------------- *
 * @TODO
 * - Make a way of randomizing a certain light sequence to make it more dynamic and realistic
 * - Get the timing right
 * ---------------------------------------------------------------------------------------------------------- */

namespace IngameScript
{
    partial class Program
    {

        public class LightController
        {
            private Grid Blocks;

            public LightController(Grid Blocks)
            {
                this.Blocks = Blocks;
                return;
            }

            public int Count { get; private set; }

            public IEnumerable<double> Sequence()
            {
                float chanceRange = (float)0.005; // 0.15 becomes 15% see one line below
                int chancePercentage = (int)((float)chanceRange * (float)100); // 0.15 * 100 = 15%

                IMyLightingBlock[] Lights = this.Blocks.Lights.All.ToArray();

                for (int n = 0; n < Lights.Length; n++)
                { 
                    int chanceRandom = Rand.Next(100);
                    if (chanceRandom <= chancePercentage)
                    {

                        if (Lights[n].Closed) break;
                        Lights[n].Falloff = (float)0.5;
                        Lights[n].Enabled = true;
                        yield return 1;
                        Lights[n].Falloff = (float)1;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)1.5;
                        Lights[n].Enabled = true;
                        yield return 0.6;
                        Lights[n].Falloff = (float)2;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)2.5;
                        Lights[n].Enabled = true;
                        yield return 1;
                        Lights[n].Falloff = (float)0.5;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)1;
                        Lights[n].Enabled = true;
                        yield return 0.2;
                        Lights[n].Falloff = (float)1.5;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)2;
                        Lights[n].Enabled = true;
                        yield return 1;
                        Lights[n].Falloff = (float)2.5;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)1.5;
                        Lights[n].Enabled = true;
                        yield return 3;
                        Lights[n].Falloff = (float)0.5;
                        Lights[n].Enabled = false;
                        yield return 0.1;
                        Lights[n].Falloff = (float)1;
                        Lights[n].Enabled = true;
                        yield return 1;
                    }
                }

                yield return 1;
            }
        }
    }
}
