using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;


//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod MakeBuilds.BuildAll

class MakeBuilds : MonoBehaviour
{
    public const string scene = "Assets/Scenes/MainAndOnlyScene.unity";
    public const string runName = "Game";
    public const string gameName = "Space_is_running_out_of_space";
    public static string version = "0.2.3";
    
    [MenuItem("Build/All")]
    public static void BuildAll()
    {
        Debug.Log("Building game");
        
        Debug.Log("Loading version # from file");
        string[] lines = File.ReadAllLines(@"..\version.txt");
        version = lines[0];
        Debug.Log("Version: " + version);

        Debug.Log("Incrementing build #");
        string[] split = version.Split('.');
        int build = int.Parse(split[3]) + 1;
        version = split[0] + "." + split[1] + "." + split[2] + "." + build;

        try 
        {
            File.Delete(@"..\version.txt");
            
            StreamWriter sw = new StreamWriter(@"..\version.txt");

            //Write a line of text
            sw.WriteLine(version);

            //Close the file
            sw.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }       
                
        Debug.Log("Cleaning output folder");
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose export location", EditorApplication.applicationPath + "/../builds", "");
        
        //Clear old build
        if(Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        //Create directory
        Directory.CreateDirectory(path);

        // Build windows
        Build(BuildTarget.StandaloneWindows, path, "windows_32", ".exe");
        Build(BuildTarget.StandaloneWindows64, path, "windows_64", ".exe");
        
        // Build linux
        Build(BuildTarget.StandaloneLinuxUniversal, path, "linux", "");
        
        // Build Mac... questionable at best :P      
        Build(BuildTarget.StandaloneOSX, path, "osx", "");
    }
    
    public static void Build(BuildTarget buildTarget, string path, string type, string sufix)
    {
        Debug.Log("Building: " + type);
        string folder = path + "/" + gameName + "_" + type + "/";
        BuildPipeline.BuildPlayer(new string[] {scene}, folder + runName + sufix, buildTarget, BuildOptions.None);
        MoveFiles(folder);
    }
    
    public static void ZipFile(string path, string type)
    {
        //TODO
    }
    
    public static void MoveFiles(string path)
    {
        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.ReplaceFile("../README.md", path + "/README.md");
    }
    
    //https://docs.unity3d.com/Manual/BuildPlayerPipeline.html
    //https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildPlayer.html
    //https://docs.unity3d.com/ScriptReference/BuildTarget.html
}
