using System;
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
using System.Data;
using System.Data.SQLite;

namespace Member3
{
    /// <summary>
    /// Interaction logic for View_Lecturers.xaml
    /// </summary>
    public partial class View_Lecturers : Window
    {
        private SQLiteConnection connection = DBConnection.connect();

        private int empID;
        private string empName;
        private string faculty;
        private string department;
        private string center;
        private string building;
        private string level;
        private double rank;

        public View_Lecturers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
            loadComboBoxes();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = LecDataGrid.SelectedItem as DataRowView;
            empID = int.Parse(row.Row["EmployeeID"].ToString());

            connection.Open();
            try
            {
                SQLiteCommand command = connection.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = "Delete from Lecturer where EmployeeID = @EmployeeID";
                command.Parameters.AddWithValue("@EmployeeID", empID);

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Lecturer Data has been Deleted!");
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            loadDataGrid();
        }

        private void loadDataGrid()
        {
            connection.Open();

            try
            {
                DataTable dataTable = new DataTable();

                SQLiteCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "Select * from Lecturers";

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);

                dataAdapter.Fill(dataTable);

                LecDataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }
        private void clearFields()
        {
            UpdEmpIdTxt.Text = "";
            UpdEmpNameTxt.Text = "";
            UpdFacultyCombo.Text = "";
            UpdEmpDepTxt.Text = "";
            UpdCenterCombo.Text = "";
            UpdBuildingCombo.Text = "";
            UpdLevelCombo.Text = "";
            UpdRankTxt.Text = "";
        }

        private void loadComboBoxes()
        {
            connection.Open();

            try
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * from Building";

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UpdBuildingCombo.Items.Add(reader.GetString("name"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = LecDataGrid.SelectedItem as DataRowView;

            UpdEmpIdTxt.Text = row.Row["EmployeeID"].ToString();
            UpdEmpNameTxt.Text = row.Row["Name"].ToString();
            UpdFacultyCombo.Text = row.Row["Faculty"].ToString();
            UpdEmpDepTxt.Text = row.Row["Department"].ToString();
            UpdCenterCombo.Text = row.Row["Center"].ToString();
            UpdBuildingCombo.Text = row.Row["Building"].ToString();
            UpdLevelCombo.Text = row.Row["Level"].ToString();
            UpdRankTxt.Text = row.Row["Rank"].ToString();
        }

        private void UpdLevelCombo_DropDownClosed(object sender, EventArgs e)
        {
            if (UpdLevelCombo.Text.Equals("Professor") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"1.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Assistant Professor") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"2.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Senior Lecturer(HG)") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"3.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Senior Lecturer") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"4.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Lecturer") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"5.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Assistant Lecturer") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"6.{UpdEmpIdTxt.Text}";
            }
            else if (UpdLevelCombo.Text.Equals("Instructors") && UpdEmpIdTxt.Text != null)
            {
                UpdRankTxt.Text = $"7.{UpdEmpIdTxt.Text}";
            }
        }
    }
}
