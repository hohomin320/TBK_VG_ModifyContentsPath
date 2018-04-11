using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Text;

namespace ModifyContentsPath
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string modifyPath = "ddd:";
        private string installFolderPath = Environment.CurrentDirectory;
        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ModifyContentsDataJsonFile();
                //ModifyContentsDataFile();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "파일 변경에 실패했습니다.",
                    "오예",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1,
                    System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                throw ex;
            }
            System.Windows.Forms.MessageBox.Show(
                "파일 변경이 완료되었습니다.",
                "오예",
                MessageBoxButtons.OK,
                MessageBoxIcon.None,
                MessageBoxDefaultButton.Button1,
                System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            //ProcessDirectory("dd", installFolderPath);
        }


        private void ModifyContentsDataFile()
        {



            //if (!Directory.Exists(installFolderPath))
            DirectoryInfo di = new DirectoryInfo(installFolderPath);
            

            //폴더에 있는 파일을 찾는다
            foreach (FileInfo file in di.GetFiles())
            {
                //찾은 파일의 정보
                if (file.Extension == ".json")
                {
                    try
                    {
                        //int counter = 0;
                        //string line;

                        //// Read the file and display it line by line.  
                        //System.IO.StreamReader file =
                        //    new System.IO.StreamReader(@"c:\test.txt");
                        //while ((line = file.ReadLine()) != null)
                        //{
                        //    System.Console.WriteLine(line);
                        //    counter++;
                        //}

                        //file.Close();
                        //System.Console.WriteLine("There were {0} lines.", counter);
                        //// Suspend the screen.  
                        //System.Console.ReadLine();

                        ///

                        //string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");

                        //// Display the file contents to the console. Variable text is a string.
                        //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

                        //// Example #2
                        //// Read each line of the file into a string array. Each element
                        //// of the array is one line of the file.
                        //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");

                        //// Display the file contents by using a foreach loop.
                        //System.Console.WriteLine("Contents of WriteLines2.txt = ");
                        //foreach (string line in lines)
                        //{
                        //    // Use a tab to indent each line of the file.
                        //    Console.WriteLine("\t" + line);
                        //}




                        ///

                        //string key = "install_path";
                        string key = "zip_path";
                        // Open the stream and read it back.
                        using (StreamReader sr = file.OpenText())
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                if (s.Contains(key))
                                {
                                    Console.WriteLine("원본 : "+ s);
                                    int cutIndex = s.IndexOf(key) + key.Length + 4;


                                    string 키값 = s.Substring(0,s.IndexOf("\": ") + 3);
                                    Console.WriteLine(키값 + " 키값");
                                    string 실제필요한주소 = s.Substring(s.IndexOf("\\Contents"));

                                    string 입력받은주소 = "f:";
                                    //입력받은주소 += "\\";
                                    string 변경된주소 = $"{입력받은주소}\\{실제필요한주소}";

                                    Console.WriteLine(키값 + 변경된주소 + " 키값 + 변경된주소");
                                    Console.WriteLine("여기까지" + s.LastIndexOf("\"install_path:\""));
                                }
                            }
                        }

                        //File.WriteAllText(file.FullName, modyfyJsonFileData.ToString());

                        /////

                        //using (StreamReader reader = new StreamReader(file.ToString()))
                        //{

                        //    string readStrLine = reader.ReadLine();
                        //    Console.WriteLine(readStrLine + " ___readStrLine");
                        //    Console.WriteLine(reader.ReadToEnd() + "       ReadToEnd ");
                        //}
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                    //JObject JsonFileData = GetJsonFileData(modifyPath, file.FullName);


                    //JObject modyfyJsonFileData = ModifiedJsonFileData(JsonFileData, "install_path");
                    //modyfyJsonFileData = ModifiedJsonFileData(modyfyJsonFileData, "zip_path");

                    //File.WriteAllText(file.FullName, modyfyJsonFileData.ToString());

                }
            }

        }









        private void ModifyContentsDataJsonFile()
        {
            //if (!Directory.Exists(installFolderPath))
            DirectoryInfo di = new DirectoryInfo(installFolderPath);

            //폴더에 있는 파일을 찾는다
            foreach (var file in di.GetFiles())
            {
                //찾은 파일의 정보
                if (file.Extension == ".json")
                {
                    JObject JsonFileData = GetJsonFileData(modifyPath, file.FullName);


                    JObject modyfyJsonFileData = ModifiedJsonFileData(JsonFileData, "install_path");
                    modyfyJsonFileData = ModifiedJsonFileData(modyfyJsonFileData, "zip_path");

                    File.WriteAllText(file.FullName, modyfyJsonFileData.ToString());

                }
            }
        }

        /// <summary>
        /// Jsonfile의 데이터를 뽑아온다.
        /// </summary>
        /// <param name="modifyPath"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private JObject GetJsonFileData(string modifyPath, string filePath)
        {
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
            }
            string json = File.ReadAllText(filePath);

            //return JsonConvert.DeserializeObject(json);
            return JObject.Parse(json);
        }

        /// <summary>
        /// 입력받은 key에 해당하는 밸류 경로를 수정한다
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="modifyJsonFileKey"></param>
        /// <returns></returns>
        public JObject ModifiedJsonFileData(JObject jsonObj, string modifyJsonFileKey)
        {
            string jsonstring = (string)jsonObj[modifyJsonFileKey] ?? null;
            if (string.IsNullOrEmpty(jsonstring))
                return jsonObj;

            string[] StringArray = jsonstring.Split(new string[] { "\\" }, StringSplitOptions.None);

                bool addPath = false;
                string changedPath = null;

                for (int i = 0; i < StringArray.Length; i++)
                {
                    if (StringArray[i] == "Contents")
                    {
                        addPath = true;
                    }

                    if (addPath && !String.IsNullOrEmpty(StringArray[i]))
                    {
                        changedPath += $"\\{StringArray[i]}";
                    }
                }
                changedPath = $"{modifyPath}\\VirtualGate{changedPath}";

                Console.WriteLine(changedPath + " ___Content path modification");
                jsonObj[modifyJsonFileKey] = changedPath;

                return jsonObj;
        }



        private void inputPath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            modifyPath = inputPath.Text;
        }
    }










    [System.Serializable]
    public class ContentsInfo
    {
        public bool is_purchase { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string contents_type { get; set; }
        public string version { get; set; }
        public string execute_file { get; set; }
        public string execute_file_args { get; set; }
        public string install_path { get; set; }
        public string zip_path { get; set; }
        public long file_size { get; set; }
        public string cover_image { get; set; }
        public double progress { get; set; }
        public string binary_path { get; set; }
        public string developer_name { get; set; }

        public int noContents { get; set; }
    }
}
