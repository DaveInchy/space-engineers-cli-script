U X;const UpdateType Y=UpdateType.Trigger|UpdateType.Terminal;const UpdateType Z=UpdateType.Update1|UpdateType.Update10|
UpdateType.Update100|UpdateType.Trigger;int a=0;int b=0;MyCommandLine c=new MyCommandLine();Dictionary<string,Action>d=new
Dictionary<string,Action>(StringComparer.OrdinalIgnoreCase);List<string>e=new List<string>();Program(){Runtime.UpdateFrequency=
UpdateFrequency.Update100;X=new U();this.m(X);}void Save(){string f="Saving is not Implemented";Echo(f);throw new Exception(f);}void
Main(string g,UpdateType h){try{if((h&Y)!=0){Action i;d["/help"]=H;d["/lcd"]=I;if(c.TryParse(g)){string j=c.Argument(0);e=
new List<string>();e.AddRange(c.Items);if(j==null){Echo("[CommandLineActions]\nError: No command specified");}else if(d.
TryGetValue(j,out i)){i();b++;}else{Echo($"[CommandLineActions]\nError: Unknown command {j}");}}}else if((h&Z)!=0){string k="";for(
int l=0;l<=e.Count();l++){k+=$"{e[l]} ";}Echo($"[CommandLineActions] Statistics:"+$"\nUpdated {a.ToString()} times"+
$"\n----------------------"+$"\nExecuted Commands {b.ToString()} times"+$"\nRecent Command:\n\t=> {k}"+$"\nAmount Args: {e.Count()}");}}catch(
Exception e){Echo($"[CommandLineActions]\nError: Caught Exception => {e.ToString()}");}a++;return;}void m(U X){try{
GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>(X.C,o=>o!=null);GridTerminalSystem.GetBlocksOfType<IMyButtonPanel>(X.D,p=>p!=null);
GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(X.E,q=>q!=null);GridTerminalSystem.GetBlocksOfType<IMyTimerBlock>(X.F,W=>W!=null);
GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(X.G,A=>A!=null);}catch(Exception e){Echo(
$"[CommandLineActions]\nError: Caught Exception:\n > {e.ToString()}");}return;}class U{public List<IMyUserControllableGun>B=new List<IMyUserControllableGun>();public List<IMyLightingBlock>
C=new List<IMyLightingBlock>();public List<IMyButtonPanel>D=new List<IMyButtonPanel>();public List<IMySensorBlock>E=new
List<IMySensorBlock>();public List<IMyTimerBlock>F=new List<IMyTimerBlock>();public List<IMyTextPanel>G=new List<
IMyTextPanel>();}void H(){Echo($"[CommandLineActions] use '/help [<SubCommand>]'."+$"\n"+$"\nSubCommand's:"+
$"\n----------------------"+$"\n/help => Get help for each command or in general."+
$"\n/lcd => Change LCD states, Using pre-programmed sprites and text / info."+$"\n----------------------"+$"\n"+$"\nPage: 1 of 1 | use '/help [<Index>]'");}void I(){string J=e[2];if(J==null)return;
IMyTextPanel K=(IMyTextPanel)GridTerminalSystem.GetBlockWithName(J);switch(e[1]){case"show":string L=e[4];if(J==null)break;if(L==
null)break;this.R(K,L);break;case"toggle":string M=e[3];string N=e[4];if(J==null)break;if(M==null)break;if(N==null)break;
this.O(K,M,N);break;default:Echo($"Use '/help lcd' to learn more about this command.");break;}return;}IMyTextPanel O(
IMyTextPanel K,string P,string Q){K.SetValue<long>("Font",1147350002);K.FontSize=3F;K.TextPadding=25;K.Alignment=TextAlignment.
CENTER;K.ContentType=ContentType.TEXT_AND_IMAGE;if(K.GetText().Substring(6)==Q){K.BackgroundColor=new Color(0,255,0);K.
WriteText("✓ ✓ ✓"+$"\n{P}");}else{K.BackgroundColor=new Color(225,0,0);K.WriteText("X X X"+$"\n{Q}");}return K;}IMyTextPanel R(
IMyTextPanel K,string S){if(e[3]==null)return K;K.SetValue<long>("Font",1147350002);K.FontSize=3F;K.TextPadding=25;K.Alignment=
TextAlignment.CENTER;K.ContentType=ContentType.TEXT_AND_IMAGE;switch(e[3]){case"warning":K.BackgroundColor=new Color(255,150,0);K.
WriteText("‼ ‼ ‼"+$"\n{S}"+$"\n‼ ‼ ‼");break;case"info":K.BackgroundColor=new Color(100,100,255);K.WriteText("? ? ?"+$"\n{S}"+
$"\n? ? ?");break;case"danger":K.BackgroundColor=new Color(225,0,0);K.WriteText("X X X"+$"\n{S}"+$"\nX X X");break;case"success":K
.BackgroundColor=new Color(0,255,0);K.WriteText("✓ ✓ ✓"+$"\n{S}"+$"\n✓ ✓ ✓");break;default:K.BackgroundColor=new Color(
255,255,255);K.FontColor=new Color(0,0,0);K.WriteText("ERROR"+$"\n{e[3]}"+$"\nUNKNOWN");break;}return K;}IMyTextPanel T(
IMyTextPanel K,string[]V){return K;}