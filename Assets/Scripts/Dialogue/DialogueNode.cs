using UnityEngine;

/// <summary>
/// Abstract base class for dialogue nodes so that dialogue choices can lead to different things.
/// </summary>
public abstract class DialogueNode : ScriptableObject
{
    public abstract void Execute();
}