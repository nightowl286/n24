Chart.defaults.color = "#d7d7d7";


(async function () {
	const data = [
		{ day: 0, hours: 7.3 },
		{ day: 1, hours: 7.4 },
		{ day: 2, hours: 7.2 },
		{ day: 3, hours: 7.3 },
		{ day: 4, hours: 7.3 },
		{ day: 5, hours: 7.3 },
		{ day: 6, hours: 7.7 },
	];

	new Chart(
		document.getElementById('weekday-sleep'),
		{
			type: 'bar',
			options: {
				plugins: {
					title: {
						display: true,
						text: "Average (mean) sleep (in hours) per week day"
					}
				}
			},
			data: {
				labels: data.map(row => ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'][row.day]),
				datasets: [
					{
						label: 'nightowl286',
						data: data.map(row => row.hours)
					}
				]
			}
		}
	);
})();



