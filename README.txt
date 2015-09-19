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
OpenLisp.NET aims to be compatible with the Common Lisp standard library while providing first class for .NET objects.
OpenLisp.NET is a tiny Clojure-like LISP written in C# and targeting the .NET platform; meaning: OpenLisp.NET will use Software 
Transactional Memory and have first class support for actor based concurrency and immutable collections.
The OpenLisp.Core library contains all datatypes, interfaces, classes, utility functions to parse OpenLisp S-Expressions.

Why another LISP?
=================
A valid question!  OpenLisp.NET is primarily an internal research project with the aims of writing a blazingly fast
LISP in managed code targeting .NET/CoreCLR.  Should the research prove fruitful, OpenLisp.NET may be used as an internal
scripting language, or as a standalone language.  Eventually, OpenLisp.NET should be able to emit IL assemblies.

System Components
=================

IoC Logic
=========
Clients are constructed by ClientProviders.
Services are constructed by ServiceProviders.
Contracts are constructed by ContractProviders.
ClientProviders are constructed by Injectors that provide Contracts that are agreed upon between Client and ServiceProvider.
ServiceProviders are constructed by Injectors that provie Contracts that are agreed upon between ServiceProvider and Client.
ContractProviders are constructed by Injectors and provided to Clients and Services.
Clients and Services communicate via ServiceBuses constructed by Injectors.

Any References?
===============
Please read the following items:

  * https://github.com/kanaka/mal/blob/master/cs/
  * http://www-formal.stanford.edu/jmc/recursive/recursive.html
  * https://en.wikipedia.org/wiki/Dependency_injection

Current Requirements
====================
  * .NET 4.5.2+
  * Roslyn
