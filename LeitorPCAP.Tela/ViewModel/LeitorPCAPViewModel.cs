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

        public LeitorPCAPViewModel(IEnumerable<Pacote> pacotes)
        {
            Pacotes = pacotes;
            PacoteSelecionado = Pacotes.FirstOrDefault();
        }

        public IEnumerable<Pacote> Pacotes { get; }

        public Pacote PacoteSelecionado
        {
            get => pacoteSelecionado;
            set
            {
                pacoteSelecionado = value;
                this.OnPropertyChanged(nameof(pacoteSelecionado));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
