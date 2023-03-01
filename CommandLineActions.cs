using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace CommandLineActions
{

    partial class Program : MyGridProgram
    {
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project,
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
        //
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.

        class BlockLists
        {
            public List<IMySensorBlock> Sensors = new List<IMySensorBlock>();
            public List<IMyTimerBlock> Timers = new List<IMyTimerBlock>();
            public List<IMyTextPanel> LCDs = new List<IMyTextPanel>();
        }

        BlockLists Blocks;
        List<IMyUserControllableGun> Guns = new List<IMyUserControllableGun>();

        const UpdateType CommandUpdate = UpdateType.Trigger | UpdateType.Terminal;
        const UpdateType BlockUpdate = UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100 | UpdateType.Trigger;
        List<string> args = new List<string>();

        MyCommandLine CommandLine = new MyCommandLine();
        Dictionary<string, Action> Commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }

        public void Save()
        {
            // Called when the program needs to save its state.
        }

        public void Main(string argument, UpdateType updateSource)
        {
            try
            {
                Blocks = new BlockLists();
                InitBlocks();

                List<IMySensorBlock> sensorBlocks = Blocks.Sensors;
                List<IMyTimerBlock> timerBlocks = Blocks.Timers;
                List<IMyTextPanel> textPanels = Blocks.LCDs;

                if ((updateSource & CommandUpdate) != 0)
                {

                    // Prepare the command execution
                    Action init;

                    // Register command execution methods
                    Commands["/help"] = Help;
                    Commands["/lcd"] = LCD;

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
                        }
                        else
                        {
                            Echo($"[CommandLineActions]\nError: Unknown command {initiator}");
                        }
                    }
                } else if ((updateSource & BlockUpdate) != 0)
                {
                    return;
                }
            }
            catch (System.Exception e)
            {
                Echo($"[CommandLineActions]\nError: Caught Exception => {e.ToString()}");
            }
        }

        private void Help()
        {
            Echo($"[CommandLineActions] use command '/help [<subcommand>]' to get help for a specific command."
                + $"\n"
                + $"\n Commands:"
                + $"\n /help\t  \t  \rGet help for each command or in general."
                + $"\n /lcd\t   \t  \rChange LCD states, Using pre-programmed sprites and text / info."
                + $"\n"
                + $"\n ----------------------------------"
                + $"\n Arguments Count: {args.Count()}"
            );
        }

        public void InitBlocks()
        {
            try
            {
                GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(Blocks.Sensors, Sensor => Sensor.Enabled == true);
                GridTerminalSystem.GetBlocksOfType<IMyTimerBlock>(Blocks.Timers, Timer => Timer.Enabled == true);
                GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(Blocks.LCDs, LCD => LCD != null);
            }
            catch (System.Exception e)
            {
                Echo($"[CommandLineActions]\nError: Caught Exception:\n > {e.ToString()}");
            }

            return;
        }

        private void LCD()
        {
            switch(args[1])
            {

                case "show":
                    string blockName = args[2];
                    
                    if (blockName == null) break;

                    IMyTextPanel panel = (IMyTextPanel)GridTerminalSystem.GetBlockWithName(blockName);
                    Blocks.LCDs.Add(LcdShow(panel));

                    break;

                case "toggle":
                    string block = args[2];
                    string pos = args[3];
                    string neg = args[4];

                    if (block == null) break;
                    if (pos == null) break;
                    if (neg == null) break;

                    IMyTextPanel panel2 = (IMyTextPanel)GridTerminalSystem.GetBlockWithName(block);
                    Blocks.LCDs.Add(lcdToggle(panel2, pos, neg));
                    break;

                default:
                    Echo($"Use '/help lcd' to learn more about this command.");
                    break;
            }
            return;
        }

        private IMyTextPanel lcdToggle(IMyTextPanel panel, string positive, string negative)
        {
            panel.SetValue<long>("Font", 1147350002);

            panel.FontSize = 3;
            panel.TextPadding = 25;

            panel.Alignment = TextAlignment.CENTER;
            panel.ContentType = ContentType.TEXT_AND_IMAGE;

            if (panel.GetText().Substring(6) == negative)
            {
                panel.BackgroundColor = new Color(0, 255, 0);
                panel.WriteText(
                    "✓ ✓ ✓"
                    + $"\n{positive}"
                );
            } else
            {
                panel.BackgroundColor = new Color(225, 0, 0);
                panel.WriteText(
                    "X X X"
                    + $"\n{negative}"
                );
            }

            return panel;
        }

        private IMyTextPanel LcdShow(IMyTextPanel panel)
        {
            UnicodeImage Image = new UnicodeImage(panel);
            
            return panel;
        }

        private IMyTextPanel LcdUpdate(string blockName, IMyTextPanel panel)
        {
            Echo($"[{blockName}] SET :: {args[3]} => {args[4]}");
            return panel;
        }
    }
}
