using System.Collections.Generic;
using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Destroy Global Dictionary")]
public class DestroyGlobalDictionary : Unit
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
            SystemManager.DestroyGlobalDictionary(name);

            return Out;
        });
    }
}