using System.Collections.Generic;
using Unity.VisualScripting;

public class OnListTransfer : ReflectiveEventUnit<OnListTransfer>
{
    [OutputType(typeof(List<object>))]
    public ValueOutput FromList;
    [OutputType(typeof(List<object>))]
    public ValueOutput ToList;

    public static void Invoke(List<object> fromList, List<object> toList)
    {
        ModularInvoke(null, ("FromList", fromList), ("ToList", toList));
    }
}