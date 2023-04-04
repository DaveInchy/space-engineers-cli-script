using System;

namespace IngameScript
{
    partial class Program
    {
        public class Controller<T1, T2>
        {
            private Object obj;
            private Action exe;

            public Controller(Object controller, Action execute)
            {
                this.obj = controller;
                this.exe = execute;
            }

            internal void execute()
            {
                this.exe();
            }
        }

    }
}