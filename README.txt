OpenLisp.NET
v0.0.1
The Wizard & The Wyrd, LLC

rev: September 19, 2015

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

Language Extensibiity
=====================
The OpenLisp.Core.StaticClasses contain two partial static classes that are the extension points for the OpenLisp.NET language:

	* public static partial class OpenLispSymbols
	Contains public static readonly string literals like @"nil?", etc. 

	* public static partial class CoreNameSpace
	public static IDictionary<string, OpenLispVal> Ns contains keys matching the OpenLispSymbols whose values
	are static methods inheriting from the OpenLispVal class; i.e.,

	        {"nil?",        ScalarFuncs.NilQ},
            {"true?",       ScalarFuncs.TrueQ},
            {"false?",      ScalarFuncs.FalseQ},
            {"symbol",      ScalarFuncs.Symbol},
            {"symbol?",     ScalarFuncs.SymbolQ},
            {"keyword",     ScalarFuncs.Keyword},
            {"keyword?",    ScalarFuncs.KeywordQ},

	This dictionary acts like a function lookup table.  Language extensions will have to add new key/value pairs
	so that the Ast can be parsed and walked to invoke methods.

	Here is an example from the ScalarFuncs static class:

			using OpenLisp.Core.DataTypes;

			namespace OpenLisp.Core.StaticClasses.Funcs
			{
				public class ScalarFuncs
				{
					public static OpenLispFunc NilQ = new OpenLispFunc(x => x[0] 
						== StaticOpenLispTypes.Nil 
						? StaticOpenLispTypes.True 
						: StaticOpenLispTypes.False);
						
				// snipped for brevity
				}
			}
			
	The usual pattern is to assign a new keyword's static method to a new OpenLispFunc, etc.	

Opening this namespace and these classes in 3rd party assemblies allows for language additions.
TODO: provide an easy method for dynamically loading assemblies via reflection without needing to recompile the REPL

Planned Extensions
==================
An intersting field of development is Software-Defined Networking.  Giving the power of a Lisp,
creating a SDN Language Extension via a new assembly should prove interesting; in particular, we will be
implementing the standard SDN architecture at:

  * https://en.wikipedia.org/wiki/Software-defined_networking#Architectural_components	

System Components
=================
TODO: Add architecture diagrams.

IoC Logic (revisit this)
========================
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
