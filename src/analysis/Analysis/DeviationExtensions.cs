namespace N24.Analysis;

/// <summary>
/// 	Contains various extension methods related to value deviation.
/// </summary>
public static partial class DeviationExtensions
{
	#region Methods
	/// <summary>Calculates the <paramref name="deviation"/> of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The types of the <paramref name="values"/> to calculate the <paramref name="deviation"/> for.</typeparam>
	/// <param name="values">The values to calculate the <paramref name="deviation"/> for.</param>
	/// <param name="deviation">The calculated deviation.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> GetDeviation<T>(this IEnumerable<T> values, out T deviation)
		where T : INumberBase<T>
	{
		T mean = T.AdditiveIdentity, squared = T.AdditiveIdentity, count = T.AdditiveIdentity;
		List<T> collected = [];

		checked
		{
			foreach (T value in values)
			{
				count += T.One;
				T old = mean;

				mean += (value - mean) / count;
				squared += (value - mean) * (value - old);

				collected.Add(value);
			}

			deviation = squared / (count - T.One);
		}

		return collected;
	}

	/// <summary>Calculates the deviation of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The types of the <paramref name="values"/> to calculate the deviation for.</typeparam>
	/// <param name="values">The values to calculate the deviation for.</param>
	/// <returns>The calculated deviation.</returns>
	public static T GetDeviation<T>(this IEnumerable<T> values)
		where T : INumberBase<T>
	{
		T mean = T.AdditiveIdentity, squared = T.AdditiveIdentity, count = T.AdditiveIdentity;

		checked
		{
			foreach (T value in values)
			{
				count += T.One;
				T old = mean;

				mean += (value - mean) / count;
				squared += (value - mean) * (value - old);
			}

			T deviation = squared / (count - T.One);
			return deviation;
		}
	}

	/// <summary>Calculates the <paramref name="deviation"/> of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The types of the <paramref name="values"/> to calculate the <paramref name="deviation"/> for.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to calculate the <paramref name="deviation"/> for.</param>
	/// <param name="deviation">The deviation of the given <paramref name="values"/>.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> GetDeviation<T, TNumber>(this IEnumerable<T> values, out T deviation, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : INumberBase<TNumber>, IRootFunctions<TNumber>
	{
		List<T> collected = [];

		TNumber sum = TNumber.AdditiveIdentity, count = TNumber.AdditiveIdentity;

		foreach (T value in values)
		{
			sum = checked(sum + convertDelegate.Invoke(value));
			count = checked(count + TNumber.One);

			collected.Add(value);
		}

		TNumber mean = sum / count;
		TNumber squared = TNumber.AdditiveIdentity;

		foreach (T value in collected)
		{
			TNumber delta = convertDelegate.Invoke(value) - mean;
			squared += delta * delta;
		}

		TNumber temp = TNumber.Sqrt(squared / (count - TNumber.One));
		deviation = convertBackDelegate.Invoke(temp);

		return collected;
	}

	/// <summary>Calculates the deviation of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The types of the <paramref name="values"/> to calculate the deviation for.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to calculate the deviation for.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The calculated deviation.</returns>
	public static T GetDeviation<T, TNumber>(this IEnumerable<T> values, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : INumberBase<TNumber>
	{
		TNumber mean = TNumber.AdditiveIdentity, squared = TNumber.AdditiveIdentity, count = TNumber.AdditiveIdentity;

		checked
		{
			foreach (T value in values)
			{
				count += TNumber.One;
				TNumber old = mean;
				TNumber numeric = convertDelegate.Invoke(value);

				mean += (numeric - mean) / count;
				squared += (numeric - mean) * (numeric - old);
			}

			TNumber deviation = squared / (count - TNumber.One);
			return convertBackDelegate.Invoke(deviation);
		}
	}
	#endregion
}

partial class TimeSpanExtensions
{
	#region Methods
	/// <summary>Calculates the <paramref name="deviation"/> of the given <paramref name="values"/>.</summary>
	/// <param name="values">The values to calculate the <paramref name="deviation"/> for.</param>
	/// <param name="deviation">The calculated deviation.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<TimeSpan> GetDeviation(this IEnumerable<TimeSpan> values, out TimeSpan deviation) => values.GetDeviation(out deviation, FromTimeSpan, ToTimeSpan);

	/// <summary>Calculates the deviation of the given <paramref name="values"/>.</summary>
	/// <param name="values">The values to calculate the deviation for.</param>
	/// <returns>The calculated deviation.</returns>
	public static TimeSpan GetDeviation(this IEnumerable<TimeSpan> values) => values.GetDeviation(FromTimeSpan, ToTimeSpan);
	#endregion
}