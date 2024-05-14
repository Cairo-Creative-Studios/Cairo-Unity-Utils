using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

public class SystemManager : Singleton<SystemManager>
{
    /// <summary>
    /// A cache of components attached to game objects, indexed by type.
    /// </summary>
    private Dictionary<GameObject, Dictionary<Type, Component>> _componentCache = new();

    private Dictionary<string, List<object>> _globalLists = new();
    /// <summary>
    /// Gets a copy of the collection of global lists that store arbitrary objects, indexed by name.
    /// </summary>
    public static Dictionary<string, List<object>> GlobalLists { get => Instance._globalLists.ToDictionary(x => x.Key, x => x.Value); }


    private Dictionary<string, AotDictionary> _globalDictionaries = new();
    /// <summary>
    /// Gets a copy of the collection of global dictionaries that store arbitrary objects, indexed by name and key.
    /// </summary>
    public static Dictionary<string, AotDictionary> GlobalDictionaries { get => Instance._globalDictionaries.ToDictionary(x => x.Key, x => x.Value); }

    private readonly SerializableDictionary<string, GlobalTimer> _globalTimers = new();
    /// <summary>
    /// Gets a copy of the global timers list.
    /// </summary>
    /// <returns>A list of <see cref="GlobalTimer"/> objects.</returns>
    public static SerializableDictionary<string, GlobalTimer> GlobalTimers => new(Instance._globalTimers);

    private SerializableDictionary<Type, Singleton> _singletons = new();
    public static SerializableDictionary<Type, Singleton> Singletons => new(Instance._singletons);

    private SerializableDictionary<Type, List<string>> _actionLocks;
    /// <summary>
    /// Action Locks can be created by adding a listener to an ActionImplementaion's OnPerformed Event. 
    /// The Lock is given a name, and will only allow the functionality of the Action to continue being performed 
    /// when FreeLock(lockName) is called and there are no other locks for the Action.
    /// </summary>
    public static SerializableDictionary<Type, List<string>> ActionLocks => Instance._actionLocks ??= new();

    /// <summary>
    /// Gets a cached component of a specified type attached to a game object.
    /// </summary>
    /// <param name="gameObject">The game object to get the component from.</param>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <returns>The component of type T, or null if not found.</returns>
    public static T GetCachedComponent<T>(GameObject gameObject) where T : Component
    {
        if(!Instance._componentCache.ContainsKey(gameObject))
            Instance._componentCache.Add(gameObject, new Dictionary<Type, Component>());
        if (!Instance._componentCache[gameObject].ContainsKey(typeof(T)))
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) return null;
            Instance._componentCache[gameObject][typeof(T)] = component;
        }
        return (T)Instance._componentCache[gameObject][typeof(T)];
    }

    /// <summary>
    /// Creates a List with the given Name in the Global scope, so that it may be accessed by only it's name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="items"></param>
    public static List<object> CreateGlobalList(string name, List<object> items = null)
    {
        List<object> list = new();
        if (items != null) list.AddRange(items);

        Instance._globalLists.Add(name, list);
        return list;
    }

    /// <summary>
    /// Removed the Named List from the global scope. This will not immediately destroy the List if there
    /// are references to it in other instances.
    /// </summary>
    /// <param name="name"></param>
    public static void DestroyGlobalList(string name)
    {
        Instance._globalLists.Remove(name);
    }

    /// <summary>
    /// Returns the List saved in the Global scope with the given Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static List<object> GetGlobalList(string name)
    {
        return Instance._globalLists[name];
    }

    /// <summary>
    /// Creates a Dictionary with the given Name in the Global scope, so that it may be accessed by only it's name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="items"></param>
    public static AotDictionary CreateGlobalDictionary(string name, AotDictionary items = null)
    {
        AotDictionary dictionary = new();
        if (items != null)
        {
            foreach(var key in items.Keys)
            {
                dictionary.Add(key, items[key]);
            }
        }

        Instance._globalDictionaries.Add(name, dictionary);
        return dictionary;
    }

    /// <summary>
    /// Removed the Named Dictionary from the global scope. This will not immediately destroy the Dictionary if there
    /// are references to it in other instances.
    /// </summary>
    /// <param name="name"></param>
    public static void DestroyGlobalDictionary(string name)
    {
        Instance._globalDictionaries.Remove(name);
    }

    /// <summary>
    /// Returns the List saved in the Global scope with the given Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AotDictionary GetGlobalDictionary(string name)
    {
        return Instance._globalDictionaries[name];
    }

    public static void StartTimer(string timerName, float duration)
    {
        if(Instance._globalTimers.ContainsKey(timerName))
        {
            Instance._globalTimers[timerName].Paused = false;
        }
        Instance._globalTimers.Add(timerName, new(timerName, Time.time, duration));
    }

    public static void PauseTimer(string timerName)
    {
        if (Instance._globalTimers.ContainsKey(timerName))
        {
            Instance._globalTimers[timerName].Paused = true;
        }
    }

    public static void StopTimer(string timerName)
    {
        if (Instance._globalTimers.ContainsKey(timerName))
        {
            Instance._globalTimers.Remove(timerName);
        }
    }

    public static void AddSingleton(object singleton)
    {
        if(!Instance._singletons.ContainsKey(singleton.GetType()))
            Instance._singletons.Add(singleton.GetType(), (Singleton)singleton);
    }

    private void Update()
    {
        foreach(var timer in _globalTimers.Values)
        {
            timer.Tick(Time.time);
            if (timer.Complete)
                Instance._globalTimers.Remove(timer.Name);
        }
    }

    /// <summary>
    /// Returns false if no <see cref="ActionLocks">Action Locks</see> exist for the <see cref="ActionImplementation">Action</see> with the given <paramref name="actionImplementationType"/>
    /// </summary>
    /// <param name="actionImplementationType"></param>
    /// <returns></returns>
    public static bool IsActionLocked(Type actionImplementationType)
    {
        return ActionLocks.ContainsKey(actionImplementationType);
    }    

    public static void StartActionCoroutine<T>(ActionImplementation actionImplementation, params object[] args) where T : ActionImplementation, new()
    {
        Instance.StartCoroutine(Instance.PerformAction<T>(actionImplementation, args));
    }

    private IEnumerator PerformAction<T>(ActionImplementation actionImplementation, params object[] args) where T : ActionImplementation, new()
    {
        var allArgs = args.Append(actionImplementation.internalArguments.Values.Select(x => x.Value)).ToArray();

        OnActionInvoked<T>.PerformedAction = (T)actionImplementation;
        OnActionInvoked<T>.Invoke(actionImplementation, allArgs);

        yield return null;

        while(IsActionLocked(actionImplementation.GetType()))
        {
            yield return null;
        }

        actionImplementation.OnPerformGeneric(args);
        yield return null;

        OnActionPerformed<T>.PerformedAction = (T)actionImplementation;
        OnActionPerformed<T>.Invoke(actionImplementation, allArgs);
        yield break;
    }
}