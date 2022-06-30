using PacketDotNet;
using System;
using System.Net;
using System.Text;

namespace LeitorPCAP.Tela.Dominio
{
    public record Pacote
    {
        private readonly string[] flagNames = new string[]
                {
                "Fin",
                "Syn",
                "Reset",
                "Push",
                "Acknowledgemnt",
                "Urgent",
                "Echo",
                "Congestion",
                "Nonce"
                };

        public Pacote(IPPacket pacotePai, TransportPacket transportPacket, TimeSpan timeStamp, int index)
        {
            Index = index;
            TimeStamp = timeStamp;

            PortaOrigem = transportPacket.SourcePort;
            PortaDestino = transportPacket.DestinationPort;

            EnderecoOrigem = pacotePai.SourceAddress;
            EnderecoDestino = pacotePai.DestinationAddress;

            Protocolo = pacotePai.Protocol;

            Versao = pacotePai.Version;

            TamanhoCabecalho = pacotePai.HeaderLength;

            if(transportPacket is TcpPacket pacoteTCP)
            {
                Flags = PegarFlags(pacoteTCP.Flags);

                ACK = pacoteTCP.AcknowledgmentNumber;

                SEQ = pacoteTCP.SequenceNumber;

                Janela = pacoteTCP.WindowSize;
            }

            Tamanho = transportPacket.TotalPacketLength;

            Checksum = transportPacket.Checksum;

            source = transportPacket;
        }

        private TransportPacket source;

        public int Index { get; }
        public TimeSpan TimeStamp { get; }
        public ushort PortaOrigem { get; }
        public ushort PortaDestino { get; }
        public ushort Janela { get; }
        public ushort Checksum { get; }
        public IPAddress EnderecoOrigem { get; }
        public IPAddress EnderecoDestino { get; }

        public ProtocolType Protocolo { get; }
        public IPVersion Versao { get; }
        public int TamanhoCabecalho { get; }
        public int Tamanho { get; }

        public string Flags { get; }
        public uint ACK { get; }
        public uint SEQ { get; }

        string PegarFlags(ushort flags)
        {
            var str = new StringBuilder();

            for(int i = 0; i < flagNames.Length; i++)
            {
                var mask = (ushort)Math.Pow(2, i);

                if((mask & flags) == mask)
                    str.Append($"{flagNames[i]}, ");
            }

            return str.ToString()[0..^2];
        }
    }
}