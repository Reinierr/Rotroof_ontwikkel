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


namespace Test_grafiek
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
      List<object>[] information = test.Select("SELECT Maandnaam, Count(*) AS TotalMaand FROM straatroof WHERE Maandnaam IS NOT NULL group by Maandnaam");
      this.createGrid10x10(information[0]);
      /*foreach (cdagdeel hah in list[0])
      {
        he.Inlines.Add(Convert.ToString(hah.Id));
        he.Inlines.Add(Convert.ToString(hah.dagdeel));
      }
      */

    }
    private void _canvasPlaceSingleColor(Canvas canvas, Color color, int height , int i)
    {
      Rectangle rect = new Rectangle();
      Brush paint = new SolidColorBrush(color);
      rect.Fill = paint;
      rect.Width = 30;
      rect.Height = height;
      Canvas.SetLeft(rect, i*35);
      Canvas.SetBottom(rect, 0);

      myCanvas.Children.Add(rect);
    }
    private void _placeSingleColorColumn(Grid grid, Color color, int height, int colNum, int maxHeight)
    {
      Brush brush = new SolidColorBrush(color);

      Rectangle rect = new Rectangle();
      double hoog = grid.Height;
      rect.Fill = brush;
      rect.Width = 50;
      Grid.SetColumn(rect, colNum);
      Grid.SetRow(rect,maxHeight - height);
      Grid.SetRowSpan(rect, height);

      grid.Children.Add(rect);
    }

    private void _createLabelsvert(Grid grid, string[] labels)
    {
      RowDefinition rowDefnLabels = new RowDefinition();
      grid.RowDefinitions.Add(rowDefnLabels);

      for (int i = 0; i < labels.Length; i++)
      {
        TextBlock block = new TextBlock();
        block.Text = labels[i];
        block.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        Grid.SetRow(block, i);
        Grid.SetColumn(block, grid.ColumnDefinitions.Count);
        grid.Children.Add(block);
      }
    }
    private void _createLabelshor(Grid grid, string[] labels)
    {
      RowDefinition rowDefnLabels = new RowDefinition();
      grid.RowDefinitions.Add(rowDefnLabels);

      for (int i = 0; i < labels.Length; i++)
      {
        TextBlock block = new TextBlock();
        block.Text = labels[i];
        block.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        Grid.SetColumn(block, i);
        Grid.SetRow(block, grid.RowDefinitions.Count);
        grid.Children.Add(block);
      }
    }
    public void createGrid10x10(List<object> information)
    {
      Random random = new Random();
      int i = 0;
      // lengthe loop is array
      foreach (cstraatroof sr in information)
      {
        string[] aLabels = "300,270,240,210,180,150,120,90,60,30".Split(',');
        _createLabelsvert(this.myGridLeft, aLabels);

        ColumnDefinition colDef = new ColumnDefinition();
        ColumnDefinition colDef2 = new ColumnDefinition();
        myGridMain.ColumnDefinitions.Add(colDef);
        myGridBottom.ColumnDefinitions.Add(colDef2);

        RowDefinition rowDef = new RowDefinition();
        myGridMain.RowDefinitions.Add(rowDef);

        Color color = i % 2 == 0 ? (Color)ColorConverter.ConvertFromString("#AEAEAE") : (Color)ColorConverter.ConvertFromString("#EAEAEA");



          _placeSingleColorColumn(this.myGridMain, color, sr.Total/30, i, 10);
        _canvasPlaceSingleColor(myCanvas, color, sr.Total,i);


      i++;
      }
      
      string[] bLabels = "Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Aug,Sep,Okt,Nov,Dec".Split(',');
      _createLabelshor(this.myGridBottom, bLabels);
    }
  }


}
