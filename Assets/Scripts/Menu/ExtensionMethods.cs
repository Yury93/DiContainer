using System;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods 
{
    public static void Subscribe(this Button button, Action action)
    {
        button.onClick.AddListener(()=>action?.Invoke());
    }
    public static void Unsubscribe(this Button button, Action action)
    {
        button.onClick.RemoveListener(() => action?.Invoke());
    }
    
}
