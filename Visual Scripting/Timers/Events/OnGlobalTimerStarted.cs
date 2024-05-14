using Unity.VisualScripting;
using UnityEngine;

[UnitCategory("Timers")]
[UnitTitle("On Global Timer Started")]
public class OnGlobalTimerStarted : ReflectiveEventUnit<OnGlobalTimerStarted>
{
    [OutputType(typeof(string))]
    public ValueOutput Name;
    [OutputType(typeof(float))]
    public ValueOutput Duration;
    [OutputType(typeof(float))]
    public ValueOutput TimeStarted;

    public static void Invoke(string name, float timeStarted, float duration)
    {
        ModularInvoke(null, ("Name", name), ("TimeStarted", timeStarted), ("Duration", duration));
    }
}