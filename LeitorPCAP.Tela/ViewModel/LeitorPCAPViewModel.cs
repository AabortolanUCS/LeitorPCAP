using LeitorPCAP.Tela.Dominio;
using PacketDotNet;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LeitorPCAP.Tela.ViewModel
{
    public class LeitorPCAPViewModel: INotifyPropertyChanged
    {
        private Pacote pacoteSelecionado;
        private string arquivoSelecionado;
        private IEnumerable<Pacote> pacotes;

        public IEnumerable<Pacote> Pacotes
        {
            get => pacotes;
            set
            {
                pacotes = value;
                this.OnPropertyChanged(nameof(Pacotes));
                PacoteSelecionado = pacotes.FirstOrDefault();
            }
        }

        public Pacote PacoteSelecionado
        {
            get => pacoteSelecionado;
            set
            {
                pacoteSelecionado = value;
                this.OnPropertyChanged(nameof(PacoteSelecionado));
            }
        }

        public string ArquivoSelecionado
        {
            get => arquivoSelecionado;
            set
            {
                arquivoSelecionado = value;
                this.OnPropertyChanged(nameof(ArquivoSelecionado));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
