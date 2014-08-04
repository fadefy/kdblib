using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

namespace Kdb4Net.Test
{
    /// <summary>
    /// Interaction logic for LineChart.xaml
    /// </summary>
    public partial class LineChart : UserControl
    {
        public LineChart()
        {
            InitializeComponent();
            
        }

        public void AddLine(ISeries line)
        {
            chart.Series.Add(line);
        }
    }
}
