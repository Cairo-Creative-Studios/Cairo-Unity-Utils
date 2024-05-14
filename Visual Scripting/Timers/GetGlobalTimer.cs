using System.Linq;
using Unity.VisualScripting;

[UnitCategory("Timers")]
[UnitTitle("Get Timer Elapsed Time")]
public class GetGlobalTimer : Unit
{
    public ValueInput Name;
    public ValueOutput Timer;
    public ValueOutput Time;
    public ValueOutput TimeAlpha;

    protected override void Definition()
    {
        Name = ValueInput<string>("Name", "");
        Timer = ValueOutput<GlobalTimer>("Timer", (flow) =>
        {
            return SystemManager.GlobalTimers[flow.GetValue<string>(Name)];
        });
        Time = ValueOutput<float>("Time Elapsed", (flow) =>
        {
            var timer = SystemManager.GlobalTimers[flow.GetValue<string>(Name)];
            return timer == null ? 0 : timer.Time;
        });
        TimeAlpha = ValueOutput<float>("Alpha of Time Elapsed", (flow) =>
        {
            var timer = SystemManager.GlobalTimers[flow.GetValue<string>(Name)];
            return timer == null ? 0 : timer.Alpha;
        });
    }
}