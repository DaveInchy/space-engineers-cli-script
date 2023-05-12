using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
    partial class Program
    {
        private void DoorState()
        {
            string blockName = args[2];

            if (blockName == null) return;

            IMyDoor door = (IMyDoor)GridTerminalSystem.GetBlockWithName(blockName);

            switch (args[1])
            {
                case "set":

                    bool onOff = false;

                    if (args[3].ToLower() == "on" || args[3].ToLower() == "true" || args[3].ToLower() == "open")
                    {
                        onOff = true;
                    }
                    else
                    if (args[3].ToLower() == "off" || args[3].ToLower() == "false" || args[3].ToLower() == "close" || args[3].ToLower() == "closed")
                    {
                        onOff = false;
                    }
                    else
                    {
                        logError("Wrong third argument, use /door set <NAME> <open|close>");
                    }

                    if (door.Status == DoorStatus.Open && onOff == false) // is open - wants to close
                    {
                        door.ToggleDoor();
                    }
                    else if (door.Status == DoorStatus.Closed && onOff == true) // is closed - wants to open
                    {
                        door.ToggleDoor();
                    }

                    break;

                default:
                    string msg = "Use '/help door' to learn more about this command.";
                    logError(msg);
                    break;
            }
        }
    }
}
