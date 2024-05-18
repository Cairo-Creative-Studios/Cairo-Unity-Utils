using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;

namespace Framework.VisualScripting
{
    [UnitCategory("Actions")]
    [UnitTitle("Perform Action")]
    [SerializationVersion("A")]
    public class PerformAction<T> : Unit where T : ActionImplementation, new()
    {
        public ControlInput In;
        public ControlOutput Out;
        public List<ValueInput> ParameterInputs;
        public ValueOutput Action;
        public List<ValueOutput> ArgumentList = new();

        protected override void Definition()
        {
            In = ControlInput("", (flow) =>
            {
                var actionImplementation = ActionImplementation.GetOrCreateInstance<T>();
                flow.SetValue(Action, actionImplementation);
                var args = new object[ParameterInputs.Count];

                int i = 0;
                foreach (var internalArg in actionImplementation.internalArguments.Keys)
                {
                    flow.SetValue(ArgumentList[i], actionImplementation.internalArguments[internalArg].Value);
                    i++;
                }

                for(int j = 0; j < ParameterInputs.Count; j++)
                {
                    args[j] = flow.GetValue<object>(ParameterInputs[j]);
                }

                actionImplementation.PerformAction(args);
                return Out;
            });
            Out = ControlOutput("");
            Action = ValueOutput<T>(typeof(T).Name);

            var actionType = typeof(T);
            var method = actionType?.GetMethod("OnPerform");
            var parameters = method?.GetParameters();
            var parameterCount = parameters?.Length ?? 0;

            // Define the parameter inputs based on the Action's parameter types
            ParameterInputs = new List<ValueInput>();
            for (int i = 0; i < parameterCount; i++)
            {
                var parameter = parameters[i];
                var inputName = parameter.Name;
                ParameterInputs.Add(ValueInput(parameter.ParameterType, inputName));
            }
            ArgumentList = new();
            // Add the Action Implementation's Implied Arguments
            var reference = ActionImplementation.GetOrCreateInstance<T>();
            foreach (var argKey in reference.internalArguments.Keys)
            {
                ArgumentList.Add(ValueOutput(reference.internalArguments[argKey].Type, argKey));
            }
        }
    }
}
