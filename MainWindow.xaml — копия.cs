using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace pajeroTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        
        private void btn_senf_Click(object sender, RoutedEventArgs e)
        {
            
            string connStr = "";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "SELECT Message FROM PejerText";
            MySqlCommand command = new MySqlCommand(sql, conn);
            string name = command.ExecuteScalar().ToString();
            txt_blok.Text = name;
            conn.Close();

        }

        private void btn_fuk_Click(object sender, RoutedEventArgs e)
        {
            string connStr = "";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string send = txt_blok1.Text;
            string sql = "UPDATE PejerText SET Message='"+send+"'";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteScalar();
            
            conn.Close();
        }
    }
}
