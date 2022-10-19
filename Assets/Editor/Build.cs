using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Build 
{
    public static void BuildAndroid()
    {
        Debug.Log("===build");
        DownloadGradle611();
        Bld();
        EditorApplication.Exit(0);
    }

    [MenuItem("test/install gradle")]
    public static void DownloadGradle611()
    {
        Debug.Log("===Download 1");
        string projPath = Application.dataPath.Replace("/Assets", "");
        string folderPath = Path.Combine(projPath, "gradle");
        string archiveUrl = "https://services.gradle.org/distributions/gradle-6.1.1-bin.zip";
        string archiveName = projPath + "/Temp/gradle.zip";

        if (!Directory.Exists(folderPath))
        {
            Debug.Log("===Download 2");
            using (var client = new WebClient())
            {
                client.DownloadFile(archiveUrl, archiveName);
            }
            Debug.Log("===Download 3");
            ZipFile.ExtractToDirectory(archiveName, folderPath);
            AndroidExternalToolsSettings.gradlePath = folderPath + "/gradle-6.1.1";
            Debug.Log("===Download 4");
        }

        if (!Directory.Exists(folderPath))
        {
            throw new UnityException("Can't download gradle 6.1.1");
        }
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
}
