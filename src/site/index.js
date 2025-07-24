import './external/chartjs.umd.min.js';
import { charts } from './index.charts.js';

Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#808080";

for (const key in charts) {
	if (Object.prototype.hasOwnProperty.call(charts, key)) {
		const chart = charts[key];

		const canvas = document.getElementById(`${key}-chart`);
		new Chart(canvas, chart);
	}
}