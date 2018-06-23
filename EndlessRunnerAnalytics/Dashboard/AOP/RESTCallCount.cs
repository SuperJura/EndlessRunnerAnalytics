using PostSharp.Aspects;
using PostSharp.Serialization;
using Metrics;
using System.Collections.Generic;

namespace Dashboard.AOP
{
	[PSerializable]
	public class RESTCallCount : OnMethodBoundaryAspect
	{
		public override void OnSuccess(MethodExecutionArgs args)
		{
			Counter counter = getCounterForMethod(args.Method.Name);
			counter.Increment();
		}

		[PNonSerialized]
		static Dictionary<string, Counter> countersForMethods;

		static Counter getCounterForMethod(string methodName)
		{
			if(countersForMethods == null) countersForMethods = new Dictionary<string, Counter>();

			if(countersForMethods.ContainsKey(methodName)) return countersForMethods[methodName];
			else
			{
				Counter counter = Metric.Counter(methodName, Unit.Calls);
				countersForMethods[methodName] = counter;
				return counter;
			}
		}
	}
}