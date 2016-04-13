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
    private void _placeSingleColorColumn(Grid grid, Color color, int height, int colNum, int maxHeight)
    {
      Brush brush = new SolidColorBrush(color);

      Rectangle rect = new Rectangle();
      double hoog = grid.Height;
      rect.Fill = brush;
      rect.Width = 50;
      Grid.SetColumn(rect, colNum);
      Grid.SetRow(rect, Convert.ToInt32(hoog) - height);
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
        string[] aLabels = "10,9,8,7,6,5,4,3,2,1".Split(',');
        _createLabelsvert(this.myGridLeft, aLabels);

        ColumnDefinition colDef = new ColumnDefinition();
        ColumnDefinition colDef2 = new ColumnDefinition();
        myGridMain.ColumnDefinitions.Add(colDef);
        myGridBottom.ColumnDefinitions.Add(colDef2);

        RowDefinition rowDef = new RowDefinition();
        myGridMain.RowDefinitions.Add(rowDef);

        Color color = i % 2 == 0 ? (Color)ColorConverter.ConvertFromString("#AEAEAE") : (Color)ColorConverter.ConvertFromString("#EAEAEA");

        if(sr.Maandnaam != "feb")
        {
          _placeSingleColorColumn(this.myGridMain, color, sr.Total, i, 300);
        }
        

        i++;
      }
      
      string[] bLabels = "Dogs,Cats,Birds,Snakes,Rabbits,Hamsters,Horses,Rats,Bats,Unicorns".Split(',');
      _createLabelshor(this.myGridBottom, bLabels);
    }
  }


}
