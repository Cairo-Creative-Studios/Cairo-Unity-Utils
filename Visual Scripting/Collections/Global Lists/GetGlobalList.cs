using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Get Global List")]
public class GetGlobalList : Unit
{
    public ValueInput Name;
    public ValueOutput List;
    protected override void Definition()
    {
        this.Name = ValueInput<string>("Name", "Default");
        this.List = ValueOutput("List", (flow) =>
        {
            return SystemManager.GetGlobalList(flow.GetValue<string>(Name));
        });
    }
}