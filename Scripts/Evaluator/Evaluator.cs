using System;
using Popcron.Console;
using UnityEngine;

/// <summary>
/// Expression evaluator for complex expressions including custom functions
/// </summary>
public class Evaluator : MonoBehaviour
{
    public string command;
    public ConsoleWindow consoleWindow;

    [ContextMenu("Evaluate")]
    public void EvaluateDebug()
    {
        Debug.Log(Console.Run(command));
    }


    public static void Evaluate(string iCommand)
    {
        Console.Run(iCommand);
    }
}