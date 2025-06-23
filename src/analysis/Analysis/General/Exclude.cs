namespace N24.Analysis;

partial class GeneralExtensions
{
	#region Methods
	/// <summary>Excludes the <paramref name="values"/> that are outside of the given <paramref name="min"/> and <paramref name="max"/> range.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to filter.</typeparam>
	/// <param name="values">The values to filter.</param>
	/// <param name="min">The minimum value in the allowed range.</param>
	/// <param name="max">The maximum value in the allowed range.</param>
	/// <returns>The filtered <paramref name="values"/>.</returns>
	public static IEnumerable<T> ExcludeOutside<T>(this IEnumerable<T> values, T min, T max)
		where T : IComparable<T>
	{
		foreach (T value in values)
		{
			if (IsBetween(value, min, max))
				yield return value;
		}
	}

	/// <summary>Excludes the <paramref name="values"/> that are outside of the given <paramref name="deviation"/> from the given <paramref name="mean"/> value.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to filter.</typeparam>
	/// <param name="values">The values to filter.</param>
	/// <param name="mean">The mean of the given <paramref name="values"/>.</param>
	/// <param name="deviation">The deviation of the given <paramref name="values"/>.</param>
	/// <returns>The filtered <paramref name="values"/>.</returns>
	public static IEnumerable<T> ExcludeDeviation<T>(this IEnumerable<T> values, T mean, T deviation)
		where T : IComparable<T>, ISubtractionOperators<T, T, T>, IAdditionOperators<T, T, T>
	{
		T min = checked(mean - deviation);
		T max = checked(mean + deviation);

		return ExcludeOutside(values, min, max);
	}

	/// <summary>Filters the given <paramref name="values"/> to exclude any <see langword="null"/> values.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to filter.</typeparam>
	/// <param name="values">The values exclude any <see langword="null"/> values from.</param>
	/// <returns>The filtered values.</returns>
	public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> values)
	{
		foreach (T? value in values)
		{
			if (value is not null)
				yield return value;
		}
	}
	#endregion

	#region Helpers
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsBetween<T>(T value, T min, T max)
		where T : IComparable<T>
	{
		Debug.Assert(min.CompareTo(max) <= 0);

		if (value.CompareTo(min) < 0)
			return false;

		if (value.CompareTo(max) > 0)
			return false;

		return true;
	}
	#endregion
}

partial class TimeSpanExtensions
{
	#region Methods
	/// <summary>Excludes the <paramref name="values"/> that are outside of the given <paramref name="deviation"/> from the given <paramref name="mean"/> value.</summary>
	/// <param name="values">The values to filter.</param>
	/// <param name="mean">The mean of the given <paramref name="values"/>.</param>
	/// <param name="deviation">The deviation of the given <paramref name="values"/>.</param>
	/// <returns>The filtered <paramref name="values"/>.</returns>
	public static IEnumerable<TimeSpan> ExcludeDeviation(this IEnumerable<TimeSpan> values, TimeSpan mean, TimeSpan deviation)
	{
		TimeSpan min = checked(mean - deviation);
		TimeSpan max = checked(mean + deviation);

		return values.ExcludeOutside(min, max);
	}
	#endregion
}