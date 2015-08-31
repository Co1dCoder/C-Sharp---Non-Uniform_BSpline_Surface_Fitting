using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PTL.Geometry;
using PTL.Geometry.MathModel;

namespace Non_uniform_BSpline_Surface_Fitting
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double[][] data1 = PTL.Tools.FileOperation.Json.ReadJsonFile<double[][]>("DataPoints.txt");
            XYZ[] dataPoints1 = new XYZ[data1.Length];
            for (int i = 0; i < data1.Length; i++)
                dataPoints1[i] = new XYZ(data1[i]);

            double[][] data2 = PTL.Tools.FileOperation.Json.ReadJsonFile<double[][]>("DataPoints.txt");
            XYZ[] dataPoints2 = new XYZ[data2.Length];
            for (int i = 0; i < data2.Length; i++)
            {
                dataPoints2[i] = new XYZ(data2[i]);
                dataPoints2[i].X -= 10;
                dataPoints2[i].Y += 10;
            }

            double[][] data3 = PTL.Tools.FileOperation.Json.ReadJsonFile<double[][]>("DataPoints.txt");
            XYZ[] dataPoints3 = new XYZ[data3.Length];
            for (int i = 0; i < data3.Length; i++)
            {
                dataPoints3[i] = new XYZ(data3[i]);
                dataPoints3[i].Y += 20;
            }

            double[][] data4 = PTL.Tools.FileOperation.Json.ReadJsonFile<double[][]>("DataPoints.txt");
            XYZ[] dataPoints4 = new XYZ[data4.Length];
            for (int i = 0; i < data4.Length; i++)
            {
                dataPoints4[i] = new XYZ(data4[i]);
                dataPoints4[i].X += 10;
                dataPoints4[i].Y += 30;
                dataPoints4[i].Z -= 5;
            }

            double[][] data5 = PTL.Tools.FileOperation.Json.ReadJsonFile<double[][]>("DataPoints.txt");
            XYZ[] dataPoints5 = new XYZ[data5.Length];
            for (int i = 0; i < data5.Length; i++)
            {
                dataPoints5[i] = new XYZ(data5[i]);
                dataPoints5[i].Y += 40;
                dataPoints5[i].Z -= 10;
            }

            Non_Uniform_B_Spline_Curve nurbs_Curve1 = new Non_Uniform_B_Spline_Curve(dataPoints1);
            Non_Uniform_B_Spline_Curve nurbs_Curve2 = new Non_Uniform_B_Spline_Curve(dataPoints2);
            Non_Uniform_B_Spline_Curve nurbs_Curve3 = new Non_Uniform_B_Spline_Curve(dataPoints3);
            Non_Uniform_B_Spline_Curve nurbs_Curve4 = new Non_Uniform_B_Spline_Curve(dataPoints4);
            Non_Uniform_B_Spline_Curve nurbs_Curve5 = new Non_Uniform_B_Spline_Curve(dataPoints5);
            Non_Uniform_B_Spline_Surface nurbs_Surface2 = new Non_Uniform_B_Spline_Surface(new XYZ[][] { dataPoints1, dataPoints2, dataPoints3, dataPoints4, dataPoints5 });

            PTL.Tools.DebugTools.Plot plot = new PTL.Tools.DebugTools.Plot();
            plot.Window.View.AutoScale = false;
            plot.ParameterPlot((para) => nurbs_Curve1.CurveFunc(para), 0, 1, 1000);
            plot.ParameterPlot((para) => nurbs_Curve2.CurveFunc(para), 0, 1, 1000);
            plot.ParameterPlot((para) => nurbs_Curve3.CurveFunc(para), 0, 1, 1000);
            plot.ParameterPlot((para) => nurbs_Curve4.CurveFunc(para), 0, 1, 1000);
            plot.ParameterPlot((para) => nurbs_Curve5.CurveFunc(para), 0, 1, 1000);
            plot.ParameterPlot((u, v) => nurbs_Surface2.SurfaceFunc(u, v), 0, 1, 100, 0, 1, 100, (tf) => { tf.SurfaceDisplayOption = PTL.Geometry.SurfaceDisplayOptions.EdgeOnly; });
            

            PointD[] dataPointD1 = (from coordinate in dataPoints1
                                   select new PointD(coordinate.Value) { OpenGLDisplaySize = 0.005 }).ToArray();
            plot.Window.View.AddSomeThing2Show(dataPointD1);
            PointD[] dataPointD2 = (from coordinate in dataPoints2
                                   select new PointD(coordinate.Value) { OpenGLDisplaySize = 0.005 }).ToArray();
            plot.Window.View.AddSomeThing2Show(dataPointD2);
        }
    }
}
