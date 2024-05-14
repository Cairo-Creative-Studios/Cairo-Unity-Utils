using System;
using System.Collections.Generic;

[Serializable]
public class VSList : List<object>
{
    public VSList() { }
    public VSList(List<object> list) : base(list) { }

    public new void Add(object item)
    {
        OnListItemAdded.Invoke(this, item);
        base.Add(item);
    }

    public new void AddRange(IEnumerable<object> range)
    {
        OnListRangeAdded.Invoke(this, range);
        base.AddRange(range);
    }

    public new void Remove(object item)
    {
        OnListItemRemoved.Invoke(this, item);
        base.Remove(item);
    }

    public new void RemoveAt(int index)
    {
        OnListItemRemovedAtIndex.Invoke(this, index);
        base.RemoveAt(index);
    }
}