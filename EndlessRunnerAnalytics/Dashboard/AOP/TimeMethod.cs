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
			Debug.WriteLine("Start " + args.Method.Name + " " + args.Method.DeclaringType.Name);
			timer = Metric.Timer("method duration", Unit.Requests);
			timer.StartRecording();
		}

		public override void OnSuccess(MethodExecutionArgs args)
		{
			Debug.WriteLine("End " + args.Method.Name + " " + args.Method.DeclaringType.Name);
			timer.EndRecording();
		}
	}
}