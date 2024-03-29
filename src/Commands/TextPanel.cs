﻿using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        private void LCDState()
        {
            string blockName = args[2];

            if (blockName == null) return;

            IMyTextPanel panel = (IMyTextPanel)GridTerminalSystem.GetBlockWithName(blockName);

            switch (args[1])
            {

                case "show":
                    string message = args[4];

                    if (blockName == null) break;
                    if (message == null) break;

                    this.ShowTextPanel(panel, message);

                    break;

                case "toggle":
                    string pos = args[3];
                    string neg = args[4];

                    if (blockName == null) break;
                    if (pos == null) break;
                    if (neg == null) break;

                    this.ToggleTextPanel(panel, pos, neg);
                    break;

                default:
                    string msg = $"Use '/help lcd' to learn more about this command.";
                    errLog += msg;
                    Echo(msg);
                    break;
            }
            return;
        }

        private IMyTextPanel ToggleTextPanel(IMyTextPanel panel, string positive, string negative)
        {
            panel.SetValue<long>("Font", 1147350002);

            panel.FontSize = 3F;
            panel.TextPadding = 25;

            panel.Alignment = TextAlignment.CENTER;
            panel.ContentType = ContentType.TEXT_AND_IMAGE;

            if (panel == null)
            {

                string msg = "\nPanel computes to null within this grid";
                Echo(msg);
                errLog += msg;
                return panel;

            }
            else
            {

                string subString = "";
                int strLength = panel.GetText().ToCharArray().Length;

                if (strLength >= 0)
                {
                    subString = panel.GetText().Substring(6);
                }

                if (negative.Equals(subString))
                {
                    panel.BackgroundColor = new Color(0, 255, 0);
                    panel.WriteText(
                        "✓ ✓ ✓"
                        + $"\n{positive}"
                    );
                }
                else
                {
                    panel.BackgroundColor = new Color(225, 0, 0);
                    panel.WriteText(
                        "X X X"
                        + $"\n{negative}"
                    );
                }

            }



            return panel;
        }

        private IMyTextPanel ShowTextPanel(IMyTextPanel panel, string Message)
        {
            if (args[3] == null) return panel;

            panel.SetValue<long>("Font", 1147350002);

            panel.FontSize = (float)2.2;
            panel.TextPadding = 25;

            panel.Alignment = TextAlignment.CENTER;
            panel.ContentType = ContentType.TEXT_AND_IMAGE;

            switch (args[3])
            {
                case "warning":
                    panel.BackgroundColor = new Color(255, 150, 0);
                    panel.WriteText(
                        "‼ ‼ ‼"
                        + $"\n{Message}"
                        + $"\n‼ ‼ ‼"
                    );
                    break;
                case "info":
                    panel.BackgroundColor = new Color(100, 100, 255);
                    panel.WriteText(
                        "? ? ?"
                        + $"\n{Message}"
                        + $"\n? ? ?"
                    );
                    break;
                case "danger":
                    panel.BackgroundColor = new Color(225, 0, 0);
                    panel.WriteText(
                        "X X X"
                        + $"\n{Message}"
                        + $"\nX X X"
                    );
                    break;
                case "success":
                    panel.BackgroundColor = new Color(0, 255, 0);
                    panel.WriteText(
                        "✓ ✓ ✓"
                        + $"\n{Message}"
                        + $"\n✓ ✓ ✓"
                    );
                    break;

                default:
                    panel.BackgroundColor = new Color(255, 255, 255);
                    panel.FontColor = new Color(0, 0, 0);
                    panel.WriteText(
                        "ERROR"
                        + $"\n{args[3]}"
                        + $"\nUNKNOWN"
                    );
                    string msg = "\nError: Unknown ARG[3] ERROR";
                    errLog += msg;
                    Echo(msg);
                    break;
            }

            return panel;
        }

        private IMyTextPanel WriteTextPanel(IMyTextPanel panel, string[] Text)
        {
            // Make it so the recieving notification LCD's get the message as notification.
            return panel;
        }
    }
}
