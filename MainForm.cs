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

        private string[] parseCSVLine(string lines) // parsing 핵심 함수
        {
            string[] result = lines.Split(',');     // 받아온 lines를 ',' 기준으로 나누어 result 배열에 저장
            for (int j = 0; j < result.Length; j++)    //  result를 돌면서
            {
                if (result[j].Contains('"'))	//  더블퀘테이션(")을 만나면
                {
                    result[j] = result[j].Remove(0, 1); // "을 제거
                    for (int k = j + 1; k < result.Length; k++) 	// 한 번 더 " 을 찾기 위해 반복
                    {
                        result[j] += result[k]; 	// 반복하면서 string을 합쳐줍니다

                        if (result[k].Contains('"')) 	// 현재 방문한 배열에 "가 있다면(짝이 될" 을 찾았다면)
                        { 	// 배열을 삭제하기 위해 임시로 List에 넣어서 삭제 후 반환해줍니다
                            result[j] = result[j].Substring(0, result[j].Length - 1); // 맨뒤에 있는 "을 제거
                            List<string> tmp = new List<string>(result);
                            for (int h = j + 1; h < k + 1; h++) 	// j+1부터 k까지
                                tmp.RemoveAt(h); 	// 하나씩 삭제
                            result = tmp.ToArray(); // 그 후 result에 배열로 바꾸어 반환 
                            break;
                        }
                    }
                } // 짝이 될 ” 가 있는 곳까지 삭제하였으므로 j+1부터 다시 반복
            }	// 전부 순회가 완료되면

            return result;   // 결과값으로 result를 넘겨줍니다
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

        Dictionary<string, int> headerIndexDict = new Dictionary<string, int>();

        private void SetHeaderList(string header)
        {
            var values = parseCSVLine(header);
            listBoxHeaders.Items.Clear();

            for(int i = 0; i < values.Length; i++)
            {
                string column = values[i];
                listBoxHeaders.Items.Add(column);
                headerIndexDict[column] = i;
            }    

        }

        private List<List<string>> MakeRowbaseDataStructure(string filename)
        {
            StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open), Encoding.Default);


            string lines = sr.ReadLine();
            SetHeaderList(lines);


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
        private void printRowData_rowbased(List<List<string>> data, TextBox textBox)
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
            textBox.Text = str;
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

        List<List<string>> data;

        private void ButtonOpenCSV_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            DialogResult dResult = FD.ShowDialog();

            if (dResult == DialogResult.OK)
            {
                string filename = FD.FileName;
                data = MakeRowbaseDataStructure(filename);
                printRowData_rowbased(data, textBoxCSVView);
            }

            //printColumnData_rowbased(data, 5);

            //List<List<string>> data = MakeColumnarDataStructure();
            //printColumnData_columnbased(data, 1);
            //printRowData_columnbased(data);
        }

        private void DoSearch(string keyword, string column)
        {
            // 1. 대상 컬럼 확인
            // int targetColumnIndex = listBoxHeaders.SelectedIndex; 
            if(headerIndexDict.ContainsKey(column) == false)
            {
                MessageBox.Show("검색 대상 컬럼을 입력하세요.", "확인");
                return;
            }

            int targetColumnIndex = headerIndexDict[column];


            // 2. 대상 컬럼에서 키워드를 포함하는지 확인
            List<List<string>> resultdata = new List<List<string>>();
            foreach (List<string> row in data)
            {
                string value = row[targetColumnIndex];
                if(value.Contains(keyword))
                {
                    resultdata.Add(row);
                }
            }

            printRowData_rowbased(resultdata, textBoxSearchResult);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;

            if (keyword == "")
            {
                MessageBox.Show("검색 키워드를 입력하세요.", "확인");
                return;
            }

            if(listBoxHeaders.SelectedItem == null)
            {
                MessageBox.Show("검색 대상 컬럼을 입력하세요.", "확인");
                return;
            }
            string column = listBoxHeaders.SelectedItem.ToString();
            if(column == "")
            {
                MessageBox.Show("검색 대상 컬럼을 입력하세요.", "확인");
                return;
            }
            MessageBox.Show("키워드 = " + keyword + "  컬럼 = " + column, "확인");

            DoSearch(keyword, column);

        }
    }
}
