using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using static System.Console;
using Microsoft.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis.Emit;
using System.Runtime.Loader;

namespace Compiler
{
    /* SYNTAX
     * 
     * This class basically have all the syntaxis for X#
     * Yeah
     *
     */
    public class Syntax
    {
        public static void Maine(string[] args)
        {
            CommonCompiler.Compile("Classer.Subclasser",@"D:\X#\XSharp\XSharp\bin\Debug\net5.0\Test.xs", "test");
        }
        const string obj_declaration = "::";
        const string and_not = "!&";
        const string import = "import";
        
        const string voidify = "";
        // ACCESS MODIFIERS
        const string independant = "indep"; // not depends on current env, equivalent of public
        const string constraint = "constr"; // private. only available in current class
        const string immovable = "immov"; // equivalent of static
        const string movable = "movable";
        const string constant = "constant"; // equivalent of const
        const string superclass = "superclass"; // namespace equivalent
        // SIMPLE TYPES
        const string _str = "str";
        const string _int = "integer32";
        const string _long = "integer64";

        // SOMETHING COMPLEX
        const string function = "funct:";
        const string nullifier = "[Nullify]";
        const string neccessity = "[Neccessity]";
        const string objectify = "[Objectify]";
        const string _return = "recurse";
        const string self = "self";

        const string loop = "loop";
        const string match = "match";
        const string ifis = "ifis";
        const string ifnot = "ifnot(";
        const string send = "Send";
        // SIMPLE 32 BIT INTEGER DECLARATION: 
        // indep mov var: integer32 test = 394145
        // A BIT MORE COMPLEX DECLARATIONS:
        // constr immov var: list{5} str test = ["a", "b", "c", "d", "e"]?


        
        // EXTRA STUFF
        public SortedDictionary<string, string> pairs = new SortedDictionary<string, string>();
        
        public Syntax()
        {
            pairs.Add(send, "Console.WriteLine");
            pairs.Add(match, "switch");
            pairs.Add(loop, "for");
            pairs.Add(objectify, "return new object");
            pairs.Add(self, "this");
            pairs.Add(_return, "return");
            pairs.Add(obj_declaration, "object ");
            pairs.Add(neccessity, "// COMPILED BY X# COMMON COMPILER\nusing System");
            pairs.Add(and_not, "!&&");
            pairs.Add(independant, "public");
            pairs.Add(constraint, "private");
            pairs.Add(immovable, "static");
            pairs.Add(movable, "");
            pairs.Add(constant, "const");
            pairs.Add(_str, "string");
            pairs.Add(_int, "int");
            pairs.Add(_long, "long");
            pairs.Add(superclass, "namespace");
            pairs.Add(import, "using");
            pairs.Add(function, "object ");
            pairs.Add(nullifier, "return null");
        }
    }

    public class CommonCompiler 
    {
        public static Syntax logic = new Syntax();
        public static string classname = "";

       
        public static void Exec(string dataLines, string class_name, string args)
        {
            WriteLine("Starting X# Compiler...");
            Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(dataLines);

            string assemblyName = Path.GetRandomFileName();
            var refPaths = new[] {
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll")
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            WriteLine("Adding the following references");
            foreach (var r in refPaths)
                WriteLine(r);

            Console.WriteLine("Compiling Code...");
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    WriteLine("Compilation failed!");
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    WriteLine("Compilation successful! Now instantiating and executing the code ...");
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType(class_name);
                    var instance = assembly.CreateInstance(class_name);
                    var meth = type.GetMember("main").First() as MethodInfo;
                    Console.WriteLine("======================================================================");
                    string o =
@"
























 >
";
                    Console.WriteLine(o);
                    meth.Invoke(instance, new[] { args });
                }
            }

        }

        public static string Replace(string original)
        {
            original.Replace("\n", " ");
            Regex.Replace(original, "\\s+\"", " ");
            original.Replace("\r", " ");
            string[] split = original.Split(" ");
            string[] keys = new List<string>(logic.pairs.Keys).ToArray();
            string[] vals = new List<string>(logic.pairs.Values).ToArray();
            for (int i = 0; i < split.Length; i++)
            {
                split[i].Replace("\n", " ");
                Regex.Replace(split[i], "\\s+\"", " ");
                split[i].Replace("\r", " ");
                string[] subsplit = split[i].Split(" ");
                for(int j = 0; j < subsplit.Length; j++)
                {
                    for (int u = 0; u < keys.Length; u++) 
                    {
                        subsplit[j] = subsplit[j].Replace(keys[u], vals[u]);
                    };
                }
                try { split[i] = string.Join(" ", subsplit); }
                catch(IndexOutOfRangeException ex) { };
            }
            return string.Join(" ", split);
        }

        public static string ReadFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            return string.Join("\n", lines);
        }

        public static void Compile(string class_name, string filepath, string arg)
        {
            classname = class_name;
            string lines = ReadFile(filepath);
            string encoded = Replace(lines);
            Exec(encoded, classname, arg);
        }
    }
}
