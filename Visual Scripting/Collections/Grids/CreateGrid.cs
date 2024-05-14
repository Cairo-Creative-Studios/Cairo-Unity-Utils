using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Create a Grid")]
[UnitCategory("Scriptable Grids")]
public class CreateGrid : Unit
{
    public ControlInput In;
    public ControlOutput Out;

    public ValueInput Size;
    public ValueInput Values;

    public ValueOutput Grid;

    public ControlOutput Create(Flow flow)
    {
        var values = flow.GetValue<List<object>>(Values);
        var size = flow.GetValue<Vector2>(Size);

        var grid = ScriptableObject.CreateInstance<GenericGrid>();
        grid.SetupGrid((int)size.x, (int)size.y);
        flow.SetValue(Grid, grid);

        if (values != null)
        {
            for (int i = 0; i < (grid.Width * grid.Height); i++)
            {
                var x = i % grid.Width;
                var y = i / grid.Width;
                //if (i < flattenedGrid.Count-1) break;
                grid[x, y].Value = values[i];
                grid[x, y].Value.SetField("Name", x + ", " + y);
            }
        }

        return Out;
    }

    protected override void Definition()
    {
        In = ControlInput("", Create);
        Out = ControlOutput("");

        Size = ValueInput<Vector2>("Size");
        Values = ValueInput<List<object>>("Optional Fill Values", null);

        Grid = ValueOutput<GenericGrid>("Created Grid");
    }
}