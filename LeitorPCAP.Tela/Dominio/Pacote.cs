using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeitorPCAP.Tela.Dominio
{
    public record Pacote
    {
        public Pacote(IPPacket pacotePai, TcpPacket pacoteTCP)
        {
            PortaOrigem = pacoteTCP.SourcePort;
            PortaDestino = pacoteTCP.DestinationPort;

            EnderecoOrigem = pacotePai.SourceAddress;
            EnderecoDestino = pacotePai.DestinationAddress;

            Protocolo = pacotePai.Protocol;

            Versao = pacotePai.Version;

            TamanhoCabecalho = pacotePai.HeaderLength;

            Flags = DecimalParaBinario(pacoteTCP.Flags);

            ACK = pacoteTCP.AcknowledgmentNumber;

            SEQ = pacoteTCP.SequenceNumber;
        }

        public ushort PortaOrigem { get;  }
        public ushort PortaDestino { get; }
        public IPAddress EnderecoOrigem { get; }
        public IPAddress EnderecoDestino { get; }

        public ProtocolType Protocolo { get;  }
        public IPVersion Versao { get; }
        public int TamanhoCabecalho { get; }

        public string Flags { get;  }
        public uint ACK { get; }
        public uint SEQ { get;  }

        string DecimalParaBinario(ushort n)
        {
            ushort resto;
            var result = "";
            while(n > 0)
            {
                resto = (ushort)(n % 2);
                n /= 2;
                result = resto.ToString() + result;
            }
            return result.ToString();
        }

    }
}
