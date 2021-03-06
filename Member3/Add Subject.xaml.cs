﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SQLite;

namespace Member3
{
    /// <summary>
    /// Interaction logic for Add_Subject.xaml
    /// </summary>
    public partial class Add_Subject : Window
    {
        private SQLiteConnection connection = DBConnection.connect();
        private string sbjCode;
        private string sbjName;
        private string sbjYear;
        private string sbjSem;
        private int numOfLecHr;
        private int numOfTuteHr;
        private int numOfLabHr;
        private int numOfEvlHr;

        public Add_Subject()
        {
            InitializeComponent();
        }

        private void BtnviewSbj_Click(object sender, RoutedEventArgs e)
        {
            View_Subjects view_Subjects = new View_Subjects();
            this.Close();
            view_Subjects.Show();
        }

        private void BtnAddSbj_Click(object sender, RoutedEventArgs e)
        {
            sbjCode = SbjCodeTxt.Text;
            sbjName = SbjNameTxt.Text;
            sbjYear = OffYearTxt.Text;
            sbjSem = OffSemTxt.Text;

            numOfLecHr = int.Parse(NumLecHrTxt.Text);
            numOfTuteHr = int.Parse(NumTuteHrTxt.Text);
            numOfLabHr = int.Parse(NumLabHrTxt.Text);
            numOfEvlHr = int.Parse(NumEvaHrTxt.Text);

            connection.Open();

            try
            {
                SQLiteCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into Subject" +
                        "(Subject_Code, Subject_Name, Offered_Year, Offered_Semester, Lecture_Hours, Tutorial_Hours, Lab_Hours, Evaluation_Hours)" +
                        " Values" +
                        "(@Subject_Code, @Subject_Name, @Offered_Year, @Offered_Semester, @Lecture_Hours, @Tutorial_Hours, @Lab_Hours, @Evaluation_Hours)";

                cmd.Parameters.AddWithValue("@Subject_Code", sbjCode);
                cmd.Parameters.AddWithValue("@Subject_Name", sbjName);
                cmd.Parameters.AddWithValue("@Offered_Year", sbjYear);
                cmd.Parameters.AddWithValue("@Offered_Semester", sbjSem);
                cmd.Parameters.AddWithValue("@Lecture_Hours", numOfLecHr);
                cmd.Parameters.AddWithValue("@Tutorial_Hours", numOfTuteHr);
                cmd.Parameters.AddWithValue("@Lab_Hours", numOfLabHr);
                cmd.Parameters.AddWithValue("@Evaluation_Hours", numOfEvlHr);

                int rows = cmd.ExecuteNonQuery();


                if (rows > 0)
                {
                    MessageBox.Show("Data has been Inserted");
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            clearFields();
        }

        private void clearFields()
        {
            SbjCodeTxt.Text = "";
            SbjNameTxt.Text= "";
            OffYearTxt.Text = "";
            OffSemTxt.Text = "";

            NumLecHrTxt.Text = "";
            NumTuteHrTxt.Text = "";
            NumLabHrTxt.Text = "";
            NumEvaHrTxt.Text = "";
        }
    }
}
