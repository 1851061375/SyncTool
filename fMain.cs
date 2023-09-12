using Newtonsoft.Json;
using SyncTool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TD.QLDL.Library;
using TD.QLDL.Library.Models;
using TD.QLDL.Library.Repositories;

namespace SyncToolOld
{
    public partial class fMain : Form
    {
        private static int needUp;
        private static int total;
        private static string pathImage = @"C:\inetpub\wwwroot\wss\VirtualDirectories\8101\_forms\images";
        public fMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Get all CSLT
        /// </summary>
        public async static Task<List<CoSoLuuTru>> GetCoSoLuuTru()
        {
            Func<List<CoSoLuuTru>> myfunc = () => {
                using (var context = new QLDLDbContext())
                {
                    var data = context.CoSoLuuTrus.Take(1);
                    return data.ToList();
                }
            };

            Task<List<CoSoLuuTru>> task = new Task<List<CoSoLuuTru>>(myfunc);
            task.Start();
            await task;

            List<CoSoLuuTru> result = task.Result;
            return result;
        }
        /// <summary>
        /// Get all table name from database
        /// </summary>
        public async static Task<List<string>> GetAllTable()
        {
            Func<List<string>> myfunc = () => {
                using (var context = new QLDLDbContext())
                {
                    var query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME like 'cslt_%'";
                    var tableNames = context.Database.SqlQuery<string>(query).ToList();
                    return tableNames;
                }
            };

            Task<List<string>> task = new Task<List<string>>(myfunc);
            task.Start();
            await task;

            List<string> result = task.Result; 
            return result;
        }
        /// <summary>
        /// Get new url image after upload
        /// </summary>
        /// <param name="path">old url image</param>
        /// <returns>new url image</returns>
        public static string GetNewUrlImage(string path)
        {
            Image img = Utils.GetImageFromUrl(path);
            Image resize = Utils.ResizeImage(img);
            var taskUpload = UploadImage(resize, path);
            taskUpload.Wait();
            var data = taskUpload.Result;
            return data;
        }
        /// <summary>
        /// Upload CSLT and TuLieuAnhs with new image resize
        /// </summary>
        public async static Task<CoSoLuuTru> ProcessCSLT(CoSoLuuTru coSoLuuTru)
        {
            Func<CoSoLuuTru> myfunc = () => {
                using (var context = new QLDLDbContext())
                {
                    //&& x.ResizedImage == null
                    var _item = context.CoSoLuuTrus
                        .Where(x => x.ID == coSoLuuTru.ID)
                        .FirstOrDefault();
                    if (_item != null && !string.IsNullOrWhiteSpace(_item.AnhDaiDien))
                    {
                        string path = Utils.domain + _item.AnhDaiDien.Split(',')[0];
                        _item.ResizedImage = GetNewUrlImage(path);
                        Logger.Write("Updated CSLT item: " + _item.Ten);

                        var tuLieuAnhs = context.TuLieuAnhs
                            .Where(x => x.CoSoLuuTruID == _item.ID).ToList();
                        if (tuLieuAnhs.Count > 0)
                        {
                            foreach(var tuLieu in tuLieuAnhs)
                            {
                                string[] lines = tuLieu.Anh.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                                if (lines.Length > 0)
                                {
                                    var lstNew = new List<string>();
                                    foreach(var line in lines)
                                    {
                                        string rPath = Utils.domain + line.Split(',')[0];
                                        lstNew.Add(GetNewUrlImage(rPath));
                                    }
                                    tuLieu.ResizedImage = string.Join(",", lstNew);
                                }
                                Logger.Write("Updated TuLieuAnhs of: " + _item.Ten);
                            }
                        }

                        context.SaveChanges();
                        return _item;
                    }
                    return coSoLuuTru;
                }
            };
            
            var task = new Task<CoSoLuuTru>(myfunc);
            task.Start();
            await task;

            var result = task.Result;
            return result;
        }
        /// <summary>
        /// Save Image to public forlder
        /// </summary>
        public async static Task<string> UploadImage(Image image, string path)
        {
            Func<string> myfunc = () =>
            {
                string fileName = Path.GetFileName(new Uri(path).AbsolutePath);
                string destinationPath = Path.Combine(pathImage, fileName);
                image.Save(destinationPath);
                return "https://ql-sdl.hanhchinhcong.net/_forms/images/" + fileName;
            };

            var task = new Task<string>(myfunc);
            task.Start();
            await task;

            var result = task.Result;
            return result;
        }
        /// <summary>
        /// Get logs to show listview
        /// </summary>
        public async static Task<List<string>> GetLogsView()
        {
            Func<List<string>> myfunc = () =>
                Logger.Read();

            var task = new Task<List<string>>(myfunc);
            task.Start();
            await task;
            var result = task.Result;
            return result;
        }

        public static async Task<string> LoginMoca()
        {
            var url = "https://api.kiengiangdiscovery.com/v1/loginWithUserAndPassword";
            var info = new
            {
                instanceId = "randomID",
                emailAddress = "lapphung99@gmail.com",
                password = "moca2023"
            };
            string data = JsonConvert.SerializeObject(info);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var postData = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, postData);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
            }
        }

        public static async Task<string> CreateAccommodation()
        {
            string url = "https://api.example.com/graphql";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var query = @"query {
                        mutation CreateItem ($input : CreateItemInput!)
                        {
                            createItem(input : $input) {
                                id
                                name
                                type
                               }
                        }
                    }";
                    var variables = new
                    {
                        id = "123456"
                    };

                    var requestPayload = new{ query, variables };

                    var jsonData = JsonConvert.SerializeObject(requestPayload);

                    var postData = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, postData);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
            }
        }

        private async void fMain_Load(object sender, EventArgs e)
        {
            var taskLog = GetLogsView();
            await taskLog;
            var logs = taskLog.Result;
            foreach (string item in logs)
            {
                lvLogs.Items.Add(item);
            }

            var task = GetAllTable();
            
            btnStart.Enabled = false;
            await task;
            var data = task.Result;
            cbList.DataSource = data;
            btnStart.Enabled = true;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {

            btnStart.Enabled = false;
            btnSync.Enabled = false;
            // Process data
            var taskGetData = GetCoSoLuuTru();
            await taskGetData;
            var coSoLuuTrus = taskGetData.Result.Where(x => x.ID == 4).ToList();
            total = coSoLuuTrus.Count;
            foreach (var item in coSoLuuTrus)
            {
                var taskProcess = ProcessCSLT(item);
                await taskProcess;
                var coSo = taskProcess.Result;
                needUp++;
                lbUploading.Text = needUp + " / " + total;
            }
            
            // Show logs
            var taskLog = GetLogsView();
            await taskLog;
            var logs = taskLog.Result;
            foreach (string log in logs)
            {
                lvLogs.Items.Add(log);
            }
            btnStart.Enabled = true;
            btnSync.Enabled = true;
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            var taskLogin = LoginMoca();
            await taskLogin;
            var data = taskLogin.Result;
            var tmp = JsonConvert.DeserializeObject<LoginMoca>(data);
            var accessToken = tmp.accessToken;
        }
    }
}
