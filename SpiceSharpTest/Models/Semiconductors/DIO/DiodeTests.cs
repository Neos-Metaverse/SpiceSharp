﻿using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

namespace SpiceSharpTest.Models
{
    /// <summary>
    /// From LTSpice
    /// .model 1N914 D(Is= 2.52n Rs = .568 N= 1.752 Cjo= 4p M = .4 tt= 20n Iave = 200m Vpk= 75 mfg= OnSemi type= silicon)
    /// </summary>
    [TestClass]
    public class DiodeTests : Framework
    {
        /// <summary>
        /// Create a diode with a model
        /// </summary>
        /// <param name="name">Diode name</param>
        /// <param name="anode">Anode</param>
        /// <param name="cathode">Cathode</param>
        /// <param name="model">Model</param>
        /// <param name="modelparams">Model parameters</param>
        /// <returns></returns>
        Diode CreateDiode(Identifier name, Identifier anode, Identifier cathode, Identifier model, string modelparams)
        {
            Diode d = new Diode(name);
            DiodeModel dm = new DiodeModel(model);
            ApplyParameters(dm, modelparams);
            d.SetModel(dm);
            d.Connect(anode, cathode);
            return d;
        }

        [TestMethod]
        public void When_DiodeDC_Expect_Spice3f5Reference()
        {
            /*
             * DC voltage shunted by a diode
             * Current is to behave like the reference
             */

            // Build circuit
            Circuit ckt = new Circuit();
            ckt.Objects.Add(
                CreateDiode("D1", "OUT", "0", "1N914", "Is=2.52e-9 Rs=0.568 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9"),
                new VoltageSource("V1", "OUT", "0", 0.0)
                );

            // Create simulation
            Dc dc = new Dc("DC", "V1", -1.0, 1.0, 10e-3);

            // Create exports
            Export<double>[] exports = { new RealPropertyExport(dc, "V1", "i") };

            // Create reference
            double[][] references =
            {
                new double[] { 2.520684772022719e-09, 2.520665232097485e-09, 2.520645248083042e-09, 2.520624819979389e-09, 2.520603725741921e-09, 2.520582409459848e-09, 2.520560649088566e-09, 2.520538000538863e-09, 2.520515129944556e-09, 2.520491593216434e-09, 2.520467612399102e-09, 2.520442965447955e-09, 2.520417652362994e-09, 2.520391229055008e-09, 2.520364583702417e-09, 2.520336828126801e-09, 2.520307962328161e-09, 2.520278874484916e-09, 2.520248454374041e-09, 2.520216701995537e-09, 2.520184505527823e-09, 2.520150754747874e-09, 2.520115449655691e-09, 2.520079700474298e-09, 2.520041952891461e-09, 2.520002650996389e-09, 2.519962460922898e-09, 2.519919828358752e-09, 2.519875419437767e-09, 2.519829456204548e-09, 2.519781050480674e-09, 2.519730646355356e-09, 2.519677799739384e-09, 2.519621844498943e-09, 2.519563668812452e-09, 2.519502162456888e-09, 2.519437547476855e-09, 2.519369379783143e-09, 2.519297437331147e-09, 2.519221276031658e-09, 2.519140673840070e-09, 2.519055408711779e-09, 2.518964592468365e-09, 2.518868003065222e-09, 2.518765085390839e-09, 2.518655506378309e-09, 2.518538155804606e-09, 2.518412922647428e-09, 2.518278474639146e-09, 2.518133923601340e-09, 2.517978381355590e-09, 2.517810848701174e-09, 2.517629882348160e-09, 2.517434039006616e-09, 2.517221764364308e-09, 2.516991060019791e-09, 2.516739705527016e-09, 2.516465702484538e-09, 2.516165609200982e-09, 2.515836872163391e-09, 2.515475161501968e-09, 2.515076480413825e-09, 2.514635832895351e-09, 2.514147445786818e-09, 2.513604324683172e-09, 2.512998475978634e-09, 2.512320573799798e-09, 2.511559182849510e-09, 2.510700980451475e-09, 2.509729757349533e-09, 2.508625973618450e-09, 2.507366425597013e-09, 2.505921525841615e-09, 2.504256357838130e-09, 2.502326790221332e-09, 2.500077533884593e-09, 2.497439421933478e-09, 2.494324247148683e-09, 2.490618655759391e-09, 2.486175321170236e-09, 2.480800564974572e-09, 2.474236482363779e-09, 2.466134130241215e-09, 2.456014613905211e-09, 2.443208080293857e-09, 2.426758793916406e-09, 2.405272869765440e-09, 2.377086694149710e-09, 2.341755483969976e-09, 2.297702500486665e-09, 2.242774105321033e-09, 2.174284835509965e-09, 2.088886258411193e-09, 1.982402894618041e-09, 1.849628367134315e-09, 1.684070757845824e-09, 1.477634958835239e-09, 1.220227058285062e-09, 8.992606936875092e-10, 4.990415580774510e-10, -4.208324063460023e-23, -6.222658915921997e-10, -1.398183520351370e-09, -2.365693620165477e-09, -3.572105541915782e-09, -5.076410555804323e-09, -6.952166481388744e-09, -9.291094477115180e-09, -1.220756418174318e-08, -1.584418615752092e-08, -2.037878504834723e-08, -2.603309548487864e-08, -3.308360396747645e-08, -4.187506874586688e-08, -5.283737797290300e-08, -6.650657008444583e-08, -8.355104497148602e-08, -1.048042475026989e-07, -1.313054202034536e-07, -1.643504193848955e-07, -2.055550786805860e-07, -2.569342167357824e-07, -3.210001533471285e-07, -4.008855497006358e-07, -5.004965768495850e-07, -6.247039000539800e-07, -7.795808144028804e-07, -9.727001679671332e-07, -1.213504582209257e-06, -1.513768057126441e-06, -1.888171507258285e-06, -2.355020333966173e-06, -2.937139061076621e-06, -3.662986687191783e-06, -4.568047149322574e-06, -5.696562662471649e-06, -7.103694343424394e-06, -8.858215224893939e-06, -1.104586649613992e-05, -1.377353976839135e-05, -1.717448782878606e-05, -2.141481549700064e-05, -2.670156304629412e-05, -3.329276978536466e-05, -4.150999799246158e-05, -5.175391113643180e-05, -6.452363948283857e-05, -8.044083562419591e-05, -1.002795274224200e-04, -1.250031216609715e-04, -1.558102032684916e-04, -1.941911156088105e-04, -2.419976971548277e-04, -3.015289829039203e-04, -3.756361388815854e-04, -4.678503519079946e-04, -5.825377853404534e-04, -7.250859365310891e-04, -9.021256392514054e-04, -1.121792314356274e-03, -1.394028558000970e-03, -1.730927332259435e-03, -2.147110349238757e-03, -2.660129130047428e-03, -3.290866177790397e-03, -4.063900589218683e-03, -5.007786885759424e-03, -6.155179858715831e-03, -7.542725667060601e-03, -9.210636215301937e-03, -1.120187715360643e-02, -1.356093587232876e-02, -1.633219609264214e-02, -1.955802291413677e-02, -2.327673904312388e-02, -2.752072521737903e-02, -3.231488373640667e-02, -3.767565498752745e-02, -4.361068391378264e-02, -5.011912351695047e-02, -5.719246701131020e-02, -6.481574162201031e-02, -7.296888192813844e-02, -8.162812138207687e-02, -9.076728201979800e-02, -1.003588891563969e-01, -1.103750792457605e-01, -1.207882998568939e-01, -1.315718201008491e-01, -1.427000796200868e-01, -1.541489071231517e-01, -1.658956380366401e-01, -1.779191572005734e-01, -1.901998880621483e-01, -2.027197453741645e-01, -2.154620644191900e-01, -2.284115164341036e-01, -2.415540172223232e-01, -2.548766338536659e-01, -2.683674927728656e-01, -2.820156914701786e-01 }
            };

            // Run test
            AnalyzeDC(dc, ckt, exports, references);
        }

        [TestMethod]
        public void When_DiodeSmallSignal_Expect_Spice3f5Reference()
        {
            /*
             * DC voltage source shunted by a diode
             * Current is expected to behave like the reference
             */
            // Build circuit
            Circuit ckt = new Circuit();
            ckt.Objects.Add(
                CreateDiode("D1", "0", "OUT", "1N914", "Is=2.52e-9 Rs=0.568 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9"),
                new VoltageSource("V1", "OUT", "0", 1.0)
                );
            ckt.Objects["V1"].ParameterSets.SetProperty("acmag", 1.0);

            // Create simulation
            Ac ac = new Ac("ac", new SpiceSharp.Simulations.Sweeps.DecadeSweep(1e3, 10e6, 5));

            // Create exports
            Export<Complex>[] exports = { new ComplexPropertyExport(ac, "V1", "i") };

            // Create references
            double[] ri_ref = { -1.945791742986885e-12, -1.904705637099517e-08, -1.946103289747125e-12, -3.018754997881332e-08, -1.946885859826953e-12, -4.784404245850086e-08, -1.948851586992178e-12, -7.582769719229839e-08, -1.953789270386556e-12, -1.201788010800761e-07, -1.966192170307985e-12, -1.904705637099495e-07, -1.997346846331992e-12, -3.018754997881245e-07, -2.075603854314768e-12, -4.784404245849736e-07, -2.272176570837208e-12, -7.582769719228451e-07, -2.765944910274710e-12, -1.201788010800207e-06, -4.006234902415568e-12, -1.904705637097290e-06, -7.121702504803603e-12, -3.018754997872460e-06, -1.494740330300116e-11, -4.784404245814758e-06, -3.460467495474045e-11, -7.582769719089195e-06, -8.398150889530617e-11, -1.201788010744768e-05, -2.080105080892987e-10, -1.904705636876583e-05, -5.195572682013223e-10, -3.018754996993812e-05, -1.302127347221150e-09, -4.784404242316795e-05, -3.267854507347871e-09, -7.582769705163549e-05, -8.205537869558709e-09, -1.201788005200868e-04, -2.060843758802494e-08, -1.904705614805916e-04 };
            Complex[][] references = new Complex[1][];
            references[0] = new Complex[ri_ref.Length / 2];
            for (int i = 0; i < ri_ref.Length; i += 2)
            {
                references[0][i / 2] = new Complex(ri_ref[i], ri_ref[i + 1]);
            }

            // Run test
            AnalyzeAC(ac, ckt, exports, references);
        }

        [TestMethod]
        public void When_DiodeRectifier_Expect_Spice3f5Reference()
        {
            /*
             * Pulsed voltage source towards a resistive voltage divider between 0V and 5V
             * Output voltage is expected to behavior like the reference
             */
            // Build circuit
            Circuit ckt = new Circuit();
            ckt.Objects.Add(
                new VoltageSource("V1", "in", "0", new Pulse(0, 5, 1e-6, 10e-9, 10e-9, 1e-6, 2e-6)),
                new VoltageSource("Vsupply", "vdd", "0", 5.0),
                new Resistor("R1", "vdd", "out", 10.0e3),
                new Resistor("R2", "out", "0", 10.0e3),
                CreateDiode("D1", "in", "out", "1N914", "Is = 2.52e-9 Rs = 0.568 N = 1.752 Cjo = 4e-12 M = 0.4 tt = 20e-9"));

            // Create simulation
            Transient tran = new Transient("tran", 1e-9, 10e-6);

            // Create exports
            Export<double>[] exports = { new RealVoltageExport(tran, "out") };

            // Create references
            double[][] references =
            {
                new double[] { 2.499987387600927e+00, 2.499987387600927e+00, 2.499987387600927e+00, 2.499987387600931e+00, 2.499987387600930e+00, 2.499987387600930e+00, 2.499987387600929e+00, 2.499987387600930e+00, 2.499987387600933e+00, 2.499987387600932e+00, 2.499987387600934e+00, 2.499987387600934e+00, 2.499987387600928e+00, 2.499987387600928e+00, 2.499987387600927e+00, 2.499987387600927e+00, 2.499987387600926e+00, 2.499987387600928e+00, 2.499987387600927e+00, 2.499987387600927e+00, 2.499987387600927e+00, 2.961897877874934e+00, 3.816739529558831e+00, 5.191526587205253e+00, 6.033962809915079e+00, 5.858560892037079e+00, 5.542221840643947e+00, 5.047948278928924e+00, 4.566221446174492e+00, 4.496222403679634e+00, 4.470638440255675e+00, 4.461747124949804e+00, 4.458828418109562e+00, 4.458043432344522e+00, 4.458104168570753e+00, 4.458101940017489e+00, 4.458103007267981e+00, 4.458102313327546e+00, 4.458102774372732e+00, 4.458102484881135e+00, 3.958696587137276e+00, 2.960963748330885e+00, 9.726613059613628e-01, -5.081214667849954e-01, -5.008294438061146e-01, -4.823457266305946e-01, -4.093596340082378e-01, 2.927533877100476e-01, 1.580990201373001e+00, 2.297731671229221e+00, 2.465955512323740e+00, 2.496576390164448e+00, 2.500278606332533e+00, 2.499872222320282e+00, 2.500061542030393e+00, 2.499929263366373e+00, 2.500032946810125e+00, 2.499951677124709e+00, 2.500009659840817e+00, 2.961918369398067e+00, 3.816756624765787e+00, 5.191537959402029e+00, 6.033971024560395e+00, 5.858568428923525e+00, 5.542228175717445e+00, 5.047952819852995e+00, 4.566222065264992e+00, 4.496222553506734e+00, 4.470638537994252e+00, 4.461747150642569e+00, 4.458828425715773e+00, 4.458043431871857e+00, 4.458104168387089e+00, 4.458101940091086e+00, 4.458103007220202e+00, 4.458102313359292e+00, 4.458102774351643e+00, 4.458102484892605e+00, 3.958696587148540e+00, 2.960963748341582e+00, 9.726613059724690e-01, -5.081214667705706e-01, -5.008294437900119e-01, -4.823457266089376e-01, -4.093596339411046e-01, 2.927533879025108e-01, 1.580990201486391e+00, 2.297731671249843e+00, 2.465955512329101e+00, 2.496576390165203e+00, 2.500278606332566e+00, 2.499872222320265e+00, 2.500061542030405e+00, 2.499929263366364e+00, 2.500032946810133e+00, 2.499951677124703e+00, 2.500009659840779e+00, 2.961918369398032e+00, 3.816756624765757e+00, 5.191537959401995e+00, 6.033971024560496e+00, 5.858568428923665e+00, 5.542228175717561e+00, 5.047952819853082e+00, 4.566222065265002e+00, 4.496222553506737e+00, 4.470638537994254e+00, 4.461747150642569e+00, 4.458828425715771e+00, 4.458043431871858e+00, 4.458104168387090e+00, 4.458101940091087e+00, 4.458103007220201e+00, 4.458102313359291e+00, 4.458102774351643e+00, 4.458102484892708e+00, 3.958696587148749e+00, 2.960963748341791e+00, 9.726613059726750e-01, -5.081214667704262e-01, -5.008294437900225e-01, -4.823457266089518e-01, -4.093596339411494e-01, 2.927533879023823e-01, 1.580990201486315e+00, 2.297731671249816e+00, 2.465955512329098e+00, 2.496576390165200e+00, 2.500278606332570e+00, 2.499872222320263e+00, 2.500061542030407e+00, 2.499929263366362e+00, 2.500032946810134e+00, 2.499951677124702e+00, 2.500009659840780e+00, 2.961918369398029e+00, 3.816756624765742e+00, 5.191537959401949e+00, 6.033971024560461e+00, 5.858568428923622e+00, 5.542228175717498e+00, 5.047952819852998e+00, 4.566222065264983e+00, 4.496222553506735e+00, 4.470638537994251e+00, 4.461747150642568e+00, 4.458828425715771e+00, 4.458043431871856e+00, 4.458104168387091e+00, 4.458101940091087e+00, 4.458103007220201e+00, 4.458102313359291e+00, 4.458102774351643e+00, 4.458102484892708e+00, 3.958696587148324e+00, 2.960963748341793e+00, 9.726613059726769e-01, -5.081214667704226e-01, -5.008294437900186e-01, -4.823457266089463e-01, -4.093596339411322e-01, 2.927533879024313e-01, 1.580990201486346e+00, 2.297731671249819e+00, 2.465955512329098e+00, 2.496576390165201e+00, 2.500278606332567e+00, 2.499872222320263e+00, 2.500061542030407e+00, 2.499929263366362e+00, 2.500032946810135e+00, 2.499951677124702e+00, 2.500009659840780e+00, 2.961918369398421e+00, 3.816756624765698e+00, 5.191537959401959e+00, 6.033971024560470e+00, 5.858568428923640e+00, 5.542228175717541e+00, 5.047952819853064e+00, 4.566222065265000e+00, 4.496222553506738e+00, 4.470638537994254e+00, 4.461747150642569e+00, 4.458828425715772e+00, 4.458043431871857e+00, 4.458104168387089e+00, 4.458101940091087e+00, 4.458103007220202e+00, 4.458102313359291e+00, 4.458102774351643e+00, 4.458102489285038e+00 }
            };

            // Run test
            AnalyzeTransient(tran, ckt, exports, references);
        }

        [TestMethod]
        public void When_DiodeNoise_Expect_Spice3f5Reference()
        {
            // Build the circuit
            Circuit ckt = new Circuit();
            ckt.Objects.Add(
                new VoltageSource("V1", "in", "0", 1.0),
                new Resistor("R1", "in", "out", 10e3),
                CreateDiode("D1", "out", "0", "1N914", "Is= 2.52e-9 Rs = 0.568 N= 1.752 Cjo= 4e-12 M = 0.4 tt= 20e-9 Kf=1e-14 Af=0.9"));
            ckt.Objects["V1"].ParameterSets.SetProperty("acmag", 1.0);

            // Create the noise, exports and reference values
            Noise noise = new Noise("Noise", "out", "V1", new SpiceSharp.Simulations.Sweeps.DecadeSweep(10, 10e9, 10));
            Export<double>[] exports = { new InputNoiseDensityExport(noise), new OutputNoiseDensityExport(noise) };
            double[][] references =
            {
                new double[] { 1.458723146141516e-11, 1.158744449455564e-11, 9.204629008621217e-12, 7.311891390005244e-12, 5.808436458613778e-12, 4.614199756974079e-12, 3.665583825917668e-12, 2.912071407970298e-12, 2.313535219179339e-12, 1.838101024918417e-12, 1.460450220663579e-12, 1.160471523977627e-12, 9.221899753841851e-13, 7.329162135225879e-13, 5.825707203834413e-13, 4.631470502194715e-13, 3.682854571138305e-13, 2.929342153190935e-13, 2.330805964399977e-13, 1.855371770139055e-13, 1.477720965884216e-13, 1.177742269198265e-13, 9.394607206048226e-14, 7.501869587432254e-14, 5.998414656040787e-14, 4.804177954401090e-14, 3.855562023344681e-14, 3.102049605397311e-14, 2.503513416606353e-14, 2.028079222345432e-14, 1.650428418090594e-14, 1.350449721404642e-14, 1.112168172811200e-14, 9.228944109496031e-15, 7.725489178104568e-15, 6.531252476464870e-15, 5.582636545408461e-15, 4.829124127461094e-15, 4.230587938670135e-15, 3.755153744409214e-15, 3.377502940154375e-15, 3.077524243468425e-15, 2.839242694874982e-15, 2.649968933013386e-15, 2.499623439874239e-15, 2.380199769710269e-15, 2.285338176604629e-15, 2.209986934809892e-15, 2.150133315930796e-15, 2.102589896504704e-15, 2.064824816079220e-15, 2.034826946410625e-15, 2.010998791551281e-15, 1.992071415365121e-15, 1.977036866051207e-15, 1.965094499034810e-15, 1.955608339724246e-15, 1.948073215544772e-15, 1.942087853656863e-15, 1.937333511714254e-15, 1.933557003671705e-15, 1.930557216704845e-15, 1.928174401218911e-15, 1.926281663600295e-15, 1.924778208668904e-15, 1.923583971967264e-15, 1.922635356036208e-15, 1.921881843618260e-15, 1.921283307429469e-15, 1.920807873235208e-15, 1.920430222430954e-15, 1.920130243734267e-15, 1.919891962185674e-15, 1.919702688423812e-15, 1.919552342930673e-15, 1.919432919260510e-15, 1.919338057667404e-15, 1.919262706425609e-15, 1.919202852806730e-15, 1.919155309387304e-15, 1.919117544306878e-15, 1.919087546437210e-15, 1.919063718282350e-15, 1.919044790906164e-15, 1.919029756356850e-15, 1.919017813989834e-15, 1.919008327830523e-15, 1.919000792706344e-15, 1.918994807344456e-15, 1.918990053002513e-15, 1.918986276494471e-15 },
                new double[] { 8.534638344391308e-14, 6.779535126890105e-14, 5.385407086373468e-14, 4.278011820970267e-14, 3.398376494660514e-14, 2.699657318711771e-14, 2.144644949112401e-14, 1.703782953318393e-14, 1.353593822442315e-14, 1.075428708293890e-14, 8.544743042104929e-15, 6.789639824603726e-15, 5.395511784087090e-15, 4.288116518683889e-15, 3.408481192374137e-15, 2.709762016425394e-15, 2.154749646826024e-15, 1.713887651032016e-15, 1.363698520155939e-15, 1.085533406007514e-15, 8.645790019241169e-16, 6.890686801739967e-16, 5.496558761223330e-16, 4.389163495820129e-16, 3.509528169510376e-16, 2.810808993561634e-16, 2.255796623962265e-16, 1.814934628168257e-16, 1.464745497292180e-16, 1.186580383143755e-16, 9.656259790603582e-17, 7.901156573102381e-17, 6.507028532585746e-17, 5.399633267182545e-17, 4.519997940872793e-17, 3.821278764924051e-17, 3.266266395324682e-17, 2.825404399530675e-17, 2.475215268654597e-17, 2.197050154506173e-17, 1.976095750422776e-17, 1.800585428672656e-17, 1.661172624620993e-17, 1.550433098080673e-17, 1.462469565449698e-17, 1.392597647854824e-17, 1.337096410894887e-17, 1.293010211315486e-17, 1.257991298227878e-17, 1.230174786813036e-17, 1.208079346404696e-17, 1.190528314229684e-17, 1.176587033824518e-17, 1.165513081170486e-17, 1.156716727907389e-17, 1.149729536147901e-17, 1.144179412451907e-17, 1.139770792493967e-17, 1.136268901185206e-17, 1.133487250043722e-17, 1.131277706002888e-17, 1.129522602785387e-17, 1.128128474744870e-17, 1.127021079479467e-17, 1.126141444153157e-17, 1.125442724977209e-17, 1.124887712607609e-17, 1.124446850611815e-17, 1.124096661480939e-17, 1.123818496366791e-17, 1.123597541962708e-17, 1.123422031640957e-17, 1.123282618836906e-17, 1.123171879310365e-17, 1.123083915777734e-17, 1.123014043860140e-17, 1.122958542623180e-17, 1.122914456423600e-17, 1.122879437510513e-17, 1.122851620999098e-17, 1.122829525558689e-17, 1.122811974526514e-17, 1.122798033246109e-17, 1.122786959293455e-17, 1.122778162940192e-17, 1.122771175748433e-17, 1.122765625624737e-17, 1.122761217004779e-17, 1.122757715113470e-17, 1.122754933462328e-17, 1.122752723918288e-17 }
            };
            AnalyzeNoise(noise, ckt, exports, references);
        }
    }
}
