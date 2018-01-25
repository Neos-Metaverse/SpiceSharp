﻿using SpiceSharp.Circuits;
using SpiceSharp.Attributes;
using SpiceSharp.Behaviors.VCCS;
using SpiceSharp.Components.VCCS;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A voltage-controlled current source
    /// </summary>
    [PinsAttribute("V+", "V-", "VC+", "VC-"), ConnectedAttribute(0, 1)]
    public class VoltageControlledCurrentsource : Component
    {
        /// <summary>
        /// Nodes
        /// </summary>
        [PropertyName("pos_node"), PropertyInfo("Positive node of the source")]
        public int VCCSposNode { get; private set; }
        [PropertyName("neg_node"), PropertyInfo("Negative node of the source")]
        public int VCCSnegNode { get; private set; }
        [PropertyName("cont_p_node"), PropertyInfo("Positive node of the controlling source voltage")]
        public int VCCScontPosNode { get; private set; }
        [PropertyName("cont_n_node"), PropertyInfo("Negative node of the controlling source voltage")]
        public int VCCScontNegNode { get; private set; }

        /// <summary>
        /// Private constants
        /// </summary>
        public const int VCCSpinCount = 4;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the voltage-controlled current source</param>
        public VoltageControlledCurrentsource(Identifier name) 
            : base(name, VCCSpinCount)
        {
            // Add parameters
            Parameters.Add(new BaseParameters());

            // Add factories
            AddFactory(typeof(LoadBehavior), () => new LoadBehavior(Name));
            AddFactory(typeof(FrequencyBehavior), () => new FrequencyBehavior(Name));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the voltage-controlled current source</param>
        /// <param name="pos">The positive node</param>
        /// <param name="neg">The negative node</param>
        /// <param name="cont_pos">The positive controlling node</param>
        /// <param name="cont_neg">The negative controlling node</param>
        /// <param name="gain">The transconductance gain</param>
        public VoltageControlledCurrentsource(Identifier name, Identifier pos, Identifier neg, Identifier cont_pos, Identifier cont_neg, double gain) 
            : base(name, VCCSpinCount)
        {
            // Add parameters
            Parameters.Add(new BaseParameters(gain));

            // Add factories
            AddFactory(typeof(LoadBehavior), () => new LoadBehavior(Name));
            AddFactory(typeof(FrequencyBehavior), () => new FrequencyBehavior(Name));

            // Connect
            Connect(pos, neg, cont_pos, cont_neg);
        }

        /// <summary>
        /// Setup the voltage-controlled current source
        /// </summary>
        /// <param name="ckt">Circuit</param>
        public override void Setup(Circuit ckt)
        {
            var nodes = BindNodes(ckt);
            VCCSposNode = nodes[0].Index;
            VCCSnegNode = nodes[1].Index;
            VCCScontPosNode = nodes[2].Index;
            VCCScontNegNode = nodes[3].Index;
        }
    }
}
