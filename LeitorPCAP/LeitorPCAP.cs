using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace LeitorPCAP
{
    public class LeitorPCAP
    {
        private IList<(int index, IPPacket pacoteIP, TransportPacket pacoteTCP, TimeSpan tempo)> _pacotes = new List<(int, IPPacket, TransportPacket, TimeSpan)>();
        public IEnumerable<(int index, IPPacket pacoteIP, TransportPacket pacoteTCP, TimeSpan tempo)> PegarPacotes(string caminhoArquivo)
        {
            ICaptureDevice device;

            try
            {
                device = new CaptureFileReaderDevice(caminhoArquivo);
                device.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Erro ao abrir o arquivo: " + e.ToString());
                return null;
            }

            device.OnPacketArrival +=
                new PacketArrivalEventHandler(RecebePacote);

            device.Capture();

            device.Close();

            return _pacotes;
        }

        public static int numeroPacote = 1;
        public static DateTime tempoPrimeiroPacote;

        private void RecebePacote(object sender, PacketCapture e)
        {
            var tempo = e.Header.Timeval.Date;
            var rawPacket = e.GetPacket();

            if(tempoPrimeiroPacote == default(DateTime))
            {
                tempoPrimeiroPacote = tempo;
            }

            var pacote = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var pacoteTcp = pacote.Extract<TransportPacket>();

            if(pacoteTcp is not null)
                _pacotes.Add((numeroPacote++, (IPPacket)pacoteTcp.ParentPacket, pacoteTcp, tempo - tempoPrimeiroPacote));
        }
    }
}
