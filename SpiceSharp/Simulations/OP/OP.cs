﻿using SpiceSharp.Circuits;

namespace SpiceSharp.Simulations
{
    /// <summary>
    /// Operating Point analysis
    /// </summary>
    public class Op : BaseSimulation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the simulation</param>
        public Op(Identifier name) : base(name)
        {
        }

        /// <summary>
        /// Execute the DC simulation
        /// </summary>
        protected override void Execute()
        {
            // Setup the state
            var state = RealState;
            var baseconfig = BaseConfiguration;
            state.UseIc = false; // UseIC is only used in transient simulations
            state.UseDc = true;
            state.Domain = RealState.DomainType.None;
            state.Gmin = baseconfig.Gmin;

            Op(baseconfig.DcMaxIterations);

            var exportargs = new ExportDataEventArgs(RealState);
            Export(exportargs);
        }
    }
}
