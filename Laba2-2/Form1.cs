using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Laba2_2
{
    public partial class Form1 : Form
    {
        private List<LibToolscs> list = new List<LibToolscs>();
        public Form1()
        {
            InitializeComponent();
            string[] pathsDll = Directory.GetFiles("Plugins", "*.dll"); //название папки и расширения, которые надо извлечь
            foreach (var path in pathsDll)
            {
				try
				{
					list.Add(new LibToolscs(path)); //добавляет в список
				}
				catch { }
			}

            foreach (var method in list)
                comboBox1.Items.Add(method.FuncName()); //добавляем функции в комбобокс
        }

		private void Func()
        {
			cartesianChart1.Series.Clear();
			ChartValues<ObservablePoint> points = new ChartValues<ObservablePoint>();
			Func<string> funcName = null;
			Func<double, double> theFunc = null;
			var name = comboBox1.Text;
			foreach (var method in from method in list
								   where name == method.FuncName()
								   select method)
			{
				funcName = method.FuncName;
				theFunc = method.TheFunc;
			}

			for (double x = 0; x <= 10; x += 0.5)
				points.Add(new ObservablePoint(x, theFunc(x)));

			cartesianChart1.Series.Clear();
			cartesianChart1.Series.Add(new LineSeries
			{
				Values = points,
				Title = funcName()
			});
		}

        private void button1_Click(object sender, EventArgs e)
        {
			Func();
		}
    }
}
