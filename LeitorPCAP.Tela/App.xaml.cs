using LeitorPCAP.Tela.Dominio;
using System.Data;
using System.Linq;
using System.Windows;

namespace LeitorPCAP.Tela
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class Tela : Application
    {
        ViewModel.LeitorPCAPViewModel viewModel;
        private void Rodar(object sender, StartupEventArgs e)
        {
            viewModel = new ViewModel.LeitorPCAPViewModel();

            var wnd = new LeitorPCAPView(viewModel);

            wnd.Show();
        }
    }
}
