using Unity.VisualScripting;

[UnitCategory("Timers")]
[UnitTitle("Start Global Timer")]
public class StopGlobalTimer : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            SystemManager.StopTimer(flow.GetValue<string>(Name));
            return Out;
        });

        Name = ValueInput<string>("Name", default);
    }
}