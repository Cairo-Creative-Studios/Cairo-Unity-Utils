using Unity.VisualScripting;

[UnitCategory("Timers")]
[UnitTitle("Start Global Timer")]
public class StartGlobalTimer : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;
    public ValueInput Time;
    public ValueOutput Timer;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            SystemManager.StartTimer(flow.GetValue<string>(Name), flow.GetValue<float>(Time));
            flow.SetValue(Timer, SystemManager.GlobalTimers[flow.GetValue<string>(Name)]);
            return Out;
        });

        Name = ValueInput<string>("Name", default);
        Time = ValueInput<string>("Duration", default);
        Timer = ValueOutput<GlobalTimer>("Created Timer");
    }
}