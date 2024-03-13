using MessageServer.Models;
using System.Net.Sockets;
using System.Net;

namespace MessageServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Int32 serverPort = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener calcServer = new TcpListener(localAddr, serverPort);

            calcServer.Start();
            while (true)
            {
                TcpClient client = calcServer.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                Byte[] bytes = new Byte[256];
                string data = null;
                string processedText;
                int i = stream.Read(bytes, 0, bytes.Length);
                data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                using (MessageDbContext db = new MessageDbContext())
                {
                    if (data != "LIST" && data != "" && data != null)
                    {
                        Message msg = new Message
                        {
                            Id = Guid.NewGuid(),
                            Data = data
                        };
                        db.Messages.Add(msg);
                        db.SaveChanges();
                        var response = System.Text.Encoding.UTF8.GetBytes($"Объявление добавлено: {data}");
                        stream.Write(response, 0, response.Length);
                    }
                    else if (data == "LIST")
                    {
                        var messages = db.Messages.ToList();
                        List<string> allMessages = new List<string>();
                        messages.ForEach(m => allMessages.Add(m.Data));
                        processedText = string.Join(";", allMessages);
                        var response = System.Text.Encoding.UTF8.GetBytes(processedText);
                        stream.Write(response, 0, response.Length);
                    }
                    
                
                }
                
                client.Close();
            }
            
        }
    }
}
