OpenLisp.NET
v0.0.1
The Wizard & The Wyrd, LLC

rev: September 19, 2015

Vision
======
"Create a modern portable LISP Machine using commodity hardware and OpenList.NET."

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
  * .NET 4.5+
  * .NET 3.5 for Cosmos
  * Roslyn
  * Visual Studio 2015

Known Issues
============
  * Cosmos is having trouble building the Kernel on Windows 8.1, and complains about a call to native code with a missing plug:
2>	Error: Exception: System.Exception: Native code encountered, plug required. Please see https://github.com/CosmosOS/Cosmos/wiki/Plugs). System.String  System.Exception.StripFileInfo(System.String, System.Boolean).
2>	 Called from :
2>	System.Exception::System.String GetStackTrace(Boolean)
2>	System.Exception::System.String get_StackTrace()
2>	OpenLisp.Core.StaticClasses.Repl::Void ReplMain(System.String[])
2>	OpenLisp.Machine.Kernel.Kernel::Void Run()
2>	Cosmos.System.Kernel::Void Run()
2>	   at Cosmos.IL2CPU.ILScanner.ScanMethod(MethodBase aMethod, Boolean aIsPlug, String sourceItem) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 525
2>	   at Cosmos.IL2CPU.ILScanner.ScanQueue() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 670
2>	   at Cosmos.IL2CPU.ILScanner.Execute(MethodBase aStartMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 255
2>	   at Cosmos.IL2CPU.CompilerEngine.Execute() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\CompilerEngine.cs:line 238

	A conditional NOSTACKTRACE compilation flag has been added to prevent stack traces in the OpenLisp.Machine OS.


  * Cosmos does not support threading via native threads since threading must be implemented by the OS developer:
5>	Error: Exception: System.Exception: Native code encountered, plug required. Please see https://github.com/CosmosOS/Cosmos/wiki/Plugs). System.Threading.Thread  System.Threading.Thread.GetCurrentThreadNative().
5>	 Called from :
5>	System.Threading.Thread::System.Threading.Thread get_CurrentThread()
5>	OpenLisp.Terminal.LineEditor::System.String Edit(System.String, System.String)
5>	OpenLisp.Core.ReadLine::System.String LineReader(System.String)
5>	OpenLisp.Core.StaticClasses.Repl::Void ReplMain(System.String[])
5>	OpenLisp.Machine.Kernel.Kernel::Void Run()
5>	Cosmos.System.Kernel::Void Run()
5>	   at Cosmos.IL2CPU.ILScanner.ScanMethod(MethodBase aMethod, Boolean aIsPlug, String sourceItem) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 525
5>	   at Cosmos.IL2CPU.ILScanner.ScanQueue() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 670
5>	   at Cosmos.IL2CPU.ILScanner.Execute(MethodBase aStartMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 255
5>	   at Cosmos.IL2CPU.CompilerEngine.Execute() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\CompilerEngine.cs:line 238

	A conditional NONATIVETHREADS compilation flag has been added to wrap native thread calls in the OpenLisp.Machine OS.


  * Cosmos has problems with the Type object's calls to native code, and requires a plug:
6>	Error: Exception: System.Exception: Native code encountered, plug required. Please see https://github.com/CosmosOS/Cosmos/wiki/Plugs). System.Boolean  System.Type.op_Inequality(System.Type, System.Type).
6>	 Called from :
6>	OpenLisp.Core.StaticClasses.StaticOpenLispTypes::Boolean OpenLispEqualQ(OpenLisp.Core.AbstractClasses.OpenLispVal, OpenLisp.Core.AbstractClasses.OpenLispVal)
6>	OpenLisp.Core.StaticClasses.CoreNameSpace+<>c::OpenLisp.Core.AbstractClasses.OpenLispVal <.cctor>b__1_0(OpenLisp.Core.DataTypes.OpenLispList)
6>	OpenLisp.Core.StaticClasses.CoreNameSpace::Void .cctor()
6>	OpenLisp.Core.StaticClasses.CoreNameSpace
6>	   at Cosmos.IL2CPU.ILScanner.ScanMethod(MethodBase aMethod, Boolean aIsPlug, String sourceItem) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 525
6>	   at Cosmos.IL2CPU.ILScanner.ScanQueue() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 670
6>	   at Cosmos.IL2CPU.ILScanner.Execute(MethodBase aStartMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 255
6>	   at Cosmos.IL2CPU.CompilerEngine.Execute() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\CompilerEngine.cs:line 238

	A conditional NOTYPEEQUALITY compilation flag has been added to wrap native Type equality calls.


  * Cosmos doesn't implement the Mkrefany OpCode, and this OpCode is found in System.Reflection.Emit:
5>	Error: Exception: System.NotImplementedException: OpCode 'Mkrefany' not implemented! Encountered in method System.Object CompareExchange[Object](System.Object ByRef, System.Object, System.Object)
5>	   at Cosmos.IL2CPU.ILOpCodes.OpType.GetNumberOfStackPops(MethodBase aMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILOpCodes\OpType.cs:line 52
5>	   at Cosmos.IL2CPU.ILOpCode.InitStackAnalysis(MethodBase aMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILOpCode.cs:line 296
5>	   at Cosmos.IL2CPU.ILReader.ProcessMethod(MethodBase aMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILReader.cs:line 407
5>	   at Cosmos.IL2CPU.ILScanner.ScanMethod(MethodBase aMethod, Boolean aIsPlug, String sourceItem) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 545
5>	   at Cosmos.IL2CPU.ILScanner.ScanQueue() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 670
5>	   at Cosmos.IL2CPU.ILScanner.Execute(MethodBase aStartMethod) in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\ILScanner.cs:line 255
5>	   at Cosmos.IL2CPU.CompilerEngine.Execute() in c:\Data\Sources\OpenSource\Cosmos\source\Cosmos.IL2CPU\CompilerEngine.cs:line 238

	The Cosmos Mkrefany throws a NotImplementedException().  The Mono project has an implementation of a Mkrefany test in ILASM:
	* https://github.com/corngood/mono/blob/ef186403b5e95a5c95c38f1f19d0c8d061f2ac37/mono/tests/generic-mkrefany.2.il

	Here is Mono's Mkrefany implementation:
	* https://github.com/corngood/mono/blob/ef186403b5e95a5c95c38f1f19d0c8d061f2ac37/mcs/class/corlib/System.Reflection.Emit/OpCodes.cs