﻿using System;
using System.Numerics;
using SpiceSharp.Sparse;
using SpiceSharp.Simulations;
using SpiceSharp.Behaviors;

namespace SpiceSharp.Components.MosfetBehaviors.Level2
{
    /// <summary>
    /// AC behavior for a <see cref="Mosfet2"/>
    /// </summary>
    public class FrequencyBehavior : Behaviors.FrequencyBehavior, IConnectedBehavior
    {
        /// <summary>
        /// Necessary behaviors
        /// </summary>
        BaseParameters bp;
        ModelBaseParameters mbp;
        LoadBehavior load;
        TemperatureBehavior temp;
        ModelTemperatureBehavior modeltemp;

        /// <summary>
        /// Shared variables
        /// </summary>
        public double Capgs { get; protected set; }
        public double Capgd { get; protected set; }
        public double Capgb { get; protected set; }
        public double Capbd { get; protected set; }
        public double Capbs { get; protected set; }

        /// <summary>
        /// Nodes
        /// </summary>
        int drainNode, gateNode, sourceNode, bulkNode, drainNodePrime, sourceNodePrime;
        protected MatrixElement DrainDrainPtr { get; private set; }
        protected MatrixElement GateGatePtr { get; private set; }
        protected MatrixElement SourceSourcePtr { get; private set; }
        protected MatrixElement BulkBulkPtr { get; private set; }
        protected MatrixElement DrainPrimeDrainPrimePtr { get; private set; }
        protected MatrixElement SourcePrimeSourcePrimePtr { get; private set; }
        protected MatrixElement DrainDrainPrimePtr { get; private set; }
        protected MatrixElement GateBulkPtr { get; private set; }
        protected MatrixElement GateDrainPrimePtr { get; private set; }
        protected MatrixElement GateSourcePrimePtr { get; private set; }
        protected MatrixElement SourceSourcePrimePtr { get; private set; }
        protected MatrixElement BulkDrainPrimePtr { get; private set; }
        protected MatrixElement BulkSourcePrimePtr { get; private set; }
        protected MatrixElement DrainPrimeSourcePrimePtr { get; private set; }
        protected MatrixElement DrainPrimeDrainPtr { get; private set; }
        protected MatrixElement BulkGatePtr { get; private set; }
        protected MatrixElement DrainPrimeGatePtr { get; private set; }
        protected MatrixElement SourcePrimeGatePtr { get; private set; }
        protected MatrixElement SourcePrimeSourcePtr { get; private set; }
        protected MatrixElement DrainPrimeBulkPtr { get; private set; }
        protected MatrixElement SourcePrimeBulkPtr { get; private set; }
        protected MatrixElement SourcePrimeDrainPrimePtr { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public FrequencyBehavior(Identifier name) : base(name) { }

        /// <summary>
        /// Setup behavior
        /// </summary>
        /// <param name="provider">Data provider</param>
        public override void Setup(SetupDataProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            // Get parameters
            bp = provider.GetParameterSet<BaseParameters>(0);
            mbp = provider.GetParameterSet<ModelBaseParameters>(1);

            // Get behaviors
            temp = provider.GetBehavior<TemperatureBehavior>(0);
            load = provider.GetBehavior<LoadBehavior>(0);
            modeltemp = provider.GetBehavior<ModelTemperatureBehavior>(1);
        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="pins">Pins</param>
        public void Connect(params int[] pins)
        {
            if (pins == null)
                throw new ArgumentNullException(nameof(pins));
            if (pins.Length != 4)
                throw new Diagnostics.CircuitException("Pin count mismatch: 4 pins expected, {0} given".FormatString(pins.Length));
            drainNode = pins[0];
            gateNode = pins[1];
            sourceNode = pins[2];
            bulkNode = pins[3];
        }

        /// <summary>
        /// Get matrix pointers
        /// </summary>
        /// <param name="matrix">Matrix</param>
        public override void GetMatrixPointers(Matrix matrix)
        {
			if (matrix == null)
				throw new ArgumentNullException(nameof(matrix));

            // Get extra equations
            sourceNodePrime = load.SourceNodePrime;
            drainNodePrime = load.DrainNodePrime;

            // Get matrix elements
            DrainDrainPtr = matrix.GetElement(drainNode, drainNode);
            GateGatePtr = matrix.GetElement(gateNode, gateNode);
            SourceSourcePtr = matrix.GetElement(sourceNode, sourceNode);
            BulkBulkPtr = matrix.GetElement(bulkNode, bulkNode);
            DrainPrimeDrainPrimePtr = matrix.GetElement(drainNodePrime, drainNodePrime);
            SourcePrimeSourcePrimePtr = matrix.GetElement(sourceNodePrime, sourceNodePrime);
            DrainDrainPrimePtr = matrix.GetElement(drainNode, drainNodePrime);
            GateBulkPtr = matrix.GetElement(gateNode, bulkNode);
            GateDrainPrimePtr = matrix.GetElement(gateNode, drainNodePrime);
            GateSourcePrimePtr = matrix.GetElement(gateNode, sourceNodePrime);
            SourceSourcePrimePtr = matrix.GetElement(sourceNode, sourceNodePrime);
            BulkDrainPrimePtr = matrix.GetElement(bulkNode, drainNodePrime);
            BulkSourcePrimePtr = matrix.GetElement(bulkNode, sourceNodePrime);
            DrainPrimeSourcePrimePtr = matrix.GetElement(drainNodePrime, sourceNodePrime);
            DrainPrimeDrainPtr = matrix.GetElement(drainNodePrime, drainNode);
            BulkGatePtr = matrix.GetElement(bulkNode, gateNode);
            DrainPrimeGatePtr = matrix.GetElement(drainNodePrime, gateNode);
            SourcePrimeGatePtr = matrix.GetElement(sourceNodePrime, gateNode);
            SourcePrimeSourcePtr = matrix.GetElement(sourceNodePrime, sourceNode);
            DrainPrimeBulkPtr = matrix.GetElement(drainNodePrime, bulkNode);
            SourcePrimeBulkPtr = matrix.GetElement(sourceNodePrime, bulkNode);
            SourcePrimeDrainPrimePtr = matrix.GetElement(sourceNodePrime, drainNodePrime);
        }

        /// <summary>
        /// Unsetup the behavior
        /// </summary>
        public override void Unsetup()
        {
            // Remove references
            DrainDrainPtr = null;
            GateGatePtr = null;
            SourceSourcePtr = null;
            BulkBulkPtr = null;
            DrainPrimeDrainPrimePtr = null;
            SourcePrimeSourcePrimePtr = null;
            DrainDrainPrimePtr = null;
            GateBulkPtr = null;
            GateDrainPrimePtr = null;
            GateSourcePrimePtr = null;
            SourceSourcePrimePtr = null;
            BulkDrainPrimePtr = null;
            BulkSourcePrimePtr = null;
            DrainPrimeSourcePrimePtr = null;
            DrainPrimeDrainPtr = null;
            BulkGatePtr = null;
            DrainPrimeGatePtr = null;
            SourcePrimeGatePtr = null;
            SourcePrimeSourcePtr = null;
            DrainPrimeBulkPtr = null;
            SourcePrimeBulkPtr = null;
            SourcePrimeDrainPrimePtr = null;
        }

        /// <summary>
        /// Initialize AC parameters
        /// </summary>
        /// <param name="simulation"></param>
        public override void InitializeParameters(FrequencySimulation simulation)
        {
			if (simulation == null)
				throw new ArgumentNullException(nameof(simulation));

            double EffectiveLength,
                OxideCap, vgs, vbs, vbd, vgd, von,
                vdsat, sargsw;

            vbs = load.Vbs;
            vbd = load.Vbd;
            vgs = load.Vgs;
            vgd = load.Vgs - load.Vds;
            von = mbp.MosfetType * load.Von;
            vdsat = mbp.MosfetType * load.Vdsat;

            EffectiveLength = bp.Length - 2 * mbp.LatDiff;
            OxideCap = modeltemp.OxideCapFactor * EffectiveLength * bp.Width;

            /* 
            * now we do the hard part of the bulk - drain and bulk - source
            * diode - we evaluate the non - linear capacitance and
            * charge
            * 
            * the basic equations are not hard, but the implementation
            * is somewhat long in an attempt to avoid log / exponential
            * evaluations
            */
            /* 
            * charge storage elements
            * 
            * .. bulk - drain and bulk - source depletion capacitances
            */
            if (vbs < temp.TDepCap)
            {
                double arg = 1 - vbs / temp.TBulkPot, sarg;
                /* 
                * the following block looks somewhat long and messy, 
                * but since most users use the default grading
                * coefficients of .5, and sqrt is MUCH faster than an
                * Math.Exp(Math.Log()) we use this special case code to buy time.
                * (as much as 10% of total job time!)
                */
                if (mbp.BulkJctBotGradingCoeff.Value == mbp.BulkJctSideGradingCoeff)
                {
                    if (mbp.BulkJctBotGradingCoeff.Value == .5)
                    {
                        sarg = sargsw = 1 / Math.Sqrt(arg);
                    }
                    else
                    {
                        sarg = sargsw = Math.Exp(-mbp.BulkJctBotGradingCoeff * Math.Log(arg));
                    }
                }
                else
                {
                    if (mbp.BulkJctBotGradingCoeff.Value == .5)
                    {
                        sarg = 1 / Math.Sqrt(arg);
                    }
                    else
                    {
                        /* NOSQRT */
                        sarg = Math.Exp(-mbp.BulkJctBotGradingCoeff * Math.Log(arg));
                    }
                    if (mbp.BulkJctSideGradingCoeff.Value == .5)
                    {
                        sargsw = 1 / Math.Sqrt(arg);
                    }
                    else
                    {
                        /* NOSQRT */
                        sargsw = Math.Exp(-mbp.BulkJctSideGradingCoeff * Math.Log(arg));
                    }
                }
                // NOSQRT
                Capbs = temp.Cbs * sarg + temp.Cbssw * sargsw;
            }
            else
            {
                Capbs = temp.F2s + temp.F3s * vbs;
            }

            if (vbd < temp.TDepCap)
            {
                double arg = 1 - vbd / temp.TBulkPot, sarg;
                /* 
                * the following block looks somewhat long and messy, 
                * but since most users use the default grading
                * coefficients of .5, and sqrt is MUCH faster than an
                * Math.Exp(Math.Log()) we use this special case code to buy time.
                * (as much as 10% of total job time!)
                */
                if (mbp.BulkJctBotGradingCoeff.Value == .5 && mbp.BulkJctSideGradingCoeff.Value == .5)
                {
                    sarg = sargsw = 1 / Math.Sqrt(arg);
                }
                else
                {
                    if (mbp.BulkJctBotGradingCoeff.Value == .5)
                    {
                        sarg = 1 / Math.Sqrt(arg);
                    }
                    else
                    {
                        /* NOSQRT */
                        sarg = Math.Exp(-mbp.BulkJctBotGradingCoeff * Math.Log(arg));
                    }
                    if (mbp.BulkJctSideGradingCoeff.Value == .5)
                    {
                        sargsw = 1 / Math.Sqrt(arg);
                    }
                    else
                    {
                        /* NOSQRT */
                        sargsw = Math.Exp(-mbp.BulkJctSideGradingCoeff * Math.Log(arg));
                    }
                }
                /* NOSQRT */
                Capbd = temp.Cbd * sarg + temp.Cbdsw * sargsw;
            }
            else
            {
                Capbd = temp.F2d + vbd * temp.F3d;
            }

            /* 
             * calculate meyer's capacitors
             */
            double icapgs, icapgd, icapgb;
            if (load.Mode > 0)
            {
                Transistor.DEVqmeyer(vgs, vgd, von, vdsat,
                    out icapgs, out icapgd, out icapgb, temp.TPhi, OxideCap);
            }
            else
            {
                Transistor.DEVqmeyer(vgd, vgs, von, vdsat,
                    out icapgd, out icapgs, out icapgb, temp.TPhi, OxideCap);
            }
            Capgs = icapgs;
            Capgd = icapgd;
            Capgb = icapgb;
        }

        /// <summary>
        /// Execute behavior for AC analysis
        /// </summary>
        /// <param name="simulation">Frequency-based simulation</param>
        public override void Load(FrequencySimulation simulation)
        {
			if (simulation == null)
				throw new ArgumentNullException(nameof(simulation));

            var state = simulation.State;
            var cstate = state;
            int xnrm, xrev;
            double EffectiveLength, GateSourceOverlapCap, GateDrainOverlapCap, GateBulkOverlapCap, capgs, capgd, capgb, xgs, xgd, xgb, xbd,
                xbs;

            if (load.Mode < 0)
            {
                xnrm = 0;
                xrev = 1;
            }
            else
            {
                xnrm = 1;
                xrev = 0;
            }
            /* 
			 * meyer's model parameters
			 */
            EffectiveLength = bp.Length - 2 * mbp.LatDiff;
            GateSourceOverlapCap = mbp.GateSourceOverlapCapFactor * bp.Width;
            GateDrainOverlapCap = mbp.GateDrainOverlapCapFactor * bp.Width;
            GateBulkOverlapCap = mbp.GateBulkOverlapCapFactor * EffectiveLength;
            capgs = Capgs + Capgs + GateSourceOverlapCap;
            capgd = Capgd + Capgd + GateDrainOverlapCap;
            capgb = Capgb + Capgb + GateBulkOverlapCap;
            xgs = capgs * cstate.Laplace.Imaginary;
            xgd = capgd * cstate.Laplace.Imaginary;
            xgb = capgb * cstate.Laplace.Imaginary;
            xbd = Capbd * cstate.Laplace.Imaginary;
            xbs = Capbs * cstate.Laplace.Imaginary;

            /* 
			 * load matrix
			 */
            GateGatePtr.Add(new Complex(0.0, xgd + xgs + xgb));
            BulkBulkPtr.Add(new Complex(load.Gbd + load.Gbs, xgb + xbd + xbs));
            DrainPrimeDrainPrimePtr.Add(new Complex(temp.DrainConductance + load.Gds + load.Gbd + xrev * (load.Gm + load.Gmbs), xgd + xbd));
            SourcePrimeSourcePrimePtr.Add(new Complex(temp.SourceConductance + load.Gds + load.Gbs + xnrm * (load.Gm + load.Gmbs), xgs + xbs));
            GateBulkPtr.Sub(new Complex(0.0, xgb));
            GateDrainPrimePtr.Sub(new Complex(0.0, xgd));
            GateSourcePrimePtr.Sub(new Complex(0.0, xgs));
            BulkGatePtr.Sub(new Complex(0.0, xgb));
            BulkDrainPrimePtr.Sub(new Complex(load.Gbd, xbd));
            BulkSourcePrimePtr.Sub(new Complex(load.Gbs, xbs));
            DrainPrimeGatePtr.Add(new Complex((xnrm - xrev) * load.Gm, -xgd));
            DrainPrimeBulkPtr.Add(new Complex(-load.Gbd + (xnrm - xrev) * load.Gmbs, -xbd));
            SourcePrimeGatePtr.Sub(new Complex((xnrm - xrev) * load.Gm, xgs));
            SourcePrimeBulkPtr.Sub(new Complex(load.Gbs + (xnrm - xrev) * load.Gmbs, xbs));
            DrainDrainPtr.Add(temp.DrainConductance);
            SourceSourcePtr.Add(temp.SourceConductance);
            DrainDrainPrimePtr.Sub(temp.DrainConductance);
            SourceSourcePrimePtr.Sub(temp.SourceConductance);
            DrainPrimeDrainPtr.Sub(temp.DrainConductance);
            DrainPrimeSourcePrimePtr.Sub(load.Gds + xnrm * (load.Gm + load.Gmbs));
            SourcePrimeSourcePtr.Sub(temp.SourceConductance);
            SourcePrimeDrainPrimePtr.Sub(load.Gds + xrev * (load.Gm + load.Gmbs));
        }
    }
}
