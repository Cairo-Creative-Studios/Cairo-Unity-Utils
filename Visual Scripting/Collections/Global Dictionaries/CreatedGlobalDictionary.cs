using System.Collections.Generic;
using Unity.VisualScripting;

[UnitCategory("Collections/Global")]
[UnitTitle("Create Global Dictionary")]
public class CreateGlobalDictionary : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;
    public ValueInput InitialItems;

    public ValueOutput CreatedDictionary;

    protected override void Definition()
    {
        this.Name = ValueInput<string>("Name", "Default");
        this.InitialItems = ValueInput<AotDictionary>("Initial Items");

        this.CreatedDictionary = ValueOutput<AotDictionary>("Created Dictionary");

        this.In = ControlInput("", (flow) =>
        {
            var name = flow.GetValue<string>(Name);
            var initialItems = flow.GetValue<AotDictionary>(InitialItems);

            flow.SetValue(CreatedDictionary, SystemManager.CreateGlobalDictionary(name, initialItems));

            return Out;
        });
    }
}