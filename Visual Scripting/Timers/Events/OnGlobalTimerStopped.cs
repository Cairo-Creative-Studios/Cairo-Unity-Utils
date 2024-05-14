using Unity.VisualScripting;
using UnityEngine;

[UnitCategory("Timers")]
[UnitTitle("On Global Timer Stopped")]
public class OnGlobalTimerStopped : ReflectiveEventUnit<OnGlobalTimerStopped>
{
    [OutputType(typeof(string))]
    public ValueOutput Name;

    public static void Invoke(string name)
    {
        ModularInvoke(null, ("Name", name));
    }
}