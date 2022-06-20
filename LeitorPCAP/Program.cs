using System;
using SharpPcap;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace ReadingCaptureFile
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("-- Insira o caminho do aqruivo: ");
            string caminhoArquivo = Console.ReadLine();
            //C:\Users\Pichau\Downloads\captura_ftp.pcap

            Console.WriteLine("Abrindo arquivo '{0}' ...", caminhoArquivo);

            ICaptureDevice device;

            try
            {
                device = new CaptureFileReaderDevice(caminhoArquivo);
                device.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao abrir o arquivo: " + e.ToString());
                return;
            }

            device.OnPacketArrival +=
                new PacketArrivalEventHandler(recebePacote);

            Console.WriteLine();
            Console.WriteLine
                ("-- Iniciada captura de '{0}', aperte 'Ctrl-C' para sair ...",
                caminhoArquivo);

            var horaDeInicio = DateTime.Now;

            device.Capture();

            device.Close();
            var horaDeTermino = DateTime.Now;
            Console.WriteLine("-- Final do arquivo alcançado.");

            var duracao = horaDeTermino - horaDeInicio;
            Console.WriteLine("Leitura do arquivo durou {0} segundos", duracao.TotalSeconds);

            Console.Write("Aperte 'Enter' para finalizar ...");
            Console.ReadLine();
        }

        public static int numeroPacote = 1;
        public static DateTime tempoPrimeiroPacote;

        private static void recebePacote(object sender, PacketCapture e)
        {
            var tempo = e.Header.Timeval.Date;
            var tamanho = e.Data.Length;
            var rawPacket = e.GetPacket();

            if(tempoPrimeiroPacote == default(DateTime))
            {
                tempoPrimeiroPacote = tempo;
            }

            var pacote = PacketDotNet.Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var pacoteTcp = pacote.Extract<PacketDotNet.TcpPacket>();
            if (pacoteTcp != null)
            {
                var pacoteIp = (PacketDotNet.IPPacket)pacoteTcp.ParentPacket;
                System.Net.IPAddress ipOrigem = pacoteIp.SourceAddress;
                System.Net.IPAddress ipDestino = pacoteIp.DestinationAddress;
                int maquinaOrigem = pacoteTcp.SourcePort;
                int maquinaDestino = pacoteTcp.DestinationPort;

                Console.WriteLine("{0} - {1}, {2}:{3} -> {4}:{5}, Tamnho={6}",
                    numeroPacote++,
                    String.Format("{0:N6}", (tempo - tempoPrimeiroPacote).TotalMilliseconds == 0 ? 0 :(tempo - tempoPrimeiroPacote).TotalMilliseconds / 1000.0), 
                    ipOrigem, maquinaOrigem, ipDestino, maquinaDestino, tamanho);
            }
        }
    }
}
