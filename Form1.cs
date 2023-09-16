using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string sql;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                screenshot();
            }
            catch { }
            try
            {
                string connectionString = "datasource=10.0.0.13;database=csharp;port=3306;username=#######;password=#######";
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmdd = new MySqlCommand();
                MySqlDataReader mdr;
                connection.Open();
                string s = " select chat from wow";
                string d = "DELETE FROM `wow` WHERE 1";
                cmd = new MySqlCommand(s, connection);
                mdr = cmd.ExecuteReader();
                if (mdr.Read())
                {
                    this.Show();
                    sql = mdr.GetString("chat");
                    connection.Close();
                }
                connection.Open();
                cmdd = new MySqlCommand(d, connection);
                cmdd.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
            try
            { 
            sendmsg();
        }
            catch { }
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void sendmsg()
        {
            if (sql == "") { }
            else
            {
                IntPtr handle = FindWindow("Wow-64", "World of Warcraft");
                if (!handle.Equals(IntPtr.Zero))
                {
                    // activate Notepad window
                    if (SetForegroundWindow(handle))
                    {
                        SendKeys.Send("{ENTER}");
                        // send "Hello World!"
                        SendKeys.Send(sql);
                        // send key "Enter"
                        SendKeys.Send("{ENTER}");
                    }
                }
            }
            string connectionString = "datasource=10.0.0.13;database=csharp;port=3306;username=######;password=#####";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmdd = new MySqlCommand();
            string d = "DELETE FROM `wow` WHERE 1";
            connection.Open();
            cmdd = new MySqlCommand(d, connection);
            cmdd.ExecuteNonQuery();
            connection.Close();
            sql = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendmsg();
        }
        public string fileName = @"C:\xampp3\htdocs\10.0.0.13\chat.jpg";
        public void screenshot()
        {
            Rectangle rect = new Rectangle(0, 0, 525, 250);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            bmp.Save(fileName, ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            screenshot();
        }
    }

}

