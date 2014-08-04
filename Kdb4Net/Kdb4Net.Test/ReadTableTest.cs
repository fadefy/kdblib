using Kdb4Net.Client.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;

namespace Kdb4Net.Test
{
    [TestClass]
    public class ReadTableTest
    {
        [TestMethod]
        [STAThread]
        public void LoadTableAndShowInLineChart()
        {
            kx.c c = new kx.c("localhost", 5000);
            var g = 30;
            var obj = c.k("monthCur", g) as Flip;
            var window = new Window();
            var lineChart = new LineChart();
            for (int i = 1; i < obj.x.Length; i++)
            {
                var name = obj.x[i];
                var values = (obj.y[i] as long[]).Select((v, index) => new KeyValuePair<int, long>(index * g, v));
                var line = new LineSeries();
                line.Title = name;
                line.SetBinding(LineSeries.ItemsSourceProperty, new Binding() { Source = values });
                line.IndependentValuePath = "Key";
                line.DependentValuePath = "Value";
                line.DataPointStyle = null;
                line.AnimationSequence = AnimationSequence.FirstToLast;
                lineChart.AddLine(line);
            }
            Assert.IsNotNull(obj);
            window.Content = lineChart;
            window.ShowDialog();
        }
    }
}
