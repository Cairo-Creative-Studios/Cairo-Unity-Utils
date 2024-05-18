using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Pool;
using Framework;

/// <summary>
/// The base class for Actions that can be performed during Gameplay. 
/// </summary>
public abstract class ActionImplementation<TAction> : ActionImplementation where TAction : ActionImplementation, new()
{
    public static void Perform()
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());
        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)]);
    }
    public override void PerformAction(params object[] args) { Perform(); }

    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform();
    }

    public abstract void OnPerform();
}
/// <summary>
/// The base class for Actions that can be performed during Gameplay. 
/// </summary>
public abstract class ActionImplementation<TAction, T1> : ActionImplementation where TAction : ActionImplementation, new()
{
    public static void Perform(T1 arg)
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());
        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)], arg);
    }
    public override void PerformAction(params object[] args) { Perform((T1)args[0]); }

    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform((T1)args[0]);
    }

    public abstract void OnPerform(T1 arg1);
}

public abstract class ActionImplementation<TAction, T1, T2> : ActionImplementation
    where TAction : ActionImplementation, new()
{
    public static void Perform(T1 arg1, T2 arg2)
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());

        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)], arg1, arg2);
    }

    public override void PerformAction(params object[] args)
    {
        Perform((T1)args[0], (T2)args[1]);
    }

    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform((T1)args[0], (T2)args[1]);
    }

    public abstract void OnPerform(T1 arg1, T2 arg2);
}

public abstract class ActionImplementation<TAction, T1, T2, T3> : ActionImplementation
    where TAction : ActionImplementation, new()
{
    public static void Perform(T1 arg1, T2 arg2, T3 arg3)
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());

        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)], arg1, arg2, arg3);
    }

    public override void PerformAction(params object[] args)
    {
        Debug.Log(typeof(T1).FullName + "," + typeof(T2).FullName + "," + typeof(T3).FullName);
        Debug.Log(args[0].GetType().FullName + "," + args[1].GetType().FullName + "," + args[2].GetType().FullName);
        var performMethodInfo = this.GetType().BaseType.GetMethod("Perform");
        Debug.Log(performMethodInfo.GetParameters()[0].ParameterType + ", " + performMethodInfo.GetParameters()[1].ParameterType);
        Perform((T1)args[0], 
            (T2)args[1], 
            (T3)args[2]);
    }

    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform((T1)args[0], (T2)args[1], (T3)args[2]);
    }

    public abstract void OnPerform(T1 arg1, T2 arg2, T3 arg3);
}

public abstract class ActionImplementation<TAction, T1, T2, T3, T4> : ActionImplementation
    where TAction : ActionImplementation, new()
{
    public static void Perform(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());

        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)], arg1, arg2, arg3, arg4);
    }

    public override void PerformAction(params object[] args)
    {
        Perform((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
    }
    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
    }

    public abstract void OnPerform(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}

public abstract class ActionImplementation<TAction, T1, T2, T3, T4, T5> : ActionImplementation
    where TAction : ActionImplementation, new() 
{
    public static void Perform(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        if (!ActionImplementations.ContainsKey(typeof(TAction)))
            ActionImplementations.Add(typeof(TAction), new());


        SystemManager.StartActionCoroutine<TAction>(ActionImplementations[typeof(TAction)], arg1, arg2, arg3, arg4, arg5);
    }

    public override void PerformAction(params object[] args)
    {
        Perform((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4]);
    }
    public override void OnPerformGeneric(params object[] args)
    {
        OnPerform((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4]);
    }

    public abstract void OnPerform(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 args5);
}

public class ActionImplementation
{
    public int Key = 0;
    private static Dictionary<Type, ActionImplementation> _actionImplementation;
    public static Dictionary<Type, ActionImplementation> ActionImplementations => _actionImplementation ??= new();

    /// <summary>
    /// Arguments Generated by the Action itself, to pass to Events that would interact with it's properties.
    /// </summary>
    public static Dictionary<string, TypedObject> InternalArguments => new();
    public virtual Dictionary<string, TypedObject> internalArguments => InternalArguments;

    public virtual void OnPerformGeneric(params object[] args) { }
    public virtual void PerformAction(params object[] args) { }


    public static ActionImplementation GetOrCreateInstance<TAction>()
    where TAction : ActionImplementation, new()
    {
        Type actionType = typeof(TAction);

        if (!ActionImplementations.TryGetValue(actionType, out ActionImplementation instance))
        {
            instance = new TAction();
            ActionImplementations[actionType] = instance;
        }

        return instance;
    }
}


[Serializable]
public class ActionImplementationReference
{
    [ReadOnly] public string ActionName;
    public bool DefaultImplementation = true;
}