using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace TryToOnTopAndImageCatch
{
    class ConnectWithServer
    {
        public static string SendMessage(string Message)
        {
           return SendMessageFromSocket(11000, Message);
        }
        static string SendMessageFromSocket(int port,string Message)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);
            
            string message = Message;
            
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            string answer= Encoding.UTF8.GetString(bytes, 0, bytesRec);

            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()


            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
            return answer;
        }
    
}
}
