using System.Text.Json;

namespace Converter;

class Program
{
	#region Fields
	private static readonly string DataDirectory = Path.GetFullPath("../../../../../../../data/");
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		IndentSize = 1,
		IndentCharacter = '\t',
		WriteIndented = false
	};
	#endregion

	#region Functions
	static void Main(string[] args)
	{
		foreach (string labelPath in Directory.GetDirectories(DataDirectory))
		{
			string label = Path.GetFileName(labelPath);
			string rawPath = Path.Combine(labelPath, "raw");
			string formatPath = Path.Combine(labelPath, "format.txt");
			string format = File.ReadAllText(formatPath).Trim();

			SleepData sleepData = format switch
			{
				"samsung" => ImportSamsung(label, rawPath),
				_ => throw new NotSupportedException($"Unhandled data format ({format}).")
			};

			string resultFile = Path.Combine(labelPath, "extracted.json");
			Save(sleepData, resultFile);
		}
	}
	static SleepData ImportSamsung(string label, string data)
	{
		Dictionary<Guid, List<SleepStage>> sleepStages = [];

		foreach (Samsung.SleepStageEntry stage in Samsung.SleepStageEntry.ParseFromDirectory(data))
		{
			if (sleepStages.TryGetValue(stage.SleepId, out List<SleepStage>? list) is false)
			{
				list = [];
				sleepStages.Add(stage.SleepId, list);
			}

			SleepStageKind kind = stage.Stage switch
			{
				40001 => SleepStageKind.N1,
				40002 => SleepStageKind.N2,
				40003 => SleepStageKind.N3,
				40004 => SleepStageKind.REM,

				_ => throw new InvalidOperationException($"Unknown sleep stage ({stage.Stage}).")
			};

			DateTimeOffset start = new(stage.Start, stage.Offset);
			DateTimeOffset end = new(stage.End, stage.Offset);

			SleepStage converted = new(kind, start, end);
			list.Add(converted);
		}

		List<SleepEntry> entries = [];

		foreach (Samsung.SleepEntry entry in Samsung.SleepEntry.ParseFromDirectory(data))
		{
			DateTimeOffset start = new(entry.Start, entry.Offset);
			DateTimeOffset end = new(entry.End, entry.Offset);

			if (sleepStages.TryGetValue(entry.Id, out List<SleepStage>? stages) is false)
				stages = [];

			SleepEntry converted = new(start, end, stages, entry.Score / 100, entry.MentalRecovery / 100, entry.PhysicalRecovery / 100, entry.Cycles);
			entries.Add(converted);
		}

		return new(label, entries);
	}
	static void Save(SleepData data, string path)
	{
		string? directory = Path.GetDirectoryName(path);
		if (directory is not null && Directory.Exists(directory) is false)
			Directory.CreateDirectory(directory);

		using FileStream fs = File.Create(path);
		JsonSerializer.Serialize(fs, data, JsonOptions);
	}
	#endregion
}
