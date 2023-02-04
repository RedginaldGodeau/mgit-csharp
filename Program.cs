using System;
using System.Diagnostics;

class MGit
{

    static void ExecCommand (string cmd)
    {
        try
            {
                Process.Start("cmd.exe", cmd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
    }


    static bool Push (string[] args)
    {
        string option = null;
        string docs = "";
        string commit = "";

        for (int i = 1; i < args.Length; i++)
        {
            string arg = args[i];
            if (arg[0] == '-' && arg.Length > 1)
            {
                option = arg.Replace("-", string.Empty);
                continue;
            }

            switch (option)
            {
                case null:
                    docs += " " + arg;
                    break;
                case "p":
                    docs += " " + arg;
                    break;
                case "m":
                    commit += " " + arg;
                    option = null;
                    break;

                default:
                   docs += " " + arg;
                    break;
            }
        }

        if (docs.Length == 0)
            return (false);

        if (commit.Length == 0)
            while ((commit = Console.ReadLine()).Length == 0)
                ;
        ExecCommand($"git add {docs}");
        ExecCommand($"git commit {commit}");

        return (true);
    }


    static void Main(string[] args)
    {
 
        // mgit push -m "Hello World" -p *.c
        // mgit clone url -mk/-k
        // mgit setkey key
        // mgit key

        if (args.Length == 0)
        {
            Console.WriteLine("No Args");
            return;
        }

        switch (args[0])
        {
            case "push": Push (args); break;
            case "clone": break;
            case "setkey": break;
            case "key": break;
            case "?": break;

            default: break;
        }

    }
}