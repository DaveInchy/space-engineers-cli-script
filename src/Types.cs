using Sandbox.ModAPI.Ingame;

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
