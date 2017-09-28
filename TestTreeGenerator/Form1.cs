using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TreeGenerator;
using System.Xml;
using System.Collections;

namespace TestTreeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ds = new DataSet();
            DataTable table = ds.Tables.Add("newtable");
            table.Columns.Add("key");
            table.Columns.Add("parentkey");
            table.NewRow();
            dataGridView1.DataSource = ds.Tables[0];



            dsview = new DataSet();

            dt2 = dsview.Tables.Add("dsview");
            dt2.Columns.Add("viewkey");
            dt2.Columns.Add("valueprop", typeof(int));
            dt2.Columns.Add("valuename", typeof(string));

            dt2.Rows.Add(new object[] {"linethick",2 });
            dt2.Rows.Add(new object[] { "fontsize", 8 });
            dt2.Rows.Add(new object[] { "fillcolor", Color.LightYellow.ToArgb() });
            dt2.Rows.Add(new object[] { "verticalspace", 30 });
            dt2.Rows.Add(new object[] { "hspace", 30 });
            dt2.Rows.Add(new object[] { "margin", 20 });
            dt2.Rows.Add(new object[] { "fontcolor", Color.Black.ToArgb() });
            dt2.Rows.Add(new object[] { "linecolor", Color.Black.ToArgb() });
            dt2.Rows.Add(new object[] { "bgcolor", Color.LightGray.ToArgb() });
            dt2.Rows.Add(new object[] { "boxheight", 60 });
            dt2.Rows.Add(new object[] { "boxwidth", 120 });
            dt2.Rows.Add(new object[] { "fontname", 0, "Times Roman" });


            dataGridView2.DataSource = dsview.Tables[0];

        }
        DataSet ds;


        DataSet dsview; DataTable dt2;
        const int d_BoxFillColor = 2; // idx into db
        const int d_verticalspace = 3;
        const int d_hspace = 4;
        const int d_margin = 5;
        const int d_fontcolor = 6;
        const int d_linecolor = 7;
        const int d_bgcolor = 8;
        const int d_boxheight = 9;
        const int d_boxwidth = 10;
        const int d_fontname = 11;


        private void btnShowTree_Click(object sender, EventArgs e)
        {

        }
       // private TreeGenerator.
        private TreeBuilder myTree=null;
        private void ShowTree()
        {
            System.IO.Stream stream = myTree.GenerateTree(-1, -1, "1", System.Drawing.Imaging.ImageFormat.Bmp, checkBoxCategory.Checked, checkBoxOverridePerson.Checked);
            if (stream != null)
                picTree.Image = Image.FromStream(stream);
            else
                MessageBox.Show("null stream");
            //picTree.Image.Save(@"d:\temp\1.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);


           
        }
        private void SetControlValues()
        {
            if (myTree != null)
            {
                //lblBGColor.BackColor = myTree.BGColor;
               // lblBoxFillColor.BackColor = myTree.BoxFillColor;
               // lblFontColor.BackColor = myTree.FontColor;
               // lblLineColor.BackColor = myTree.LineColor;
                nudBoxHeight.Value = Convert.ToDecimal( myTree.BoxHeight);
                nudBoxWidth.Value  = Convert.ToDecimal( myTree.BoxWidth);
                nudFontSize.Value = Convert.ToDecimal( myTree.FontSize);
                nudHorizontalSpace.Value = Convert.ToDecimal( myTree.HorizontalSpace);
                nudLineWidth.Value =Convert.ToDecimal(  myTree.LineWidth);
                nudMargin.Value =Convert.ToDecimal(  myTree.Margin);
                nudVerticalSpace.Value = Convert.ToDecimal(myTree.VerticalSpace);
                
            
                Bindings();
            }
        
        }
        private void Bindings()
        {
            // bind the ints
            // colors are harder and custom on each clicker
            nudLineWidth.DataBindings.Clear();
            nudLineWidth.DataBindings.Add(new Binding("value", dt2.DefaultView[0], "valueprop"));

            nudFontSize.DataBindings.Clear();
            nudFontSize.DataBindings.Add(new Binding("value", dt2.DefaultView[1], "valueprop"));

            nudVerticalSpace.DataBindings.Clear();
            nudVerticalSpace.DataBindings.Add(new Binding("value", dt2.DefaultView[d_verticalspace], "valueprop"));

            nudHorizontalSpace.DataBindings.Clear();
            nudHorizontalSpace.DataBindings.Add(new Binding("value", dt2.DefaultView[d_hspace], "valueprop"));

            nudMargin.DataBindings.Clear();
            nudMargin.DataBindings.Add(new Binding("value", dt2.DefaultView[d_margin], "valueprop"));


            nudBoxHeight.DataBindings.Clear();
            nudBoxHeight.DataBindings.Add(new Binding("value", dt2.DefaultView[d_boxheight], "valueprop"));

            nudBoxWidth.DataBindings.Clear();
            nudBoxWidth.DataBindings.Add(new Binding("value", dt2.DefaultView[d_boxwidth], "valueprop"));

            // set default colors
            myTree.BoxFillColor = Color.FromArgb((int)dt2.Rows[d_BoxFillColor][1]);
            myTree.FontColor= Color.FromArgb((int)dt2.Rows[d_fontcolor][1]);
            myTree.LineColor = Color.FromArgb((int)dt2.Rows[d_linecolor][1]);
            myTree.BGColor = Color.FromArgb((int)dt2.Rows[d_bgcolor][1]);


            button1.Font = new Font(dt2.DefaultView[d_fontname][2].ToString(), float.Parse(dt2.DefaultView[1][1].ToString()));
            myTree.FontName = button1.Font.Name;
        }

        private void lblFontColor_Click(object sender, EventArgs e)
        {
            //DialogResult result;

            //result = colorDialog1.ShowDialog();

            //if (result == DialogResult.Cancel)
            //    return;

            //myTree.FontColor = colorDialog1.Color;
            //ShowTree();

            myTree.FontColor = colorupdate(d_fontcolor);
            lblFontColor.ForeColor = myTree.FontColor;
            ShowTree();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbidx"></param>
        /// <returns></returns>
        private Color colorupdate(int dbidx)
        {
           

            DialogResult result;
            int v = (int)dt2.Rows[dbidx][1];
            colorDialog1.AnyColor = true;
            colorDialog1.Color = Color.FromArgb(v);
            result = colorDialog1.ShowDialog();

            if (result == DialogResult.Cancel)
                return colorDialog1.Color;

           // myTree.BoxFillColor = colorDialog1.Color;
            dt2.Rows[dbidx][1] = colorDialog1.Color.ToArgb();
          //  lblBoxFillColor.ForeColor = colorDialog1.Color;
            
            return colorDialog1.Color;
        }
    

        private void lblBoxFillColor_Click(object sender, EventArgs e)
        {

            myTree.BoxFillColor = colorupdate(d_BoxFillColor);
            lblBoxFillColor.ForeColor = myTree.BoxFillColor;
            ShowTree();

            
        }

        private void lblLineColor_Click(object sender, EventArgs e)
        {
            myTree.LineColor = colorupdate(d_linecolor);
            lblLineColor.ForeColor = myTree.LineColor;
            ShowTree();


           
        }

        private void lblBGColor_Click(object sender, EventArgs e)
        {
            myTree.BGColor = colorupdate(d_bgcolor);
            lblBGColor.ForeColor = myTree.BGColor;
            ShowTree();
        }

        private void nudLineWidth_ValueChanged(object sender, EventArgs e)
        {
            myTree.LineWidth =(float) nudLineWidth.Value;
            ShowTree();
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            myTree.FontSize = (int)nudFontSize.Value;
            ShowTree();
        }

        private void nudVerticalSpace_ValueChanged(object sender, EventArgs e)
        {
            myTree.VerticalSpace = (int)nudVerticalSpace.Value;
            ShowTree();
        }

        private void nudHorizontalSpace_ValueChanged(object sender, EventArgs e)
        {
            myTree.HorizontalSpace = (int)nudHorizontalSpace.Value;
            ShowTree();
        }

        private void nudMargin_ValueChanged(object sender, EventArgs e)
        {
            myTree.Margin = (int)nudMargin.Value;
            ShowTree();
        }

        private void nudBoxHeight_ValueChanged(object sender, EventArgs e)
        {
            myTree.BoxHeight = (int)nudBoxHeight.Value;
            ShowTree();
        }

        private void nudBoxWidth_ValueChanged(object sender, EventArgs e)
        {
            myTree.BoxWidth = (int)nudBoxWidth.Value;
            ShowTree();
        }

        private void picOrgChart_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle currentRect;
            //determine if the mouse clicked on a box, if so, show the  node description.
            string SelectedNode = "No Node";
            //find the node
            foreach (XmlNode oNode in myTree.xmlTree.SelectNodes("//Node"))
            { 
                //iterate through all nodes until found.
                currentRect = myTree.getRectangleFromNode(oNode);
                if (e.X >= currentRect.Left &&
                    e.X <= currentRect.Right &&
                    e.Y >= currentRect.Top &&
                    e.Y <= currentRect.Bottom)
                {
                    SelectedNode = oNode.Attributes["nodeDescription"].InnerText;
                    break;
                }
                
               
            }
            
            //MessageBox.Show(SelectedNode);
            lblNodeText.Text = string.Format("Last Clicked:{0}", SelectedNode);
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            // myTree = new TreeBuilder(GetTreeData(TreeBuilder.mode.ORG));

            //          myTree.SetSchema_OrgChart();
            //        ShowTree();

            // September 27 2017 - I disabled the above because I wanted to use the formatting I picked
            myTree.UpdateTable(GetTreeData(TreeBuilder.mode.ORG));
            ShowTree();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TreeGenerator.TreeData.TreeDataTableDataTable dtTree = new TreeData.TreeDataTableDataTable();
//dtTree.AddTreeDataTableRow("1", "", "Sumil", "");
//dtTree.AddTreeDataTableRow("2", "1", "Amit", "");
//dtTree.AddTreeDataTableRow("3", "1", "Manoj", "");


            //dtTree.AddTreeDataTableRow("1", "", "1", "");
            //dtTree.AddTreeDataTableRow("2", "1", "2", "");
            //dtTree.AddTreeDataTableRow("3", "1", "3", "");
            //dtTree.AddTreeDataTableRow("4", "2", "4", "");
            //dtTree.AddTreeDataTableRow("5", "2", "5", "");
            //dtTree.AddTreeDataTableRow("6", "2", "6", "");
            //dtTree.AddTreeDataTableRow("7", "2", "7", "");
            //dtTree.AddTreeDataTableRow("8", "7", "8", "");
            //dtTree.AddTreeDataTableRow("9", "7", "9", "");
            //dtTree.AddTreeDataTableRow("10", "3", "10", "");
            //dtTree.AddTreeDataTableRow("11", "3", "11", "");
            //dtTree.AddTreeDataTableRow("12", "10", "12", "");
            //dtTree.AddTreeDataTableRow("13", "10", "13", "");
            //dtTree.AddTreeDataTableRow("14", "10", "14", "");
            //dtTree.AddTreeDataTableRow("15", "10", "15", "");
            //dtTree.AddTreeDataTableRow("16", "10", "16", "");
            //dtTree.AddTreeDataTableRow("17", "1", "17", "");
            //dtTree.AddTreeDataTableRow("18", "1", "18", "");
            

            
            //instantiate the object
            //myOrgChart = new OrgChartGenerator.OrgChart(myOrgData);
            myTree = new TreeBuilder(GetTreeData(TreeBuilder.mode.ORG));
            SetControlValues();    
        }

        string NO_PERSON = "No person";
        string CEO = "ceo";

        private void PreloadFromDatabase()
        {
            DataTable table = ds.Tables[0];
            textBox1.Text = ""; textBoxPeople.Text = "";textBoxDuties.Text = "";
            bool first = true;
            string firstkey = "";
            foreach (DataRow dr in table.Rows)
            {
                
                if (dr[0].ToString() != "")
                {
                    string s =   dr[0] + ";" + dr[1];
                    if (!first)  s =                 Environment.NewLine + s;
                    if (first) firstkey = dr[0].ToString();
                    first = false;
                    if (s != "")
                        textBox1.Text = textBox1.Text + s;
                //textBoxPeople.Text = textBoxPeople.Text + dr[0] + ";" + dr[0] + Environment.NewLine;
                //textBoxDuties.Text = textBoxDuties.Text + "a;b" + Environment.NewLine;
                }
            }

            // fails without keying one person to CEO
            if (firstkey != "")
            {
                CEO = firstkey.ToLower();
                textBoxPeople.Text = firstkey + ";" + "*";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TreeData.TreeDataTableDataTable GetTreeData(TreeBuilder.mode mode)
        {

            List<string> strings = new List<string>();

           if (dataGridView1.DataSource != null) PreloadFromDatabase();
            

            //
            // Lowercase all strings
            //
            if (textBox1.Text == "") textBox1.Text ="ceo;";
            if (textBoxPeople.Text == "") textBoxPeople.Text = "ceo;Joe";
            if (textBoxDuties.Text == "") textBoxDuties.Text = "a;b";


            // escape apostophes -- doing this in the google sheet instead
            textBoxDuties.Text = textBoxDuties.Text.Replace("'", "#");


            textBox1.Text = textBox1.Text.ToLower();
            textBoxPeople.Text = textBoxPeople.Text.ToLower();
            textBoxDuties.Text = textBoxDuties.Text.ToLower();


            // -1 - Create People -> Rolemapping
            Hashtable peopleHash = new Hashtable();
            string[] textPeople = null;

            if (mode == TreeBuilder.mode.ORG)
            {
                textPeople = textBoxPeople.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
            }
            else
            if (mode == TreeBuilder.mode.DUTY)
            {
                textPeople = textBoxDuties.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
            }



            string category = String.Empty;
            string SOD = string.Empty;
            foreach (string s in textPeople)
            {
                if (s == "") break;

                string[] values = s.Split(new char[1] { ';' }, StringSplitOptions.None);
                if (values == null || values.Length < 2) throw new Exception("Bad Data");
                string people_ref = values[0].ToLower();
                string supervisor_ref = values[1].ToLower();

                if (values.Length >= 4)
                {
                    category = values[2];
                    SOD = values[3];
                    supervisor_ref = string.Format("{0};{1};{2}", supervisor_ref, category, SOD);
                }

               

                if (peopleHash[people_ref] == null)
                {
                    // create a list for this hash
                    // we store a List of PEOPLE assigned to a role (i.e., multiple technical designers might exist
                    peopleHash[people_ref] = new List<string>();
                    (peopleHash[people_ref] as List<string>).Add(supervisor_ref);
                }
                else
                {
                    // add to the list
                    (peopleHash[people_ref] as List<string>).Add(supervisor_ref);
                }
            }

            // 0 - Process text box
            string[] text = textBox1.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
            foreach (string s in text)
            {
                strings.Add(s);
            }
            System.Collections.Hashtable hash = new System.Collections.Hashtable();

            // 1 - Set the data up for parsing
            for (int i = 0; i < strings.Count; i++)
            {
                string[] values = strings[i].Split(new char[1] { ';' }, StringSplitOptions.None);



                if (values == null || values.Length < 2)
                {
                    MessageBox.Show("Bad Data");
                    return null;
                }
                else
                {

                    string supervisor_ref = values[1].ToLower();

                    if (mode == TreeBuilder.mode.DUTY)
                    {
                        if (supervisor_ref != "")
                            // when listing duties we use a flat org structure
                            supervisor_ref = "1";
                    }
                    string title = values[0];
                    // store the row index to later build the ORG tree
                    // if (supervisor == "") supervisor = ""; // CEO needs the number assignment

                    hash[title] = supervisor_ref;
                }

            }


            // 2 - now that processing is done we build the tree
            TreeData.TreeDataTableDataTable dt = new TreeData.TreeDataTableDataTable();
            
            for (int i = 0; i < strings.Count; i++)
            {
                string[] values = strings[i].Split(new char[1] { ';' }, StringSplitOptions.None);

                if (values == null || values.Length < 2) throw new Exception("Bad Data");
                string title_id = values[0];

                // If our supervisor is CEO we need to use "1" instead of label
                string supervisor = hash[title_id].ToString();

                
                if (supervisor == CEO)
                {
                    supervisor = "1";
                }

                //RULE first row is CEO
                if (i == 0)
                {
                    title_id = "1";
                }

                bool addedBoss = false;
                string person = "";// NO_PERSON;
              

                

                if (peopleHash.Count <= 0) person = "";
                if (peopleHash[title_id] != null)
                {
                    if (peopleHash[title_id] is List<string>)
                    {
                        person = "";
                        if (mode == TreeBuilder.mode.DUTY)
                        {
                            person = "";
                        }
                        //   string[] values_Detail = supervisor.Split(new char[1] { ';' }, StringSplitOptions.None);



                        List<string> tempList = (peopleHash[title_id] as List<string>);
                        if (tempList.Count > 1)
                        {

                            /* Intentionally breaking the Person system 9/28/2017 to allow wordwrap

                            */

                            dt.AddTreeDataTableRow(title_id, supervisor, values[0].ToUpper(), person,"","");
                            this is addedBoss test


                            addedBoss = true;
                            string _id = title_id;
                            foreach (string s in tempList)
                            {
                                string[] values2 = s.Split(new char[1] { ';' }, StringSplitOptions.None);
                                if (values2.Length >=3)
                                {
                                    category = values2[1];
                                    SOD = values2[2];
                                }

                                dt.AddTreeDataTableRow(s, _id,values2[0], "justperson",category,SOD);
                                _id = s;

                                if (mode == TreeBuilder.mode.DUTY)
                                {
                                    // Add Category if DUTY-VIEW
                                    //categ
                                    // Add person actualy doing this
                                }


                                //person = person + "\n" + s;
                            }
                        }
                        else
                        {
                            person = tempList[0];
                        }
                      
                    }
                }

                if (addedBoss == false)
                {
                    
                    dt.AddTreeDataTableRow(title_id, supervisor, values[0].ToUpper(), person, "", "");
                }
                addedBoss = false;

            }

          /*  
            dt.AddTreeDataTableRow("Project Director", "1", "PD", "This is the first child.");
            dt.AddTreeDataTableRow("3", "1", "Child 2", "This is the second child.");
            dt.AddTreeDataTableRow("4", "1", "Child 3", "This is the third child.");
            dt.AddTreeDataTableRow("5", "Project Director", "GrandChild 1", "This is the only Grandchild.");
           */
            return dt;
        }

        

        private void buttonDuties_Click(object sender, EventArgs e)
        {
            myTree = new TreeBuilder(GetTreeData(TreeBuilder.mode.DUTY));
            myTree.SetSchema_TaskChart();
            myTree.myMode = TreeBuilder.mode.DUTY;
            ShowTree();
        }

        private void checkBoxCategory_CheckedChanged(object sender, EventArgs e)
        {
            ShowTree();
        }

        private void checkBoxOverridePerson_CheckedChanged(object sender, EventArgs e)
        {
            ShowTree();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xml";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                ds.WriteXml(sd.FileName);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.DefaultExt = "xml";
            
            if (od.ShowDialog() == DialogResult.OK)
            {
               
                ds.ReadXml(od.FileName);
            }
        }



        /// <summary>
        /// save view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xml";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                dsview.WriteXml(sd.FileName);
            }
        }
        /// <summary>
        /// load view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.DefaultExt = "xml";
            if (od.ShowDialog() == DialogResult.OK)
            {

                dsview.Tables[0].Clear();
                dsview.ReadXml(od.FileName);
                dt2 = dsview.Tables[0];
                Bindings();
              
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                button1.Font = fd.Font;
                dt2.Rows[d_fontname][2] = fd.Font.Name;
                myTree.FontName = fd.Font.Name;
                ShowTree();
            }
        }
    }
}
