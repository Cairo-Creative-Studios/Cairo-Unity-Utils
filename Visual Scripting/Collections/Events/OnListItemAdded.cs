using System.Collections.Generic;
using Unity.VisualScripting;

public class OnListItemAdded : ReflectiveEventUnit<OnListItemAdded>
{
    [OutputType(typeof(List<object>))]
    public ValueOutput List;
    [OutputType(typeof(object))]
    public ValueOutput Item;

    public static void Invoke(List<object> list, object item)
    {
        ModularInvoke(list, ("List", list), ("Item", item));
    }
}