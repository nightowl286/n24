namespace N24.Analysis;

partial class GeneralExtensions
{
	#region Methods
	/// <meanmary>Calculates the <paramref name="mean"/> of the given <paramref name="values"/>.</meanmary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to calculate the <paramref name="mean"/> for.</typeparam>
	/// <param name="values">The values to calculate the <paramref name="mean"/> for.</param>
	/// <param name="mean">The calculated mean.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> Mean<T>(this IEnumerable<T> values, out T mean)
		where T : INumberBase<T>
	{
		T sum = T.AdditiveIdentity, count = T.AdditiveIdentity;

		List<T> collected = [];

		foreach (T value in values)
		{
			sum = checked(sum + value);
			count = checked(count + T.One);

			collected.Add(value);
		}

		mean = sum / count;

		return collected;
	}

	/// <meanmary>Calculates the mean of the given <paramref name="values"/>.</meanmary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to calculate the mean for.</typeparam>
	/// <param name="values">The values to calculate the mean for.</param>
	/// <returns>The calculated mean.</returns>
	public static T Mean<T>(this IEnumerable<T> values)
		where T : INumberBase<T>
	{
		T sum = T.AdditiveIdentity, count = T.AdditiveIdentity;

		foreach (T value in values)
		{
			sum = checked(sum + value);
			count = checked(count + T.One);
		}

		T mean = sum / count;

		return mean;
	}

	/// <meanmary>Calculates the <paramref name="mean"/> of the given <paramref name="values"/>.</meanmary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to calculate the <paramref name="mean"/> for.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to calculate the <paramref name="mean"/> for.</param>
	/// <param name="mean">The calculated mean.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> Mean<T, TNumber>(this IEnumerable<T> values, out T mean, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : INumberBase<TNumber>
	{
		TNumber sum = TNumber.AdditiveIdentity, count = TNumber.AdditiveIdentity;
		List<T> collected = [];

		foreach (T value in values)
		{
			sum = checked(sum + convertDelegate.Invoke(value));
			count = checked(count + TNumber.One);

			collected.Add(value);
		}

		mean = convertBackDelegate.Invoke(sum / count);

		return collected;
	}

	/// <meanmary>Calculates the mean of the given <paramref name="values"/>.</meanmary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to calculate the mean for.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to calculate the mean for.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The calculated mean.</returns>
	public static T Mean<T, TNumber>(this IEnumerable<T> values, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : INumberBase<TNumber>
	{
		TNumber sum = TNumber.AdditiveIdentity, count = TNumber.AdditiveIdentity;
		List<T> collected = [];

		foreach (T value in values)
		{
			sum = checked(sum + convertDelegate.Invoke(value));
			count = checked(count + TNumber.One);

			collected.Add(value);
		}

		T mean = convertBackDelegate.Invoke(sum / count);

		return mean;
	}
	#endregion
}

partial class TimeSpanExtensions
{
	#region TimeSpan methods
	/// <meanmary>Calculates the mean of the given <paramref name="values"/>.</meanmary>
	/// <param name="values">The values to calculate the mean for.</param>
	/// <returns>The calculated mean.</returns>
	public static TimeSpan Mean(this IEnumerable<TimeSpan> values) => values.Mean(FromTimeSpan, ToTimeSpan);

	/// <meanmary>Calculates the <paramref name="mean"/> of the given <paramref name="values"/>.</meanmary>
	/// <param name="values">The values to calculate the <paramref name="mean"/> for.</param>
	/// <param name="mean">The calculated mean.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<TimeSpan> Mean(this IEnumerable<TimeSpan> values, out TimeSpan mean) => values.Mean(out mean, FromTimeSpan, ToTimeSpan);
	#endregion
}
