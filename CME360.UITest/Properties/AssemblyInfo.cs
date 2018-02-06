using NUnit.Framework;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("CME360.UITest")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("CME360.UITest")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Note that the following settings can be inserted at the test fixture/class level and also at the test method level, instead of setting the entire project
// in the assembly
// Setting for running tests parallel across fixtures/classes only
[assembly: Parallelizable(ParallelScope.Fixtures)]
// Setting for running tests parallel across test methods
//[assembly: Parallelizable(ParallelScope.Children)]
// Setting the max amount of tests to be run in parallel
[assembly: LevelOfParallelism(12)]

[assembly: ComVisible(false)]

[assembly: Guid("5be1de69-7aff-46cc-88e8-aaf182eb0087")]

// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
