using System;
using System.Windows.Forms;

namespace Organization_Profile_TryCatch
{

    public interface ResetControlsListener
    {
        void OnReset();
    }

    public partial class frmConfirmation : Form
    {

        private ResetControlsListener listener;

        public frmConfirmation(ResetControlsListener listener)
        {
            InitializeComponent();
            this.listener = listener;
        }

        private void frmConfirmation_Load(object sender, EventArgs e)
        {
            LabelStudentNo.Text = StudentInformationClass.SetStudentNo.ToString();
            LabelName.Text = StudentInformationClass.SetFullName;
            LabelProgram.Text = StudentInformationClass.SetProgram;
            LabelBirthday.Text = StudentInformationClass.SetBirthday;
            LabelGender.Text = StudentInformationClass.SetGender;
            LabelContactNo.Text = StudentInformationClass.SetContactNo.ToString();
            LabelAge.Text = StudentInformationClass.SetAge.ToString();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure to submit this information?",
                "QUESTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(dialog == DialogResult.OK)
            {
                listener.OnReset();
                this.Close();
            }
        }
    }
}
