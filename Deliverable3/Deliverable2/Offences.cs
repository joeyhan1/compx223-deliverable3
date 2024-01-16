using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

//Name: Joey Han
//ID: 1555382

namespace Deliverable2
{
    public partial class Offences : Form
    {
        public Offences()
        {
            InitializeComponent();
            initaliseChart();
        }
        //List of offences
        List<string> offenceList = new List<string>();
        //List of fname and lnames
        List<string> fnameList = new List<string>();
        List<string> lnameList = new List<string>();
        //List of offenderID's
        List<string> offenderIDList = new List<string>();
        //All the offenders and their data
        List<string> offenderDataList = new List<string>();
        //The current user inputted data
        List<string> addingOffenderList = new List<string>();
        //List of infringement notice data
        List<string> infringementDataList = new List<string>();
        //Variables
        int numOffenders = 0;
        int dataTrackerAdd = 1;
        int dataTrackerUpdate = 2;
        int numInfringements = 0;
        int displayClicks = 0;
        int infringementClicks = 0;
        int filterCheck = 0;
        int chartOn = 0;
        string fname, lname;
        int updateButtonCounter = 0;

        /// <summary>
        /// Display all the Offence table details and the offender name to the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            //Clear all the lists items before adding items
            listClearer();
            comboBoxXandY.Items.Clear();
            //Adding data to their respective lists
            addfirstName();
            addlastName();
            addOffenderID();
            addOffence();
            addOffenceListBox();
            //Looping through all the offences in the offence list
            for (int i = 0; i < offenceList.Count; i++)
            {
                //Selecting all the offences which description are like the offence list offences
                SQL.selectQuery("select * from Offence where description LIKE " + offenceList[i]);
                //Checking if table has rows
                if (SQL.read.HasRows)
                {
                    //Read the whole table
                    while (SQL.read.Read())
                    {
                        for (int k = 0; k < 12; k++)
                        {
                            //Checking for null and implementing a string NULL if there are
                            if (SQL.read[k] == DBNull.Value)
                            {
                                offenderDataList.Add("NULL");
                            }
                            else
                            {
                                offenderDataList.Add(SQL.read[k].ToString());
                            }
                        }
                    }
                }
            }
            for (int l = 0; l < numOffenders; l++)
            {
                for (int m = 0; m < offenderIDList.Count; m++)
                {
                    if (offenderDataList[l * 12 + 11] == offenderIDList[m])
                    {
                        //Setting the name of the offender
                        fname = fnameList[m];
                        lname = lnameList[m];
                    }
                }
                int offenceDesc = l * 12 + 1;
                string output = (offenderDataList[l * 12].PadRight(5) + offenderDataList[l * 12 + 1] + "   " + offenderDataList[l * 12 + 2].PadRight(5)
                + offenderDataList[l * 12 + 3].PadRight(25) + offenderDataList[l * 12 + 4].PadRight(14) + offenderDataList[l * 12 + 5].PadRight(10)
                + offenderDataList[l * 12 + 6].PadRight(10) + offenderDataList[l * 12 + 7].PadRight(10) + offenderDataList[l * 12 + 8].PadRight(10)
                + offenderDataList[l * 12 + 9].PadRight(10) + offenderDataList[l * 12 + 10].PadRight(11) + offenderDataList[l * 12 + 11].PadRight(5)
                + fname + "   " + lname);
                addXandYBox();
                //Checking whether the offence was Speeding
                if (offenderDataList[offenceDesc] == "Speeding")
                {
                    int index = listBoxOffences.Items.IndexOf("Speeding:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
                //Checking whether the offence was Drink Driving
                else if (offenderDataList[offenceDesc] == "Drink Driving")
                {
                    int index = listBoxOffences.Items.IndexOf("Drink Driving:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
                //Checking whether the offence was Street Racing or Cruising
                else if (offenderDataList[offenceDesc] == "Racing")
                {
                    int index = listBoxOffences.Items.IndexOf("Racing:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
                //Checking whether the offence was Street Racing or Cruising
                else if (offenderDataList[offenceDesc] == "Cruising")
                {
                    int index = listBoxOffences.Items.IndexOf("Cruising:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
                //Checking whether the offence was No WOF
                else if (offenderDataList[offenceDesc] == "No Wof")
                {
                    int index = listBoxOffences.Items.IndexOf("No WOF:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
                //Checking whether the offence was Risky Driving
                else if (offenderDataList[offenceDesc] == "Risky Driving")
                {
                    int index = listBoxOffences.Items.IndexOf("Risky Driving:");
                    listBoxOffences.Items.Insert(index + 1, output);
                }
            }
            //Inserting the data at the start of the listbox to tell users what the data in the listbox refers to
            listBoxOffences.Items.Insert(0, "<offence_id><description><allegedSpeed><date_time><icn><xPos><yPos><rainConditions><lightConditions><windConditions><registration_number><offender_id><fname><lname>");
            displayClicks++;
        }

        private void addOffenceListBox()
        {
            //Adding items into the listbox to display
            listBoxOffences.Items.Add("Speeding:");
            listBoxOffences.Items.Add("Drink Driving:");
            listBoxOffences.Items.Add("Racing:");
            listBoxOffences.Items.Add("Cruising:");
            listBoxOffences.Items.Add("No WOF:");
            listBoxOffences.Items.Add("Risky Driving:");
        }

        /// <summary>
        /// Selects all the first names in the SQL Offender and add them to the name list
        /// </summary>
        private void addfirstName()
        {
            SQL.selectQuery("select fname from Offender");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string fname = SQL.read[0].ToString();
                    fnameList.Add(fname);
                }
            }
        }

        /// <summary>
        /// Selects all the last names in the SQL Offender and add them to the name list
        /// </summary>
        private void addlastName()
        {
            SQL.selectQuery("select lname from Offender");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string lname = SQL.read[0].ToString();
                    lnameList.Add(lname);
                }
            }
        }

        /// <summary>
        /// Selects all the offender id in the SQL Offender table and add them to the offenderIDList
        /// </summary>
        private void addOffenderID()
        {
            offenderIDList.Clear();
            SQL.selectQuery("select offenderId from Offender");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string id = SQL.read[0].ToString();
                    offenderIDList.Add(id);
                }
            }
            numOffenders = offenderIDList.Count;
        }

        /// <summary>
        /// Add all the variables in the offender table into a list for new offenders info
        /// </summary>
        private void addNewOffenderItems()
        {
            addingOffenderList.Add("offence_id");
            addingOffenderList.Add("description");
            addingOffenderList.Add("allegedSpeed");
            addingOffenderList.Add("date_time");
            addingOffenderList.Add("icn");
            addingOffenderList.Add("xPos");
            addingOffenderList.Add("yPos");
            addingOffenderList.Add("rainConditions");
            addingOffenderList.Add("lightConditions");
            addingOffenderList.Add("windConditions");
            addingOffenderList.Add("registration_number");
            addingOffenderList.Add("offender_id");
        }


        /// <summary>
        /// Add offences to the offence list
        /// </summary>
        private void addOffence()
        {
            offenceList.Add("'%Speeding%'");
            offenceList.Add("'%Drink Driving%'");
            offenceList.Add("'%racing%'");
            offenceList.Add("'%cruising%'");
            offenceList.Add("'%no wof%'");
            offenceList.Add("'%Risky Driving%'");
        }

        /// <summary>
        /// Clear all the lists
        /// </summary>
        private void listClearer()
        {
            listBoxOffences.Items.Clear();
            offenceList.Clear();
            fnameList.Clear();
            lnameList.Clear();
            offenderIDList.Clear();
            offenderDataList.Clear();
        }

        private void updateListBoxAdd()
        {
            int offenceDesc = numOffenders * 12 + 1;
            for (int m = 0; m < offenderIDList.Count; m++)
            {
                if (offenderDataList[numOffenders * 12 + 11] == offenderIDList[m])
                {
                    //Setting the name of the offender
                    fname = fnameList[m];
                    lname = lnameList[m];
                }
            }

            string output = (offenderDataList[numOffenders * 12].PadRight(5) + offenderDataList[numOffenders * 12 + 1] + "   "
            + offenderDataList[numOffenders * 12 + 2].PadRight(5) + offenderDataList[numOffenders * 12 + 3].PadRight(25)
            + offenderDataList[numOffenders * 12 + 4].PadRight(14) + offenderDataList[numOffenders * 12 + 5].PadRight(10)
            + offenderDataList[numOffenders * 12 + 6].PadRight(10) + offenderDataList[numOffenders * 12 + 7].PadRight(10)
            + offenderDataList[numOffenders * 12 + 8].PadRight(10) + offenderDataList[numOffenders * 12 + 9].PadRight(10)
            + offenderDataList[numOffenders * 12 + 10].PadRight(11) + offenderDataList[numOffenders * 12 + 11].PadRight(5)
            + fname + "   " + lname);
            //Checking whether the offence was Speeding
            if (offenderDataList[offenceDesc] == "Speeding")
            {
                int index = listBoxOffences.Items.IndexOf("Speeding:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
            //Checking whether the offence was Drink Driving
            else if (offenderDataList[offenceDesc] == "Drink Driving")
            {
                int index = listBoxOffences.Items.IndexOf("Drink Driving:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
            //Checking whether the offence was Street Racing or Cruising
            else if (offenderDataList[offenceDesc] == "Racing")
            {
                int index = listBoxOffences.Items.IndexOf("Racing:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
            //Checking whether the offence was Street Racing or Cruising
            else if (offenderDataList[offenceDesc] == "Cruising")
            {
                int index = listBoxOffences.Items.IndexOf("Cruising:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
            //Checking whether the offence was No WOF
            else if (offenderDataList[offenceDesc] == "No Wof")
            {
                int index = listBoxOffences.Items.IndexOf("No WOF:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
            //Checking whether the offence was Risky Driving
            else if (offenderDataList[offenceDesc] == "Risky Driving")
            {
                int index = listBoxOffences.Items.IndexOf("Risky Driving:");
                listBoxOffences.Items.Insert(index + 1, output);
            }
        }

        private void buttonAddOffence_Click(object sender, EventArgs e)
        {
            //Testing purposes
            Console.WriteLine(offenderDataList.Count);
            Console.WriteLine(fnameList.Count);
            Console.WriteLine(lnameList.Count);
            if(listBoxOffences.Items.Count == 0)
            {
                MessageBox.Show("You must display the offences first before adding!");
                
            }
            else if((numOffenders * 12) == offenderDataList.Count)
            {
                if (labelInput.Text == "User Input:")
                {
                    labelInput.Text = "description:";
                }
                MessageBox.Show("Please input the data into the textbox according to the labels!");
                MessageBox.Show("Once you inputted all needed data, the button will be available for you to press to add the inputted data into the listbox and database");
                buttonEnter.Enabled = true;
                textBoxInput.ReadOnly = false;
                addNewOffenderItems();
                //For Testing purposes
                for (int i = 0; i < addingOffenderList.Count; i++)
                {
                    Console.WriteLine(addingOffenderList[i]);
                }
                buttonAddOffence.Enabled = false;
            }
            else if ((numOffenders * 12) != offenderDataList.Count)
            {
                //For Testing purposes
                for (int i = (numOffenders * 12); i < addingOffenderList.Count; i++)
                {
                    Console.WriteLine(addingOffenderList[i]);
                }
                var rain = offenderDataList[numOffenders * 12 + 7];
                var light = offenderDataList[numOffenders * 12 + 8];
                var wind = offenderDataList[numOffenders * 12 + 9];
                var regNo = offenderDataList[numOffenders * 12 + 10];
                DateTime oDate = DateTime.ParseExact(offenderDataList[numOffenders * 12 + 3], "dd-MM-yyyy HH:mm:ss", null);
                var sqlFormattedDate = oDate.Date.ToString("yyyy-MM-dd HH:mm:ss");
                updateListBoxAdd();
                string offenceInsert = ("(" + offenderDataList[numOffenders * 12] + "," + "'" + offenderDataList[numOffenders * 12 + 1] + "',"
                + offenderDataList[numOffenders * 12 + 2] + "," + "@DateTime" + "," + offenderDataList[numOffenders * 12 + 4] + ","
                + offenderDataList[numOffenders * 12 + 5] + "," + offenderDataList[numOffenders * 12 + 6] + "," + "@Rain, @Light, @Wind,@RegNo"
                + "," + offenderDataList[numOffenders * 12 + 11] + ")");
                if(rain == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Rain", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Rain", SqlDbType.VarChar).Value = rain;
                }
                if(light == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Light", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Light", SqlDbType.VarChar).Value = light;
                }
                if(wind == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Wind", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Wind", SqlDbType.VarChar).Value = wind;
                }
                SQL.cmd.Parameters.Add("@RegNo", SqlDbType.VarChar).Value = regNo;
                SQL.cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = sqlFormattedDate;
                SQL.executeQuery("INSERT INTO offence VALUES" + offenceInsert);
                numOffenders++;
                SQL.cmd.Parameters.Clear();
            }
        }

        private void buttonUpdateOffence_Click(object sender, EventArgs e)
        {
            if(listBoxOffences.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item in the list to update!");
            }
            else if (updateButtonCounter == 0)
            {
                if (labelInput.Text == "User Input:")
                {
                    labelInput.Text = "allegedSpeed";
                }
                MessageBox.Show("Please input the data into the textbox according to the labels!");
                MessageBox.Show("Once you inputted all needed data, the button will be available for you to press to update the selected data in the listbox and database");
                buttonEnter.Enabled = true;
                textBoxInput.ReadOnly = false;
                addNewOffenderItems();
                //For Testing purposes
                for (int i = 0; i < addingOffenderList.Count; i++)
                {
                    Console.WriteLine(addingOffenderList[i]);
                }
                buttonUpdateOffence.Enabled = false;
                updateButtonCounter = 1;
            }
            else if(updateButtonCounter == 1)
            {

                string updateLine = Regex.Replace(listBoxOffences.SelectedItem.ToString(), " {2,}", " ");
                string[] words = updateLine.Split(' ');
                int itemIndex = listBoxOffences.SelectedIndex;
                //Testing purposes
                //for(int i = 0; i < words.Length; i++)
                //{
                //    Console.WriteLine(words[i]);
                //}
                int counter = 2;
                int selectedOffenderNum = offenderDataList.IndexOf(words[0]);
                Console.WriteLine(selectedOffenderNum);
                Console.WriteLine(offenderDataList[selectedOffenderNum]);
                for(int i = selectedOffenderNum + 2; i < selectedOffenderNum + 5; i++)
                {
                    offenderDataList[i] = addingOffenderList[counter];
                    Console.WriteLine(offenderDataList[i]);
                    counter++;
                }
                counter = 7;
                for (int i = selectedOffenderNum + 7; i < selectedOffenderNum + 10; i++)
                {
                    offenderDataList[i] = addingOffenderList[counter];
                    Console.WriteLine(offenderDataList[i]);
                    counter++;
                }
                for (int m = 0; m < offenderIDList.Count; m++)
                {
                    if (offenderDataList[selectedOffenderNum + 11] == offenderIDList[m])
                    {
                        //Setting the name of the offender
                        fname = fnameList[m];
                        lname = lnameList[m];
                    }
                }

                string output = (offenderDataList[selectedOffenderNum].PadRight(5) + offenderDataList[selectedOffenderNum + 1] + "   "
                + offenderDataList[selectedOffenderNum + 2].PadRight(5) + offenderDataList[selectedOffenderNum + 3].PadRight(25)
                + offenderDataList[selectedOffenderNum + 4].PadRight(14) + offenderDataList[selectedOffenderNum + 5].PadRight(10)
                + offenderDataList[selectedOffenderNum + 6].PadRight(10) + offenderDataList[selectedOffenderNum + 7].PadRight(10)
                + offenderDataList[selectedOffenderNum + 8].PadRight(10) + offenderDataList[selectedOffenderNum + 9].PadRight(10)
                + offenderDataList[selectedOffenderNum + 10].PadRight(11) + offenderDataList[selectedOffenderNum + 11].PadRight(5)
                + fname + "   " + lname);

                var speed = offenderDataList[selectedOffenderNum + 2];
                DateTime oDate = DateTime.ParseExact(offenderDataList[selectedOffenderNum + 3], "dd-MM-yyyy HH:mm:ss", null);
                var sqlFormattedDate = oDate.Date.ToString("yyyy-MM-dd HH:mm:ss");
                var icn = offenderDataList[selectedOffenderNum + 4];
                var rain = offenderDataList[selectedOffenderNum + 7];
                var light = offenderDataList[selectedOffenderNum + 8];
                var wind = offenderDataList[selectedOffenderNum + 9];
                if (rain == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Rain", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Rain", SqlDbType.VarChar).Value = rain;
                }
                if (light == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Light", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Light", SqlDbType.VarChar).Value = light;
                }
                if (wind == "NULL")
                {
                    SQL.cmd.Parameters.AddWithValue("@Wind", DBNull.Value);
                }
                else
                {
                    SQL.cmd.Parameters.Add("@Wind", SqlDbType.VarChar).Value = wind;
                }
                SQL.cmd.Parameters.Add("@Speed", SqlDbType.VarChar).Value = speed;
                SQL.cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = sqlFormattedDate;
                SQL.cmd.Parameters.Add("@ICN", SqlDbType.VarChar).Value = icn;

                listBoxOffences.Items.RemoveAt(itemIndex);
                listBoxOffences.Items.Insert(itemIndex, output);

                SQL.executeQuery("UPDATE offence SET speedAlleged = @Speed, dateTime = @DateTime, icn = @ICN,rainConditions = @Rain, lightConditions = @Light, windConditions = @Wind WHERE offenceId = " + offenderDataList[selectedOffenderNum]);
                SQL.cmd.Parameters.Clear();
                updateButtonCounter = 0;
                addingOffenderList.Clear();
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if(buttonAddOffence.Enabled == false)
            {
                addOffenceData();
            }
            if(buttonUpdateOffence.Enabled == false)
            {
                updateOffenceData();
            }
        }

        private void addOffenceData()
        {
            if (buttonAddOffence.Enabled == false)
            {
                //Checking if the datatracker is less than 11
                if (dataTrackerAdd < 11)
                {
                    //Checking description
                    if (dataTrackerAdd == 1)
                    {
                        string pattern = "^[a-zA-Z]{1,100}$";
                        if (!Regex.IsMatch(textBoxInput.Text, pattern))
                        {
                            MessageBox.Show("This is a string only field and only 1 - 100 characters!");
                            MessageBox.Show("Please add a description!");
                            dataTrackerAdd--;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        else
                        {
                            addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        //Testing purposes
                        MessageBox.Show(textBoxInput.Text);
                    }
                    //Checking speedAlleged
                    if (dataTrackerAdd == 2)
                    {
                        string pattern1 = "^[0-9]$|^[0-9][0-9]$|^[0-2][0-9][0-9]$";
                        if (!Regex.IsMatch(textBoxInput.Text, pattern1))
                        {
                            MessageBox.Show("This is a pattern only field in the format of [0-9] or [0-9][0-9] or [0-2][0-9][0-9]");
                            dataTrackerAdd--;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        else
                        {
                            addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        //Testing purposes
                        MessageBox.Show(textBoxInput.Text);
                    }
                    //Checking dateTime
                    if (dataTrackerAdd == 3)
                    {
                        DateTime dt;
                        string format = "dd-MM-yyyy HH:mm:ss";
                        if (!DateTime.TryParseExact(textBoxInput.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            MessageBox.Show("This is a date only field in the format of dd-MM-yyyy HH:mm:ss");
                            dataTrackerAdd--;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        else
                        {
                            addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        //Testing purposes
                        MessageBox.Show(textBoxInput.Text);
                    }
                    //Checking icn, xPos, yPos
                    if (dataTrackerAdd >= 4 && dataTrackerAdd <= 6)
                    {
                        if (dataTrackerAdd == 4)
                        {
                            string pattern = "^[0-9]{0,20}$";
                            if (!Regex.IsMatch(textBoxInput.Text, pattern))
                            {
                                MessageBox.Show("This is a integer pattern only field and only 0 - 20 characters");
                                dataTrackerAdd--;
                                labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            }
                            else
                            {
                                addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                                labelInput.Text = "X and Y Position:";
                                comboBoxXandY.Visible = true;
                                textBoxInput.Visible = false;
                            }
                        }
                        if (dataTrackerAdd == 5)
                        {
                            if(comboBoxXandY.SelectedItem == null)
                            {
                                MessageBox.Show("Please select a x and y position!");
                                dataTrackerAdd--;
                            }
                            else
                            {
                                string data = comboBoxXandY.SelectedItem.ToString();
                                data = Regex.Replace(data, " {2,}", " ");
                                string[] position = data.Split(' ');
                                addingOffenderList[5] = position[0].ToString();
                                addingOffenderList[6] = position[1].ToString();
                                comboBoxXandY.Visible = false;
                                textBoxInput.Visible = true;
                                dataTrackerAdd = 6;
                                labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            }
                        }
                    }
                    //Checking for rain/light/wind conditions
                    if (dataTrackerAdd == 7 || dataTrackerAdd == 8 || dataTrackerAdd == 9)
                    {
                        string pattern = "^[a-zA-Z]{1,20}$";
                        if (Regex.IsMatch(textBoxInput.Text, pattern))
                        {
                            addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            if(dataTrackerAdd == 9)
                            {
                                dataTrackerAdd = 10;
                                labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            }
                        }
                        else if (string.IsNullOrEmpty(textBoxInput.Text))
                        {
                            addingOffenderList[dataTrackerAdd] = "NULL";
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            if (dataTrackerAdd == 9)
                            {
                                dataTrackerAdd = 10;
                                labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                            }
                        }
                        else
                        {
                            MessageBox.Show("This is a string only field and only 0 - 20 characters");
                            dataTrackerAdd--;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        //Testing purposes
                        MessageBox.Show(textBoxInput.Text);
                    }
                }
                //Checking Offender ID
                if (dataTrackerAdd == 11)
                {
                    int inputValue = int.Parse(textBoxInput.Text);
                    if(inputValue <= numOffenders && inputValue > 0)
                    {
                        string pattern1 = "^[0-9]$|^[0-9][0-9]$|^[0-9][0-9][0-9]$";
                        if (!Regex.IsMatch(textBoxInput.Text, pattern1))
                        {
                            MessageBox.Show("This is a pattern only field in the format of [0-9] or [0-9][0-9] or [0-9][0-9][0-9]");
                            dataTrackerAdd--;
                            labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                        }
                        else
                        {
                            addingOffenderList[dataTrackerAdd] = textBoxInput.Text;
                            for (int i = 1; i < offenderDataList.Count; i++)
                            {
                                if (addingOffenderList[dataTrackerAdd] == offenderDataList[i])
                                {
                                    addingOffenderList[10] = offenderDataList[i - 1];
                                }
                            }
                        }
                        //Testing purposes
                        MessageBox.Show(textBoxInput.Text);
                    }
                    else
                    {
                        MessageBox.Show("Please input a valid offender id!");
                        dataTrackerAdd--;
                        labelInput.Text = addingOffenderList[dataTrackerAdd + 1] + ": ";
                    }
                }
                dataTrackerAdd++;
                //Checking to clear and reset the button
                if (dataTrackerAdd == 12)
                {
                    addingOffenderList[0] = (numOffenders + 1).ToString();
                    if (buttonAddOffence.Enabled == false)
                    {
                        for (int i = 0; i < addingOffenderList.Count; i++)
                        {
                            offenderDataList.Add(addingOffenderList[i]);
                            //Testing Purposes
                            Console.WriteLine(addingOffenderList[i]);
                        }
                        dataTrackerAdd = 1;
                        addingOffenderList.Clear();
                        labelInput.Text = "User Input:";
                        buttonEnter.Enabled = false;
                        textBoxInput.ReadOnly = true;
                        buttonAddOffence.Enabled = true;
                    }
                }
                textBoxInput.Text = "";
            }
            //Testing purposes
            Console.WriteLine(offenderDataList.Count);
            Console.WriteLine(dataTrackerAdd);
        }

        private void buttonInfringement_Click(object sender, EventArgs e)
        {
            listBoxInfringements.Items.Clear();
            addInfringementData();
            //Inserting the data at the start of the listbox to tell users what the data in the listbox refers to
            listBoxInfringements.Items.Insert(0, "<noticeNumber><issueDate><amount><status><offenceID>");
            Console.WriteLine(numInfringements);
            Console.WriteLine(infringementDataList.Count.ToString());
            for(int i = 0; i < numInfringements; i++)
            {
                string output = (infringementDataList[i * 5] + "   " + infringementDataList[i * 5 + 1] + "   " + infringementDataList[i * 5 + 2] 
                + "   " + infringementDataList[i * 5 + 3].PadRight(10) + infringementDataList[i * 5 + 4]);
                listBoxInfringements.Items.Add(output);
            }
            infringementClicks++;
        }

        private void addInfringementData()
        {
            SQL.selectQuery("select * from infringementNotice");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string noticeNum = SQL.read[0].ToString();
                    string issueDate = SQL.read[1].ToString();
                    //Convert the string into a date
                    DateTime date = Convert.ToDateTime(issueDate);
                    string amount = SQL.read[2].ToString();
                    string status = SQL.read[3].ToString();
                    string offenceID = SQL.read[4].ToString();
                    //Convert the date to my desired string format
                    string issueDateFormatted = date.ToString("dd/MM/yyyy");
                    infringementDataList.Add(noticeNum);
                    infringementDataList.Add(issueDateFormatted);
                    infringementDataList.Add(amount);
                    infringementDataList.Add(status);
                    infringementDataList.Add(offenceID);
                    numInfringements++;
                    //Testing Purposes
                    Console.WriteLine(issueDateFormatted);
                }
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if(displayClicks == 0 || infringementClicks == 0)
            {
                MessageBox.Show("Please display offences and display infringements before you use the filter button!");
            }
            else
            {
                listBoxOffences.Items.Clear();
                addOffenceListBox();
                for (int l = 0; l < numOffenders; l++)
                {
                    for (int m = 0; m < offenderIDList.Count; m++)
                    {
                        if (offenderDataList[l * 12 + 11] == offenderIDList[m])
                        {
                            //Setting the name of the offender
                            fname = fnameList[m];
                            lname = lnameList[m];
                        }
                    }
                    for(int k = 0; k < numInfringements; k++)
                    {
                        //Checking for offences with infringement notice
                        if(offenderDataList[l * 12] == infringementDataList[k * 5 + 4])
                        {
                            filterCheck = 1;
                            DateTime oDate = DateTime.ParseExact(infringementDataList[k * 5 + 1], "dd/MM/yyyy", null);
                            //Checking for unpaid infringement
                            if (infringementDataList[k * 5 + 3] == "Unpaid")
                            {
                                filterCheck = 0;
                                break;
                            }
                            else if (DateTime.Now > oDate.AddDays(28))
                            {
                                filterCheck = 0;
                            }
                        }
                    }
                    //Checking for offences without infringement notice
                    if(filterCheck == 0)
                    {
                        string output = (offenderDataList[l * 12].PadRight(5) + offenderDataList[l * 12 + 1] + "   " + offenderDataList[l * 12 + 2].PadRight(5)
                        + offenderDataList[l * 12 + 3].PadRight(25) + offenderDataList[l * 12 + 4].PadRight(14) + offenderDataList[l * 12 + 5].PadRight(10)
                        + offenderDataList[l * 12 + 6].PadRight(10) + offenderDataList[l * 12 + 7].PadRight(10) + offenderDataList[l * 12 + 8].PadRight(10)
                        + offenderDataList[l * 12 + 9].PadRight(10) + offenderDataList[l * 12 + 10].PadRight(11) + offenderDataList[l * 12 + 11].PadRight(5)
                        + fname + "   " + lname);
                        int offenceDesc = l * 12 + 1;
                        //Checking whether the offence was Speeding
                        if (offenderDataList[offenceDesc] == "Speeding")
                        {
                            int index = listBoxOffences.Items.IndexOf("Speeding:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                        //Checking whether the offence was Drink Driving
                        else if (offenderDataList[offenceDesc] == "Drink Driving")
                        {
                            int index = listBoxOffences.Items.IndexOf("Drink Driving:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                        //Checking whether the offence was Street Racing or Cruising
                        else if (offenderDataList[offenceDesc] == "Racing")
                        {
                            int index = listBoxOffences.Items.IndexOf("Racing:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                        //Checking whether the offence was Street Racing or Cruising
                        else if (offenderDataList[offenceDesc] == "Cruising")
                        {
                            int index = listBoxOffences.Items.IndexOf("Cruising:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                        //Checking whether the offence was No WOF
                        else if (offenderDataList[offenceDesc] == "No Wof")
                        {
                            int index = listBoxOffences.Items.IndexOf("No WOF:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                        //Checking whether the offence was Risky Driving
                        else if (offenderDataList[offenceDesc] == "Risky Driving")
                        {
                            int index = listBoxOffences.Items.IndexOf("Risky Driving:");
                            listBoxOffences.Items.Insert(index + 1, output);
                        }
                    }
                    filterCheck = 0;
                }
                //Inserting the data at the start of the listbox to tell users what the data in the listbox refers to
                listBoxOffences.Items.Insert(0, "<offence_id><description><allegedSpeed><date_time><icn><xPos><yPos><rainConditions><lightConditions><windConditions><registration_number><offender_id><fname><lname>");
            }
        }

        private void updateOffenceData()
        {
            if (buttonUpdateOffence.Enabled == false)
            {
                //Checking speedAlleged
                if (dataTrackerUpdate == 2)
                {
                    string pattern1 = "^[0-9]$|^[0-9][0-9]$|^[0-2][0-9][0-9]$";
                    if (!Regex.IsMatch(textBoxInput.Text, pattern1))
                    {
                        MessageBox.Show("This is a pattern only field in the format of [0-9] or [0-9][0-9] or [0-2][0-9][0-9]");
                        dataTrackerUpdate--;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    else
                    {
                        addingOffenderList[dataTrackerUpdate] = textBoxInput.Text;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    //Testing purposes
                    MessageBox.Show(textBoxInput.Text);
                }
                //Checking dateTime
                if (dataTrackerUpdate == 3)
                {
                    DateTime dt;
                    string format = "dd-MM-yyyy HH:mm:ss";
                    if (!DateTime.TryParseExact(textBoxInput.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    {
                        MessageBox.Show("This is a date only field in the format of dd-MM-yyyy HH:mm:ss");
                        dataTrackerUpdate--;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    else
                    {
                        addingOffenderList[dataTrackerUpdate] = textBoxInput.Text;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    //Testing purposes
                    MessageBox.Show(textBoxInput.Text);
                }
                //Checking icn
                if (dataTrackerUpdate == 4)
                {
                    string pattern = "^[0-9]{0,20}$";
                    if (!Regex.IsMatch(textBoxInput.Text, pattern))
                    {
                        MessageBox.Show("This is a integer pattern only field and only 0 - 20 characters");
                        dataTrackerUpdate--;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    else
                    {
                        addingOffenderList[dataTrackerUpdate] = textBoxInput.Text;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 3] + ": ";
                        dataTrackerUpdate = 6;
                    }
                    //Testing purposes
                    MessageBox.Show(textBoxInput.Text);
                }
                //Checking for rain/light/wind conditions
                if (dataTrackerUpdate == 7 || dataTrackerUpdate == 8 || dataTrackerUpdate == 9)
                {
                    string pattern = "^[a-zA-Z]{1,20}$";
                    if (Regex.IsMatch(textBoxInput.Text, pattern))
                    {
                        addingOffenderList[dataTrackerUpdate] = textBoxInput.Text;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    else if (string.IsNullOrEmpty(textBoxInput.Text))
                    {
                        addingOffenderList[dataTrackerUpdate] = "NULL";
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    else
                    {
                        MessageBox.Show("This is a string only field and only 0 - 20 characters");
                        dataTrackerUpdate--;
                        labelInput.Text = addingOffenderList[dataTrackerUpdate + 1] + ": ";
                    }
                    //Testing purposes
                    MessageBox.Show(textBoxInput.Text);
                }
                dataTrackerUpdate++;
                //Checking to clear and reset the button
                if (dataTrackerUpdate == 10)
                {
                    if (buttonUpdateOffence.Enabled == false)
                    {
                        dataTrackerUpdate = 2;
                        labelInput.Text = "User Input:";
                        buttonEnter.Enabled = false;
                        textBoxInput.ReadOnly = true;
                        buttonUpdateOffence.Enabled = true;
                    }
                }
                textBoxInput.Text = "";
            }
        }

        private void buttonSummary_Click(object sender, EventArgs e)
        {
            listBoxSummary.Items.Add("Database Summary Report:");
            SQL.selectQuery("SELECT COUNT(offenceId) from offence");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string count = SQL.read[0].ToString();
                    listBoxSummary.Items.Add("Number of recorded offenses: " + count);
                }
            }
            SQL.selectQuery("SELECT SUM(amount) from infringementNotice");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string amount = SQL.read[0].ToString();
                    listBoxSummary.Items.Add("Sum of infringement fees: " + amount);
                }
            }
            SQL.selectQuery("SELECT AVG(o.speedAlleged-l.speedLimit) as Difference from offence o join location l on o.locationX = l.locationX AND o.locationY = l.locationY");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string average = SQL.read[0].ToString();
                    listBoxSummary.Items.Add("Average of speed limit exceeded: " + average);
                }
            }
            SQL.selectQuery("Select Top 1 DATEPART(dw, dateTime), count(*) from offence group by DATEPART(dw, dateTime) order by DATEPART(dw, dateTime) desc");
            if (SQL.read.HasRows)
            {
                while (SQL.read.Read())
                {
                    string day = SQL.read[0].ToString();
                    if(day == "1")
                    {
                        day = "Monday";
                    }
                    if (day == "2")
                    {
                        day = "Tuesday";
                    }
                    if (day == "3")
                    {
                        day = "Wednesday";
                    }
                    if (day == "4")
                    {
                        day = "Thursday";
                    }
                    if (day == "5")
                    {
                        day = "Friday";
                    }
                    if (day == "6")
                    {
                        day = "Saturday";
                    }
                    if (day == "7")
                    {
                        day = "Sunday";
                    }

                    listBoxSummary.Items.Add("Average of speed limit exceeded: " + day);
                }
            }
        }

        private void buttonToggleChart_Click(object sender, EventArgs e)
        {
            if (chartOn == 0)
            {
                turnChartOn();
            }
            else if(chartOn == 1)
            {
                turnChartOff();
            }
        }

        private void turnChartOn()
        {
            listBoxInfringements.Visible = false;
            listBoxOffences.Visible = false;
            listBoxSummary.Visible = false;
            buttonAddOffence.Visible = false;
            buttonDisplay.Visible = false;
            buttonEnter.Visible = false;
            buttonFilter.Visible = false;
            buttonInfringement.Visible = false;
            buttonUpdateOffence.Visible = false;
            buttonSummary.Visible = false;
            labelInput.Visible = false;
            textBoxInput.Visible = false;
            chartOffences.Visible = true;
            comboBoxChart.Visible = true;
            labelChart.Visible = true;
            chartOn = 1;
        }

        private void turnChartOff()
        {
            listBoxInfringements.Visible = true;
            listBoxOffences.Visible = true;
            listBoxSummary.Visible = true;
            buttonAddOffence.Visible = true;
            buttonDisplay.Visible = true;
            buttonEnter.Visible = true;
            buttonFilter.Visible = true;
            buttonInfringement.Visible = true;
            buttonUpdateOffence.Visible = true;
            buttonSummary.Visible = true;
            labelInput.Visible = true;
            textBoxInput.Visible = true;
            chartOffences.Visible = false;
            comboBoxChart.Visible = false;
            labelChart.Visible = false;
            chartOn = 0;
        }

        private void addXandYBox()
        {
            for(int i = 0; i < numOffenders; i++)
            {
                string output = offenderDataList[i * 12 + 5] + "   " + offenderDataList[i * 12 + 6];
                comboBoxXandY.Items.Add(output);
            }
        }

        private void comboBoxChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            initaliseChart();
        }

        private void initaliseChart()
        {
            addOffenderID();
            List<string> offenceAgeList = new List<string>();
            List<string> ageCountList = new List<string>();
            List<string> colourList = new List<string>();
            List<string> colourCountList = new List<string>();
            SeriesChartType chartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            if(comboBoxChart.SelectedIndex == 0)
            {
                Chart chart = (Chart)this.Controls["chartOffences"];

                SQL.selectQuery("Select DATEDIFF(YEAR, dateOfBirth, CURRENT_TIMESTAMP) as Age, count(*) from offence, " +
                "offender Where offence.offenderId = offender.offenderId AND dateOfBirth IS NOT NULL Group by DATEDIFF(YEAR, dateOfBirth, CURRENT_TIMESTAMP)");
                if (SQL.read.HasRows)
                {
                    while (SQL.read.Read())
                    {
                        string age = SQL.read[0].ToString();
                        string countAge = SQL.read[1].ToString();
                        offenceAgeList.Add(age);
                        ageCountList.Add(countAge);
                    }
                }

                chart.Series.Clear();
                chart.Titles.Clear();

                chart.Titles.Add("Age vs Number of Offences");
                chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
                //chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineWidth = 0;
                chart.ChartAreas["ChartArea1"].AxisY.Title = "Number of Offences";
                chart.ChartAreas["ChartArea1"].AxisX.Title = "Age of Offenders";
                Series series1 = new Series("Offences");
                
                series1.ChartType = chartType;

                for (int i = 0; i < offenceAgeList.Count; i++)
                {
                    series1.Points.AddXY(offenceAgeList[i], ageCountList[i]);
                    series1.Points[i].BorderColor = Color.Black;
                }

                chart.Series.Add(series1);
                chart.Series[0].IsVisibleInLegend = false;

            }
            if(comboBoxChart.SelectedIndex == 1)
            {
                Chart chart = (Chart)this.Controls["chartOffences"];

                SQL.selectQuery("Select colour, COUNT(colour) AS counter from vehicle v join offence o on v.registrationNumber = o.registrationNumber GROUP BY colour");
                if (SQL.read.HasRows)
                {
                    while (SQL.read.Read())
                    {
                        string colour = SQL.read[0].ToString();
                        string countColour = SQL.read[1].ToString();
                        colourList.Add(colour);
                        colourCountList.Add(countColour);
                    }
                }

                chart.Series.Clear();
                chart.Titles.Clear();

                chart.Titles.Add("Colour of Vehicles vs Number of Offences");
                chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
                //chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineWidth = 0;
                chart.ChartAreas["ChartArea1"].AxisY.Title = "Number of Offences";
                chart.ChartAreas["ChartArea1"].AxisX.Title = "Colour of Vehicles";
                Series series1 = new Series("Offences");
                
                series1.ChartType = chartType;

                for (int i = 0; i < colourList.Count; i++)
                {
                    series1.Points.AddXY(colourList[i], colourCountList[i]);
                    Color colour = Color.FromName(colourList[i]);
                    series1.Points[i].Color = colour;
                    series1.Points[i].BorderColor = Color.Black;
                }
                chart.Series.Add(series1);
                chart.Series[0].IsVisibleInLegend = false;
            }
        }


    }
}
