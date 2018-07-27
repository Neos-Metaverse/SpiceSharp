﻿using SpiceSharp.Attributes;

namespace SpiceSharp.Components.MosfetBehaviors.Level2
{
    /// <summary>
    /// Base parameters for a <see cref="Mosfet2Model"/>
    /// </summary>
    public class ModelBaseParameters : ParameterSet
    {
        /// <summary>
        /// Parameters
        /// </summary>
        [ParameterName("tnom"), DerivedProperty(), ParameterInfo("Parameter measurement temperature")]
        public double NominalTemperatureCelsius
        {
            get => NominalTemperature - Circuit.CelsiusKelvin;
            set => NominalTemperature.Value = value + Circuit.CelsiusKelvin;
        }
        public GivenParameter<double> NominalTemperature { get; } = new GivenParameter<double>(Circuit.ReferenceTemperature);
        [ParameterName("vto"), ParameterName("vt0"), ParameterInfo("Threshold voltage")]
        public GivenParameter<double> Vt0 { get; } = new GivenParameter<double>();
        [ParameterName("kp"), ParameterInfo("Transconductance parameter")]
        public GivenParameter<double> Transconductance { get; } = new GivenParameter<double>();
        [ParameterName("gamma"), ParameterInfo("Bulk threshold parameter")]
        public GivenParameter<double> Gamma { get; } = new GivenParameter<double>();
        [ParameterName("phi"), ParameterInfo("Surface potential")]
        public GivenParameter<double> Phi { get; } = new GivenParameter<double>(.6);
        [ParameterName("lambda"), ParameterInfo("Channel length modulation")]
        public GivenParameter<double> Lambda { get; } = new GivenParameter<double>();
        [ParameterName("rd"), ParameterInfo("Drain ohmic resistance")]
        public GivenParameter<double> DrainResistance { get; } = new GivenParameter<double>();
        [ParameterName("rs"), ParameterInfo("Source ohmic resistance")]
        public GivenParameter<double> SourceResistance { get; } = new GivenParameter<double>();
        [ParameterName("cbd"), ParameterInfo("B-D junction capacitance")]
        public GivenParameter<double> CapBd { get; } = new GivenParameter<double>();
        [ParameterName("cbs"), ParameterInfo("B-S junction capacitance")]
        public GivenParameter<double> CapBs { get; } = new GivenParameter<double>();
        [ParameterName("is"), ParameterInfo("Bulk junction sat. current")]
        public GivenParameter<double> JunctionSatCur { get; } = new GivenParameter<double>(1e-14);
        [ParameterName("pb"), ParameterInfo("Bulk junction potential")]
        public GivenParameter<double> BulkJunctionPotential { get; } = new GivenParameter<double>(.8);
        [ParameterName("cgso"), ParameterInfo("Gate-source overlap cap.")]
        public GivenParameter<double> GateSourceOverlapCapFactor { get; } = new GivenParameter<double>();
        [ParameterName("cgdo"), ParameterInfo("Gate-drain overlap cap.")]
        public GivenParameter<double> GateDrainOverlapCapFactor { get; } = new GivenParameter<double>();
        [ParameterName("cgbo"), ParameterInfo("Gate-bulk overlap cap.")]
        public GivenParameter<double> GateBulkOverlapCapFactor { get; } = new GivenParameter<double>();
        [ParameterName("cj"), ParameterInfo("Bottom junction cap per area")]
        public GivenParameter<double> BulkCapFactor { get; } = new GivenParameter<double>();
        [ParameterName("mj"), ParameterInfo("Bottom grading coefficient")]
        public GivenParameter<double> BulkJunctionBotGradingCoefficient { get; } = new GivenParameter<double>(.5);
        [ParameterName("cjsw"), ParameterInfo("Side junction cap per area")]
        public GivenParameter<double> SidewallCapFactor { get; } = new GivenParameter<double>();
        [ParameterName("mjsw"), ParameterInfo("Side grading coefficient")]
        public GivenParameter<double> BulkJunctionSideGradingCoefficient { get; } = new GivenParameter<double>(.33);
        [ParameterName("js"), ParameterInfo("Bulk jct. sat. current density")]
        public GivenParameter<double> JunctionSatCurDensity { get; } = new GivenParameter<double>();
        [ParameterName("tox"), ParameterInfo("Oxide thickness")]
        public GivenParameter<double> OxideThickness { get; } = new GivenParameter<double>(1e-7);
        [ParameterName("ld"), ParameterInfo("Lateral diffusion")]
        public GivenParameter<double> LateralDiffusion { get; } = new GivenParameter<double>();
        [ParameterName("rsh"), ParameterInfo("Sheet resistance")]
        public GivenParameter<double> SheetResistance { get; } = new GivenParameter<double>();
        [ParameterName("u0"), ParameterName("uo"), ParameterInfo("Surface mobility")]
        public GivenParameter<double> SurfaceMobility { get; } = new GivenParameter<double>(600);
        [ParameterName("fc"), ParameterInfo("Forward bias jct. fit parm.")]
        public GivenParameter<double> ForwardCapDepletionCoefficient { get; } = new GivenParameter<double>(.5);
        [ParameterName("nsub"), ParameterInfo("Substrate doping")]
        public GivenParameter<double> SubstrateDoping { get; } = new GivenParameter<double>();
        [ParameterName("tpg"), ParameterInfo("Gate type")]
        public GivenParameter<double> GateType { get; } = new GivenParameter<double>();
        [ParameterName("nss"), ParameterInfo("Surface state density")]
        public GivenParameter<double> SurfaceStateDensity { get; } = new GivenParameter<double>();
        [ParameterName("nfs"), ParameterInfo("Fast surface state density")]
        public GivenParameter<double> FastSurfaceStateDensity { get; } = new GivenParameter<double>();
        [ParameterName("delta"), ParameterInfo("Width effect on threshold")]
        public GivenParameter<double> NarrowFactor { get; } = new GivenParameter<double>();
        [ParameterName("uexp"), ParameterInfo("Crit. field exp for mob. deg.")]
        public GivenParameter<double> CriticalFieldExp { get; } = new GivenParameter<double>();
        [ParameterName("vmax"), ParameterInfo("Maximum carrier drift velocity")]
        public GivenParameter<double> MaxDriftVelocity { get; } = new GivenParameter<double>();
        [ParameterName("xj"), ParameterInfo("Junction depth")]
        public GivenParameter<double> JunctionDepth { get; } = new GivenParameter<double>();
        [ParameterName("neff"), ParameterInfo("Total channel charge coeff.")]
        public GivenParameter<double> ChannelCharge { get; } = new GivenParameter<double>(1);
        [ParameterName("ucrit"), ParameterInfo("Crit. field for mob. degradation")]
        public GivenParameter<double> CriticalField { get; } = new GivenParameter<double>(1e4);

        /// <summary>
        /// Methods
        /// </summary>
        [ParameterName("nmos"), ParameterInfo("N type MOSfet model")]
        public void SetNmos(bool value)
        {
            if (value)
                MosfetType = 1.0;
        }
        [ParameterName("pmos"), ParameterInfo("P type MOSfet model")]
        public void SetPmos(bool value)
        {
            if (value)
                MosfetType = -1.0;
        }
        [ParameterName("type"), ParameterInfo("N-channel or P-channel MOS")]
        public string TypeName
        {
            get
            {
                if (MosfetType > 0)
                    return "nmos";
                return "pmos";
            }
        }
        public double MosfetType { get; private set; } = 1.0;
        public double OxideCapFactor { get; private set; }

        /// <summary>
        /// Calculate default parameters
        /// </summary>
        public override void CalculateDefaults()
        {
            OxideCapFactor = 3.9 * 8.854214871e-12 / OxideThickness;
            if (!Transconductance.Given)
                Transconductance.RawValue = SurfaceMobility * 1e-4 * OxideCapFactor;
        }
    }
}
