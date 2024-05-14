using System.Collections.Generic;
using Unity.VisualScripting;

public class OnListRangeAdded : ReflectiveEventUnit<OnListRangeAdded>
{
    [OutputType(typeof(List<object>))]
    public ValueOutput List;
    [OutputType(typeof(List<object>))]
    public ValueOutput Range;

    public static void Invoke(List<object> list, IEnumerable<object> range)
    {
        ModularInvoke(list, ("List", list), ("Range", range));
    }
}