﻿using System;
using SpiceSharp.Diagnostics;
using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;

namespace SpiceSharp.Components.ResistorBehaviors
{
    /// <summary>
    /// Temperature behavior for a <see cref="Resistor"/>
    /// </summary>
    public class TemperatureBehavior : Behaviors.TemperatureBehavior
    {
        /// <summary>
        /// Necessary parameters
        /// </summary>
        ModelBaseParameters _mbp;
        BaseParameters _bp;

        /// <summary>
        /// Gets the default conductance for this model
        /// </summary>
        public double Conductance { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public TemperatureBehavior(Identifier name) : base(name) { }

        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="provider"></param>
        public override void Setup(SetupDataProvider provider)
        {
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

            // Get parameters
            _bp = provider.GetParameterSet<BaseParameters>("entity");
            if (!_bp.Resistance.Given)
                _mbp = provider.GetParameterSet<ModelBaseParameters>("model");
        }
        
        /// <summary>
        /// Execute behavior
        /// </summary>
        /// <param name="simulation">Base simulation</param>
        public override void Temperature(BaseSimulation simulation)
        {
			if (simulation == null)
				throw new ArgumentNullException(nameof(simulation));

            double factor;
            double difference;
            double reSresist = _bp.Resistance;

            // Default Value Processing for Resistor Instance
            if (!_bp.Temperature.Given)
                _bp.Temperature.Value = simulation.RealState.Temperature;
            if (!_bp.Width.Given)
                _bp.Width.Value = _mbp?.DefaultWidth ?? 0.0;

            if (_mbp != null)
            {
                if (_mbp.SheetResistance.Given && (_mbp.SheetResistance != 0) && (_bp.Length != 0))
                    reSresist = _mbp.SheetResistance * (_bp.Length - _mbp.Narrow) / (_bp.Width - _mbp.Narrow);
                else
                {
                    CircuitWarning.Warning(this, "{0}: resistance=0, set to 1000".FormatString(Name));
                    reSresist = 1000;
                }

                difference = _bp.Temperature - _mbp.NominalTemperature;
                factor = 1.0 + (_mbp.TemperatureCoefficient1) * difference + (_mbp.TemperatureCoefficient2) * difference * difference;
            }
            else
            {
                difference = _bp.Temperature - 300.15;
                factor = 1.0;
            }

            Conductance = 1.0 / (reSresist * factor);
        }
    }
}
