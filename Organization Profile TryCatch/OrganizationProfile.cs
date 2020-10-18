using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Organization_Profile_TryCatch
{
    public partial class OrganizationProfile : Form, ResetControlsListener
    {

        private string _FullName;
        private int _Age;
        private long _ContactNo;
        private long _StudentNo;


        public OrganizationProfile()
        {
            InitializeComponent();
        }

        private void OrganizationProfile_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]
            {
                "BS Information Technology",
                "BS Computer Science",
                "BS Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management",
            };

            string[] ListOfGender = new string[]
            {
                "Male",
                "Female"
            };

            for (int i = 0; i < ListOfProgram.Length; i++)
            {
                CbPrograms.Items.Add(ListOfProgram[i]);
            }

            for (int i = 0; i < ListOfGender.Length; i++)
            {
                CbGender.Items.Add(ListOfGender[i]);
            }

            DatePickerBirthday.MaxDate = new DateTime(year: DateTime.Today.Year - 18, month: DateTime.Today.Month, day: DateTime.Today.Day - 1);
        }

        public long StudentNumber(string studNum)
        {
            try
            {
                
                if(studNum.Length == 0)
                {
                    throw new ArgumentNullException();
                }

                if(studNum.Length < 10)
                {
                    throw new IndexOutOfRangeException();
                }

                if (long.TryParse(studNum, out long j))
                {
                    _StudentNo = j;
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                MessageBox.Show("Student Number must be a number.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _StudentNo = 0;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Student Number should not be null", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _StudentNo = 0;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Student Number must be atleast 10 numbers", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _StudentNo = 0;
            }

            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {
            try
            {

                if(Contact.Length == 0)
                {
                    throw new ArgumentNullException();
                }

                if (Contact.Length != 11)
                {
                    throw new IndexOutOfRangeException();
                }

                if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
                {
                    _ContactNo = long.Parse(Contact);
                } else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                MessageBox.Show("Contact number must be a number.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ContactNo = 0;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Contact number should not be null", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ContactNo = 0;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Contact number must be 11 numbers", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ContactNo = 0;
            }

            return _ContactNo;
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {

            try
            {
                if (LastName.Length == 0)
                {
                    if (FirstName.Length == 0)
                    {
                        throw new ArgumentNullException("Last name and First name should not be empty");
                    }
                    else
                    {
                        throw new ArgumentNullException("Last name should not be empty");
                    }
                        
                }

                if (FirstName.Length == 0)
                {
                    throw new ArgumentNullException("First name should not be empty");
                }


                if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") || Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") || Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
                {
                    _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
                } else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                MessageBox.Show("Last, First, and Middle name must be in letters.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FullName = "";
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FullName = "";
            }

            return _FullName;
        }

        public int Age(string age)
        {
            try
            {

                if(age.Length == 0)
                {
                    throw new ArgumentNullException();
                }

                if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
                {
                    
                    _Age = Int32.Parse(age);
                    if (_Age < 17 || _Age > 100)
                        throw new IndexOutOfRangeException();
                        
                }
                else
                {
                    throw new FormatException();
                }
            } catch(FormatException e) 
            {
                MessageBox.Show("Age must be a number.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Age = 0;
            } catch(ArgumentNullException ex)
            {
                MessageBox.Show("Age should not be null", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Age = 0;
            }
            catch (IndexOutOfRangeException ez)
            {
                MessageBox.Show("Age must be greater than 17 and less than 100", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Age = 0;
            }

            return _Age;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            StudentInformationClass.SetFullName = FullName(TextLastName.Text,
                TextFirstName.Text, TextMiddleName.Text);
            StudentInformationClass.SetStudentNo = StudentNumber(TextStudentNo.Text);
            if (CbPrograms.SelectedItem != null) {
                StudentInformationClass.SetProgram = CbPrograms.SelectedItem.ToString();
            }
            else {
                MessageBox.Show("You must pick a program.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CbGender.SelectedItem != null) {
                StudentInformationClass.SetGender = CbGender.SelectedItem.ToString();
            }
            else {
                MessageBox.Show("You must pick a gender.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StudentInformationClass.SetContactNo = ContactNo(TextContactNo.Text);
            StudentInformationClass.SetAge = Age(TextAge.Text);
            StudentInformationClass.SetBirthday = DatePickerBirthday.Value.ToString("yyyy-MM-dd");

            if(StudentInformationClass.SetFullName.Length == 0)
            {
                return;
            }

            if (StudentInformationClass.SetStudentNo == 0)
            {
                return;
            }

            if (StudentInformationClass.SetContactNo == 0)
            {
                return;
            }

            if (StudentInformationClass.SetAge == 0)
            {
                return;
            }


            frmConfirmation frm = new frmConfirmation(this);
            frm.ShowDialog();
        }

        public void OnReset()
        {
            TextStudentNo.Text = "";
            TextLastName.Text = "";
            TextFirstName.Text = "";
            TextMiddleName.Text = "";
            TextAge.Text = "";
            TextContactNo.Text = "";
            CbPrograms.SelectedIndex = -1;
            CbGender.SelectedIndex = -1;
            DatePickerBirthday.Value = new DateTime(year: DateTime.Today.Year - 18, month: DateTime.Today.Month, day: DateTime.Today.Day - 1);
        }

        private void DatePickerBirthday_ValueChanged(object sender, EventArgs e)
        {

            int EstimatedAge = DateTime.Today.Year - DatePickerBirthday.Value.Year;
            int month = DateTime.Today.Month - DatePickerBirthday.Value.Month;
            int day = DateTime.Today.Day - DatePickerBirthday.Value.Day;
            if(month == 0)
            {
                if (day == 0)
                {
                    MessageBox.Show("Happy Birthday!", "WOW", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (day > 0)
                {
                    EstimatedAge -= 1;
                }
            } else if(month < 0)
            {
                EstimatedAge -= 1;
            }
            TextAge.Text = EstimatedAge.ToString();
        }
    }
}
