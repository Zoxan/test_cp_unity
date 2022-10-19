using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Build 
{
    public static void BuildAndroid()
    {
        Debug.Log("===build");
        Bld();
        EditorApplication.Exit(0);
        // "asdf".Bash
    }


    private static void Bld()
    {
        var target = BuildTarget.Android;
        var outPath = "build/build.apk";
        BuildPlayerOptions ops = new BuildPlayerOptions();
        var scenes = EditorBuildSettings.scenes;
        List<string> buildScenes = new List<string>();
        foreach (EditorBuildSettingsScene settingsScene in scenes)
        {
            if (settingsScene.enabled)
            {
                buildScenes.Add(settingsScene.path);
            }
        }
        ops.target = target;
        ops.scenes = buildScenes.ToArray();
        ops.locationPathName = outPath;
        var report = BuildPipeline.BuildPlayer(ops);
		
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("===ContinuousIntegration script build failed:");
            Debug.Log($"{JsonConvert.SerializeObject(report)}");
            var msgs = report.steps.Select(x => $"{x.name}:\n{string.Join("\n", x.messages)}");
            throw new UnityException(string.Join("\n", msgs));
        }
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
