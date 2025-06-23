namespace N24.Analysis;

/// <summary>
/// 	Contains various extension methods for general maths functions.
/// </summary>
public static partial class GeneralExtensions
{
}

/// <summary>
/// 	Contains various extension methods for <see cref="TimeSpan"/> values.
/// </summary>
public static partial class TimeSpanExtensions
{
	#region TimeSpan methods
	private static double FromTimeSpan(TimeSpan value) => value.TotalMinutes;
	private static TimeSpan ToTimeSpan(double value) => TimeSpan.FromMinutes(value);

	/// <summary>Converts the given time-span <paramref name="value"/> to a formatted <see langword="string"/>.</summary>
	/// <param name="value">The value to format.</param>
	/// <param name="includeMilliseconds">Whether the milliseconds should be included in the formatted string.</param>
	/// <returns>The formatted <see langword="string"/>.</returns>
	public static string Pretty(this TimeSpan value, bool includeMilliseconds = false)
	{
		List<string> parts = [];

		if (value.Days > 0) parts.Add($"{value.Days:n0}d");
		if (value.Hours > 0) parts.Add($"{value.Hours}h");
		if (value.Minutes > 0) parts.Add($"{value.Minutes}m");

		if (includeMilliseconds)
		{
			if (value.Seconds > 0) parts.Add($"{value.Seconds}.{value.Milliseconds / 10:n0}s");
			else if (parts.Count is 0 || value.Milliseconds > 0) parts.Add($"{value.Milliseconds}ms");
		}
		else if (parts.Count is 0 || value.Seconds > 0) parts.Add($"{value.Seconds}s");

		return string.Join(' ', parts);
	}
	#endregion
}