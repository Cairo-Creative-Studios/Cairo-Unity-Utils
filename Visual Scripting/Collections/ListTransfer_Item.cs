using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Transfer List Item")]
public class ListTransfer_Item : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput FromList;
    public ValueInput ToList;

    public ValueInput Item;

    public ValueOutput ModifiedList;

    protected override void Definition()
    {
        this.FromList = ValueInput<List<object>>("Sender");
        this.ToList = ValueInput<List<object>>("Reciever");
        this.Item = ValueInput<object>("Item");

        this.ModifiedList = ValueOutput<List<object>>("Modified List");

        this.In = ControlInput("", (flow) => 
        {
            var fromList = flow.GetValue<List<object>>(FromList);
            var toList = flow.GetValue<List<object>>(ToList);
            var item = flow.GetValue<object>(Item);

            if(toList.Contains(item))
            {
                toList.Add(item);
                toList.Remove(item);
            }

            flow.SetValue(ModifiedList, toList);

            return Out; 
        });
    }
}