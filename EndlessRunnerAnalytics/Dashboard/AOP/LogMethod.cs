using Dashboard.Logger;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Dashboard.AOP
{
	[PSerializable]
	public class LogMethod : OnMethodBoundaryAspect
	{
		ILogger logger = LoggerFactory.GetInstance().GetLogger();

		public override void OnExit(MethodExecutionArgs args)
		{
			logger.Write("Finished execution of " + args.Method.Name + " on object of type " + args.Method.DeclaringType.Name, LoggerTag.INFO);
		}
	}
}