using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        /// <summary>
        /// Quick usage:
        /// <para>1. A persistent instance for each sequence you want to run in parallel.</para>
        /// <para>2. Create instance(s) in Program() and execute <see cref="Run"/> in Main().</para>
        /// </summary>
        public class Scheduler
        {
            public readonly Program Program;

            /// <summary>
            /// Wether the timer starts automatically at initialization and auto-restarts it's done iterating.
            /// </summary>
            public bool AutoStart { get; set; }

            /// <summary>
            /// <para>Returns true if a sequence is actively being cycled through.</para>
            /// <para>False if it ended, got stopped or no sequence is assigned anymore.</para>
            /// </summary>
            public bool Running { get; private set; }

            /// <summary>
            /// <para>The sequence used by Start(). Can be null.</para>
            /// <para>Setting this will not automatically start it.</para>
            /// </summary>
            public IEnumerable<double> Sequence { get; set; }

            /// <summary>
            /// Time left until the next part is called.
            /// </summary>
            public double SequenceTimer { get; private set; }

            private IEnumerator<double> sequenceSM;

            public Scheduler(Program program, IEnumerable<double> sequence = null, bool autoStart = false)
            {
                Program = program;
                Sequence = sequence;
                AutoStart = autoStart;

                if (AutoStart)
                {
                    Start();
                }
            }

            /// <summary>
            /// <para>Starts or restarts the sequence declared in Sequence property.</para>
            /// <para>If it's already running, it will be stoped and started from the begining.</para>
            /// <para>Don't forget to set Runtime.UpdateFrequency and call this class' Run() in Main().</para>
            /// </summary>
            public void Start()
            {
                SetSequenceSM(Sequence);
            }

            /// <summary>
            /// <para>Stops the sequence from progressing.</para>
            /// <para>Calling Start() after this will start the sequence from the begining (the one declared in Sequence property).</para>
            /// </summary>
            public void Stop()
            {
                SetSequenceSM(null);
            }

            /// <summary>
            /// <para>Call this in your Program's Main() and have a reasonable update frequency, usually Update10 is good for small delays, Update100 for 2s or more delays.</para>
            /// <para>Checks if enough time passed and executes the next chunk in the sequence.</para>
            /// <para>Does nothing if no sequence is assigned or it's ended.</para>
            /// </summary>
            public void Run()
            {
                if (sequenceSM == null)
                    return;

                SequenceTimer -= Program.Runtime.TimeSinceLastRun.TotalSeconds;

                if (SequenceTimer > 0)
                    return;

                bool hasValue = sequenceSM.MoveNext();

                if (hasValue)
                {
                    SequenceTimer = sequenceSM.Current;

                    if (SequenceTimer <= -0.5)
                        hasValue = false;
                }

                if (!hasValue)
                {
                    if (AutoStart)
                        SetSequenceSM(Sequence);
                    else
                        SetSequenceSM(null);
                }
            }

            private void SetSequenceSM(IEnumerable<double> seq)
            {
                Running = false;
                SequenceTimer = 0;

                sequenceSM?.Dispose();
                sequenceSM = null;

                if (seq != null)
                {
                    Running = true;
                    sequenceSM = seq.GetEnumerator();
                }
            }
        }
    }
}
