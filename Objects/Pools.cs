using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pools : Singleton<Pools>
{
    private static List<PrefabPool> pools = new();

    /// <summary>
    /// Calling this method will either Create a new Instance from the given Prefab, or return one that has been pooled.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject InstantiateOrActivate(GameObject prefab)
    {
        var pool = pools.FirstOrDefault(pool => pool.Prefab == prefab);
        
		if(pool == null)
        {
            pool = new(prefab);
        }
        GameObject instance;
        if(pool.Inactive.Count == 0)
            instance = Instantiate(prefab);
        else 
            instance = pool.Inactive[0];

        return instance;
    }

    /// <summary>
    /// Call this function to remove an instance from the game, but keep it pooled
    /// </summary>
    /// <param name="instance"></param>
    public static void Pool(GameObject instance)
    {
        var pool = pools.FirstOrDefault(pool => pool.Active.Contains(instance));
        if(pool != null)
        {
            pool.Active.TransferTo(pool.Inactive, instance);
        }
    }

    public class PrefabPool
    {
        public GameObject Prefab;
        public List<GameObject> Active = new();
        public List<GameObject> Inactive = new();
        public PrefabPool(GameObject prefab)
        {
            this.Prefab = prefab;
        }
    }
}