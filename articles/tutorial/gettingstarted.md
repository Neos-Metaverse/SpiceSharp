# Getting started
In this section we will try to quickly go over everything you need to create a simple circuit and simulate it using Spice#.

## Installation

The easiest way to install Spice# is by installing the NuGet package Spice#.

[![NuGet Badge](https://buildstats.info/nuget/spicesharp)](https://www.nuget.org/packages/SpiceSharp/)

You can also clone the repository directly. However, while you get the latest features and bug fixes, the documentation might not be up to date!

| Platform | Status |
|:---------|-------:|
| AppVeyor CI (Windows) build status | [![Build status](https://ci.appveyor.com/api/projects/status/tg6q7y8m5725g8ou/branch/master?svg=true)](https://ci.appveyor.com/project/SpiceSharp/spicesharp/branch/master) |
| Travis CI (Linux/Mono) build status | [![Build Status](https://travis-ci.org/SpiceSharp/SpiceSharp.svg?branch=master)](https://travis-ci.org/SpiceSharp/SpiceSharp) |

## Building a circuit
Let's start with a very simple circuit known as a *resistive voltage divider*. The schematic looks as follows.

<p align="center"><img src="images/example01.svg" width="256px" /></p>

The output voltage of this circuit is 2/3 times the input voltage.

Creating this circuit is done using the **[Circuit](xref:SpiceSharp.Circuit)**-class. This is a container of multiple so-called entities (**[IEntity](xref:SpiceSharp.Entities.IEntity)**), such as voltage sources and resistors. The **[Circuit](xref:SpiceSharp.Circuit)**-class is defined in the namespace *@SpiceSharp*, while all default components are typically specified in the namespace *@SpiceSharp.Components*.

[!code-csharp[Circuit](../../SpiceSharpTest/BasicExampleTests.cs#example01_build)]

## Running a DC analysis on the circuit

A **[DC](xref:SpiceSharp.Simulations.DC)** simulation will (by default) sweep a voltage or current source value and it will assume a "quiet" circuit. The result is a DC transfer curve in function of the swept parameter.

We will sweep the input voltage source from -1V to 1V in steps of 200mV.

[!code-csharp[Simulation](../../SpiceSharpTest/BasicExampleTests.cs#example01_simulate)]

By default, access to simulation output data can be achieved by registering to the *[](xref:SpiceSharp.Simulations.IEventfulSimulation.ExportSimulationData)* event. This event is automatically fired by the simulation when the data is ready to be read.

The output will show:

```
-1 V : -0.667 V
-0.8 V : -0.533 V
-0.6 V : -0.4 V
-0.4 V : -0.267 V
-0.2 V : -0.133 V
0 V : 0 V
0.2 V : 0.133 V
0.4 V : 0.267 V
0.6 V : 0.4 V
0.8 V : 0.533 V
1 V : 0.667 V
```

## Using exports

Using **[Export<T>](xref:SpiceSharp.Simulations.Export`1)** gives faster and more access to circuit properties. These exports also allow easier access to properties of components. For example, we could be interested in the current through voltage source V1. In which case we can define some exports:

[!code-csharp[Simulation](../../SpiceSharpTest/BasicExampleTests.cs#example01_simulate2)]

This will lead to the result:

```
-1 V : -0.667 V. 3.33E-05 A
-0.8 V : -0.533 V. 2.67E-05 A
-0.6 V : -0.4 V. 2E-05 A
-0.4 V : -0.267 V. 1.33E-05 A
-0.2 V : -0.133 V. 6.67E-06 A
0 V : 0 V. 0 A
0.2 V : 0.133 V. -6.67E-06 A
0.4 V : 0.267 V. -1.33E-05 A
0.6 V : 0.4 V. -2E-05 A
0.8 V : 0.533 V. -2.67E-05 A
1 V : 0.667 V. -3.33E-05 A
```

These export classes can setup automatically. During setup, these classes build a method that allow extracting the data more efficiently during the *[ExportSimulationData](xref:SpiceSharp.Simulations.IEventfulSimulation.ExportSimulationData)* event than using the event arguments.
