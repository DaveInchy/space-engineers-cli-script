Scheduler Task;Scheduler Task2;Program(){Runtime.UpdateFrequency=UpdateFrequency.Update10;try{this.Blocks=new Grid(
GridTerminalSystem as IMyGridTerminalSystem);DoorController Doors=new DoorController(this.Blocks);LightController Lights=new
LightController(this.Blocks);Task=new Scheduler(this,Lights.Sequence(),true);Task2=new Scheduler(this,Doors.Sequence(),true);this.
Controllers.Add(new Controller<Object>(controller:Doors,execute:Task2.Run));this.Controllers.Add(new Controller<Object>(controller:
Lights,execute:Task.Run));}catch(System.Exception e){string msg=$"\nError: Caught Exception:\n > {e.ToString()}";logError(msg)
;}}void Save(){string msg="\nError: Saving is not Implemented";logError(msg);}void Main(string argument,UpdateType
updateSource){try{if((updateSource&CommandUpdate)!=0){Action init;Commands["/help"]=Help;Commands["/lcd"]=TextPanel;if(CommandLine.
TryParse(argument)){string initiator=CommandLine.Argument(0);args=new List<string>();args.AddRange(CommandLine.Items);if(
initiator==null){string msg="\nError: No command specified";logError(msg);}else if(Commands.TryGetValue(initiator,out init)){init
();ExecutionCounter++;}else{string msg=$"\nError: Unknown command {initiator}";logError(msg);}}}else if((updateSource&
BlockUpdate)!=0){string cmd="";for(int n=0;n<args.Count();n++){cmd+=$"{args[n]} ";}var compute=(Runtime.CurrentInstructionCount/
Runtime.MaxInstructionCount)*100;Echo($"[CommandLineActions] Statistics:"+$"\nITER :: {UpdateCounter.ToString()}x"+
$"\nLOAD :: {compute}%"+$"\n----------------------"+$"\nNUM COMMANDS :: {ExecutionCounter.ToString()}x"+$"\nCOMMAND ARGS :: {args.Count()}x"+
$"\nLAST COMMAND :: '{cmd}'"+$"\n\nLOG:\n[CommandLineActions]\t{errLog}");}List<Controller<Object>>controllers=this.Controllers;for(int n=0;n<
controllers.ToArray().Length;n++){controllers.ToArray()[n].Run();}this.Controllers=controllers;}catch(System.Exception e){string
msg=$"\nError: Caught Exception => {e.ToString()}";logError(msg);}if(!Task.Running)Task.Start();UpdateCounter++;return;}
class Controller<T1>{private T1 controller;private Action exe;public Controller(T1 controller,Action execute){this.controller
=controller;this.exe=execute;}internal void Run(){this.exe();}}Grid Blocks;const UpdateType CommandUpdate=UpdateType.
Trigger|UpdateType.Terminal;const UpdateType BlockUpdate=UpdateType.Update1|UpdateType.Update10|UpdateType.Update100|UpdateType
.Trigger;int UpdateCounter=0;int ExecutionCounter=0;List<Controller<Object>>Controllers=new List<Controller<Object>>();
MyCommandLine CommandLine=new MyCommandLine();Dictionary<string,Action>Commands=new Dictionary<string,Action>(StringComparer.
OrdinalIgnoreCase);List<string>args=new List<string>();string errLog="";static Random Rand=new Random();class DoorController{private Grid
Blocks;public DoorController(Grid Blocks){this.Blocks=Blocks;return;}public IEnumerable<double>Sequence(){yield return 2.5;
closeDoors();yield return 0;}private void closeDoors(){IMyDoor[]Doors=this.Blocks.Doors.All.ToArray();for(int n=0;n<Doors.Length;n
++){if(Doors[n]!=null){if(Doors[n].Status==DoorStatus.Open&&!Doors[n].CustomName.Contains("[exclude]")){Doors[n].
ApplyAction("Open_Off");}}}}}class Grid:GridBlocks{public List<IMyButtonPanel>Buttons=new List<IMyButtonPanel>();public List<
IMySensorBlock>Sensors=new List<IMySensorBlock>();public List<IMyTimerBlock>Timers=new List<IMyTimerBlock>();public List<IMyTextPanel>
LCDs=new List<IMyTextPanel>();public Grid(IMyGridTerminalSystem Grid){this.Grid=Grid;if(this.Grid==null)return;Grid.
GetBlocksOfType<IMyButtonPanel>(this.Buttons);Grid.GetBlocksOfType<IMySensorBlock>(this.Sensors);Grid.GetBlocksOfType<IMyTimerBlock>(
this.Timers);Grid.GetBlocksOfType<IMyTextPanel>(this.LCDs);this.Doors=new DoorBlocks();Grid.GetBlocksOfType<IMyDoor>(this.
Doors.All);Grid.GetBlocksOfType<IMyAdvancedDoor>(this.Doors.Advanced);Grid.GetBlocksOfType<IMyAirtightDoorBase>(this.Doors.
AirTight);Grid.GetBlocksOfType<IMyAirtightSlideDoor>(this.Doors.Sliding);Grid.GetBlocksOfType<IMyAirtightHangarDoor>(this.Doors.
Hangar);this.Lights=new LightBlocks();Grid.GetBlocksOfType<IMyLightingBlock>(this.Lights.All);Grid.GetBlocksOfType<
IMyInteriorLight>(this.Lights.Interior);Grid.GetBlocksOfType<IMyReflectorLight>(this.Lights.Spot);return;}}class GridBlocks{public
IMyGridTerminalSystem Grid;public DoorBlocks Doors;public LightBlocks Lights;public class LightBlocks{public List<IMyLightingBlock>All=new
List<IMyLightingBlock>();public List<IMyInteriorLight>Interior=new List<IMyInteriorLight>();public List<IMyReflectorLight>
Spot=new List<IMyReflectorLight>();}public class DoorBlocks{public List<IMyDoor>All=new List<IMyDoor>();public List<
IMyAdvancedDoor>Advanced=new List<IMyAdvancedDoor>();public List<IMyAirtightDoorBase>AirTight=new List<IMyAirtightDoorBase>();public
List<IMyAirtightSlideDoor>Sliding=new List<IMyAirtightSlideDoor>();public List<IMyAirtightHangarDoor>Hangar=new List<
IMyAirtightHangarDoor>();}}void Help(){Echo($"[CommandLineActions] use '/help [<SubCommand>]'."+$"\n"+$"\nSubCommand's:"+
$"\n----------------------"+$"\n/help => Get help for each command or in general."+
$"\n/lcd => Change LCD profile's, Using pre-programmed sprites and text / info."+$"\n/light => Change Light profile's, Using pre-programmed states."+$"\n----------------------"+$"\n"+
$"\nPage: 1 of 1 | use '/help [<Index>]'");}class LightController{private Grid Blocks;public LightController(Grid Blocks){this.Blocks=Blocks;return;}public int
Count{get;private set;}public IEnumerable<double>Sequence(){float chanceRange=(float)0.005;int chancePercentage=(int)((float)
chanceRange*(float)100);IMyLightingBlock[]Lights=this.Blocks.Lights.All.ToArray();for(int n=0;n<Lights.Length;n++){int chanceRandom
=Rand.Next(100);if(chanceRandom<=chancePercentage){if(Lights[n].Closed)break;Lights[n].Falloff=(float)0.5;Lights[n].
Enabled=true;yield return 1;Lights[n].Falloff=(float)1;Lights[n].Enabled=false;yield return 0.1;Lights[n].Falloff=(float)1.5;
Lights[n].Enabled=true;yield return 0.6;Lights[n].Falloff=(float)2;Lights[n].Enabled=false;yield return 0.1;Lights[n].Falloff=
(float)2.5;Lights[n].Enabled=true;yield return 1;Lights[n].Falloff=(float)0.5;Lights[n].Enabled=false;yield return 0.1;
Lights[n].Falloff=(float)1;Lights[n].Enabled=true;yield return 0.2;Lights[n].Falloff=(float)1.5;Lights[n].Enabled=false;yield
return 0.1;Lights[n].Falloff=(float)2;Lights[n].Enabled=true;yield return 1;Lights[n].Falloff=(float)2.5;Lights[n].Enabled=
false;yield return 0.1;Lights[n].Falloff=(float)1.5;Lights[n].Enabled=true;yield return 3;Lights[n].Falloff=(float)0.5;Lights
[n].Enabled=false;yield return 0.1;Lights[n].Falloff=(float)1;Lights[n].Enabled=true;yield return 1;}}yield return 1;}}
bool logError(string errMsg){try{errMsg='\n'+errMsg;errLog+=errMsg;}catch(Exception err){errLog+='\n'+err.Message;Echo(err.
Message);return false;}Echo(errLog);return true;}class Scheduler{public Program Program;public bool AutoStart{get;set;}public
bool Running{get;private set;}public IEnumerable<double>Sequence{get;set;}public double SequenceTimer{get;private set;}
private IEnumerator<double>sequenceSM;public Scheduler(Program program,IEnumerable<double>sequence=null,bool autoStart=false){
Program=program;Sequence=sequence;AutoStart=autoStart;if(AutoStart){Start();}}public void Start(){SetSequenceSM(Sequence);}
public void Stop(){SetSequenceSM(null);}public void Run(){if(sequenceSM==null)return;SequenceTimer-=Program.Runtime.
TimeSinceLastRun.TotalSeconds;if(SequenceTimer>0)return;bool hasValue=sequenceSM.MoveNext();if(hasValue){SequenceTimer=sequenceSM.
Current;if(SequenceTimer<=-0.5)hasValue=false;}if(!hasValue){if(AutoStart)SetSequenceSM(Sequence);else SetSequenceSM(null);}}
private void SetSequenceSM(IEnumerable<double>seq){Running=false;SequenceTimer=0;sequenceSM?.Dispose();sequenceSM=null;if(seq!=
null){Running=true;sequenceSM=seq.GetEnumerator();}}}void TextPanel(){string blockName=args[2];if(blockName==null)return;
IMyTextPanel panel=(IMyTextPanel)GridTerminalSystem.GetBlockWithName(blockName);switch(args[1]){case"show":string message=args[4];if
(blockName==null)break;if(message==null)break;this.ShowTextPanel(panel,message);break;case"toggle":string pos=args[3];
string neg=args[4];if(blockName==null)break;if(pos==null)break;if(neg==null)break;this.ToggleTextPanel(panel,pos,neg);break;
default:string msg=$"Use '/help lcd' to learn more about this command.";errLog+=msg;Echo(msg);break;}return;}IMyTextPanel
ToggleTextPanel(IMyTextPanel panel,string positive,string negative){panel.SetValue<long>("Font",1147350002);panel.FontSize=3F;panel.
TextPadding=25;panel.Alignment=TextAlignment.CENTER;panel.ContentType=ContentType.TEXT_AND_IMAGE;if(panel==null){string msg=
"\nPanel computes to null within this grid";Echo(msg);errLog+=msg;return panel;}else{string subString="";int strLength=panel.GetText().ToCharArray().Length;if(
strLength>=0){subString=panel.GetText().Substring(6);}if(negative.Equals(subString)){panel.BackgroundColor=new Color(0,255,0);
panel.WriteText("✓ ✓ ✓"+$"\n{positive}");}else{panel.BackgroundColor=new Color(225,0,0);panel.WriteText("X X X"+
$"\n{negative}");}}return panel;}IMyTextPanel ShowTextPanel(IMyTextPanel panel,string Message){if(args[3]==null)return panel;panel.
SetValue<long>("Font",1147350002);panel.FontSize=(float)2.2;panel.TextPadding=25;panel.Alignment=TextAlignment.CENTER;panel.
ContentType=ContentType.TEXT_AND_IMAGE;switch(args[3]){case"warning":panel.BackgroundColor=new Color(255,150,0);panel.WriteText(
"‼ ‼ ‼"+$"\n{Message}"+$"\n‼ ‼ ‼");break;case"info":panel.BackgroundColor=new Color(100,100,255);panel.WriteText("? ? ?"+
$"\n{Message}"+$"\n? ? ?");break;case"danger":panel.BackgroundColor=new Color(225,0,0);panel.WriteText("X X X"+$"\n{Message}"+
$"\nX X X");break;case"success":panel.BackgroundColor=new Color(0,255,0);panel.WriteText("✓ ✓ ✓"+$"\n{Message}"+$"\n✓ ✓ ✓");break;
default:panel.BackgroundColor=new Color(255,255,255);panel.FontColor=new Color(0,0,0);panel.WriteText("ERROR"+$"\n{args[3]}"+
$"\nUNKNOWN");string msg="\nError: Unknown ARG[3] ERROR";errLog+=msg;Echo(msg);break;}return panel;}IMyTextPanel WriteTextPanel(
IMyTextPanel panel,string[]Text){return panel;}