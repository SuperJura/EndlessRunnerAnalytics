using PostSharp.Aspects;
using PostSharp.Serialization;
using Metrics;

namespace Dashboard.AOP
{
	[PSerializable]
	public class TimeMethod : OnMethodBoundaryAspect
	{
		[PNonSerialized] private TimerContext context;

		public override void OnEntry(MethodExecutionArgs args)
		{
			context = Metric.Timer("method duration", Unit.Requests).NewContext();
		}

		public override void OnExit(MethodExecutionArgs args)
		{
			context.Dispose();
		}
	}
}