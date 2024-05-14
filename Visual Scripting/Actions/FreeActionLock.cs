using System;
using Unity.VisualScripting;

public class FreeActionLock : Unit
{
    public ControlInput In;
    public ControlOutput Out;
    public ValueInput Action;
    public ValueInput LockName;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            SystemManager.ActionLocks[flow.GetValue<ActionImplementation>(Action).GetType()].Remove(flow.GetValue<string>(LockName));
            return Out;
        });
        Out = ControlOutput("");
        Action = ValueInput<ActionImplementation>("Action");
        LockName = ValueInput<string>("Lock Name");
    }
}