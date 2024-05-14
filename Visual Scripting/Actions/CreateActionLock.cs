using System;
using Unity.VisualScripting;

public class CreateActionLock : Unit
{
    public ControlInput In;
    public ControlOutput Out;
    public ValueInput Action;
    public ValueInput LockName;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            var actionType = flow.GetValue<ActionImplementation>(Action).GetType();
            if (!SystemManager.ActionLocks.ContainsKey(actionType)) SystemManager.ActionLocks.Add(actionType, new());
            SystemManager.ActionLocks[actionType].Add(flow.GetValue<string>(LockName));
            return Out;
        });
        Out = ControlOutput("");
        Action = ValueInput<ActionImplementation>("Action");
        LockName = ValueInput<string>("Lock Name");
    }
}