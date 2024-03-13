using System.Net.Sockets;
using System.Net;
using System;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 serverPort = 13371;
            IPAddress localAddr = IPAddress.Parse("185.221.162.111");

            TcpClient client = new TcpClient();
            client.Connect(localAddr, serverPort);
            NetworkStream stream = client.GetStream();

            while (true)
            {
                byte[] bytes = new byte[256];
                Console.WriteLine("Введите строку: ");
                string data = Console.ReadLine();
                if (!string.IsNullOrEmpty(data))
                {
                    bytes = System.Text.Encoding.UTF8.GetBytes(data);
                    stream.Write(bytes, 0, bytes.Length);

                    bytes = new byte[256];
                    int i = stream.Read(bytes, 0, bytes.Length);
                    string response = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine(response);
                }
                else
                {
                    client.Close();
                    break;
                }
            }

        }
    }
}
