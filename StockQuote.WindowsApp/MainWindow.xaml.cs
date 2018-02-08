using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockQuote.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String ROOT_URL = "http://ichart.yahoo.com/table.csv?s=";
        private static DateTime? from_date, to_date;
        private static List<Model.StockSearch> DATA_SERVICE;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            to_date = toDate.SelectedDate;
        }

        private void fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            from_date = fromDate.SelectedDate;
        }

        private void toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            to_date = toDate.SelectedDate;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string userStocks = textBox.Text;
            StringBuilder urlParamBuilder = new StringBuilder(userStocks.Trim().ToUpper());

            if (userStocks.Length == 0)
            {
                // Download all S&amp;P500 stocks
                MessageBox.Show("Please enter symbol");
            }
            else if (from_date == null || to_date == null)
            {
                MessageBox.Show("Please select From date and To date");
            }
            else
            {
                DateTime fDate, tDate;
                fDate = (DateTime)from_date;
                tDate = (DateTime)to_date;
                // Read user input and download those stocks
                urlParamBuilder.Append("&a=");
                urlParamBuilder.Append(fDate.Month - 1);
                urlParamBuilder.Append("&b=");
                urlParamBuilder.Append(fDate.Day);
                urlParamBuilder.Append("&c=");
                urlParamBuilder.Append(fDate.Year);
                urlParamBuilder.Append("&d=");
                urlParamBuilder.Append(tDate.Month - 1);
                urlParamBuilder.Append("&e=");
                urlParamBuilder.Append(tDate.Day);
                urlParamBuilder.Append("&f=");
                urlParamBuilder.Append(tDate.Year);

                if (isWeekly.IsChecked != null && isWeekly.IsChecked.Value)
                {
                    urlParamBuilder.Append("&g=w");
                }


                String urlParam = urlParamBuilder.ToString();


                using (WebClient client = new WebClient())
                {
                    try
                    {
                        // get company full name
                        client.DownloadFile("http://finance.yahoo.com/d/quotes.csv?s=" + userStocks + "&f=sn"
                        , userStocks + ".csv");

                        // get data
                        client.DownloadFile(ROOT_URL + urlParam + "&ignore=.csv",
                            userStocks + "+" + fDate.ToString("yyyyMMdd") + "+" + tDate.ToString("yyyyMMdd") + ".csv"
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cannot retrieve data");
                    }
                }

                // get the bin folder
                String getBinFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // Check if path is a folder
                FileAttributes attr = File.GetAttributes(getBinFolder);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    var file = System.IO.Path.Combine(getBinFolder,
                        userStocks + "+" + fDate.ToString("yyyyMMdd") + "+" + tDate.ToString("yyyyMMdd") + ".csv"
                     );
                    try
                    {
                        //listView.ItemsSource = null;
                        dataGrid.ItemsSource = null;
                        DATA_SERVICE = readCSV(file);
                        dataGrid.ItemsSource = DATA_SERVICE;

                        //listView.View = createGridView();
                        listView.Visibility = Visibility.Visible;

                        label6.Content = "How is " + userStocks + " doing?";
                        label6.Visibility = Visibility.Visible;

                        button1.Visibility = Visibility.Visible;
                    }
                    catch (InvalidOperationException iOE)
                    {
                        if (MessageBox.Show("Cannot display data. View in Excel?", "Cannot display", MessageBoxButton.OKCancel)
                         == MessageBoxResult.OK)
                        {
                            Process.Start(file);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Cannot save retrieved data to disk");
                }


            }


        }

        //private GridView createGridView()
        //{
        //    GridView grid = new GridView();
        //    grid.AllowsColumnReorder = true;

        //    GridViewColumn dateColumn = new GridViewColumn();
        //    dateColumn.DisplayMemberBinding = new Binding("Date");
        //    dateColumn.Header = "Date";

        //    GridViewColumn openColumn = new GridViewColumn();
        //    openColumn.DisplayMemberBinding = new Binding("Open");
        //    openColumn.Header = "Open";

        //    return grid;
        //}

        private List<Model.StockSearch> readCSV(String filePath)
        {
            StringBuilder reading = new StringBuilder();

            try
            {

                using (var reader = new StreamReader(File.OpenRead(filePath)))
                {
                    while (!reader.EndOfStream)
                    {
                        reading.Append(reader.ReadLine());
                        reading.Append("X");
                    }
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                MessageBox.Show("File Not Found", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return getDataList(reading.ToString(), filePath.Substring(0, 4));
        } // end read csv
        // TODO: finish method with real data parameters
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder("<html><head><title>Chart</title>");
            // Open header
            sb.Append("<script type=\"text/javascript\" src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js\"></script> ");
            // Open javscript function
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("function makeChart(chrt, myarr){var data = {labels: [\"January\", \"February\", \"March\", \"April\", \"May\", \"June\", \"July\", 'test2', 'test3', 'test5', 'test6'], datasets: [{'label': \"Dataset\", 'fillColor': \"rgba(230,159,0,0.8)\",'strokeColor': \"rgba(220,220,220,0.8)\",'highlightFill': \"rgba(220,220,220,0.75)\",'highlightStroke': \"rgba(220,220,220,1)\",'data': myarr},]};return new Chart(chrt).Bar(data);}");
            // TODO: call the function
            sb.Append("var chrt = document.getElementById(\"mycanvas\").getContext(\"2d\");var myarr = [8.7, 8.5, 7, 8.3, 8.2, 8.6, 8.8, 8.7, 6.5, 8.5, 7.4, 7.5, 8.8, 8.7, 5.8, 8.1, 8.9, 7.2, 7.0, 8.0, 7.6, 8.3, 8.5, 8.5, 8.2]; var myFirstChart = makeChart(chrt, myarr); ");
            sb.Append("</script></head><body>");
            sb.Append("<canvas id=\"mycanvas\" width=\"600\" height=\"400\"></canvas>");
            sb.Append("</body></html>");

            String fileName = textBox.Text + from_date.ToString().Replace("/", "-") +
                to_date.ToString().Replace("/", "-") + ".html";

            fileName.Replace(":", "");
            fileName.Replace(" ", "");

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(sb.ToString());
                    }
                }// end filestream


                var file = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
                //Process.Start(file);
                WebBrowser wb = new WebBrowser();


            }
            catch (NotSupportedException nsE)
            {
                MessageBox.Show("Sorry cannot show chart.");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<Model.StockSearch> getDataList(string v, String symbol)
        {
            List<Model.StockSearch> list = new List<Model.StockSearch>();
            var data = v.Split(new string[] { "Date,Open,High,Low,Close,Volume,Adj Close" }, StringSplitOptions.None);
            v = data[1];
            String[] listEntry = v.Split('X');
            try
            {
                foreach (String s in listEntry)
                {
                    //String[] ss = LineSplitter(s).ToArray();
                    if (s != null && s != String.Empty)
                    {
                        String[] ss = s.Split(',');
                        list.Add(
                            new Model.StockSearch(
                                symbol,
                                Date: ss[0].Trim(),
                                Open: Convert.ToDecimal(ss[1]),
                                High: Convert.ToDecimal(ss[2]),
                                Low: Convert.ToDecimal(ss[3]),
                                Close: Convert.ToDecimal(ss[4]),
                                Volume: Convert.ToDecimal(ss[5]),
                                Adj_Close: Convert.ToDecimal(ss[6])
                                )
                         );
                    }
                } // end foreach
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something is wrong");
            }
            return list;
        }

        //IEnumerable<string> LineSplitter(string line)
        //{
        //    int fieldStart = 0;
        //    for (int i = 0; i < line.Length; i++)
        //    {
        //        if (line[i] == ',')
        //        {
        //            yield return line.Substring(fieldStart, i - fieldStart);
        //            fieldStart = i + 1;
        //        }
        //        if (line[i] == '"')
        //            for (i++; line[i] != '"'; i++) { }
        //    }
        //}
    }

}
