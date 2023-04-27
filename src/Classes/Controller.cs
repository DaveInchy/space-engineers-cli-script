using System;

namespace IngameScript
{
    partial class Program
    {
        public class Controller<T1>
        {
            private T1 controller;
            private Action exe;

            public Controller(T1 controller, Action execute)
            {
                this.controller = controller;
                this.exe = execute;
            }

            internal void Run()
            {

                this.exe();
            }
        }

    }
}