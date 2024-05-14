using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;


[SerializationVersion("A")]
public class OnActionPerformed<T> : ReflectiveEventUnit<OnActionPerformed<T>> where T : ActionImplementation, new()
{
    public ValueOutput Action;
    public List<ValueOutput> ArgumentList;
    public static T PerformedAction;

    public static void Invoke(ActionImplementation action, object[] args)
    {
        ModularInvoke(null, ("Type", action), ("ArgumentList", args.ToList()));
    }

    protected override void Definition()
    {
        base.Definition();

        var actionType = typeof(T);
        var method = actionType?.GetMethod("OnPerform");
        var parameters = method?.GetParameters();
        var parameterCount = parameters?.Length ?? 0;

        // Define the parameter inputs based on the Action's parameter types
        ArgumentList = new List<ValueOutput>();
        for (int i = 0; i < parameterCount; i++)
        {
            var parameter = parameters[i];
            var inputName = parameter.Name;
            ArgumentList.Add(ValueOutput(parameter.ParameterType, inputName));
        }

        // Add the Action Implementation's Implied Arguments
        var reference = ActionImplementation.GetOrCreateInstance<T>();
        foreach (var argKey in reference.internalArguments.Keys)
        {
            ArgumentList.Add(ValueOutput(reference.internalArguments[argKey].Type, argKey));
        }
    }

    protected override void AssignArguments(Flow flow, SerializableDictionary<string, object> parameters)
    {
        base.AssignArguments(flow, parameters);

        foreach (var param in parameters.Keys)
        {
            var argument = ArgumentList.FirstOrDefault(arg => arg.key == param);
            if (argument == null) continue;
            flow.SetValue(argument, parameters[param]);
        }

        foreach (var arg in ArgumentList)
        {
            if (!PerformedAction.internalArguments.Any(x => x.Key == arg.key)) continue;
            var internalArg = PerformedAction.internalArguments.FirstOrDefault(x => x.Key == arg.key).Value; 
            flow.SetValue(arg, internalArg.Value);
        }
    }
}