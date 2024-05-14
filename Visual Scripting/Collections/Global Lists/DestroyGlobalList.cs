using System.Collections.Generic;
using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Destroy Global List")]
public class DestroyGlobalList : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;

    protected override void Definition()
    {
        this.Name = ValueInput<string>("Name", "Default");

        this.In = ControlInput("", (flow) =>
        {
            var name = flow.GetValue<string>(Name);
            SystemManager.DestroyGlobalList(name);

            return Out;
        });
    }
}