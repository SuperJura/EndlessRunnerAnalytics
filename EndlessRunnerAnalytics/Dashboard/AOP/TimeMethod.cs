using PostSharp.Aspects;
using PostSharp.Serialization;
using Metrics;
using System.Diagnostics;

namespace Dashboard.AOP
{
	[PSerializable]
	public class TimeMethod : OnMethodBoundaryAspect
	{
		[PNonSerialized] private TimerContext context;
		[PNonSerialized] private Timer timer;

		public override void OnEntry(MethodExecutionArgs args)
		{
			timer = Metric.Timer("Method " + args.Method.Name + " duration", Unit.Requests);
			context = timer.NewContext();
		}

		public override void OnSuccess(MethodExecutionArgs args)
		{
			context.Dispose();
		}
	}
}