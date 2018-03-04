﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

namespace SpiceSharpTest.Models
{
    [TestClass]
    public class VoltageSwitchTests : Framework
    {
        /// <summary>
        /// Create a voltage switch
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="pos">Positive node</param>
        /// <param name="neg">Negative node</param>
        /// <param name="contPos">Controlling positive node</param>
        /// <param name="contNeg">Controlling negative node</param>
        /// <param name="modelName">Model name</param>
        /// <param name="modelParameters">Model parameters</param>
        /// <returns></returns>
        VoltageSwitch CreateVoltageSwitch(Identifier name, Identifier pos, Identifier neg, Identifier contPos, Identifier contNeg, Identifier modelName, string modelParameters)
        {
            VoltageSwitchModel model = new VoltageSwitchModel(modelName);
            ApplyParameters(model, modelParameters);

            VoltageSwitch vsw = new VoltageSwitch(name, pos, neg, contPos, contNeg);
            vsw.SetModel(model);
            return vsw;
        }

        [TestMethod]
        public void When_VSWSwitchDC_Expect_Spice3f5Reference()
        {
            // NOTE: The hysteresis is chosen such that it does not switch on the same point as a sweep. If that happens, then the smallest
            // numerical error can lead to a big output change, causing a mismatch between the reference.

            // Build the circuit
            Circuit ckt = new Circuit();
            ckt.Objects.Add(
                CreateVoltageSwitch("S1", "out", "0", "in", "0", "myswitch", "VT=0.5 RON=1 ROFF=1e3 VH=0.2001"),
                new VoltageSource("V1", "in", "0", 0),
                new VoltageSource("V2", "vdd", "0", 5.0),
                new Resistor("R1", "vdd", "out", 1e3)
                );

            // Create the simulation, exports and references
            Dc dc = new Dc("DC", "V1", -3, 3, 10e-3);
            Export<double>[] exports = { new RealVoltageExport(dc, "out") };
            double[][] references =
            {
                new double[] { 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 2.500000000000000e+00, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03, 4.995004995004996e-03 }
            };
            AnalyzeDC(dc, ckt, exports, references);
        }
    }
}
