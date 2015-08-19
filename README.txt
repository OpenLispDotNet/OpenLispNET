OpenLisp.NET
v0.0.1
The Wizard & The Wyrd, LLC

rev: August 19, 2015

Vision
======
"Create a modern portable LISP Machine using custom hardware and OpenList.NET."

The goal of OpenLisp.NET is to form the basis of an OpenLisp.NET EMACS-like environment as the primary shell of an
operating system created on CosmosOS (https://github.com/CosmosOS/Cosmos).  The target hardware for this project
includes, as primary build targets, an NetDuino and RaspberryPI, and other tiny devices.  Amongst these devices,
we will support virtual machines with the goal of creating a portable OpenLisp.NET operating system.

Summary
=======
OpenLisp.NET is a tiny Clojure-like LISP written in C# and targeting the .NET platform.
The OpenLisp.Core library contains all datatypes, interfaces, classes, utility functions to parse OpenLisp S-Expressions.

Why another LISP?
=================
A valid question!  OpenLisp.NET is primarily an internal research project with the aims of writing a blazingly fast
LISP in managed code targeting .NET/CoreCLR.  Should the research prove fruitful, OpenLisp.NET may be used as an internal
scripting language, or as a standalone language.  Eventually, OpenLisp.NET should be able to emit IL assemblies.

Any References?
===============
Please read the following items:

  * https://github.com/kanaka/mal/blob/master/cs/
  * http://www-formal.stanford.edu/jmc/recursive/recursive.html

Current Requirements
====================
  * .NET 4.5.2+
  * Roslyn
