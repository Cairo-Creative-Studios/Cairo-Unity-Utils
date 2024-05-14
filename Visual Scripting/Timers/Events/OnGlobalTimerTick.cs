using Unity.VisualScripting;
using UnityEngine;

[UnitCategory("Timers")]
[UnitTitle("On Global Timer Tick")]
public class OnGlobalTimerTick : ReflectiveEventUnit<OnGlobalTimerTick>
{
    [OutputType(typeof(string))]
    public ValueOutput Name;
    [OutputType(typeof(float))]
    public ValueOutput ElapsedTime;
    [OutputType(typeof(float))]
    public ValueOutput Duration;
    [OutputType(typeof(float))]
    public ValueOutput TimeAlpha;

    public static void Invoke(string name, float elapsedTime, float duration)
    {
        ModularInvoke(null, ("Name", name), ("ElapsedTime", elapsedTime), ("Duration", duration), ("TimeAlpha", Mathf.Max(elapsedTime)/duration));
    }
}