namespace Converter;

public class CsvFile(string[] headers, string[,] cells)
{
	#region Fields
	private readonly string[] _headers = headers;
	private readonly string[,] _cells = cells;
	#endregion

	#region Properties
	public IReadOnlyList<string> Headers => _headers;
	public int RowCount => _cells.GetLength(1);
	#endregion

	#region Indexers
	public string this[string column, int row]
	{
		get
		{
			int columnIndex = Array.IndexOf(_headers, column);

			return _cells[columnIndex, row];
		}
	}
	#endregion

	#region Functions
	public static CsvFile Load(string path, int linesToSkip = 0)
	{
		using StreamReader reader = new(path);

		for (int i = 0; i < linesToSkip; i++)
			_ = reader.ReadLine();

		string headersStr = reader.ReadLine() ?? throw new InvalidDataException("Not enough rows in the file to detect the headers.");
		string[] headers = headersStr.Split(',');

		List<string[]> rows = [];
		string? line = reader.ReadLine();

		while (line is not null)
		{
			string[] row = line.Split(',');
			Debug.Assert(row.Length >= headers.Length);

			rows.Add(row);
			line = reader.ReadLine();
		}

		string[,] cells = new string[headers.Length, rows.Count];

		for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
		{
			string[] row = rows[rowIndex];
			Debug.Assert(row.Length >= headers.Length);

			for (int columnIndex = 0; columnIndex < headers.Length; columnIndex++)
			{
				string value = row[columnIndex];
				cells[columnIndex, rowIndex] = value;
			}
		}

		return new(headers, cells);
	}
	#endregion
}
