using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ChatConsole {

    public class Runner {
        static void Main (string[] args) {

            Console.WriteLine("[1] Join Server\n[2] Host Server\n[3] Quit\n");
            int Option = int.Parse(Console.ReadLine());

            if (Option == 1) {
                Thread ClientThread = new Thread(new ThreadStart(ConnectAsClient));
                ClientThread.Start();
            } else if (Option == 2) {
                Thread ServerThread = new Thread(new ThreadStart(ConnectAsServer));
                ServerThread.Start();
            } else if (Option == 3) {
                return;
            } else {
                Console.WriteLine("Invalid input.");
            }
        }

        public static void ConnectAsServer() {
            TcpListener Listener = new TcpListener(46464);
            Listener.Start();

            TcpClient Client = Listener.AcceptTcpClient();
            Console.WriteLine("Client Conected . . .");

            NetworkStream Stream = Client.GetStream();
            while (true) {

                try {

                    byte[] Received = new byte[1024];
                    Stream.Read(Received, 0, Received.Length);
                    Console.WriteLine(Received);

                    byte[] Sent = Encoding.ASCII.GetBytes("Username(S): "); 
                    Console.WriteLine(Sent.ToString());
                    Stream.Write(Sent, 0, Sent.Length);

                } catch (Exception e) {
                    Console.WriteLine("Critical error: " + e + "\nClosing Server");
                }


            }

        }

        public static void ConnectAsClient() {
            //Client User = new Client(46464
            Console.WriteLine("Enter Server IP: ");
            TcpClient User = new TcpClient(Console.ReadLine(), 46464);
            Console.WriteLine("Connection successful");

            NetworkStream Stream = User.GetStream();
            byte[] Message = new byte[1024];

            while (true) {

                byte[] Received = new byte[1024];
                Stream.Read(Received, 0, Received.Length);
                Console.WriteLine(Received);

                byte[] Sent = Encoding.ASCII.GetBytes(Console.ReadLine());
                Console.WriteLine(Sent.ToString());
                Stream.Write(Sent, 0, Sent.Length);

            }

        }
        
        

    }

    class Client {
        bool Run = true;
        byte[] Recieved = new byte[1024];
        

        public Client (int Port) {
            Console.WriteLine("Enter Server IP: ");
            String ServerIP = Console.ReadLine();
            TcpClient User = new TcpClient(ServerIP, Port);
            NetworkStream Stream = User.GetStream();

            while (Run) {
                Recieved = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine());
                Stream.Write(Recieved, 0, Recieved.Length);
            }

        }

        public void SendMessage (String Message) {

        }

    }

}
