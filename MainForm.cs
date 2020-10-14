using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string[] parseCSVLine(string lines)
        {
            string[] result = lines.Split(',');
            for (int j = 0; j < result.Length; j++)
            {
                if (result[j].Contains('"'))
                {
                    for (int k = j + 1; k < result.Length; k++)
                    {
                        result[j] += result[k];

                        if (result[k].Contains('"'))
                        {
                            List<string> tmp = new List<string>(result);
                            for (int h = j + 1; h < k + 1; h++)
                                tmp.RemoveAt(h);
                            result = tmp.ToArray();
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private List<List<string>> MakeColumnarDataStructure()
        {
            StreamReader sr = new StreamReader(new FileStream("market.csv", FileMode.Open), Encoding.Default);
            

            var lines = sr.ReadLine();
            var headers = lines.Split(',');

            List<List<string>> data = new List<List<string>>();

            foreach (string header in headers)
            {
                List<string> list = new List<string>();
                list.Add(header);

                data.Add(list);
            }

            while (sr.EndOfStream == false)
            {
                lines = sr.ReadLine();
                var values = parseCSVLine(lines);

                for(int i = 0; i < values.Length; i++)
                {
                    data[i].Add(values[i]);
                }
            }            

            sr.Close();
            return data;
        }
        
        private void printColumnData_columnbased(List<List<string>> data, int columnIdx)
        {
            

            string str = "";
            foreach (string value in data[columnIdx])
            {
                str += value + "\r\n";
            }

            textBoxCSVView.Text = str;
        }

        private void printRowData_columnbased(List<List<string>> data)
        {
            string str = "";
            for(int i = 0; i<data[0].Count; i++) { 
                foreach (List<string> value in data)
                {
                    str += value[i] + "\t";
                }
                str += "\r\n";
            }

            textBoxCSVView.Text = str;
        }

        private List<List<string>> MakeRowbaseDataStructure()
        {
            StreamReader sr = new StreamReader(new FileStream("market.csv", FileMode.Open), Encoding.Default);


            string lines = "";

            List<List<string>> data = new List<List<string>>();

            while (sr.EndOfStream == false)
            {
                lines = sr.ReadLine();
                var values = parseCSVLine(lines);

                data.Add(values.ToList());
            }

            sr.Close();
            return data;
        }
        private void printRowData_rowbased(List<List<string>> data)
        {
            string str = "";
            foreach(List<string> rowlist in data)
            {
                foreach(string value in rowlist)
                {
                    str += value + "\t";
                }
                str += "\r\n";
            }
            textBoxCSVView.Text = str;
        }
        private void printColumnData_rowbased(List<List<string>> data, int index)
        {
            string str = "";

            foreach(List<string> rowlist in data)
            {
                str += rowlist[index] + "\r\n";
            }                

            textBoxCSVView.Text = str;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<List<string>> data = MakeRowbaseDataStructure();
            printRowData_rowbased(data);
            printColumnData_rowbased(data, 5);

            //List<List<string>> data = MakeColumnarDataStructure();
            //printColumnData_columnbased(data, 1);
            //printRowData_columnbased(data);
        }
    }
}
