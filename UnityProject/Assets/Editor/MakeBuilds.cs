using UnityEditor;
using UnityEngine;
using System.IO;


//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod MakeBuilds.BuildAll

class MakeBuilds : MonoBehaviour
{
    public const string scene = "Assets/Scenes/MainAndOnlyScene.unity";
    public const string runName = "Game";
    
    [MenuItem("Build/All")]
    public static void BuildAll()
    {
       Debug.Log("Building windows version of game");
        
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
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/windows/" + runName + ".x32.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/windows/" + runName + ".x86_64.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        
        // Build linux
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/linux/" + runName, BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
        
        // Build Mac... questionable at best :P
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/osx/" + runName, BuildTarget.StandaloneOSX, BuildOptions.None);
        

        MoveFiles(path);
    }
    
    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        Debug.Log("Building windows version of game");
        
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose export location", EditorApplication.applicationPath + "/../", "");

        // Build player.
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/windows/" + runName + ".x32.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        BuildPipeline.BuildPlayer(new string[] {scene}, path + "/windows/" + runName + ".x86_64.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);

        MoveFiles(path);
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
