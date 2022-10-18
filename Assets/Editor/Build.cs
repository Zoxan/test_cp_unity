using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class Build 
{
    public static void BuildAndroid()
    {
        Debug.Log("===build");
        // "asdf".Bash
    }

    private static void InstallGradle611()
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "echo \"asdf\" > test.txt" ,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            },
            EnableRaisingEvents = true
        };
        process.Start();
        process.WaitForExit(60*1000);
    }
}
