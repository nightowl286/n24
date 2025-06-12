namespace Converter.Samsung;

public static class Parser
{
	#region Functions
	public static TimeSpan ParseOffset(this string offset)
	{
		if (offset.Length is not 8)
			throw new ArgumentException($"Expected the offset to be 8 character, was {offset.Length:n0} instead.", nameof(offset));

		if (offset.StartsWith("UTC+") is false)
			throw new ArgumentException("Expected the offset to start with the 'UTC+' string", nameof(offset));

		int hours = int.Parse(offset.AsSpan(4, 2));
		int minutes = int.Parse(offset.AsSpan(6, 2));

		return new(hours, minutes, 0);
	}
	#endregion
}
