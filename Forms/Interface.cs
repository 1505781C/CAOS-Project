using classobj;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CPUConsole;
using VirtualConsole;
using MainConsole;
using System.Data;
using System.IO;

namespace ComputerArchitectureAndOperatingSystems.Forms
{
    public partial class Interface : Form {

        public static int partitionCount = 4;
        public int[] partitionSize = new int[partitionCount];
        public int pageSize = 16;
        string[,] processTime = new string[10,10];
        bool manualCheck = false;

        public Interface() {
            InitializeComponent();
        }

        private void Interface_Load(object sender, EventArgs e) {
            load_Home();
            this.Width = 616;
            this.Height = 389;
            this.BackColor = Color.AliceBlue;

            this.panelHome.Visible = true;
            this.panelHome.Location = new Point(0, 0);

            // Fake Default Values
            partitionSize[0] = 180;
            partitionSize[1] = 130;
            partitionSize[2] = 80;

            this.ddlMethod.SelectedIndex = 0;
            this.ddlReal.SelectedIndex = 0;
            this.ddlVirtual.SelectedIndex = 0;
            this.ddlCount.SelectedIndex = 0;
            this.ddlMethod2.SelectedIndex = 0;
            this.ddlReal2.SelectedIndex = 0;
            this.ddlVirtual2.SelectedIndex = 0;

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            gantt_chart.Series.Clear();
            for(int i = 0; i < 10; i ++) {
                Series series = this.gantt_chart.Series.Add("Charter" + i);
                this.gantt_chart.Series["Charter" + i].ChartType = SeriesChartType.StackedBar;
                this.gantt_chart.Series["Charter" + i].BorderColor = Color.Black;
                series.Points.Add(2);
            }
            gantt_chart.ChartAreas[0].RecalculateAxesScale();

            real_parition.Series.Clear(); int j = 2;
            for (int i = 0; i < 3; i++)
            {
                Series series = this.real_parition.Series.Add("Paritioner" + i);
                this.real_parition.Series["Paritioner" + i].ChartType = SeriesChartType.StackedColumn;
                this.real_parition.Series["Paritioner" + i].BorderColor = Color.Black;
                this.real_parition.Series["Paritioner" + i].Label = partitionSize[j].ToString() + "kb";
                series.Points.AddXY(1, 1);

                Series series2 = this.real_parition2.Series.Add("Paritioner2" + i);
                this.real_parition2.Series["Paritioner2" + i].ChartType = SeriesChartType.StackedColumn;
                this.real_parition2.Series["Paritioner2" + i].BorderColor = Color.Black;
                series2.Points.AddXY(1, 1);

                j--;
            }
            real_parition.ChartAreas[0].RecalculateAxesScale();
            real_parition2.ChartAreas[0].RecalculateAxesScale();
        }

        private void btnAutomatic_Click(object sender, EventArgs e) {
            this.ddlMethod.SelectedIndex = 0;
            this.ddlReal.SelectedIndex = 0;
            this.ddlVirtual.SelectedIndex = 0;
            this.ddlCount.SelectedIndex = 0;
            this.ddlMethod2.SelectedIndex = 0;
            this.ddlReal2.SelectedIndex = 0;
            this.ddlVirtual2.SelectedIndex = 0;
            load_Process("Auto");
        }

        private void btnManual_Click(object sender, EventArgs e) {
            this.ddlMethod.SelectedIndex = 0;
            this.ddlReal.SelectedIndex = 0;
            this.ddlVirtual.SelectedIndex = 0;
            this.ddlCount.SelectedIndex = 0;
            this.ddlMethod2.SelectedIndex = 0;
            this.ddlReal2.SelectedIndex = 0;
            this.ddlVirtual2.SelectedIndex = 0;
            load_Process("Manual");
        }

        private void load_Home() {
            this.Text = "Computer Architecture And Operating Systems - Home";
            this.panelHome.Visible = true;
            this.panelHome.Location = new Point(0, 0);
            this.panelHome.BringToFront();
        }
        private void load_Process(String mode) {
            this.panelHome.Visible = false;

            if (mode == "Auto") {
                load_Auto();
            }

            if (mode == "Manual") {
                load_Manual();
            }
        }
        private void load_Auto() {
            this.Text = "Computer Architecture And Operating Systems - Automatic Process";
            this.panelProcessA.Visible = true;
            this.panelProcessA.Location = new Point(0, 0);
            this.panelProcessA.BringToFront();
        }
        private void load_Manual() {
            this.Text = "Computer Architecture And Operating Systems - Manual Process";
            this.panelProcessB.Visible = true;
            this.panelProcessB.Location = new Point(0, 0);
            this.panelProcessB.BringToFront();
        }
        private void load_Manual_1() {
            this.Text = "Computer Architecture And Operating Systems - Manual Process";
            this.panelProcessB_1.Visible = true;
            this.panelProcessB_1.Location = new Point(0, 0);
            this.panelProcessB_1.BringToFront();

            tab_Process.TabPages.Clear();
            txtQuantum.Text = "";

            if (ddlMethod2.Text == "Shortest Job First (SJF)" || ddlMethod2.Text == "First Come First Serve (FCFS)") {
                add_ControlTab(); add_ControlTab();
            }
            else {
                if (ddlMethod2.Text == "Round Robin Scheduling") {
                    add_ControlTab3(); add_ControlTab3();
                }
                else {
                    add_ControlTab2(); add_ControlTab2();
                }
            }
        }
        private void load_Results() {
            this.Text = "Computer Architecture And Operating Systems - Results";
            this.panelResults.Visible = true;
            this.panelResults.Location = new Point(0, 0);
            this.panelResults.BringToFront();

            this.tabResultPage.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (ddlMethod2.Text == "Shortest Job First (SJF)" || ddlMethod2.Text == "First Come First Serve (FCFS)") {
                add_ControlTab();
                check_ControlTabl();
            }
            else {
                if (ddlMethod2.Text == "Round Robin Scheduling") {
                    add_ControlTab3();
                    check_ControlTabl();
                }
                else {
                    add_ControlTab2();
                    check_ControlTabl();
                }
            }
            
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tab_Process.TabCount > 2)
                tab_Process.TabPages.Remove(tab_Process.SelectedTab);
            else
                MessageBox.Show("Minimum Process: 2", "Alert Box");
            check_ControlTabl();
        }
        private void btnAdd2_Click(object sender, EventArgs e) {
            
        }
        private void btnRemove2_Click(object sender, EventArgs e) {

        }

        private void btnSettings_Click(object sender, EventArgs e) {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }

        private void btnReturn1_Click(object sender, EventArgs e) {
            this.panelProcessA.Visible = false;
            this.panelProcessA.Location = new Point(606, 0);
            load_Home();
        }
        private void btnReturn2_Click(object sender, EventArgs e) {
            this.panelProcessB.Visible = false;
            this.panelProcessB.Location = new Point(0, 356);
            load_Home();
        }
        private void btnReturn2_1_Click(object sender, EventArgs e)
        {
            this.panelProcessB_1.Visible = false;
            this.panelProcessB_1.Location = new Point(606, 356);
            load_Home();
        }
        private void btnReturn3_Click(object sender, EventArgs e) {
            this.panelResults.Visible = false;
            this.panelResults.Location = new Point(606, 712);
            load_Home();
        }

        private void check_ControlTabl() {
            for (int i = 0; i < tab_Process.TabCount; i++)
            {
                this.tab_Process.SelectedIndex = i;
                tab_Process.SelectedTab.Text = "Process" + (i + 1);
            }
        }

        private void add_ControlTab() { // FCFS & SJF
            if (tab_Process.TabCount < 8) {
                string title = "Process " + (tab_Process.TabCount + 1).ToString();
                TabPage myTabPage = new TabPage(title);
                tab_Process.TabPages.Add(myTabPage);
                myTabPage.BackColor = Color.White;

                Label lblArrivalTime = new Label();
                lblArrivalTime.Text = "Arrival Time : ";
                lblArrivalTime.Location = new Point(25, 45);
                lblArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblArrivalTime);

                Label lblBurstTime = new Label();
                lblBurstTime.Text = "Burst Time : ";
                lblBurstTime.Location = new Point(25, 85);
                lblBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblBurstTime);

                Label lblProcessSize = new Label();
                lblProcessSize.Text = "Size : ";
                lblProcessSize.Location = new Point(25, 125);
                lblProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblProcessSize);

                TextBox txtArrivalTime = new TextBox();
                txtArrivalTime.Location = new Point(170, 40);
                txtArrivalTime.AutoSize = false;
                txtArrivalTime.Size = new System.Drawing.Size(250, 28);
                txtArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtArrivalTime);

                TextBox txtBurstTime = new TextBox();
                txtBurstTime.Location = new Point(170, 82);
                txtBurstTime.AutoSize = false;
                txtBurstTime.Size = new System.Drawing.Size(250, 28);
                txtBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtBurstTime);

                TextBox txtProcessSize = new TextBox();
                txtProcessSize.Location = new Point(170, 124);
                txtProcessSize.AutoSize = false;
                txtProcessSize.Size = new System.Drawing.Size(250, 28);
                txtProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtProcessSize);

                lblQuantum.Hide();
                txtQuantum.Hide();
            }
            else
                MessageBox.Show("Maximum Process: 8", "Alert Box");
        }
        private void add_ControlTab2() { // Priority
            if (tab_Process.TabCount < 8) {
                string title = "Process " + (tab_Process.TabCount + 1).ToString();
                TabPage myTabPage = new TabPage(title);
                tab_Process.TabPages.Add(myTabPage);
                myTabPage.BackColor = Color.White;

                Label lblArrivalTime = new Label();
                lblArrivalTime.Text = "Arrival Time : ";
                lblArrivalTime.Location = new Point(25, 25);
                lblArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblArrivalTime);

                Label lblBurstTime = new Label();
                lblBurstTime.Text = "Burst Time : ";
                lblBurstTime.Location = new Point(25, 65);
                lblBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblBurstTime);

                Label lblProcessSize = new Label();
                lblProcessSize.Text = "Size : ";
                lblProcessSize.Location = new Point(25, 105);
                lblProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblProcessSize);

                Label lblPriority = new Label();
                lblPriority.Text = "Priority : ";
                lblPriority.Location = new Point(25, 145);
                lblPriority.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblPriority);

                TextBox txtArrivalTime = new TextBox();
                txtArrivalTime.Location = new Point(170, 20);
                txtArrivalTime.AutoSize = false;
                txtArrivalTime.Size = new System.Drawing.Size(250, 28);
                txtArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtArrivalTime);

                TextBox txtBurstTime = new TextBox();
                txtBurstTime.Location = new Point(170, 62);
                txtBurstTime.AutoSize = false;
                txtBurstTime.Size = new System.Drawing.Size(250, 28);
                txtBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtBurstTime);

                TextBox txtProcessSize = new TextBox();
                txtProcessSize.Location = new Point(170, 104);
                txtProcessSize.AutoSize = false;
                txtProcessSize.Size = new System.Drawing.Size(250, 28);
                txtProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtProcessSize);

                TextBox txtPriority = new TextBox();
                txtPriority.Location = new Point(170, 144);
                txtPriority.AutoSize = false;
                txtPriority.Size = new System.Drawing.Size(250, 28);
                txtPriority.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtPriority);

                lblQuantum.Hide();
                txtQuantum.Hide();
            }
            else
                MessageBox.Show("Maximum Process: 8", "Alert Box");
        }
        private void add_ControlTab3() { // Round Robin
            if (tab_Process.TabCount < 8) {
                string title = "Process " + (tab_Process.TabCount + 1).ToString();
                TabPage myTabPage = new TabPage(title);
                tab_Process.TabPages.Add(myTabPage);
                myTabPage.BackColor = Color.White;

                Label lblArrivalTime = new Label();
                lblArrivalTime.Text = "Arrival Time : ";
                lblArrivalTime.Location = new Point(25, 45);
                lblArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblArrivalTime);

                Label lblBurstTime = new Label();
                lblBurstTime.Text = "Burst Time : ";
                lblBurstTime.Location = new Point(25, 85);
                lblBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblBurstTime);

                Label lblProcessSize = new Label();
                lblProcessSize.Text = "Size : ";
                lblProcessSize.Location = new Point(25, 125);
                lblProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(lblProcessSize);

                TextBox txtArrivalTime = new TextBox();
                txtArrivalTime.Location = new Point(170, 40);
                txtArrivalTime.AutoSize = false;
                txtArrivalTime.Size = new System.Drawing.Size(250, 28);
                txtArrivalTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtArrivalTime);

                TextBox txtBurstTime = new TextBox();
                txtBurstTime.Location = new Point(170, 82);
                txtBurstTime.AutoSize = false;
                txtBurstTime.Size = new System.Drawing.Size(250, 28);
                txtBurstTime.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtBurstTime);

                TextBox txtProcessSize = new TextBox();
                txtProcessSize.Location = new Point(170, 124);
                txtProcessSize.AutoSize = false;
                txtProcessSize.Size = new System.Drawing.Size(250, 28);
                txtProcessSize.Font = new System.Drawing.Font("Arial", 12);
                myTabPage.Controls.Add(txtProcessSize);

                lblQuantum.Show();
                txtQuantum.Show();
            }
            else
                MessageBox.Show("Maximum Process: 8", "Alert Box");
        }

        private void btnGenerateA_Click(object sender, EventArgs e) {
            Random random = new Random();

            manualCheck = false;
            txtQuestion.Text = "";
            txtTunaround.Text = "";
            txtWaiting.Text = "";

            int arrivalTime = 0;
            int burstTime = 0;
            int memorySize = 0;
            int priority = 0;
            int waitingTime = 0;
            int turnaroundTime = 0;

            string[,] processTime = new string[10, 10];
            int processCount = Convert.ToInt32(ddlCount.Text);

            List<Process> cpuProcess = new List<Process>();
            List<Scheduled_Process> cpuResults = new List<Scheduled_Process>();
            CPUScheduling cpu = new CPUScheduling();
            MainMemory real = new MainMemory();
            VirtualMemory fake = new VirtualMemory();

            List<int> partitions = new List<int>();
            partitions.Add(partitionSize[0]);
            partitions.Add(partitionSize[1]);
            partitions.Add(partitionSize[2]);
            int pageCount = 4;

            gantt_chart.Series.Clear();

            // Set Random Numbers
            for (int i = 0; i < processCount; i++) {
                processTime[i, 0] = random.Next(0, 5).ToString(); // Arrival Time
                processTime[i, 1] = random.Next(1, 8).ToString(); // Burst TIme
                processTime[i, 2] = random.Next(50, 200).ToString(); // Memory Size
                if (ddlMethod.Text == "Prority Based Scheduling")
                    processTime[i, 3] = random.Next(1, processCount + 1).ToString(); // Priority

                // CPU Scheduling
                arrivalTime = Convert.ToInt32(processTime[i, 0]);
                burstTime = Convert.ToInt32(processTime[i, 1]);
                memorySize = Convert.ToInt32(processTime[i, 2]);
                if (ddlMethod.Text == "Prority Based Scheduling") {
                    cpuProcess.Add(new Process(i, arrivalTime, burstTime, memorySize, priority));
                    cpuResults = cpu.Nonpremptive_Priority(cpuProcess);
                }
                else {
                    cpuProcess.Add(new Process(i, arrivalTime, burstTime, memorySize));
                    cpuResults = cpu.First_Come_First_Serve(cpuProcess);
                }

                txtQuestion.Text += Environment.NewLine + "Process " + (i + 1) + ":" + Environment.NewLine;
                txtQuestion.Text += " Arrival Time - " + processTime[i, 0] + "s" + Environment.NewLine;
                txtQuestion.Text += " Burst Time - " + processTime[i, 1] + "s" + Environment.NewLine;
                txtQuestion.Text += " Size of Process - " + processTime[i, 2] + "kb" + Environment.NewLine;
                if (ddlMethod.Text == "Prority Based Scheduling") txtQuestion.Text += " Priority - " + processTime[i, 3] + Environment.NewLine;
            }

            int x = 0;
            foreach (Scheduled_Process block in cpuResults)
            {
                Series series = this.gantt_chart.Series.Add("Charter" + x);
                this.gantt_chart.Series["Charter" + x].ChartType = SeriesChartType.StackedBar;
                this.gantt_chart.Series["Charter" + x].Color = Color.LightGray;
                this.gantt_chart.Series["Charter" + x].BorderColor = Color.Black;

                if (!block.isIdle)
                {
                    turnaroundTime = block.process.turnaround_time;
                    waitingTime = block.process.waiting_time;

                    txtTunaround.Text += "Process " + (block.process.process_id + 1) + ": " + turnaroundTime.ToString() + "s" + Environment.NewLine;
                    txtWaiting.Text += "Process " + (block.process.process_id + 1) + ": " + waitingTime.ToString() + "s" + Environment.NewLine;

                    int i = block.process.process_id + 1;
                    int process_burstTime = block.process.burst_time;

                    this.gantt_chart.Series["Charter" + x].Label = "P" + i;
                    series.Points.Add(process_burstTime);
                }
                else
                {
                    int i = block.process.process_id + 1;
                    int process_burstTime = block.end_timestamp - block.start_timestamp;
                    this.gantt_chart.Series["Charter" + x].Label = "Idle";
                    series.Points.Add(process_burstTime);
                }
                gantt_chart.ChartAreas[0].RecalculateAxesScale();
                x++;
            }

            // Real Memory
            string processName = "";

            if (ddlReal.Text == "First Fit") {
                MainMemoryObj realResults = real.First_Fit(partitions, cpuProcess);
                int z = realResults.partitions.Count -1;
                lbl_Internal.Text = realResults.internalfrag.ToString();
                lbl_External.Text = realResults.externalfrag.ToString();
                for (int j = 0; j < realResults.partitions.Count; j++)
                {
                    List<Process> residing_processes = realResults.partitions[j].residing_processes;
                    processName = "";
                    for (int i = 0; i < residing_processes.Count; i++)
                        processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString();
                    this.real_parition2.Series["Paritioner2" + z].Label = processName;

                    z--;

                    // Virtual Memory
                    if (ddlVirtual.Text == "First In First Out (FIFO)")
                    {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }
            }
            else if (ddlReal.Text == "Best Fit") {
                MainMemoryObj realResults = real.Best_Fit(partitions, cpuProcess);
                int z = realResults.partitions.Count -1;
                lbl_Internal.Text = realResults.internalfrag.ToString();
                lbl_External.Text = realResults.externalfrag.ToString();
                for (int j = 0; j < realResults.partitions.Count; j++)
                {
                    List<Process> residing_processes = realResults.partitions[j].residing_processes;
                    processName = "";
                    for (int i = 0; i < residing_processes.Count; i++)
                        processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString();
                    this.real_parition2.Series["Paritioner2" + z].Label = processName;

                    z--;

                    // Virtual Memory
                    if (ddlVirtual.Text == "First In First Out (FIFO)")
                    {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }
            }
            else if (ddlReal.Text == "Worse Fit") {
                MainMemoryObj realResults = real.Worst_Fit(partitions, cpuProcess);
                int z = realResults.partitions.Count - 1;
                lbl_Internal.Text = realResults.internalfrag.ToString();
                lbl_External.Text = realResults.externalfrag.ToString();
                for (int j = 0; j < realResults.partitions.Count; j++)
                {
                    List<Process> residing_processes = realResults.partitions[j].residing_processes;
                    processName = "";
                    for (int i = 0; i < residing_processes.Count; i++)
                        processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString();
                    this.real_parition2.Series["Paritioner2" + z].Label = processName;

                    z--;

                    // Virtual Memory
                    if (ddlVirtual.Text == "First In First Out (FIFO)")
                    {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }
            }

            this.panelProcessA.Visible = false;
            this.panelProcessA.Location = new Point(0, 356);

            load_Results();
        }
        private void btnGenerateB_Click(object sender, EventArgs e) {
            // Clear all Results after testing
            manualCheck = false;
            txtQuestion.Text = "";
            txtTunaround.Text = "";
            txtWaiting.Text = "";

            int arrivalTime = 0;
            int burstTime = 0;
            int memorySize = 0;
            int priority = 0;
            int waitingTime = 0;
            int turnaroundTime = 0;
            string[,] processTime = new string[10, 10];

            List<Process> cpuProcess = new List<Process>();
            CPUScheduling cpu = new CPUScheduling();
            MainMemory real = new MainMemory();
            VirtualMemory fake = new VirtualMemory();
            List<Scheduled_Process> cpuResults = new List<Scheduled_Process>();

            List<int> partitions = new List<int>();
            partitions.Add(partitionSize[0]);
            partitions.Add(partitionSize[1]);
            partitions.Add(partitionSize[2]);
            int pageCount = 4;

            gantt_chart.Series.Clear();

            for (int i = 0; i < tab_Process.TabCount; i++) {
                this.tab_Process.SelectedIndex = i; int j = 0;
                foreach (TextBox t in tab_Process.SelectedTab.Controls.OfType<TextBox>()) {
                    if (t.Text.Length > 0) {
                        processTime[i, j] = t.Text;
                        j++;
                    }
                    else {
                        manualCheck = true;
                    }
                }
                if (ddlMethod2.Text == "Round Robin Scheduling") {
                    if (txtQuantum.TextLength == 0) {
                        manualCheck = true;
                    }
                }

                // CPU Scheduling
                arrivalTime = Convert.ToInt32(processTime[i, 0]);
                burstTime = Convert.ToInt32(processTime[i, 1]);
                memorySize = Convert.ToInt32(processTime[i, 2]);
                if (ddlMethod2.Text == "Prority Based Scheduling")
                    priority = Convert.ToInt32(processTime[i, 3]);

                if (ddlMethod2.Text == "Prority Based Scheduling")
                {
                    cpuProcess.Add(new Process(i, arrivalTime, burstTime, memorySize, priority));
                    cpuResults = cpu.Nonpremptive_Priority(cpuProcess);
                }
                else
                {
                    cpuProcess.Add(new Process(i, arrivalTime, burstTime, memorySize));
                    cpuResults = cpu.First_Come_First_Serve(cpuProcess);
                }

                txtQuestion.Text += Environment.NewLine + "Process " + (i + 1) + ":" + Environment.NewLine;
                txtQuestion.Text += " Arrival Time - " + processTime[i, 0] + "s" + Environment.NewLine;
                txtQuestion.Text += " Burst Time - " + processTime[i, 1] + "s" + Environment.NewLine;
                txtQuestion.Text += " Size of Process - " + processTime[i, 2] + "kb" + Environment.NewLine;
                if (ddlMethod2.Text == "Prority Based Scheduling") txtQuestion.Text += " Priority - " + processTime[i, 3] + Environment.NewLine;
            }

            int x = 0;
            foreach (Scheduled_Process block in cpuResults)
            {
                Series series = this.gantt_chart.Series.Add("Charter" + x);
                this.gantt_chart.Series["Charter" + x].ChartType = SeriesChartType.StackedBar;
                this.gantt_chart.Series["Charter" + x].Color = Color.LightGray;
                this.gantt_chart.Series["Charter" + x].BorderColor = Color.Black;

                if (!block.isIdle)
                {
                    turnaroundTime = block.process.turnaround_time;
                    waitingTime = block.process.waiting_time;

                    txtTunaround.Text += "Process " + (block.process.process_id + 1) + ": " + turnaroundTime.ToString() + "s" + Environment.NewLine;
                    txtWaiting.Text += "Process " + (block.process.process_id + 1) + ": " + waitingTime.ToString() + "s" + Environment.NewLine;

                    int i = block.process.process_id + 1;
                    int process_burstTime = block.process.burst_time;

                    this.gantt_chart.Series["Charter" + x].Label = "P" + i;
                    series.Points.Add(process_burstTime);
                }
                else
                {
                    int i = block.process.process_id + 1;
                    int process_burstTime = block.end_timestamp - block.start_timestamp;
                    this.gantt_chart.Series["Charter" + x].Label = "Idle";
                    series.Points.Add(process_burstTime);
                }
                gantt_chart.ChartAreas[0].RecalculateAxesScale();
                x++;
            }

            if (ddlMethod2.Text == "Round Robin Scheduling") txtQuestion.Text += Environment.NewLine + "Time Quantum - " + txtQuantum.Text + "s" + Environment.NewLine;

            if (manualCheck == false) {
                // Real Memory
                string processName = "";
                if (ddlReal2.Text == "First Fit") {
                    MainMemoryObj realResults = real.First_Fit(partitions, cpuProcess);
                    int z = realResults.partitions.Count -1;
                    lbl_Internal.Text = realResults.internalfrag.ToString();
                    lbl_External.Text = realResults.externalfrag.ToString();
                    for (int j = 0; j < realResults.partitions.Count; j ++)
                    {
                        List<Process> residing_processes = realResults.partitions[j].residing_processes;
                        processName = "";
                        for (int i = 0; i < residing_processes.Count; i++)
                            processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString() + Environment.NewLine;
                        this.real_parition2.Series["Paritioner2" + z].Label = processName;

                        z--;
                    }

                    // Virtual Memory
                    if (ddlVirtual2.Text == "First In First Out (FIFO)") {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings) {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table) {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual2.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }
                else if (ddlReal2.Text == "Best Fit") {
                    MainMemoryObj realResults = real.Best_Fit(partitions, cpuProcess);
                    int z = realResults.partitions.Count - 1;
                    lbl_Internal.Text = realResults.internalfrag.ToString();
                    lbl_External.Text = realResults.externalfrag.ToString();
                    for (int j = 0; j < realResults.partitions.Count; j++)
                    {
                        List<Process> residing_processes = realResults.partitions[j].residing_processes;
                        processName = "";
                        for (int i = 0; i < residing_processes.Count; i++)
                            processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString();
                        this.real_parition2.Series["Paritioner2" + z].Label = processName;

                        z--;
                    }

                    // Virtual Memory
                    if (ddlVirtual2.Text == "First In First Out (FIFO)")
                    {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual2.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }
                else if (ddlReal2.Text == "Worse Fit") {
                    MainMemoryObj realResults = real.Worst_Fit(partitions, cpuProcess);
                    int z = realResults.partitions.Count - 1;
                    lbl_Internal.Text = realResults.internalfrag.ToString();
                    lbl_External.Text = realResults.externalfrag.ToString();
                    for (int j = 0; j < realResults.partitions.Count; j++)
                    {
                        List<Process> residing_processes = realResults.partitions[j].residing_processes;
                        processName = "";
                        for (int i = 0; i < residing_processes.Count; i++)
                            processName += " P" + (Convert.ToInt32(residing_processes[i].process_id) + 1).ToString();
                        this.real_parition2.Series["Paritioner2" + z].Label = processName;

                        z--;
                    }

                    // Virtual Memory
                    if (ddlVirtual2.Text == "First In First Out (FIFO)")
                    {
                        VirtualMemoryObj fakeResults = fake.First_in_First_out(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                    else if (ddlVirtual2.Text == "Least Recently Used")
                    {
                        VirtualMemoryObj fakeResults = fake.Least_Recently_Used(pageCount, pageSize, cpuProcess);
                        lblFaults.Text = fakeResults.faults.ToString();

                        List<int> refStrings = fakeResults.refStrings;
                        List<List<int>> table = fakeResults.table;

                        DataTable dt = new DataTable();

                        foreach (int rs in refStrings)
                        {
                            dt.Columns.Add(new DataColumn());
                        }

                        foreach (List<int> row in table)
                        {
                            dt.Rows.Add(row.ConvertAll<object>(item => (object)item).ToArray());
                        }

                        dataVirtual.DataSource = dt;
                        dataVirtual.ClearSelection();
                        dataVirtual.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        dataVirtual.DefaultCellStyle.SelectionBackColor = dataVirtual.DefaultCellStyle.BackColor;
                        dataVirtual.DefaultCellStyle.SelectionForeColor = dataVirtual.DefaultCellStyle.ForeColor;

                        int column_index = 0;
                        foreach (int rs in refStrings)
                        {
                            dataVirtual.Columns[column_index].HeaderText = rs.ToString();
                            column_index++;
                        }
                    }
                }

                this.panelProcessB.Visible = false;
                this.panelProcessB.Location = new Point(0, 356);
                
                load_Results();
            }
            else {
                MessageBox.Show("There's Missing Field(s)", "Alert Box");
            }
        }
        private void btnGenerateB_Click_1(object sender, EventArgs e) {
            load_Manual_1();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string date_time = DateTime.Now.ToString();
                    date_time = date_time.Replace(":", "_");
                    date_time = date_time.Replace("/", "_");
                    date_time = date_time.Replace(" ", "_");

                    string path = fbd.SelectedPath.ToString() + "\\Process_" + date_time + ".txt";

                    TextWriter txt = new StreamWriter(path);

                    string data_to_save = "";

                    data_to_save += "Question: " + txtQuestion.Text + Environment.NewLine + Environment.NewLine;
                    data_to_save += "CPU Scheduling: " + Environment.NewLine + "Turn Around Time - " + Environment.NewLine + txtTunaround.Text + Environment.NewLine + Environment.NewLine + "Waiting Time  - " + Environment.NewLine + txtWaiting.Text + Environment.NewLine + Environment.NewLine;
                    data_to_save += "Main Memory: " + Environment.NewLine + "Internal Fragmentation = " + lbl_Internal.Text + Environment.NewLine + "External Fragmentation = " + lbl_External.Text + Environment.NewLine + Environment.NewLine;
                    data_to_save += "Virtual Memory: " + Environment.NewLine + "Page Faults = " + lblFaults.Text + Environment.NewLine + Environment.NewLine;

                    txt.Write(data_to_save);
                    txt.Close();

                    MessageBox.Show("Text File has been Saved.", "Alert Message");
                    load_Home();
                }
            }
        }
    }
}
