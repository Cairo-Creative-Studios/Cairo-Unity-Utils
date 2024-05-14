using System.Linq;
using UnityEngine;

public class Singleton : MonoBehaviour
{
}

/// <summary>
/// A simple Singleton MonoBehaviour which can be accessed statically via the Instance property.
/// </summary>
/// <typeparam name="TSingleton">This Singleton Class (for accessing the Singleton statically).</typeparam>
public class Singleton<TSingleton> : Singleton where TSingleton : Singleton
{
    private static GameObject _instanceObject;
    /// <summary>
    /// A static property that returns the game object that contains the singleton component.
    /// If the game object does not exist, it will try to load it from the resources or create a new one.
    /// The game object will not be destroyed when loading a new scene.
    /// </summary>
    public static GameObject InstanceObject
    {
        get
        {
            _instanceObject
                ??= (ExtendedResources.LoadAllByComponent<TSingleton>("")?.FirstOrDefault()
                ?? new(typeof(TSingleton).Name));

            if (_instanceObject == null)
                SystemManager.AddSingleton(_instance);

            DontDestroyOnLoad(_instanceObject);

            return _instanceObject;
        }
    }

    private static TSingleton _instance;
    /// <summary>
    /// A static property that returns the singleton instance.
    /// If the instance does not exist, it will try to get it from the game object or add it as a component.
    /// </summary>
    public static TSingleton Instance => _instance
                                        ??= InstanceObject.GetComponent<TSingleton>()
                                        ?? InstanceObject.AddComponent<TSingleton>();

    /// <summary>
    /// A static method that initializes and returns the singleton instance.
    /// </summary>
    public static TSingleton StartSingleton()
    {
        return Instance;
    }
}

/// <summary>
/// A simple Singleton MonoBehaviour which can be accessed statically via the Instance property,
/// with Data that can be accessed statically via the Data,
/// which loads and caches it from Resources. The Data must first be created in Resources to be used.
/// </summary>
/// <typeparam name="TSingleton">This Singleton Class (for accessing the Singleton statically).</typeparam>
/// <typeparam name="TData">This Singleton's Data class (for accessing the Data statically).</typeparam>
public class Singleton<TSingleton, TData> : Singleton<TSingleton> where TSingleton : Singleton where TData : SingletonData
{
    private static TData _data;
    /// <summary>
    /// A static property that returns the static Data of the Singleton.
    /// If the data does not exist, it will try to load it from Resources. If it can't find it, it will create a new Instance of the Data Type.
    /// </summary>
    public static TData Data => _data ??= Resources.LoadAll<TData>("")[0] ?? ScriptableObject.CreateInstance<TData>();
}