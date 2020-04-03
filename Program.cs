using System;
using System.Net.Sockets;
using System.Threading;

namespace SocketCom
{
    class Program
    {
        static void Main(string[] args)
        {
            string IP_SERVER = "127.0.0.1";
            int PUERTO_SERVER = 8080;
            Console.WriteLine("Iniciando...");
            Console.WriteLine("Escriba: server o cliente");
            string respuesta = Console.ReadLine();


            if(respuesta == "server")
            {
                TCPServer server = new TCPServer( IP_SERVER, PUERTO_SERVER, true);
                server.Listen();
            } else if(respuesta == "cliente")
            {
                string mensaje = string.Empty;
                TcpClient cliente = new TcpClient(IP_SERVER, PUERTO_SERVER);
                NetworkStream stream = cliente.GetStream();
                while(mensaje != "salir") 
                {
                    Console.WriteLine("Escriba el mensaje a enviar (escriba salir para terminar): ");
                    mensaje = Console.ReadLine();
                    mensaje.Trim();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(mensaje);   
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Mensaje enviado: {0}", mensaje); 
                }

                stream.Write(System.Text.Encoding.ASCII.GetBytes("bye"), 0, System.Text.Encoding.ASCII.GetBytes("bye").Length);
                Thread.Sleep(1500);

                stream.Close();
                cliente.Close(); 
            }
        }
    }
}
