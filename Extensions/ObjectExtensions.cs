using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ObjectExtensions
{
    public static void DestroyChildren(this UnityEngine.Transform a)
    {
        while (a.childCount > 0)
        {
            GameObject.DestroyImmediate(a.GetChild(0).gameObject);
        }
    }

    public static Transform FindOrCreate(this UnityEngine.Transform a, string name)
    {
        return a.Find(name) ?? new GameObject(name).transform;
    }

    public static T GetCachedComponent<T>(this UnityEngine.Object a) where T : Component
    {
        return a is Component c ? SystemManager.GetCachedComponent<T>(c.gameObject) :
               a is GameObject g ? SystemManager.GetCachedComponent<T>(g) :
               null;
    }

    /// <summary>
    /// Destroys all components that are required by the given MonoBehaviour
    /// Based on user PizzaPies Answer: https://answers.unity.com/questions/1445663/how-to-auto-remove-the-component-that-was-required.html
    /// </summary>
    /// <param name="monoInstanceCaller"></param>
    public static void DestroyWithRequiredComponents(this MonoBehaviour monoInstanceCaller)
    {
        MemberInfo memberInfo = monoInstanceCaller.GetType();
        RequireComponent[] requiredComponentsAtts = Attribute.GetCustomAttributes(memberInfo, typeof(RequireComponent), true) as RequireComponent[];
        var monoInstance = monoInstanceCaller.gameObject;
        List<Type> typesToDestroy = new List<Type>();

        foreach (RequireComponent rc in requiredComponentsAtts)
        {
            if (rc != null && monoInstanceCaller.GetComponent(rc.m_Type0) != null)
            {
                typesToDestroy.Add(rc.m_Type0);
            }
        }

        UnityEngine.Object.DestroyImmediate(monoInstanceCaller);

        foreach (Type type in typesToDestroy)
        {
            UnityEngine.Object.DestroyImmediate(monoInstance.GetComponent(type));
        }
    }

    public static List<T> GetComponentsImplementing<T>(this GameObject gameObject) where T : class
    {
        List<T> components = new List<T>();
        foreach (Component component in gameObject.GetComponents<Component>())
        {
            if (component is T)
            {
                components.Add(component as T);
            }
        }
        return components;
    }

    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.FlattenHierarchy;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos) {
            if (pinfo.CanWrite) {
                try {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos) {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }
}



public static class ExtendedResources
{
    public static GameObject[] LoadAllByComponent<T>(string path) where T : Component
    {
        // Use the LINQ method syntax to filter the gameObjects by component type
        return Resources.LoadAll<GameObject>(path).Where(x => x.GetComponent<T>() != null).ToArray();
    }
}