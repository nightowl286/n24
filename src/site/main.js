Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#a0a0a0";

async function getChartData(id) {
	try {
		const response = await fetch(`/data/${id}.json`);
		if (!response.ok) {
			throw new Error(`Failed to obtain chart data for (${id}) (result: ${response.status}).`)
		}

		const json = await response.json();
		return json;
	}
	catch (error) {
		console.error(error);
	}
}

function getTimeText(hour) {
	hour %= 24;

	if (hour == 12) return "12pm";
	if (hour == 0) return "12am";

	if (hour < 12)
		return `${hour}am`;

	return `${hour - 12}pm`;
}

async function regularChart() {
	const canvas = document.getElementById("regular-chart");

	new Chart(canvas, {
		type: "bar",
		data: await getChartData("regular"),
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: "top"
				},
				title: {
					display: true,
					text: "Regular sleep times"
				},
				tooltip: {
					callbacks: {
						label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
						footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
					}
				}
			},
			scales: {
				y: {
					min: 22,
					max: 36,
					title: {
						display: true,
						text: "Time of the day"
					},
					ticks: {
						callback: (value, _1, _2) => getTimeText(value)
					},
					afterBuildTicks: axis => axis.ticks = Array.from({ length: 36 - 22 }, (_, i) => ({ value: i + 22 }))
				},
				x: {
					type: "category",
					title: {
						display: true,
						text: "Day of the week",
					},
				}
			}
		}
	});
}

async function dspdChart() {
	const canvas = document.getElementById("dspd-chart");

	new Chart(canvas, {
		type: "bar",
		data: await getChartData("dspd"),
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: "top"
				},
				title: {
					display: true,
					text: "DSPD vs regular sleep times"
				},
				tooltip: {
					callbacks: {
						label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
						footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
					}
				}
			},
			scales: {
				y: {
					min: 20,
					max: 40,
					title: {
						display: true,
						text: "Time of the day"
					},
					ticks: {
						callback: (value, _1, _2) => getTimeText(value)
					},
					afterBuildTicks: axis => axis.ticks = Array.from({ length: 40 - 20 }, (_, i) => ({ value: i + 20 }))
				},
				x: {
					type: "category",
					title: {
						display: true,
						text: "Day of the week",
					},
				}
			}
		}
	});
}

async function aspdChart() {
	const canvas = document.getElementById("aspd-chart");

	new Chart(canvas, {
		type: "bar",
		data: await getChartData("aspd"),
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: "top"
				},
				title: {
					display: true,
					text: "ASPD vs regular sleep times"
				},
				tooltip: {
					callbacks: {
						label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
						footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
					}
				}
			},
			scales: {
				y: {
					min: 17,
					max: 39,
					title: {
						display: true,
						text: "Time of the day"
					},
					ticks: {
						callback: (value, _1, _2) => getTimeText(value)
					},
					afterBuildTicks: axis => axis.ticks = Array.from({ length: 39 - 17 }, (_, i) => ({ value: i + 17 }))
				},
				x: {
					type: "category",
					title: {
						display: true,
						text: "Day of the week",
					},
				}
			}
		}
	});
}

document.addEventListener("DOMContentLoaded", async (event) => {

	await Promise.all([
		regularChart(),
		dspdChart(),
		aspdChart()
	]);

})
