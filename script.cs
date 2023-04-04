Program(){Runtime.UpdateFrequency=UpdateFrequency.Update100;this.Blocks=new GridBlocks(GridTerminalSystem as
IMyGridTerminalSystem);this.InitGridBlocks(this.Blocks);}void Save(){string msg="\nError: Saving is not Implemented";errLog+=msg;Echo(msg);}
void Main(string argument,UpdateType updateSource){try{if((updateSource&CommandUpdate)!=0){Action init;Commands["/help"]=
Help;Commands["/lcd"]=TextPanel;if(CommandLine.TryParse(argument)){string initiator=CommandLine.Argument(0);args=new List<
string>();args.AddRange(CommandLine.Items);if(initiator==null){string msg="\nError: No command specified";errLog+=msg;Echo(msg
);}else if(Commands.TryGetValue(initiator,out init)){init();ExecutionCounter++;}else{string msg=
$"\nError: Unknown command {initiator}";errLog+=msg;Echo(msg);}}}else if((updateSource&BlockUpdate)!=0){string cmd="";for(int n=0;n<args.Count();n++){cmd+=
$"{args[n]} ";}Echo($"[CommandLineActions] Statistics:"+$"\nUpdated {UpdateCounter.ToString()} times"+$"\n----------------------"+
$"\nExecuted Command {ExecutionCounter.ToString()} times"+$"\nNum. Arguments: {args.Count()}"+$"\nLast Command: \n\t{cmd}"+$"\n\nLOG:\n[CommandLineActions]{errLog}");}List<
Controller<Object,Action>>controllers=this.Controllers;this.Controllers=controllers;for(int n=0;n<controllers.ToArray().Length;n++
){controllers.ToArray()[n].execute();}}catch(System.Exception e){string msg=
$"\nError: Caught Exception => {e.ToString()}";errLog+=msg;Echo(msg);}UpdateCounter++;return;}class Controller<T1,T2>{private Object obj;private Action exe;public
Controller(Object controller,Action execute){this.obj=controller;this.exe=execute;}internal void execute(){this.exe();}}GridBlocks
Blocks;List<Controller<Object,Action>>Controllers=new List<Controller<Object,Action>>();const UpdateType CommandUpdate=
UpdateType.Trigger|UpdateType.Terminal;const UpdateType BlockUpdate=UpdateType.Update1|UpdateType.Update10|UpdateType.Update100|
UpdateType.Trigger;int UpdateCounter=0;int ExecutionCounter=0;MyCommandLine CommandLine=new MyCommandLine();Dictionary<string,
Action>Commands=new Dictionary<string,Action>(StringComparer.OrdinalIgnoreCase);List<string>args=new List<string>();string
errLog="";void InitGridBlocks(GridBlocks Blocks){try{var Door=new DoorController(Blocks);var ControllerClass=new
DoorController(this.Blocks);var DoorController=new Controller<Object,Action>(ControllerClass,ControllerClass.execute);this.Controllers
.Add(DoorController);}catch(System.Exception e){string msg=$"\nError: Caught Exception:\n > {e.ToString()}";errLog+=msg;
Echo(msg);}return;}class DoorController{private GridBlocks Blocks;public DoorController(GridBlocks Blocks){try{this.Blocks=
Blocks;}catch(System.Exception e){string msg=$"\nError: Caught Exception:\n > {e.ToString()}";}return;}public void execute(){
IMyDoor[]DoorsNormal=this.Blocks.Doors.Normal.ToArray();for(int n=0;n<DoorsNormal.Length;n++){if(DoorsNormal[n]!=null&&
DoorsNormal[n].Status.Equals(DoorStatus.Open)){DoorsNormal[n].ToggleDoor();}}IMyAirtightSlideDoor[]DoorsSlider=this.Blocks.Doors.
Sliding.ToArray();for(int n=0;n<DoorsSlider.Length;n++){if(DoorsSlider[n]!=null&&DoorsSlider[n].Status.Equals(DoorStatus.Open))
{DoorsSlider[n].ToggleDoor();}}return;}}class GridBlocks{private IMyGridTerminalSystem Grid;public List<IMyLightingBlock>
Lights=new List<IMyLightingBlock>();public List<IMyButtonPanel>Buttons=new List<IMyButtonPanel>();public List<IMySensorBlock>
Sensors=new List<IMySensorBlock>();public List<IMyTimerBlock>Timers=new List<IMyTimerBlock>();public List<IMyTextPanel>LCDs=new
List<IMyTextPanel>();public DoorBlocks Doors;public GridBlocks(IMyGridTerminalSystem Grid){this.Grid=Grid;if(this.Grid==null
)return;Grid.GetBlocksOfType<IMyLightingBlock>(this.Lights);Grid.GetBlocksOfType<IMyButtonPanel>(this.Buttons);Grid.
GetBlocksOfType<IMySensorBlock>(this.Sensors);Grid.GetBlocksOfType<IMyTimerBlock>(this.Timers);Grid.GetBlocksOfType<IMyTextPanel>(this.
LCDs);this.Doors=new DoorBlocks();Grid.GetBlocksOfType<IMyDoor>(this.Doors.Normal);Grid.GetBlocksOfType<IMyAdvancedDoor>(
this.Doors.Advanced);Grid.GetBlocksOfType<IMyAirtightDoorBase>(this.Doors.AirTight);Grid.GetBlocksOfType<
IMyAirtightSlideDoor>(this.Doors.Sliding);Grid.GetBlocksOfType<IMyAirtightHangarDoor>(this.Doors.Hangar);return;}public class DoorBlocks{
public List<IMyDoor>Normal=new List<IMyDoor>();public List<IMyAdvancedDoor>Advanced=new List<IMyAdvancedDoor>();public List<
IMyAirtightDoorBase>AirTight=new List<IMyAirtightDoorBase>();public List<IMyAirtightSlideDoor>Sliding=new List<IMyAirtightSlideDoor>();
public List<IMyAirtightHangarDoor>Hangar=new List<IMyAirtightHangarDoor>();}}void Help(){Echo(
$"[CommandLineActions] use '/help [<SubCommand>]'."+$"\n"+$"\nSubCommand's:"+$"\n----------------------"+$"\n/help => Get help for each command or in general."+
$"\n/lcd => Change LCD profile's, Using pre-programmed sprites and text / info."+$"\n/light => Change Light profile's, Using pre-programmed states."+$"\n----------------------"+$"\n"+
$"\nPage: 1 of 1 | use '/help [<Index>]'");}void TextPanel(){string blockName=args[2];if(blockName==null)return;IMyTextPanel panel=(IMyTextPanel)
GridTerminalSystem.GetBlockWithName(blockName);switch(args[1]){case"show":string message=args[4];if(blockName==null)break;if(message==null
)break;this.ShowTextPanel(panel,message);break;case"toggle":string pos=args[3];string neg=args[4];if(blockName==null)
break;if(pos==null)break;if(neg==null)break;this.ToggleTextPanel(panel,pos,neg);break;default:string msg=
$"Use '/help lcd' to learn more about this command.";errLog+=msg;Echo(msg);break;}return;}IMyTextPanel ToggleTextPanel(IMyTextPanel panel,string positive,string negative){
panel.SetValue<long>("Font",1147350002);panel.FontSize=3F;panel.TextPadding=25;panel.Alignment=TextAlignment.CENTER;panel.
ContentType=ContentType.TEXT_AND_IMAGE;if(panel==null){string msg="\nPanel computes to null within this grid";Echo(msg);errLog+=msg
;return panel;}else{string subString="";int strLength=panel.GetText().ToCharArray().Length;if(strLength>=0){subString=
panel.GetText().Substring(6);}if(negative.Equals(subString)){panel.BackgroundColor=new Color(0,255,0);panel.WriteText("✓ ✓ ✓"
+$"\n{positive}");}else{panel.BackgroundColor=new Color(225,0,0);panel.WriteText("X X X"+$"\n{negative}");}}return panel;
}IMyTextPanel ShowTextPanel(IMyTextPanel panel,string Message){if(args[3]==null)return panel;panel.SetValue<long>("Font",
1147350002);panel.FontSize=3F;panel.TextPadding=25;panel.Alignment=TextAlignment.CENTER;panel.ContentType=ContentType.
TEXT_AND_IMAGE;switch(args[3]){case"warning":panel.BackgroundColor=new Color(255,150,0);panel.WriteText("‼ ‼ ‼"+$"\n{Message}"+
$"\n‼ ‼ ‼");break;case"info":panel.BackgroundColor=new Color(100,100,255);panel.WriteText("? ? ?"+$"\n{Message}"+$"\n? ? ?");break
;case"danger":panel.BackgroundColor=new Color(225,0,0);panel.WriteText("X X X"+$"\n{Message}"+$"\nX X X");break;case
"success":panel.BackgroundColor=new Color(0,255,0);panel.WriteText("✓ ✓ ✓"+$"\n{Message}"+$"\n✓ ✓ ✓");break;default:panel.
BackgroundColor=new Color(255,255,255);panel.FontColor=new Color(0,0,0);panel.WriteText("ERROR"+$"\n{args[3]}"+$"\nUNKNOWN");string msg
="\nError: Unknown ARG[3] ERROR";errLog+=msg;Echo(msg);break;}return panel;}IMyTextPanel WriteTextPanel(IMyTextPanel
panel,string[]Text){return panel;}