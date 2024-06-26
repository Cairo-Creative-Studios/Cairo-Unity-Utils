﻿//Script Developed for The Cairo Engine, by Richy Mackro (Chad Wolfe), on behalf of Cairo Creative Studios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;

[Serializable]
public class Node<T> : ISerializationCallbackReceiver
{
    [NonSerialized]
    public T value;

    public string Value = "";
    [NonSerialized] public Tree<T> tree;
    [NonSerialized] public Node<T> parent;
    [HideInInspector] public int[] index;

    public Node<T> this[int index]
    {
        get
        {
            if (children == null) return null;
            return children[index];
        }
    }
        
    public List<Node<T>> children;
    public Node(Tree<T> tree, T value, int[] index, Node<T> parent = null)
    {
        this.tree = tree;
        this.value = value;
        this.parent = parent;
        this.index = index;
    }

    public Node<T>[] GetHiearchy()
    {
        List<Node<T>> hiearchy = new List<Node<T>>();
        Node<T> lastNode = this;

        foreach (int i in index)
        {
            hiearchy.Add(lastNode);
            if (lastNode.parent != null)
                lastNode = lastNode.parent;
        }

        return hiearchy.ToArray();
    }

    public T[] GetValuesInHiearchy()
    {
        Node<T>[] hiearchy = GetHiearchy();
        List<T> values = new List<T>();

        for (int i = hiearchy.Length - 1; i > -1; i--)
        {
            values.Add(hiearchy[i].value);
        }

        return values.ToArray();
    }

    /// <summary>
    /// Returns the Node within this Node at the given Inex
    /// </summary>
    /// <returns>The node.</returns>
    /// <param name="index">The index.</param>
    public Node<T> GetNode(int index)
    {
        return children[index];
    }

    /// <summary>
    /// Adds a Node as a Child to this Node
    /// </summary>
    /// <param name="node">Node.</param>
    public void Add(Node<T> node)
    {
        if (children == null) children = new List<Node<T>>();
        children.Add(node);
        node.index = index.Append(children.Count()).ToArray();
    }

    /// <summary>
    /// Adds a Range of values as Children to this Node.
    /// </summary>
    /// <param name="values"></param>
    public void AddRange(T[] values)
    {
        var valuesToAdd = values.OfType<T>().ToList();
        foreach (T value in valuesToAdd)
        {
            Add(new Node<T>(tree, value, index));
        }
    }

    /// <summary>
    /// Adds a Range of Nodes as Children to this Node.
    /// </summary>
    /// <param name="nodes"></param>
    public void AddNodes(Node<T>[] nodes)
    {
        var nodesToAdd = nodes.OfType<Node<T>>().ToList();
        foreach (Node<T> node in nodesToAdd)
        {
            Add(node);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (value != null)
                Value = value.ToString();
        }
        catch
        {
            //Ignore
        }
    }

    public void OnAfterDeserialize()
    {
        try
        {
            if (value != null)
                Value = value.ToString();
        }
        catch
        {
            //Ignore
        }
    }
}