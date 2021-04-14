using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Pager2
{

    public partial class Registred : Window
    {
        Person_Registred person = new Person_Registred();
        private string login_reg = "Логин";
        private string pass_reg = "Пароль";
        private string pass_2_reg = "Повторите пароль";
        private string email_reg = "email";        

        public Registred()
        {
            InitializeComponent();
        }
        public void txt_bx_stabilizer()
        {
            if (txt_bx_login_reg.Text == "")
            {
                txt_bx_login_reg.Text = login_reg;
            }
            
            if (txt_bx_pass_reg.Text == "")
            {
                txt_bx_pass_reg.Text = pass_reg;
            }
            
            if (txt_bx_pass_reg_2.Text == "")
            {
                txt_bx_pass_reg_2.Text = pass_2_reg;
            }
            
            if (txt_bx_mail_reg.Text == "")
            {
                txt_bx_mail_reg.Text = email_reg;
            }          

        }

        private void txt_bx_login_reg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_login_reg.Text == login_reg)
            {
                txt_bx_login_reg.Text = "";
            }
           
        }

        private void txt_bx_pass_reg_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_pass_reg.Text == pass_reg)
            {
                txt_bx_pass_reg.Text = "";
            }                    
        }

        private void txt_bx_pass_reg_2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_pass_reg_2.Text == pass_2_reg)
            {
                txt_bx_pass_reg_2.Text = "";
            }                      
        }

        private void txt_bx_mail_reg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_mail_reg.Text == email_reg)
            {
                txt_bx_mail_reg.Text = "";
            }                        
        }
      
        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            
            person.protocol = "REGISTRED";
            person.mail = txt_bx_mail_reg.Text;
            person.login = txt_bx_login_reg.Text;
            person.pass = txt_bx_pass_reg.Text;
            person.mail_accepr = chk_bx_mail.IsChecked.Value;

            if (check_login() == true)
            {
                string json = JsonSerializer.Serialize<Person_Registred>(person);
                try
                {                    
                    txt_blk_server_down.Foreground = Brushes.Red;
                    txt_blk_server_down.Text = Server_connect.connect(json); 
                }
                catch (System.Exception)
                {
                    txt_blk_server_down.Foreground = Brushes.Red;
                    txt_blk_server_down.Text = "Сервер не отвечает";
                }
            }          
            
        }

        private bool check_login()
        {
            if (txt_bx_login_reg.Text == "" || txt_bx_login_reg.Text == "Логин")
            {
                txt_blk_lgn_access.Text = "Введите Логин!";
                txt_blk_lgn_access.Foreground = Brushes.Red;
                return false;
            }
            else
            {
                txt_blk_lgn_access.Text = "Логин подходит!";
                txt_blk_lgn_access.Foreground = Brushes.Green;
                return check_password();
            }
        }
        private bool check_password()
        {            

            if (txt_bx_pass_reg.Text.Length >= 6)
                {
                    if (txt_bx_pass_reg.Text == txt_bx_pass_reg_2.Text)
                    {
                        person.pass = txt_bx_pass_reg.Text;
                        txt_blk_pass_access.Text = "Пароли совпадают!";
                        txt_blk_pass_access.Foreground = Brushes.Green;
                        return true;
                    }
                    else
                    {
                        txt_blk_pass_access.Text = "Пароли не совпадают!";
                        txt_blk_pass_access.Foreground = Brushes.Red;
                        return false;
                    }
                }
                else
                {
                    txt_blk_pass_access.Text = "В пароле должно быть не меньше 6 символов!";
                    txt_blk_pass_access.Foreground = Brushes.Red;
                    return false;
                }            
        }
            
       }

    public class Person
    {
        public string protocol { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
    }
    public class Person_Registred:Person   {    
       
        public string mail { get; set; }
        public bool mail_accepr { get; set; }
    }

}
