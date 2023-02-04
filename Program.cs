using System;
using System.Management.Automation;
using System.Diagnostics;
using System.IO;
using System.Text;

class MGit
{
    static string GetDKey()
    {
        const string path = "./mgitConfig.cfg";
        FileStream fs = File.Open(path, FileMode.OpenOrCreate);
        byte[] b = new byte[1024];
        UTF8Encoding temp = new UTF8Encoding(true);
        fs.Read(b, 0, b.Length);

        string mkey = temp.GetString(b);
        
        fs.Close();
        return (mkey);
    }

    static string SetDKey(string key)
    {
        const string path = "/.mgitConfig.cfg";
        FileStream fs = File.Open(path, FileMode.Open);
        byte[] str = new UTF8Encoding(true).GetBytes(key);
        fs.Write(str, 0, str.Length);
        fs.Close();
        return (key);
    }

    static bool ExecCmd (string cmd, string arg)
    {
        Process.Start(cmd, arg);
        return (true);
    }

    static bool GPush (string[] args)
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

        ExecCmd("git", @"add " + docs);
        ExecCmd("git", @"commit -m " + '\"' +commit + '\"');
        ExecCmd("git", @"push ");

        return (true);
    }
    static bool GClone (string[] args, string key)
    {
        string option = null;
        string dkey = "";
        List<string> urls = new List<string>();

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
                    urls.Add(arg);
                    break;
                case "u":
                    urls.Add(arg);
                    break;
                case "mk":
                    urls.Add(arg);
                    break;
                case "k":
                    dkey = arg;
                    option = null;
                    break;
                default:
                    urls.Append(arg);
                    break;
            }
        }

        foreach (string url in urls)
        {
            string curl = url.Insert(8, dkey + '@');
            ExecCmd("git", @"clone " + curl);
        }

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

        string Key = GetDKey();

        switch (args[0])
        {
            case "push": GPush (args); break;
            case "clone": GClone(args, Key); break;
            case "setkey": Key = SetDKey( args.Length > 1 ? args[1] : Key); break;
            case "key": Console.WriteLine(Key); break;
            case "?": break;

            default: break;
        }
    }
}