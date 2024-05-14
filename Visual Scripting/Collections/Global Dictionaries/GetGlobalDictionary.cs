using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Get Global Dictionary")]
public class GetGlobalDictionary : Unit
{
    public ValueInput Name;
    public ValueOutput Dictionary;
    protected override void Definition()
    {
        this.Name = ValueInput<string>("Name", "Default");
        this.Dictionary = ValueOutput("Dictionary", (flow) =>
        {
            return SystemManager.GetGlobalDictionary(flow.GetValue<string>(Name));
        });
    }
}