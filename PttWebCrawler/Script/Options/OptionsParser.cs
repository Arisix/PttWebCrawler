using System;
using System.Collections.Generic;

namespace Options
{
    class OptionsParser
    {
        static List<string> parameter;

        public static void Parse(string[] args)
        {
            try
            {
                parameter = OptionSetting.Setting.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                Environment.Exit(0);
            }
            finally
            {
                if (OptionSetting.IsShowHelp)
                {
                    ShowHelp(OptionSetting.Setting);
                    Environment.Exit(0);
                }
                else if (OptionSetting.IsShowVersion)
                {
                    ShowVersion();
                    Environment.Exit(0);
                }
            }
        }

        private static void ShowHelp(OptionSet optionSetting)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            optionSetting.WriteOptionDescriptions(Console.Out);
        }

        private static void ShowVersion()
        {
            Console.WriteLine(string.Format("Version {0}", Config.Version));
        }
    }
}
