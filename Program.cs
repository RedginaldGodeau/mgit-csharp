using System;
using System.Management.Automation;
using System.Diagnostics;

class MGit
{
    static bool ExecCmd (string cmd, string arg)
    {
        /*PowerShell ps = PowerShell.Create();
        foreach (string cmd in cmds)
            ps.AddCommand(@cmd);

        ps.Invoke();*/
        Process.Start(cmd, arg);

        return (true);
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
        /*ExecCmd(new string[] {$"git add {docs}", $"git commit -m {commit}", $"git push"});*/

        ExecCmd("git", @"add " + docs);
        ExecCmd("git", @"commit -m " + '\"' +commit + '\"');
        ExecCmd("git", @"push ");

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