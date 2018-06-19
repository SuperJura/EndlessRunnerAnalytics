using PostSharp.Serialization;

namespace Dashboard.Logger
{
	[PSerializable]
	public class ConsoleLogger : ILogger
	{
		public void Write(string message, LoggerTag tag)
		{
			System.Diagnostics.Debug.WriteLine("[" + tag + "] " + message);
		}
	}
}