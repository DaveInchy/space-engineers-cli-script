namespace CommandLineActions
{

    static public class TextPanel
    {
        public IMyTextPanel Panel;

        public TextPanel(IMyTextPanel panel)
        {
            this.Panel = panel;
            return this;
        }

        public void toggle()
        {
            return;
        }
    }
}