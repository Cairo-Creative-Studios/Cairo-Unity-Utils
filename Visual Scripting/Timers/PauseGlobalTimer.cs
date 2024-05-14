using Unity.VisualScripting;

[UnitCategory("Timers")]
[UnitTitle("Pause Global Timer")]
public class PauseGlobalTimer : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            SystemManager.PauseTimer(flow.GetValue<string>(Name));
            return Out;
        });

        Name = ValueInput<string>("Name", default);
    }
}