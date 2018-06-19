namespace Dashboard.Logger
{
	public interface ILogger
	{
		void Write(string message, LoggerTag tag);
	}
}