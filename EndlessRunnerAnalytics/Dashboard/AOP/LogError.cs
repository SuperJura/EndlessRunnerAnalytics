using Dashboard.Logger;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Dashboard.AOP
{
	[PSerializable]
	public class LogError : OnMethodBoundaryAspect
	{
		ILogger logger = LoggerFactory.GetInstance().GetLogger();

		public override void OnException(MethodExecutionArgs args)
		{
			logger.Write(args.Method.Name + " on object of type" + args.Method.DeclaringType.Name + " failed!", LoggerTag.ERROR);
			logger.Write(args.Exception.Message, LoggerTag.ERROR);
		}
	}
}