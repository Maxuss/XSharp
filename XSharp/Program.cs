using System;
using Compiler;
using System.Windows;
/* X#, A C# Programming Mask
 * Copyright (c) 2021 Maxus Industries
 * Visit https://maxus.space/xsharp for more info
 * Thanks for understanding
 */

namespace XSharp
{
    class Base
    {
        public static void Main(string[] args)
        {
            PseudoGUI.Create();
        }
        
    }

    class PseudoGUI
    {
        public static string path;
        public static string classname;
        public static string args;
        public static void Create()
        {
            const string logo =

@"
$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
$$$$$$$$$$$$$MFFFFFFFFFFFFFFN$$$$$$$$$$$$$
$$$$$$$$$$$M*...............:VN$$$$$$$$$$$
$$$$$$$$$M**VV*.......*V*.....:VN$$$$$$$$$
$$$$$$$M*..*$$N:.....V$$N*......:VN$$$$$$$
$$$$$M*.....I$$V....*$$N*.........:VN$$$$$
$$$M*.......*$$$*..*N$N*............:VN$$$
$$$V.........F$$V.*$$M:...............I$$$
$$$V.........*$$$F$$$:..:**::**:......I$$$
$$$V..........V$$$$N*.::*$$**$$V::....I$$$
$$$V..........*$$$N*.*$$$$$$$$$$$$*...I$$$
$$$V.........:F$$$M:..::*$$**$$V::....I$$$
$$$V........:I$$$$$V..V$$$$$$$$$$I....I$$$
$$$V.......:M$$IV$$N:.:*V$$VV$$V*:....I$$$
$$$NV:....:M$$F::M$$V...:FF::FF*....:V$$$$
$$$$$NV:.:M$$F:..*$$N.............:V$$$$$$
$$$$$$$NVFN$V.....I$$*..........:V$$$$$$$$
$$$$$$$$$NV*......*NI*........:V$$$$$$$$$$
$$$$$$$$$$$NV:..............:V$$$$$$$$$$$$
$$$$$$$$$$$$$NNNNNNNNNNNNNNN$$$$$$$$$$$$$$
$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

      X SHARP CONSOLE EDITION
";
            Console.WriteLine(logo);
            Console.WriteLine("Thanks for using my C# Programming Mask!");
            Console.WriteLine("Please, paste path to .xs file to be executed here:");
            path = Console.ReadLine();
            Console.WriteLine($"Compiler is going to execute this file: {path}");
            Console.WriteLine("Now enter classname. Example: Hello.World. Please, note that compiler will execute 'main' method!");
            classname = Console.ReadLine();
            Console.WriteLine($"Compiler is going to execute this method: {classname}.main()");
            Console.WriteLine($"Insert any arguments for main method here, or press ENTER to skip");
            args = Console.ReadLine();
            Console.WriteLine("Compiler will run soon.");
            Console.WriteLine("Please wait...");
            CommonCompiler.Compile(classname, path, args);
            Console.WriteLine
(@"


Finished executing code!
");
            Console.ReadLine();
        }
    }

}