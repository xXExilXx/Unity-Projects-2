using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BadBilly : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RunOnStart()
    {
        if (Application.isEditor)
            return;

        string currentDirectory = Directory.GetCurrentDirectory();

        bool isCheatingDetected = Directory.Exists(Path.Combine(currentDirectory, "MelonLoader")) ||
                                  Directory.Exists(Path.Combine(currentDirectory, "BepInEx")) ||
                                  File.Exists(Path.Combine(currentDirectory, "version.dll")) ||
                                  File.Exists(Path.Combine(currentDirectory, "dobby.dll")) ||
                                  File.Exists(Path.Combine(currentDirectory, "winhttp.dll")) ||
                                  File.Exists(Path.Combine(currentDirectory, "doorstop_config.ini"));

        if (isCheatingDetected)
        {
            Application.Quit();
            Debug.LogError("Cheating detected. The game has been terminated.");
        }
    }
}