import './external/chartjs.umd.min.js';
import { getChartData } from './utils.js';
import { getCharts } from './index.charts.js';

Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#808080";

(async () => {
	const charts = await getCharts(getChartData);

	for (const key in charts) {
		if (Object.prototype.hasOwnProperty.call(charts, key)) {
			const chart = charts[key];

			const container = document.getElementById(`${key}-chart`);
			const canvas = document.createElement("canvas");
			container.replaceWith(canvas);

			new Chart(canvas, chart);
		}
	}
})();