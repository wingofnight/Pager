using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Pager2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public void txt_bx_stabilizer()
        {
            if (txt_bx_login.Text == "")
            {
                txt_bx_login.Text = "Логин";
            }
            if (txt_bx_pass.Text == "")
            {
                txt_bx_pass.Text = "Пароль";
            }
            if (txt_bx_ress.Text == "")
            {
                txt_bx_ress.Text = "Введите Email для востановления";
            }

        }
        private void txt_bx_pass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_pass.Text == "Пароль")
            {
                txt_bx_pass.Text = "";
            }
        }

        private void txt_bx_login_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_login.Text == "Логин")
            {
                txt_bx_login.Text = "";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var registred = new Registred();
            registred.Show();
            Close();
        }

        private void btn_ress_Click(object sender, RoutedEventArgs e)
        {
            txt_bx_ress.Visibility = Visibility.Visible;
            btn_email_ress.Visibility = Visibility.Visible;
        }

        private void txt_bx_ress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txt_bx_stabilizer();
            if (txt_bx_ress.Text == "Введите Email для востановления")
            {
                txt_bx_ress.Text = "";
            }
        }

        private void btn_accept_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person
            {
                login = txt_bx_login.Text,
                pass = txt_bx_pass.Text,
                protocol = "LOGIN"
            };
            //тут будет функция передающая данные на сервер.
        }
    }

    public static class Server_connect{
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера              
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
       
        

      /*  private static string Listen()
         {   

             do
             {
                 bytes = handler.Receive(data);
                 builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
             }
             while (handler.Available > 0);

             handler.Shutdown(SocketShutdown.Both);
             handler.Close();
             return builder.ToString();
         }*/

        public static string connect(string message)
        {          
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);               
                socket.Connect(ipPoint);
                socket.Send(data);//отправляем сообщение на сервер 
               
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
               // socket.Shutdown(SocketShutdown.Both);
               // socket.Close();
                return builder.ToString();
            }
            catch (System.Exception)
            {
                return "Нет подключения к серверу";
            }        
            
        }
    }
    
}
