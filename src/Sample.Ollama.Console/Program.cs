using OllamaSharp;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Debug()
		.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
		.CreateLogger();

Log.Information("Iniciando");

var uri = new Uri("http://localhost:11434");

var ollama = new OllamaApiClient(uri) { SelectedModel = "phi3:mini" };

Log.Information("Qual é a sua pergunta?");

var chat = ollama.Chat(stream => Console.Write(stream.Message?.Content ?? ""));
while (true)
{
	var message = Console.ReadLine();
	Console.WriteLine();

	if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
		break;

	await chat.Send(message);
}