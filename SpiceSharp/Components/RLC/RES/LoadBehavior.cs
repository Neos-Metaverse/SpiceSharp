﻿using SpiceSharp.Circuits;
using SpiceSharp.Attributes;
using SpiceSharp.Sparse;
using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;
using System;

namespace SpiceSharp.Components.ResistorBehaviors
{
    /// <summary>
    /// General behavior for <see cref="Resistor"/>
    /// </summary>
    public class LoadBehavior : Behaviors.LoadBehavior, IConnectedBehavior
    {
        /// <summary>
        /// Parameters
        /// </summary>
        [PropertyName("v"), PropertyInfo("Voltage")]
        public double GetVoltage(State state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return state.Solution[PosourceNode] - state.Solution[NegateNode];
        }
        [PropertyName("i"), PropertyInfo("Current")]
        public double GetCurrent(State state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return (state.Solution[PosourceNode] - state.Solution[NegateNode]) * Conductance;
        }
        [PropertyName("p"), PropertyInfo("Power")]
        public double GetPower(State state)
        {
			if (state == null)
				throw new ArgumentNullException(nameof(state));

            double v = state.Solution[PosourceNode] - state.Solution[NegateNode];
            return v * v * Conductance;
        }

        /// <summary>
        /// Nodes
        /// </summary>
        int PosourceNode, NegateNode;
        protected MatrixElement PosPosPtr { get; private set; }
        protected MatrixElement NegNegPtr { get; private set; }
        protected MatrixElement PosNegPtr { get; private set; }
        protected MatrixElement NegPosPtr { get; private set; }

        /// <summary>
        /// Conductance
        /// </summary>
        public double Conductance { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public LoadBehavior(Identifier name) : base(name) { }

        /// <summary>
        /// Create export method
        /// </summary>
        /// <param name="propertyName">Property</param>
        /// <returns></returns>
        public override Func<State, double> CreateExport(string propertyName)
        {
            switch (propertyName)
            {
                case "v": return GetVoltage;
                case "c":
                case "i": return GetCurrent;
                case "p": return GetPower;
                default: return null;
            }
        }

        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="provider">Data provider</param>
        public override void Setup(SetupDataProvider provider)
        {
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

            // Get parameters
            var p = provider.GetParameterSet<BaseParameters>(0);

            // Depending on whether or not the resistance is given, get behaviors
            if (!p.Resistance.Given)
            {
                var temp = provider.GetBehavior<TemperatureBehavior>(0);
                Conductance = temp.Conductance;
            }
            else
            {
                if (p.Resistance.Value < 1e-12)
                    Conductance = 1e12;
                else
                    Conductance = 1.0 / p.Resistance.Value;
            }
        }
        
        /// <summary>
        /// Connect the behavior to nodes
        /// </summary>
        /// <param name="pins">Pins</param>
        public void Connect(params int[] pins)
        {
            if (pins == null)
                throw new ArgumentNullException(nameof(pins));
            if (pins.Length != 2)
                throw new Diagnostics.CircuitException("Pin count mismatch: 2 pins expected, {0} given".FormatString(pins.Length));
            PosourceNode = pins[0];
            NegateNode = pins[1];
        }
        
        /// <summary>
        /// Get matrix pointers
        /// </summary>
        /// <param name="matrix">Matrix</param>
        public override void GetMatrixPointers(Nodes nodes, Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            // Get matrix elements
            PosPosPtr = matrix.GetElement(PosourceNode, PosourceNode);
            NegNegPtr = matrix.GetElement(NegateNode, NegateNode);
            PosNegPtr = matrix.GetElement(PosourceNode, NegateNode);
            NegPosPtr = matrix.GetElement(NegateNode, PosourceNode);
        }
        
        /// <summary>
        /// Unsetup the behavior
        /// </summary>
        public override void Unsetup()
        {
            // Remove references
            PosPosPtr = null;
            NegNegPtr = null;
            PosNegPtr = null;
            NegPosPtr = null;
        }

        /// <summary>
        /// Execute behavior
        /// </summary>
        /// <param name="simulation">Base simulation</param>
        public override void Load(BaseSimulation simulation)
        {
            PosPosPtr.Add(Conductance);
            NegNegPtr.Add(Conductance);
            PosNegPtr.Sub(Conductance);
            NegPosPtr.Sub(Conductance);
        }
    }
}
