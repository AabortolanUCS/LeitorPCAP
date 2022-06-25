using LeitorPCAP.Tela.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LeitorPCAP.Tela
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class Tela : Application
    {
        private void Rodar(object sender, StartupEventArgs e)
        {
            var leitor = new LeitorPCAP();

            var pacotesLidos = leitor.PegarPacotes(@"C:\Users\Leonardo Martelli\Downloads\captura_ssh.pcap");

            var viewModel = new ViewModel.LeitorPCAPViewModel(pacotesLidos.Select(p => new Pacote(p.pacoteIP, p.pacoteTCP)));

            var wnd = new LeitorPCAPView(viewModel);

            wnd.Show();
        }
    }
}
