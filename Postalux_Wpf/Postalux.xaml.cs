using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Postalux_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Postalux : Window
    {
        General _general = new General();

        public Postalux()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Display string representations of numbers for en-us culture.
                CultureInfo ci = new CultureInfo("en-us");

                string Path_App = System.AppDomain.CurrentDomain.BaseDirectory;

                XmlDocument xmldoc = new XmlDocument();
                FileStream fs = new FileStream(Path_App + "..\\..\\App_Data\\Cards.xml", FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);

                XmlNodeList xnList = xmldoc.SelectNodes("/Cards/Card");
                List<CardDisplay> items = new List<CardDisplay>();

                foreach (XmlNode xn in xnList)
                {
                    // Gather data about this card:
                    Int32 i_ScottUxNum_List = Convert.ToInt32(xn["ScottUxNum"].InnerText);
                    decimal dec_FaceVal = Convert.ToDecimal(xn["FaceVal"].InnerText);
                    string str_FaceVal = _general.FormatDollarsOrCents(dec_FaceVal);
                    string str_Descr = xn["Descr"].InnerText;
                    string str_Issued = "";
                    if (xn["Issued"] != null) str_Issued = xn["Issued"].InnerText;
                    string FileName_Card = i_ScottUxNum_List.ToString("D3", ci) + ".jpeg";
                    string RelPathedFileName_Card = "..\\Images\\Data\\" + FileName_Card;
                    string PathedFileName_Card = "C:\\Users\\Michael.C.Adams\\PD\\Postalux\\Postalux\\Images\\Data\\" + FileName_Card;

                    // Add the data to the listview item:
                    items.Add(new CardDisplay() { ScottNum = "UX" + i_ScottUxNum_List.ToString(), FaceVal = str_FaceVal, Descr = str_Descr, Issued = str_Issued });
                }
                lv_Cards.ItemsSource = items;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Cards.ItemsSource);
                view.Filter = UserFilter;

                btn_Series_All.IsChecked = true;
            }
            catch (Exception ex)
            {
            }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(textbox_SearchByDescr.Text)) return true;
            else return ((item as CardDisplay).Descr.IndexOf(textbox_SearchByDescr.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void textbox_SearchByDescr_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Cards.ItemsSource).Refresh();
        }
    }

    public class CardDisplay
    {
        public string ScottNum { get; set; }

        public string FaceVal { get; set; }

        public string Descr { get; set; }

        public string Issued { get; set; }
    }
}
