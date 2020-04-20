using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Security;
using System.Security.Permissions;
using System.Security.AccessControl;

// Check here how to implement the ThreadPool
// https://www.dotnetperls.com/threadpool
namespace PowerSearch
{
    public partial class Form1 : Form
    {
        const string YES = "Y";
        const string NO = "N";
        const string CONFIG = "PsConfig.txt";
        const int MAX_THREADS = 5;
        Dictionary<int, bool> StopAllThreads = new Dictionary<int, bool>() { { 0, false }, { 1, false }, { 2, false }, { 3, false }, { 4, false } };
        //Dictionary<int, bool> NextComputer = new Dictionary<int, bool>() { { 0, false }, { 1, false }, { 2, false }, { 3, false }, { 4, false } };
        Dictionary<string, string> ServerData = new Dictionary<string, string>();
        //Dictionary<string, string> CleanServerData = new Dictionary<string, string>();
        bool FinishListingHosts = false;
        bool RightDirection = true;
        System.Diagnostics.Stopwatch StopWatch;
        Dictionary<int, string> renamedFileDictionary = new Dictionary<int, string>();
        int ProgressBarIncrement;
        int MaxHostCount;
        int FilesNumberSearched;
        int FilesNumberWithMatchedLines;

        struct MyData : IEquatable<MyData>, IComparable<MyData>
        {
            public static int SortBy = 0;
            public static bool SortAssend = true;
            public string computer;
            public string line;

            public MyData(string computer, string line)
            {
                this.computer = computer;
                this.line = line;
            }

            public bool Equals(MyData other)
            {
                if (SortBy == 0)
                {
                    if (SortAssend)
                        return this.computer.Equals(other.computer);
                    else
                        return other.computer.Equals(this.computer);
                }
                else
                {
                    if (SortAssend)
                        return this.line.Equals(other.line);
                    else
                        return other.line.Equals(this.line);
                }
            }

            public int CompareTo(MyData other)
            {
                if (SortBy == 0)
                {
                    if (SortAssend)
                        return this.computer.CompareTo(other.computer);
                    else
                        return other.computer.CompareTo(this.computer);
                }
                else
                {
                    if (SortAssend)
                        return this.line.CompareTo(other.line);
                    else
                        return other.line.CompareTo(this.line);
                }
            }
        }

        //List<List<MyData>> laneLogs = new List<List<MyData>>{
        //    new List<MyData>(new MyData[] {new MyData("Peter", "Parker"), new MyData("Don", "Johson")}),
        //    new List<MyData>(new MyData[] {new MyData("Kacey", "Jaycob")}),
        //    new List<MyData>(new MyData[] {new MyData("Larrie", "Keanna")}),
        //    new List<MyData>(new MyData[] {new MyData("Kate", "Kelly")}),
        //    new List<MyData>(new MyData[] {new MyData("Ron", "Causy")})
        //};

        List<MyData> laneLogs = null; //new List<MyData>();
        Thread theThread = null;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            LoadPreviousSearchOptions();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (theThread != null && theThread.IsAlive)
            {
                theThread.Interrupt();
                theThread.Abort();
            }
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxSourceDirectory.Text = dlg.SelectedPath;
                checkBoxAJBDropboxes.Checked = false;
                checkBoxAJBDropboxes_CheckedChanged(sender, e);
            }
        }

        public async Task<bool> EnhancedRunSearchAsync()
        {
            if (!checkBoxAJBDropboxes.Checked)
            {
                try
                {
                    ServerData.Add(textBoxSourceDirectory.Text, Environment.MachineName);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            MaxHostCount = ServerData.Count;
            ProgressBarIncrement = 100 / ServerData.Count;

            for (int i = 0; i < ServerData.Count; i++)
            {
                string dir = "";
                string computer = "";
                string regex = "";
                var item = ServerData.ElementAt(i);

                dir = item.Key;
                regex = @"(?:(?:MIS-)?(\w+))";  // Regex pattern to match text: "\\MIS-ALECD1 and extracts "ALECD1"

                if (!regexMatchTest(item.Value, ref computer, regex, true, false))
                    computer = Environment.MachineName; // Search is done in local host

                try
                {
                    EnhancedSearchOneComputerAsync(ServerData, i);
                    //UpdateHostLabel(i % 5, computer);
                    if (i == 0)
                    {

                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    //Console.WriteLine("Invalid Directory Host#: " + i);
                }
            }

            return true;
        }

        private void SearchOneComputer(string folderName, Dictionary<string, string> directory, int index)
        {
            FindFilesInDirectoryAsync(folderName, directory, index);

            if (index == 0)
            {

            }

            //if (progressBar.InvokeRequired)
            //{
            //    this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Increment(1)));
            //}
            //else this.progressBar.Increment(1);

            //NextComputer[index] = false;
        }

        private async Task<bool> EnhancedSearchOneComputer(Dictionary<string, string> directory, int index)
        {
            FindFilesInDirectoryAsync(directory.ElementAt(index).Key, directory, index);
            MaxHostCount--;

            //if (MaxHostCount == 0)
            //    ProgressBarIncrement = 100;

            //if (progressBar.InvokeRequired)
            //    this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Increment(ProgressBarIncrement)));
            //else
            //    this.progressBar.Increment(1);

            //NextComputer[index] = false;

            if (MaxHostCount == 0)
            {
                StopWatch.Stop();
                TimeSpan t = StopWatch.Elapsed;
                string answer = string.Format("{0:D2}h.{1:D2}m.{2:D2}s.{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

                Char[] colon_zero = { ':', '0', 'h', 'm', 's', '.' };
                string text = ">>>>>>>>>>>>>>>>>>>> FINISHED SEARCHING THRU (" + ServerData.Count + ") HOST(S) " + " ELAPSED TIME: (" + answer.TrimStart(colon_zero) + ") <<<<<<<<<<<<<<<<<<<<";
                if (this.hostsLabel.InvokeRequired)
                {
                    this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = text));
                    this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = Color.MediumVioletRed));
                }
                else
                {
                    this.hostsLabel.Text = text;
                    this.hostsLabel.ForeColor = Color.MediumVioletRed;
                }
            }

            return true;
        }

        private async Task<bool> EnhancedSearchOneComputerAsync(Dictionary<string, string> directory, int index)
        {
            await Task.Run(() => EnhancedSearchOneComputer(directory, index));
            return true;
        }

        private async Task<bool> SearchTextInFile(FileInfo file, Dictionary<string, string> folder, int index)
        {
            string computer = "";
            string regex = @"(?:(MIS-?\w+))";  // Regex pattern to match text: "\\MIS-ALECD1           QASupportIA" and extract the server name from it. e.g. "MIS-ALECD1"

            if (checkBoxAJBDropboxes.Checked)
            {
                var item = ServerData.ElementAt(index);

                if (!regexMatchTest(item.Value, ref computer, regex, true, false))
                    computer = Environment.MachineName; // Search is done in local host
            }

            using (FileStream fileStream = file.Open(FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream buffstream = new BufferedStream(fileStream))
                using (StreamReader reader = new StreamReader(buffstream))
                {
                    bool anyMatch = false;
                    string matchedOutput = "";
                    var start = DateTime.Now;
                    string line = null;

                    if (this.hostsLabel.InvokeRequired)
                    {
                        // USING BACKGROUND THREAD
                    }
                    else
                    {
                        // USING UI THREAD
                    }

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (this.hostsLabel.InvokeRequired)
                        {
                            // USING BACKGROUND THREAD
                        }
                        else
                        {
                            // USING UI THREAD
                        }

                        if (regexMatchTest(line.ToString(), ref matchedOutput, textTextBox.Text, checkBoxIgnoreCaseText.Checked, !checkBoxRegexText.Checked))
                        {
                            string pattern = @"\\\b\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}\b";
                            Regex rgx = new Regex(pattern, RegexOptions.Compiled);
                            string path = rgx.Replace(file.ToString(), folder.ElementAt(index).Value);

                            laneLogs.Add(new MyData(computer, path + @"\" + line));
                            anyMatch = true;
                        }

                        //break;
                        //if (StopAllThreads[index] || NextComputer[index])
                        //    return;
                    }

                    if (anyMatch == true)
                    {
                        if (listView1.InvokeRequired)
                        {
                            lock (laneLogs)
                                this.listView1.Invoke(new MethodInvoker(() => this.listView1.VirtualListSize = laneLogs.Count));
                        }
                        else
                        {
                            //lock (listView1)
                            {
                                lock (laneLogs)
                                    listView1.VirtualListSize = laneLogs.Count;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private async Task<bool> FindFilesInDirectoryAsync(string folderName, Dictionary<string, string> folder, int index)
        {
            string matchedOutput = "";
            string[] files = null;
            //var item = folder.ElementAt(index);
            int originalLaneLogsCount = 0;

            if (index == 0)
            {
                if (folderName.Contains("comparison_original_Fil_do_not_modify"))
                {

                }
            }

            try
            {
                files = Directory.GetFileSystemEntries(folderName/*item.Key*/);

                if (files.Length < 1)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            foreach (string file in files)
            {
                if (file.ToLower().Contains("system.day20181031.log") | file.ToLower().Contains("environments"))
                {

                }

                bool isFolder = File.GetAttributes(file).HasFlag(FileAttributes.Directory);
                string subFolderName = "";

                if (isFolder)
                    subFolderName = file;
                else
                    subFolderName = Path.GetDirectoryName(file);

                if (subFolderName.Contains("tdms_wo6137_mx9_p2pe_vsd_ade_sbb_hugh_wf_63"))
                {

                }

                if ((file.IndexOf("$RECYCLE.BIN", StringComparison.CurrentCultureIgnoreCase) >= 0) || (file.IndexOf("System Volume Information", StringComparison.CurrentCultureIgnoreCase) >= 0))  // DO NOT SEARCH IN $RECYCLE.BIN NOR "System Volume Information" as causes many exceptions
                {
                    continue;
                }

                if (checkBoxRegexFolderSyntax.Checked && !string.IsNullOrWhiteSpace(textBoxSubfolderPattern.Text) && !regexMatchTest(subFolderName, textBoxSubfolderPattern.Text, CheckBoxIgnoreCaseFolder.Checked, !checkBoxRegexFolderSyntax.Checked)) // SKIP SEACH WITHIN FOLDER BECAUSE DOES NOT MATCH FOLDER REGEX SYNTAX 
                {
                    continue;
                }

                if (!isFolder && !string.IsNullOrWhiteSpace(file) && !regexMatchTest(file, filesTextBox.Text, CheckBoxIgnoreCaseFiles.Checked, !checkBoxRegexFile.Checked)) // SKIP IF FILE IS NOT MATCHED BY REGEX PATTERN
                {
                    continue;
                }

                if (!isFolder)
                {

                }

                if (!isFolder)
                    FilesNumberSearched++;

                //if (StopAllThreads[index] || NextComputer[index])
                //{
                //    return false;
                //}

                string pattern = @"\\\b\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}\b";
                Regex rgx = new Regex(pattern, RegexOptions.Compiled);
                string renamedFile = rgx.Replace(file, folder.ElementAt(index).Value);
                UpdateFileStatus(renamedFile, index);

                if (renamedFile.Contains("CARLOSB4") && renamedFile.Contains(@"\Current\td\td_hbc_saks_mx9\Fil\comparison_original_Fil_do_not_modify\AJBRTSV4\logs")/*renamedFile.Contains("system.day20180326.log")*/)
                {

                }

                try
                {
                    if (isFolder) //this is a subfolder
                    {
                        if (checkBoxSearchSubolder.Checked)
                            FindFilesInDirectoryAsync(subFolderName, folder, index);
                    }
                    else //this is a file
                    {
                        LogRecord("open file and search for the content: " + file);
                        if (!regexMatchTest(file, ref matchedOutput, filesTextBox.Text, CheckBoxIgnoreCaseFiles.Checked, !checkBoxRegexFile.Checked))
                            continue;

                        originalLaneLogsCount = laneLogs.Count;
                        SearchTextInFile(new FileInfo(file), folder, index);

                        if (originalLaneLogsCount < laneLogs.Count)
                            FilesNumberWithMatchedLines++;
                    }
                }
                catch (Exception ex)
                {
                    LogRecord(ex.Message + ": " + ex.StackTrace);
                }
            }

            return true;
        }

        private void UpdateFileStatus(String file, int index)
        {
            //if (statusLabel.InvokeRequired)
            //{
            //    this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.Text = "File Searching: " + file));
            //    this.statusLabel.Invoke(new MethodInvoker(() => this.Update())); // Background Thread
            //}
            //else
            //{
            //    this.statusLabel.Text = "File Searching: " + file;
            //    this.statusLabel.Update();
            //}

            lock (/*statusMessage*/renamedFileDictionary)
            {
                //statusMessage.Add(file);

                renamedFileDictionary[index] = file;

            }
        }

        private void UpdateFolderStatus(string displayText, bool accessable)
        {
            Color color = Color.DarkSlateGray;

            if (!accessable)
                color = Color.Red;

            if (statusLabel.InvokeRequired)
            {
                this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.Text = "!!! LISTING HOST !!!: " + displayText));
                this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.ForeColor = color));
                this.statusLabel.Invoke(new MethodInvoker(() => this.Update()));
            }
            else
            {
                this.statusLabel.Text = "!!! LISTING HOST !!!: " + displayText;
                this.statusLabel.ForeColor = color;
                this.statusLabel.Update();
            }
        }

        private void ResetFolderStatusColor()
        {
            if (statusLabel.InvokeRequired)
                this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.ForeColor = Color.Black));
            else
                this.statusLabel.ForeColor = Color.Black;
        }

        private void UpdateSearchComplete()
        {
            if (searchButton.InvokeRequired)
            {
                this.searchButton.Invoke(new MethodInvoker(() => this.searchButton.BackColor = System.Drawing.SystemColors.Control));
            }
            else this.searchButton.BackColor = System.Drawing.SystemColors.Control;

            if (searchButton.InvokeRequired)
            {
                this.searchButton.Invoke(new MethodInvoker(() => this.searchButton.ForeColor = Color.Black));
            }
            else this.searchButton.ForeColor = Color.Black;

            if (progressBar.InvokeRequired)
            {
                this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Increment(100)));
            }
            else this.progressBar.Increment(100);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            StopWatch = System.Diagnostics.Stopwatch.StartNew();

            if (checkBoxAJBDropboxes.Checked)
                ResetHostLabel();

            EnhancedSearchAsync();
            FormAnimationAsync();
        }

        private async Task<bool> EnhancedSearchAsync()
        {
            searchButton.BackColor = Color.Lime;
            searchButton.Font = new Font(searchButton.Font, FontStyle.Bold);
            StopAllThreads = StopAllThreads.ToDictionary(s => s.Key, s => false);
            //NextComputer = NextComputer.ToDictionary(n => n.Key, n => false);
            laneLogs = new List<MyData>();
            this.listView1.VirtualListSize = 0;
            ResetIndicatorsStatus();
            FilesNumberSearched = 0;

            if (checkBoxAJBDropboxes.Checked)   //Lets Check Thru the Network in the AJBDropboxes availables and List the Network AJBDropBoxes root directories where will search in.
            {
                ServerData.Clear();
                var startTime = DateTime.Now;

                UpdateStatusLabelWithStartOfNetworkListing(startTime);
                await Task.Run(() => ListAJBDropBoxHostsAsync(ServerData));

                var endTime = DateTime.Now;
                UpdateStatusLabelWithEndOfNetworkListing(startTime, endTime);
                await Task.Run(() => EnhancedRunSearchAsync());
            }
            else
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string> { { this.textBoxSourceDirectory.Text, @"\\127.0.0.1\" } };
                //SearchOneComputer(dictionary.ElementAt(0).Key, dictionary, 0);
                await Task.Run(() => FindFilesInDirectoryAsync(dictionary.ElementAt(0).Key, dictionary, 0));

                if (progressBar.InvokeRequired)
                    this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Increment(100)));
                else
                    this.progressBar.Increment(100);

                CalculateElapsedTime();
            }

            return true;
        }

        private void CalculateElapsedTime()
        {
            StopWatch.Stop();
            TimeSpan t = StopWatch.Elapsed;
            string answer = string.Format("{0:D2}h.{1:D2}m.{2:D2}s.{3:D3}ms",
            t.Hours,
            t.Minutes,
            t.Seconds,
            t.Milliseconds);

            Char[] colon_zero = { ':', '0', 'h', 'm', 's', '.' };
            string text = ">>>>>>>>>>>>>>>>>>>> FOUND " + laneLogs.Count + " MATCHING LINES IN " + FilesNumberWithMatchedLines + " FILES (OF " + FilesNumberSearched + " SEARCHED). " + " ELAPSED TIME: (" + answer.TrimStart(colon_zero) + ") <<<<<<<<<<<<<<<<<<<<";
            if (this.hostsLabel.InvokeRequired)
            {
                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = text));
                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = Color.MediumVioletRed));
            }
            else
            {
                this.hostsLabel.Text = text;
                this.hostsLabel.ForeColor = Color.MediumVioletRed;
            }
        }

        private void ResetIndicatorsStatus()
        {
            if (labelHost1.InvokeRequired)
                this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Text = "Host 1"));
            else
                this.labelHost1.Text = "Host 1";

            if (labelHost2.InvokeRequired)
                this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Text = "Host 2"));
            else
                this.labelHost2.Text = "Host 2";

            if (labelHost3.InvokeRequired)
                this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Text = "Host 3"));
            else
                this.labelHost3.Text = "Host 3";

            if (labelHost4.InvokeRequired)
                this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Text = "Host 4"));
            else
                this.labelHost4.Text = "Host 4";

            if (labelHost5.InvokeRequired)
                this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Text = "Host 5"));
            else
                this.labelHost5.Text = "Host 5";

            if (statusLabel.InvokeRequired)
                this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.Text = "[Search Status]"));
            else
                this.statusLabel.Text = "[Search Status]";

            if (progressBar.InvokeRequired)
                this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Value = 0));
            else
                this.progressBar.Value = 0;
        }

        void listViewLogs_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                //lock (e)
                {
                    if (e.ItemIndex < laneLogs.Count)
                    {
                        //MyData searchResult = laneLogs[e.ItemIndex];
                        MyData searchResult1 = laneLogs[e.ItemIndex];
                        //MyData searchResult2 = laneLogs[1].Count < 1 ? new MyData("", "") : laneLogs[1][e.ItemIndex];
                        //MyData searchResult3 = laneLogs[2].Count < 1 ? new MyData("", "") : laneLogs[2][e.ItemIndex];
                        //MyData searchResult4 = laneLogs[3].Count < 1 ? new MyData("", "") : laneLogs[3][e.ItemIndex];
                        //MyData searchResult5 = laneLogs[4].Count < 1 ? new MyData("", "") : laneLogs[4][e.ItemIndex];
                        e.Item = new ListViewItem(new string[] { searchResult1.computer, searchResult1.line });// searchResult2.computer, searchResult2.line, searchResult3.computer, searchResult3.line, searchResult4.computer, searchResult4.line, searchResult5.computer, searchResult5.line, });

                        //e.Item[0].subItems.Color = Color.Blue;
                        //e.Item.SubItems[0].BackColor = Color.IndianRed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VirtualListView Exception: " + ex.Message + ": " + ex.StackTrace);
            }
        }

        public static bool regexMatchTest(string inputToMatch, string regexPattern, bool ignoreCase, bool escaping)
        {
            string matchedOutput = "";
            return regexMatchTest(inputToMatch, ref matchedOutput, regexPattern, ignoreCase, escaping);
        }

        // Regex matching method. It will always return matched group 1 in matchedOutput variable.
        public static bool regexMatchTest(string inputToMatch, ref string matchedOutput, string regexPattern, bool ignoreCase, bool escaping)
        {
            if (escaping)
                regexPattern = Regex.Escape(regexPattern);

            try
            {
                Regex regField;
                RegexOptions regexOption = RegexOptions.Compiled | (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                System.Text.RegularExpressions.Match mc = new Regex(regexPattern, regexOption).Match(inputToMatch);
                matchedOutput = mc.Groups[1].ToString();
                return mc.Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad Regex Pattern: " + ex.Message + ": " + ex.StackTrace);
                matchedOutput = "";
                return false;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == editToolStripMenuItem)
            {
                MessageBox.Show("editToolStripMenuItem clicked");
            }
        }

        private void checkBoxRegexFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRegexFolderSyntax.Checked)
            {
                checkBoxSearchSubolder.Checked = true;
                textBoxSubfolderPattern.BackColor = SystemColors.ControlLightLight;
                textBoxSubfolderPattern.ForeColor = SystemColors.ActiveCaptionText;
            }
            else
            {
                checkBoxSearchSubolder.Checked = false;
                textBoxSubfolderPattern.BackColor = SystemColors.ScrollBar;
                textBoxSubfolderPattern.ForeColor = SystemColors.ScrollBar;
            }
        }

        private void GeneralControl_CheckedChanged(object sender, EventArgs e)
        {
            StackTrace stackTrace = new StackTrace();

            if (sender.Equals(checkBoxRegexFolderSyntax))
            {
                checkBoxRegexFolder_CheckedChanged(sender, e);
            }
            else if (sender.Equals(checkBoxSearchSubolder))
            {
                checkBoxSearchSubFolder_CheckedChanged(sender, e);
            }
            else if (sender.Equals(checkBoxAJBDropboxes))
            {
                checkBoxAJBDropboxes_CheckedChanged(sender, e);
            }
            else if (sender.Equals(folderButton))
            {
                folderButton_Click(sender, e);
            }

            if (stackTrace.ToString().Contains("LoadPreviousSearchOptions"))
                return;

            SavedSearchOptions();
        }

        private void checkBoxSearchSubFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxSearchSubolder.Checked && checkBoxRegexFolderSyntax.Checked)
                checkBoxSearchSubolder.Checked = true;
        }

        private async Task<DataTable> GetNetworkDataSourcesAsync()
        {
            return System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
        }

        private async Task<bool> ListAJBDropBoxHostsAsync(/*object*/ Dictionary<string, string> parameter)
        {
            Dictionary<string, string> serverData = /*(Dictionary<string, string>)*/parameter;

            if (false)   // Testing purposes, Don't erase, just chase condition for Production Environment :).
            {
                //serverData.Add("\\\\10.96.11.114\\AJBDropBox", "MIS-SEANZ1");
                serverData.Add("\\\\10.96.11.113\\AJBDropBox", "MIS-CARLOSB4");
                //serverData.Add("\\\\10.96.11.229\\AJBDropBox", "MIS-DOMINICY1A");
                //serverData.Add("\\\\10.96.10.175\\AJBDropBox", "MIS-RICHARDR3");
                //serverData.Add("\\\\MIS-CARLQ2\\AJBDropBox");
            }
            else
            {
                DataTable table = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
                string localHost = Environment.MachineName.ToUpper();
                string ipAddress = "";

                foreach (DataRow server in table.Rows)
                {
                    DateTime ms_init = DateTime.Now;
                    DateTime ms_end = DateTime.Now;
                    string renamedFile = "";

                    string serverName = server[table.Columns["ServerName"]].ToString();
                    IPAddress[] ips = Dns.GetHostEntry(serverName).AddressList;

                    if (serverName.Contains("CARLOSB4"))
                    {

                    }

                    foreach (IPAddress ip in ips)
                    {
                        if (ip.ToString().StartsWith("10."))
                            ipAddress = ip.ToString();
                    }

                    if (string.IsNullOrWhiteSpace(ipAddress))
                        continue;

                    string path = @"\\" + ipAddress + @"\AJBDropBox";

                    try
                    {
                        ms_init = DateTime.Now;

                        if (!VerifyDirectoryExists(new Uri(path), 500)) // Maximum delay testing for valid host with AJBDropbox folder: 208 ms
                        {
                            ms_end = DateTime.Now;
                            string pattern = @"(?:(?:MIS-)?(\w+))";
                            regexMatchTest(serverName, ref renamedFile, pattern, true, false);
                            UpdateFolderStatus(renamedFile, false);
                            continue;
                        }

                        ms_end = DateTime.Now;
                    }
                    catch (Exception e)  // server does not have AJBDropBox folder
                    {
                        //Console.WriteLine(e.Message);
                        ms_end = DateTime.Now;
                        string pattern = @"(?:(?:MIS-)?(\w+))";
                        regexMatchTest(serverName, ref renamedFile, pattern, true, false);
                        UpdateFolderStatus(renamedFile, false);
                        continue;
                    }

                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);

                        if (serverName.ToUpper().StartsWith("MIS"))
                        {
                            string pattern = @"\\\b\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}\b";
                            Regex rgx = new Regex(pattern, RegexOptions.Compiled);
                            renamedFile = rgx.Replace(dirInfo.ToString(), serverName);

                            UpdateFolderStatus(renamedFile, true);
                            serverData.Add(dirInfo.ToString(), serverName);
                        }
                    }
                    catch (Exception ex) // Key already added. Do nothing
                    {
                        //Console.WriteLine("Exception: " + ex.Message);
                    }
                }

                FinishListingHosts = true;
                ResetFolderStatusColor();
            }

            return true;
        }

        private void UpdateStatusLabelWithStartOfNetworkListing(DateTime startTime)
        {
            if (statusLabel.InvokeRequired)
            {
                this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.Text = "...Connecting to Network @ " + startTime.ToString("HH:mm:ss tt") + "  (!!! PLEASE WAIT !!!)")); // Background Thread
                this.statusLabel.Invoke(new MethodInvoker(() => this.Update())); // Background Thread
            }
            else
            {
                this.statusLabel.Text = "...Connecting to Network @ " + startTime.ToString("HH:mm:ss tt") + "  (!!! PLEASE WAIT !!!)"; // If this one is executed then we are in the Main Thread.
                this.statusLabel.Update();
            }
        }

        private void UpdateStatusLabelWithEndOfNetworkListing(DateTime startTime, DateTime endTime)
        {
            var elapsedTime = endTime - startTime;

            if (hostsLabel.InvokeRequired)
            {
                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = "Listed " + ServerData.Count + " Hosts --- Started: " + startTime.ToString("HH:mm:ss tt") + " --- Finished:" + endTime.ToString("HH:mm:ss tt") + " --- Elapsed time: [" + elapsedTime.Seconds + " secs]"));
                this.hostsLabel.Invoke(new MethodInvoker(() => this.Update()));
            }
            else
            {
                this.hostsLabel.Text = "LISTED (" + ServerData.Count + ") HOSTS     Started: [" + startTime.ToString("HH:mm:ss tt") + "]" + " ---> Finished:[" + endTime.ToString("HH:mm:ss tt") + "]" + "     ELAPSED TIME: [" + elapsedTime.Seconds + " secs]";
                this.hostsLabel.Update();
            }
        }

        private void UpdateHostLabel(int index, string computer)
        {
            //bool bool1 = false;
            //bool bool2 = false;
            //bool bool3 = false;
            //bool bool4 = false;
            //bool bool5 = false;

            //if (labelHost1.InvokeRequired)
            //{
            //    this.labelHost1.Invoke(new MethodInvoker(() =>
            //        {
            //            bool1 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //            bool2 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //            bool3 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //            bool4 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //            bool5 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //        }
            //    ));
            //}
            //else
            //{
            //    bool1 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //    bool2 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //    bool3 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //    bool4 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //    bool5 = (this.labelHost1.Font.Style == FontStyle.Bold);
            //}


            //if ((bool1 && (index == 0)) || (bool2 && (index == 1)) || (bool3 && (index == 2)) || (bool4 && (index == 3)) || (bool5 && (index == 4)))
            //{
            //    // Do nothing. Respective label is already bold
            //}
            //else    // set to FontStyle.Regular the previous control
            //{
            //    if (bool1)
            //    {
            //        if (labelHost1.InvokeRequired)
            //        {
            //            this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular)));
            //            this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
            //        }
            //        else
            //        {
            //            this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
            //            this.labelHost1.Update();
            //        }
            //    }
            //    else if (bool2)
            //    {
            //        if (labelHost2.InvokeRequired)
            //        {
            //            this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost2.Font, FontStyle.Regular)));
            //            this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
            //        }
            //        else
            //        {
            //            this.labelHost2.Font = new Font(labelHost2.Font, FontStyle.Regular);
            //            this.labelHost2.Update();
            //        }
            //    }
            //    else if (bool3)
            //    {
            //        if (labelHost3.InvokeRequired)
            //        {
            //            this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost3.Font, FontStyle.Regular)));
            //            this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
            //        }
            //        else
            //        {
            //            this.labelHost3.Font = new Font(labelHost3.Font, FontStyle.Regular);
            //            this.labelHost3.Update();
            //        }
            //    }
            //    else if (bool4)
            //    {
            //        if (labelHost4.InvokeRequired)
            //        {
            //            this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost4.Font, FontStyle.Regular)));
            //            this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
            //        }
            //        else
            //        {
            //            this.labelHost4.Font = new Font(labelHost4.Font, FontStyle.Regular);
            //            this.labelHost4.Update();
            //        }
            //    }
            //    else if (bool5)
            //    {
            //        if (labelHost5.InvokeRequired)
            //        {
            //            this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost5.Font, FontStyle.Regular)));
            //            this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
            //        }
            //        else
            //        {
            //            this.labelHost5.Font = new Font(labelHost5.Font, FontStyle.Regular);
            //            this.labelHost5.Update();
            //        }
            //    }

            //    switch (index)  // set to FontStyle.Bold the previous control and update the host name
            //    {
            //        case 0:
            //            if (labelHost1.InvokeRequired)
            //            {
            //                this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Bold)));
            //                this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Text = computer));
            //                this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
            //            }
            //            else
            //            {
            //                this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
            //                this.labelHost1.Text = computer;
            //                this.labelHost1.Update();
            //            }
            //            break;
            //        case 1:
            //            if (labelHost2.InvokeRequired)
            //            {
            //                this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost2.Font, FontStyle.Bold)));
            //                this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Text = computer));
            //                this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
            //            }
            //            else
            //            {
            //                this.labelHost2.Font = new Font(labelHost2.Font, FontStyle.Bold);
            //                this.labelHost2.Text = computer;
            //                this.labelHost2.Update();
            //            }
            //            break;
            //        case 2:
            //            if (labelHost3.InvokeRequired)
            //            {
            //                this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost3.Font, FontStyle.Bold)));
            //                this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Text = computer));
            //                this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
            //            }
            //            else
            //            {
            //                this.labelHost3.Font = new Font(labelHost3.Font, FontStyle.Regular);
            //                this.labelHost3.Text = computer;
            //                this.labelHost3.Update();
            //            }
            //            break;
            //        case 3:
            //            if (labelHost4.InvokeRequired)
            //            {
            //                this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost4.Font, FontStyle.Bold)));
            //                this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Text = computer));
            //                this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
            //            }
            //            else
            //            {
            //                this.labelHost4.Font = new Font(labelHost4.Font, FontStyle.Bold);
            //                this.labelHost4.Text = computer;
            //                this.labelHost4.Update();
            //            }
            //            break;
            //        case 4:
            //            if (labelHost5.InvokeRequired)
            //            {
            //                this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost5.Font, FontStyle.Bold)));
            //                this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Text = computer));
            //                this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
            //            }
            //            else
            //            {
            //                this.labelHost5.Font = new Font(labelHost5.Font, FontStyle.Bold);
            //                this.labelHost5.Text = computer;
            //                this.labelHost5.Update();
            //            }
            //            break;
            //    }
            //}

            switch (index)
            {
                case 0:
                    if (labelHost1.InvokeRequired)
                    {
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Bold)));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular)));

                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Text = computer));
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));

                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
                    }
                    else
                    {
                        this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Bold);
                        this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular);

                        this.labelHost1.Text = computer;
                        this.labelHost1.Update();

                        this.labelHost2.Update();
                        this.labelHost3.Update();
                        this.labelHost4.Update();
                        this.labelHost5.Update();
                    }
                    break;
                case 1:
                    if (labelHost2.InvokeRequired)
                    {
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Bold)));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular)));

                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Text = computer));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));

                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
                    }
                    else
                    {
                        this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Bold);
                        this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular);

                        this.labelHost2.Text = computer;
                        this.labelHost2.Update();

                        this.labelHost1.Update();
                        this.labelHost3.Update();
                        this.labelHost4.Update();
                        this.labelHost5.Update();
                    }
                    break;
                case 2:
                    if (labelHost3.InvokeRequired)
                    {
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Bold)));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular)));

                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Text = computer));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));

                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
                    }
                    else
                    {
                        this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Bold);
                        this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular);

                        this.labelHost3.Text = computer;
                        this.labelHost3.Update();

                        this.labelHost1.Update();
                        this.labelHost2.Update();
                        this.labelHost4.Update();
                        this.labelHost5.Update();
                    }
                    break;
                case 3:
                    if (labelHost4.InvokeRequired)
                    {
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Bold)));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular)));

                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Text = computer));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));

                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));
                    }
                    else
                    {
                        this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Bold);
                        this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Regular);

                        this.labelHost4.Text = computer;
                        this.labelHost4.Update();

                        this.labelHost1.Update();
                        this.labelHost2.Update();
                        this.labelHost3.Update();
                        this.labelHost5.Update();
                    }
                    break;
                case 4:
                    if (labelHost5.InvokeRequired)
                    {
                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular)));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Bold)));

                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Text = computer));
                        this.labelHost5.Invoke(new MethodInvoker(() => this.labelHost5.Update()));

                        this.labelHost1.Invoke(new MethodInvoker(() => this.labelHost1.Update()));
                        this.labelHost2.Invoke(new MethodInvoker(() => this.labelHost2.Update()));
                        this.labelHost3.Invoke(new MethodInvoker(() => this.labelHost3.Update()));
                        this.labelHost4.Invoke(new MethodInvoker(() => this.labelHost4.Update()));
                    }
                    else
                    {
                        this.labelHost1.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost2.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost3.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost4.Font = new Font(labelHost1.Font, FontStyle.Regular);
                        this.labelHost5.Font = new Font(labelHost1.Font, FontStyle.Bold);

                        this.labelHost5.Text = computer;
                        this.labelHost5.Update();

                        this.labelHost1.Update();
                        this.labelHost2.Update();
                        this.labelHost3.Update();
                        this.labelHost4.Update();
                    }
                    break;
            }
        }

        private async Task<bool> UdpateProgressBarAndSearchButtonAsync()
        {
            if (progressBar.InvokeRequired)
            {
                this.progressBar.Invoke(new MethodInvoker(() => this.progressBar.Value = 0));
                this.searchButton.Invoke(new MethodInvoker(() => this.searchButton.BackColor = Color.DeepSkyBlue));
            }
            else
            {
                this.progressBar.Value = 0;
                this.searchButton.BackColor = Color.DeepSkyBlue;
            }

            return true;
        }

        private void ResetHostLabel()
        {
            if (hostsLabel.InvokeRequired)
            {
                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = "[Hosts Count]"));
                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Update()));
            }
            else
            {
                this.hostsLabel.Text = "[Hosts Count]";
                this.hostsLabel.Update();
            }

            FinishListingHosts = false;
        }

        private async Task<bool> HostsCountAnimationAsync()
        {
            if (checkBoxAJBDropboxes.Checked)
                await Task.Run(() => HostsCountAnimation());

            return true;
        }

        private async Task<bool> FormAnimationAsync()
        {
            if (checkBoxAJBDropboxes.Checked)
                await Task.Run(() => HostsCountAnimation());

            await Task.Run(() => FileStatusAnimation());

            return true;
        }

        private void HostsCountAnimation()
        {
            string baseString = "Counting Hosts (0) ";
            string text = baseString;
            const int TMO = 5;
            int timeout = 10;
            string pattern = @"(\d+)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled);

            while (FinishListingHosts == false)
            {
                text = rgx.Replace(text, ServerData.Count.ToString());

                if (RightDirection)
                {
                    if (text.Length - baseString.Length < 27)
                        text = text + " *";
                    else
                    {
                        text = text + " !!! PLEASE WAIT !!! * * * * * * * * * * * * * * * *";
                        timeout = timeout + 500;
                        RightDirection = false;
                    }
                }
                else
                {
                    if (text.Length - baseString.Length > 2)
                        text = text.Substring(0, text.Length - 2);
                    else
                        RightDirection = true;
                }

                if (this.hostsLabel.InvokeRequired)
                {
                    if (timeout > TMO)
                    {
                        this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = System.Drawing.Color.Black));
                        //this.hostsLabel.Invoke(new MethodInvoker(() => this.searchButton.BackColor = Color.Black));
                    }
                    else if (RightDirection)
                    {
                        this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = System.Drawing.Color.Blue));
                        //this.hostsLabel.Invoke(new MethodInvoker(() => this.searchButton.BackColor = Color.Blue));
                    }
                    else
                    {
                        this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = System.Drawing.Color.Red));
                        //this.hostsLabel.Invoke(new MethodInvoker(() => this.searchButton.BackColor = Color.Red));
                    }

                    this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = text));
                    this.hostsLabel.Invoke(new MethodInvoker(() => this.Update()));
                }
                else
                {
                    if (timeout > TMO)
                    {
                        this.hostsLabel.ForeColor = System.Drawing.Color.Black;
                        //this.searchButton.BackColor = Color.Black;
                    }
                    else if (RightDirection)
                    {
                        this.hostsLabel.ForeColor = System.Drawing.Color.Blue;
                        //this.searchButton.BackColor = Color.Blue;
                    }
                    else
                    {
                        this.hostsLabel.ForeColor = System.Drawing.Color.Red;
                        //this.searchButton.BackColor = Color.Red;
                    }

                    this.hostsLabel.Text = text;
                    this.Update();
                }

                if (timeout < 500)
                {
                    Thread.Sleep(timeout);
                    timeout = TMO;
                }
                else
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    while (s.Elapsed < TimeSpan.FromMilliseconds(timeout))
                    {
                        text = rgx.Replace(text, ServerData.Count.ToString());

                        if (this.hostsLabel.InvokeRequired)
                        {
                            this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.Text = text));
                            this.hostsLabel.Invoke(new MethodInvoker(() => this.Update()));
                        }
                        else
                        {
                            this.hostsLabel.Text = text;
                            this.Update();
                        }
                    }

                    timeout = TMO;
                    s.Stop();
                }
            }

            if (this.hostsLabel.InvokeRequired)
            {

                this.hostsLabel.Invoke(new MethodInvoker(() => this.hostsLabel.ForeColor = System.Drawing.Color.Black));
            }
            else
            {
                this.hostsLabel.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void FileStatusAnimation()
        {
            string renamedFile = "";

            while (true)
            {
                //renamedFile = "********** FILE 1 ***********";
                //UpdateFileStatus(renamedFile);

                //renamedFile = ">>>>>>>>>> FILE 2 >>>>>>>>>>>";
                //UpdateFileStatus(renamedFile);

                UpdateStatusLabel();
                //UpdateHostLabel(i % 5, computer);

            }
        }

        private void SavedSearchOptions()
        {
            Hashtable inputData = new Hashtable();
            inputData["folder"] = textBoxSourceDirectory.Text;
            inputData["text"] = textTextBox.Text;
            inputData["files"] = filesTextBox.Text;
            inputData["subfolder_pattern"] = textBoxSubfolderPattern.Text;
            inputData["subfolder_search_checkbox"] = checkBoxSearchSubolder.Checked ? YES : NO; ;
            inputData["subfolder_regex_checkbox"] = checkBoxRegexFolderSyntax.Checked ? YES : NO; ;
            inputData["ajbdropboxes_checkbox"] = checkBoxAJBDropboxes.Checked ? YES : NO; ;
            inputData["folder_ignorecase_checkbox"] = CheckBoxIgnoreCaseFolder.Checked ? YES : NO; ;
            inputData["files_regex_checkbox"] = checkBoxRegexFile.Checked ? YES : NO; ;
            inputData["files_ignorecase_checkbox"] = CheckBoxIgnoreCaseFiles.Checked ? YES : NO; ;
            inputData["text_regex_checkbox"] = checkBoxRegexText.Checked ? YES : NO; ;
            inputData["text_ignorecase_checkbox"] = checkBoxIgnoreCaseText.Checked ? YES : NO;

            using (FileStream fs = new FileStream(CONFIG, FileMode.OpenOrCreate, FileAccess.Write))   // Save Search Options
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, inputData);
            }
        }

        private void LoadPreviousSearchOptions()
        {
            Hashtable inputData = new Hashtable();

            if (baserout.FileExists(CONFIG) == true)
            {
                using (Stream stream = File.OpenRead(CONFIG)) // Reload Last Search Options
                {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    inputData = (Hashtable)deserializer.Deserialize(stream);

                    if (inputData["folder"] != null)
                        textBoxSourceDirectory.Text = inputData["folder"].ToString();

                    if (inputData["text"] != null)
                        textTextBox.Text = inputData["text"].ToString();

                    if (inputData["files"] != null)
                        filesTextBox.Text = inputData["files"].ToString();

                    if (inputData["subfolder_pattern"] != null)
                        textBoxSubfolderPattern.Text = inputData["subfolder_pattern"].ToString();

                    if (inputData["subfolder_search_checkbox"] != null)
                        checkBoxSearchSubolder.Checked = inputData["subfolder_search_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["subfolder_regex_checkbox"] != null)
                        checkBoxRegexFolderSyntax.Checked = inputData["subfolder_regex_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["ajbdropboxes_checkbox"] != null)
                        checkBoxAJBDropboxes.Checked = inputData["ajbdropboxes_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["folder_ignorecase_checkbox"] != null)
                        CheckBoxIgnoreCaseFolder.Checked = inputData["folder_ignorecase_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["files_regex_checkbox"] != null)
                        checkBoxRegexFile.Checked = inputData["files_regex_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["files_ignorecase_checkbox"] != null)
                        CheckBoxIgnoreCaseFiles.Checked = inputData["files_ignorecase_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["text_regex_checkbox"] != null)
                        checkBoxRegexText.Checked = inputData["text_regex_checkbox"].ToString().StartsWith(YES) ? true : false;

                    if (inputData["text_ignorecase_checkbox"] != null)
                        checkBoxIgnoreCaseText.Checked = inputData["text_ignorecase_checkbox"].ToString().StartsWith(YES) ? true : false;
                }
            }
        }

        public void LogRecord(string Message)
        {
            return;
            try
            {
                string LogPath = "";
                LogPath = @"d:\ajbdropbox\";
                FileInfo file = new FileInfo(LogPath + "log.txt");
                StreamWriter srout = new StreamWriter(LogPath + "\\log.txt", true);

                // check to see if the log exists and if not create one.
                if (!File.Exists(LogPath + "\\log.txt"))
                {
                    File.Create(LogPath + "\\log.txt");
                }

                //logs grow in size. Eventually they need to be deleted or truncated
                if (file.Length > 10000000)
                    file.Delete();

                srout.WriteLine("");
                Message += " at " + DateTime.Now;
                srout.WriteLine(Message);

                // if you skip the flush, it won't work at all. You will crete a text file
                // that is black

                srout.Flush();
                srout.Close();
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }
        }

        private void textBoxSourceDirectory_TextChanged(object sender, EventArgs e)
        {

        }

        private void filesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSubfolderPattern_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxAJBDropboxes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAJBDropboxes.Checked)
            {
                textBoxSourceDirectory.BackColor = SystemColors.ScrollBar;
                textBoxSourceDirectory.ForeColor = SystemColors.ScrollBar;
            }
            else
            {
                textBoxSourceDirectory.BackColor = SystemColors.ControlLightLight;
                textBoxSourceDirectory.ForeColor = SystemColors.ActiveCaptionText;
            }
        }

        private static bool VerifyDirectoryExists(Uri uri, int timeout)
        {
            var task = new Task<bool>(() =>
            {
                var fi = new DirectoryInfo(uri.LocalPath);
                if (!fi.Exists)
                {

                }

                return fi.Exists;
            });
            task.Start();
            return task.Wait(timeout) && task.Result;
        }
        //***************
        private void CheckBoxIgnoreCaseFolder_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegexFile_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBoxIgnoreCaseFiles_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegexText_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxIgnoreCaseText_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //NextComputer = NextComputer.ToDictionary(n => n.Key, n => true);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopAllThreads = StopAllThreads.ToDictionary(s => s.Key, s => true);
        }

        private void textBoxSourceDirectory_MouseHover(object sender, EventArgs e)
        {
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            MyData.SortBy = e.Column;
            laneLogs.Sort();
            listView1.Invalidate();
            MyData.SortAssend = !MyData.SortAssend;
        }

        //List<string> statusMessage = new List<string>();
        private void UpdateStatusLabel()
        {
            string computer = "";
            string file = null;
            int index = 0;



            if (renamedFileDictionary.Count < 1)
                return;

            //while (true)
            {
                lock (/*statusMessage*/renamedFileDictionary)
                {
                    //if (statusMessage.Count > 0)
                    //if (renamedFileDictionary.Count > 0)
                    {
                        file = "File Searching: " + renamedFileDictionary.LastOrDefault().Value;
                        //file = "File Searching: " + statusMessage[statusMessage.Count - 1];

                        index = renamedFileDictionary.LastOrDefault().Key;

                    }
                    renamedFileDictionary.Clear();
                    //statusMessage.Clear();
                }

                if (checkBoxAJBDropboxes.Checked)
                {
                    string regex = @"MIS-?(\w+)";//@"(?:(?:MIS-)?(\w+))";  // Regex pattern to match text: "\\MIS-ALECD1 and extracts "ALECD1"
                    if (!regexMatchTest(file, ref computer, regex, true, false))
                        computer = Environment.MachineName; // Search is done in local host

                    UpdateHostLabel(index % 5, computer);
                }

                if (file != null)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        this.statusLabel.Invoke(new MethodInvoker(() => this.statusLabel.Text = file));
                    }
                    else
                    {
                        this.statusLabel.Text = file;
                    }
                    file = null;
                }
            }
        }
    }
}