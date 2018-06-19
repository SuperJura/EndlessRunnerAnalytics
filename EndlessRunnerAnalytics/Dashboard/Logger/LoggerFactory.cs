namespace Dashboard.Logger
{
	public class LoggerFactory
	{
		private static LoggerFactory instance;

		public static LoggerFactory GetInstance()
		{
			if (instance == null) instance = new LoggerFactory();
			return instance;
		}

		public ILogger GetLogger()
		{
			return new ConsoleLogger();
		}
	}
}