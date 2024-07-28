using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace PerformanceViewer
{
    public partial class Form1 : Form
    {
        private List<PerformanceData> dataList = new List<PerformanceData>();

        public Form1()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            string[] lines = File.ReadAllLines("performance.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                DateTime date = DateTime.ParseExact(parts[0], "yyyy/MM/dd_HH:mm:ss", CultureInfo.InvariantCulture);
                int cpuUsage = int.Parse(parts[1].Replace("[%]", ""));
                int memoryUsage = int.Parse(parts[2].Replace("[KB]", ""));
                int diskUsage = int.Parse(parts[3].Replace("[%]", ""));

                dataList.Add(new PerformanceData(date, cpuUsage, memoryUsage, diskUsage));
            }
        }

        private void CreateChart()
        {            
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd HH:mm";

            if (checkBox1.Checked)
            {
                var seriesCPU = chart1.Series.Add("CPU Usage");
                seriesCPU.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                foreach (var data in dataList)
                {
                    seriesCPU.Points.AddXY(data.Date, data.CpuUsage);
                }
            }

            if (checkBox2.Checked)
            {
                var seriesMemory = chart1.Series.Add("Memory Usage");
                seriesMemory.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                foreach (var data in dataList)
                {
                    seriesMemory.Points.AddXY(data.Date, data.MemoryUsage);
                }
            }

            if (checkBox3.Checked)
            {
                var seriesDisk = chart1.Series.Add("Disk Usage");
                seriesDisk.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                foreach (var data in dataList)
                {
                    seriesDisk.Points.AddXY(data.Date, data.DiskUsage);
                }
            }
        }

        public class PerformanceData
        {
            public DateTime Date { get; }
            public int CpuUsage { get; }
            public int MemoryUsage { get; }
            public int DiskUsage { get; }

            public PerformanceData(DateTime date, int cpuUsage, int memoryUsage, int diskUsage)
            {
                Date = date;
                CpuUsage = cpuUsage;
                MemoryUsage = memoryUsage;
                DiskUsage = diskUsage;
            }
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            CreateChart();
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
            CreateChart();
        }
    }
}
