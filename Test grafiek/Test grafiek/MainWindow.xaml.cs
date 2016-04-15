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



namespace grafiek
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DBConnection test = new DBConnection();
      List<object>[] information = test.Select("SELECT Maandnaam, Count(*) AS TotalMaand FROM straatroof WHERE Maandnaam IS NOT NULL and jaar = '2012'  group by Maandnaam");
      this.createGraph(information[0]);
      /*foreach (cdagdeel hah in list[0])
      {
        he.Inlines.Add(Convert.ToString(hah.Id));
        he.Inlines.Add(Convert.ToString(hah.dagdeel));
      }
      */

    }

    // ultra custom graph Reinier check dit!!!
    private void _canvasPlaceSingleColor(Canvas canvas, Color color, int height , int i, int col, string colName, int max , int min)
    {
      //column
      Rectangle rect = new Rectangle();
      Brush paint = new SolidColorBrush(color);
      rect.Fill = paint;
      rect.Width = ((canvas.Width / (col+1))-2);
      rect.Height = height;
      Canvas.SetLeft(rect, (i+1)*(rect.Width+2));
      Canvas.SetBottom(rect, 20);
     
      // columnName
      TextBlock txt = new TextBlock();
      txt.Text = colName;
      txt.HorizontalAlignment = HorizontalAlignment.Center;
      Canvas.SetLeft(txt, (i+1) * (rect.Width+2));
      Canvas.SetBottom(txt, 0);
      
      // columnInt
      TextBlock txt2 = new TextBlock();
      txt2.Text = Convert.ToString(height);
      Canvas.SetLeft(txt2, 0);
      Canvas.SetBottom(txt2, (height+20));

      myCanvas.Children.Add(rect);
      myCanvas.Children.Add(txt);
      if (rect.Height == max || rect.Height == min)
      {
        myCanvas.Children.Add(txt2);
      }
    }
    private void _createPyChart(Canvas canvas, Color color)
    {
      Ellipse circle = new Ellipse();
      Brush paint = new SolidColorBrush(color);
      circle.Fill = paint;
      circle.Width = 100;
      circle.Height = 100;
      Canvas.SetLeft(circle, 150);
      Canvas.SetBottom(circle, 150);
      myPyCanvas.Children.Add(circle);
    }
    
    public void createGraph(List<object> information)
    {
      int i = 0;
      int min=999999;
      int max=0;
      // lengthe loop is array
      foreach (cstraatroof sr in information)
      {
        if (sr.Total > max)
        {
          max = sr.Total;
        }
        if (sr.Total < min)
        {
          min = sr.Total;
        }
      }
      foreach (cstraatroof sr in information)
      {
        Color color = i % 2 == 0 ? (Color)ColorConverter.ConvertFromString("#AEAEAE") : (Color)ColorConverter.ConvertFromString("#EAEAEA");
        _canvasPlaceSingleColor(myCanvas, color, sr.Total, i, information.Count, sr.Maandnaam, max, min);
        i++;
      }
    }
    public void pieChart()
    {

    }
  }
}