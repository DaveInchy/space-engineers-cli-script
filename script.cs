Program(){Runtime.UpdateFrequency=UpdateFrequency.Update100;e=new U();this.l(e);}void Save(){string X=
"Saving is not Implemented";Echo(X);throw new Exception(X);}void Main(string Y,UpdateType Z){try{if((Z&p)!=0){Action a;j["/help"]=H;j["/lcd"]=I;if(
i.TryParse(Y)){string b=i.Argument(0);k=new List<string>();k.AddRange(i.Items);if(b==null){Echo(
"[CommandLineActions]\nError: No command specified");}else if(j.TryGetValue(b,out a)){a();h++;}else{Echo($"[CommandLineActions]\nError: Unknown command {b}");}}}else if((Z
&f)!=0){string c="";for(int d=0;d<=k.Count();d++){c+=$"{k[d]} ";}Echo($"[CommandLineActions] Statistics:"+
$"\nUpdated {g.ToString()} times"+$"\n----------------------"+$"\nExecuted Commands {h.ToString()} times"+$"\nRecent Command:\n\t=> {c}"+
$"\nAmount Args: {k.Count()}");}}catch(Exception e){Echo($"[CommandLineActions]\nError: Caught Exception => {e.ToString()}");}g++;return;}U e;const
UpdateType p=UpdateType.Trigger|UpdateType.Terminal;const UpdateType f=UpdateType.Update1|UpdateType.Update10|UpdateType.Update100
|UpdateType.Trigger;int g=0;int h=0;MyCommandLine i=new MyCommandLine();Dictionary<string,Action>j=new Dictionary<string,
Action>(StringComparer.OrdinalIgnoreCase);List<string>k=new List<string>();void l(U e){try{GridTerminalSystem.GetBlocksOfType<
IMyLightingBlock>(e.C,m=>m!=null);GridTerminalSystem.GetBlocksOfType<IMyButtonPanel>(e.D,o=>o!=null);GridTerminalSystem.GetBlocksOfType<
IMySensorBlock>(e.E,q=>q!=null);GridTerminalSystem.GetBlocksOfType<IMyTimerBlock>(e.F,W=>W!=null);GridTerminalSystem.GetBlocksOfType<
IMyTextPanel>(e.G,A=>A!=null);}catch(Exception e){Echo($"[CommandLineActions]\nError: Caught Exception:\n > {e.ToString()}");}return
;}class U{public List<IMyUserControllableGun>B=new List<IMyUserControllableGun>();public List<IMyLightingBlock>C=new List
<IMyLightingBlock>();public List<IMyButtonPanel>D=new List<IMyButtonPanel>();public List<IMySensorBlock>E=new List<
IMySensorBlock>();public List<IMyTimerBlock>F=new List<IMyTimerBlock>();public List<IMyTextPanel>G=new List<IMyTextPanel>();}void H(){
Echo($"[CommandLineActions] use '/help [<SubCommand>]'."+$"\n"+$"\nSubCommand's:"+$"\n----------------------"+
$"\n/help => Get help for each command or in general."+$"\n/lcd => Change LCD states, Using pre-programmed sprites and text / info."+$"\n----------------------"+$"\n"+
$"\nPage: 1 of 1 | use '/help [<Index>]'");}void I(){string J=k[2];if(J==null)return;IMyTextPanel K=(IMyTextPanel)GridTerminalSystem.GetBlockWithName(J);switch(k
[1]){case"show":string L=k[4];if(J==null)break;if(L==null)break;this.R(K,L);break;case"toggle":string M=k[3];string N=k[4
];if(J==null)break;if(M==null)break;if(N==null)break;this.O(K,M,N);break;default:Echo(
$"Use '/help lcd' to learn more about this command.");break;}return;}IMyTextPanel O(IMyTextPanel K,string P,string Q){K.SetValue<long>("Font",1147350002);K.FontSize=3F;K.
TextPadding=25;K.Alignment=TextAlignment.CENTER;K.ContentType=ContentType.TEXT_AND_IMAGE;if(K.GetText().Substring(6)==Q){K.
BackgroundColor=new Color(0,255,0);K.WriteText("✓ ✓ ✓"+$"\n{P}");}else{K.BackgroundColor=new Color(225,0,0);K.WriteText("X X X"+
$"\n{Q}");}return K;}IMyTextPanel R(IMyTextPanel K,string S){if(k[3]==null)return K;K.SetValue<long>("Font",1147350002);K.
FontSize=3F;K.TextPadding=25;K.Alignment=TextAlignment.CENTER;K.ContentType=ContentType.TEXT_AND_IMAGE;switch(k[3]){case
"warning":K.BackgroundColor=new Color(255,150,0);K.WriteText("‼ ‼ ‼"+$"\n{S}"+$"\n‼ ‼ ‼");break;case"info":K.BackgroundColor=new
Color(100,100,255);K.WriteText("? ? ?"+$"\n{S}"+$"\n? ? ?");break;case"danger":K.BackgroundColor=new Color(225,0,0);K.
WriteText("X X X"+$"\n{S}"+$"\nX X X");break;case"success":K.BackgroundColor=new Color(0,255,0);K.WriteText("✓ ✓ ✓"+$"\n{S}"+
$"\n✓ ✓ ✓");break;default:K.BackgroundColor=new Color(255,255,255);K.FontColor=new Color(0,0,0);K.WriteText("ERROR"+$"\n{k[3]}"+
$"\nUNKNOWN");break;}return K;}IMyTextPanel T(IMyTextPanel K,string[]V){return K;}