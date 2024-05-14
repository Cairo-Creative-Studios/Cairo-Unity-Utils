using System.Collections.Generic;
using Unity.VisualScripting;

[UnitCategory("Collections")]
[UnitTitle("Transfer Nth List Item")]
public class ListTransfer_Index : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput FromList;
    public ValueInput ToList;
    public ValueInput ItemIndex;

    public ValueOutput ModifiedList;

    protected override void Definition()
    {
        this.FromList = ValueInput<List<object>>("Sender");
        this.ToList = ValueInput<List<object>>("Reciever");
        this.ItemIndex = ValueInput<int>("Index");

        this.ModifiedList = ValueOutput<List<object>>("Modified List");

        In = ControlInput("", (flow) => 
        {
            var fromList = flow.GetValue<List<object>>(FromList);
            var toList = flow.GetValue<List<object>>(ToList);
            var index = flow.GetValue<int>(ItemIndex);

            if(fromList.Count < index)
            {
                toList.Add(fromList[index]);
                fromList.RemoveAt(index);
            }

            flow.SetValue(ModifiedList, toList);

            return Out; 
        });
    }
}