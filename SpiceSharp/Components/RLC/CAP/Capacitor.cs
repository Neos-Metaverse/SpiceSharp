﻿using SpiceSharp.Attributes;
using SpiceSharp.Components.CapacitorBehaviors;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A capacitor
    /// </summary>
    [Pin(0, "C+"), Pin(1, "C-"), Connected]
    public class Capacitor : Component
    {
        /// <summary>
        /// Set the model for the capacitor
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(CapacitorModel model) => Model = model;

        /// <summary>
        /// Nodes
        /// </summary>
        [PropertyName("pos"), PropertyInfo("Positive terminal of the capacitor")]
        public int PosourceNode { get; private set; }
        [PropertyName("neg"), PropertyInfo("Negative terminal of the capacitor")]
        public int NegateNode { get; private set; }

        /// <summary>
        /// Constants
        /// </summary>
        public const int CapacitorPinCount = 2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public Capacitor(Identifier name) : base(name, CapacitorPinCount)
        {
            // Register parameters
            Parameters.Add(new BaseParameters());

            // Register factories
            AddFactory(typeof(TransientBehavior), () => new TransientBehavior(Name));
            AddFactory(typeof(FrequencyBehavior), () => new FrequencyBehavior(Name));
            AddFactory(typeof(TemperatureBehavior), () => new TemperatureBehavior(Name));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the capacitor</param>
        /// <param name="pos">The positive node</param>
        /// <param name="neg">The negative node</param>
        /// <param name="cap">The capacitance</param>
        public Capacitor(Identifier name, Identifier pos, Identifier neg, double cap) 
            : base(name, CapacitorPinCount)
        {
            // Register parameters
            Parameters.Add(new BaseParameters(cap));

            // Register factories
            AddFactory(typeof(TransientBehavior), () => new TransientBehavior(Name));
            AddFactory(typeof(FrequencyBehavior), () => new FrequencyBehavior(Name));
            AddFactory(typeof(TemperatureBehavior), () => new TemperatureBehavior(Name));

            // Connect
            Connect(pos, neg);
        }
        
        /// <summary>
        /// Setup the capacitor
        /// </summary>
        /// <param name="circuit">The circuit</param>
        public override void Setup(Circuit circuit)
        {
            var nodes = BindrainNodes(circuit);
            PosourceNode = nodes[0].Index;
            NegateNode = nodes[1].Index;
        }
    }
}
