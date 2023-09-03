using System.Diagnostics;

namespace Zine.Commands;

public class TailwindBuildCommand
{
	private const string InputFilePath = "Assets/Styles/app.css";
	private const string OutputFilePath = "./wwwroot/css/build/app.css";
	private readonly ProcessStartInfo _tailwindStartInfo = new()
	{
		FileName = "npx",
		Arguments = $"tailwindcss -i {InputFilePath} -o {OutputFilePath} --watch"
	};
	
	private bool _isTailwindProcessRunning;
	private readonly Process _tailwindProcess;
	
	private readonly ILogger _logger;

	public TailwindBuildCommand(ILogger logger)
	{
		_logger = logger;
		_tailwindProcess = new Process { StartInfo = _tailwindStartInfo };
	}

	public void Start()
	{
		if (!_isTailwindProcessRunning)
		{
			_logger.LogInformation("Starting Tailwind build");
			_tailwindProcess.Start();
			_isTailwindProcessRunning = true;
		}
	}

	public void Stop()
	{
		if (_isTailwindProcessRunning)
		{
			_logger.LogInformation("Stopping Tailwind build");
			_tailwindProcess.Kill();
			_isTailwindProcessRunning = false;
		}
	}
}
