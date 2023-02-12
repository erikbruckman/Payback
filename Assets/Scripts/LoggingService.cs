using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingService : MonoBehaviour
{
    public static bool enableErrorLogs = true;

    public static bool enableAllLogs = false;

    public static void Log(string message)
    {
        if(enableAllLogs)
            Debug.Log(message);
    }

    public static void LogError(string message)
    {
        if (enableErrorLogs)
            Debug.LogError(message);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
