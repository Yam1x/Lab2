using System.Net.Sockets;
using System.Net;

Int32 serverPort = 13000;
IPAddress localAddr = IPAddress.Parse("127.0.0.1");
TcpListener calcServer = new TcpListener(localAddr, serverPort);

calcServer.Start();
TcpClient client = calcServer.AcceptTcpClient();
NetworkStream stream = client.GetStream();

while (true)
{

    Byte[] bytes = new Byte[256];
    string data = null;
    int i = stream.Read(bytes, 0, bytes.Length);
    data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
    var words = data.Split(' ');
    var wordsList = new List<string>();
    foreach (var word in words)
    {
        if (!wordsList.Contains(word))
        {
            wordsList.Add(word);
        }
    }
    wordsList.Sort();
    string processedText = string.Join("\n", wordsList);

    var response = System.Text.Encoding.UTF8.GetBytes(processedText);
    stream.Write(response, 0, response.Length);
}

