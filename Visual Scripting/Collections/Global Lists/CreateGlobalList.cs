using System.Collections.Generic;
using Unity.VisualScripting;

[UnitCategory("Collections/Global")]
[UnitTitle("Create Global List")]
public class CreateGlobalList : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Name;
    public ValueInput InitialItems;

    public ValueOutput CreatedList;

    protected override void Definition()
    {
        this.Name = ValueInput<string>("Name", "Default");
        this.InitialItems = ValueInput<List<object>>("Initial Items");

        this.CreatedList = ValueOutput<List<object>>("Created List");

        this.In = ControlInput("", (flow) =>
        {
            var name = flow.GetValue<string>(Name);
            var initialItems = flow.GetValue<List<object>>(InitialItems);

            flow.SetValue(CreatedList, SystemManager.CreateGlobalList(name, initialItems));

            return Out;
        });
    }
}