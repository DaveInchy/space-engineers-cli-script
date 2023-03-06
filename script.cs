Program(){Runtime.UpdateFrequency=UpdateFrequency.Update100;e=new C();this.o(e);}void Save(){string B=
"\nError: Saving is not Implemented";m+=B;Echo(B);}void Main(string Y,UpdateType Z){try{if((Z&f)!=0){Action a;k["/help"]=J;k["/lcd"]=K;if(j.TryParse(Y)){
string b=j.Argument(0);l=new List<string>();l.AddRange(j.Items);if(b==null){string B="\nError: No command specified";m+=B;Echo
(B);}else if(k.TryGetValue(b,out a)){a();i++;}else{string B=$"\nError: Unknown command {b}";m+=B;Echo(B);}}}else if((Z&g)
!=0){string c="";for(int d=0;d<l.Count();d++){c+=$"{l[d]} ";}Echo($"[CommandLineActions] Statistics:"+
$"\nUpdated {h.ToString()} times"+$"\n----------------------"+$"\nExecuted Commands {i.ToString()} times"+$"\nRecent Command:\n\t=> {c}"+
$"\nAmount Args: {l.Count()}"+$"\nERROR LOG:\n\r{m}");}}catch(System.Exception e){string B=$"\nError: Caught Exception => {e.ToString()}";m+=B;Echo(B
);}h++;return;}C e;const UpdateType f=UpdateType.Trigger|UpdateType.Terminal;const UpdateType g=UpdateType.Update1|
UpdateType.Update10|UpdateType.Update100|UpdateType.Trigger;int h=0;int i=0;MyCommandLine j=new MyCommandLine();Dictionary<string,
Action>k=new Dictionary<string,Action>(StringComparer.OrdinalIgnoreCase);List<string>l=new List<string>();string m="";void o(C
e){try{GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>(e.E,p=>p!=null);GridTerminalSystem.GetBlocksOfType<
IMyButtonPanel>(e.F,r=>r!=null);GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(e.G,q=>q!=null);GridTerminalSystem.GetBlocksOfType<
IMyTimerBlock>(e.H,X=>X!=null);GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(e.I,V=>V!=null);}catch(System.Exception e){string B=
$"\nError: Caught Exception:\n > {e.ToString()}";m+=B;Echo(B);}return;}class C{public List<IMyUserControllableGun>D=new List<IMyUserControllableGun>();public List<
IMyLightingBlock>E=new List<IMyLightingBlock>();public List<IMyButtonPanel>F=new List<IMyButtonPanel>();public List<IMySensorBlock>G=new
List<IMySensorBlock>();public List<IMyTimerBlock>H=new List<IMyTimerBlock>();public List<IMyTextPanel>I=new List<
IMyTextPanel>();}void J(){Echo($"[CommandLineActions] use '/help [<SubCommand>]'."+$"\n"+$"\nSubCommand's:"+
$"\n----------------------"+$"\n/help => Get help for each command or in general."+
$"\n/lcd => Change LCD states, Using pre-programmed sprites and text / info."+$"\n----------------------"+$"\n"+$"\nPage: 1 of 1 | use '/help [<Index>]'");}void K(){string L=l[2];if(L==null)return;
IMyTextPanel A=(IMyTextPanel)GridTerminalSystem.GetBlockWithName(L);switch(l[1]){case"show":string M=l[4];if(L==null)break;if(M==
null)break;this.S(A,M);break;case"toggle":string N=l[3];string O=l[4];if(L==null)break;if(N==null)break;if(O==null)break;
this.P(A,N,O);break;default:string B=$"Use '/help lcd' to learn more about this command.";m+=B;Echo(B);break;}return;}
IMyTextPanel P(IMyTextPanel A,string Q,string R){A.SetValue<long>("Font",1147350002);A.FontSize=3F;A.TextPadding=25;A.Alignment=
TextAlignment.CENTER;A.ContentType=ContentType.TEXT_AND_IMAGE;if(A.GetText().Substring(6)==R){A.BackgroundColor=new Color(0,255,0);A.
WriteText("✓ ✓ ✓"+$"\n{Q}");}else{A.BackgroundColor=new Color(225,0,0);A.WriteText("X X X"+$"\n{R}");}return A;}IMyTextPanel S(
IMyTextPanel A,string T){if(l[3]==null)return A;A.SetValue<long>("Font",1147350002);A.FontSize=3F;A.TextPadding=25;A.Alignment=
TextAlignment.CENTER;A.ContentType=ContentType.TEXT_AND_IMAGE;switch(l[3]){case"warning":A.BackgroundColor=new Color(255,150,0);A.
WriteText("‼ ‼ ‼"+$"\n{T}"+$"\n‼ ‼ ‼");break;case"info":A.BackgroundColor=new Color(100,100,255);A.WriteText("? ? ?"+$"\n{T}"+
$"\n? ? ?");break;case"danger":A.BackgroundColor=new Color(225,0,0);A.WriteText("X X X"+$"\n{T}"+$"\nX X X");break;case"success":A
.BackgroundColor=new Color(0,255,0);A.WriteText("✓ ✓ ✓"+$"\n{T}"+$"\n✓ ✓ ✓");break;default:A.BackgroundColor=new Color(
255,255,255);A.FontColor=new Color(0,0,0);A.WriteText("ERROR"+$"\n{l[3]}"+$"\nUNKNOWN");string B=
"\nError: Unknown ARG[3] ERROR";m+=B;Echo(B);break;}return A;}IMyTextPanel U(IMyTextPanel A,string[]W){return A;}