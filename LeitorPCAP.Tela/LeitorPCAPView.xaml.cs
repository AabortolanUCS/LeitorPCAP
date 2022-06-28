using LeitorPCAP.Tela.Dominio;
using LeitorPCAP.Tela.ViewModel;
using System.Linq;
using System.Windows;

namespace LeitorPCAP.Tela
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LeitorPCAPView: Window
    {
        public LeitorPCAPView(LeitorPCAPViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewModel = (LeitorPCAPViewModel)DataContext;

            viewModel.PacoteSelecionado = (Pacote)e.AddedItems[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (LeitorPCAPViewModel)DataContext;

            var leitor = new LeitorPCAP();
            var pacotesLidos = leitor.PegarPacotes(viewModel.ArquivoSelecionado);

            viewModel.Pacotes = pacotesLidos.Select(p => new Pacote(p.pacoteIP, p.pacoteTCP));
        }
    }
}
