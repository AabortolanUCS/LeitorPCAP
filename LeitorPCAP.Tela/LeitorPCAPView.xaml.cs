using LeitorPCAP.Tela.Dominio;
using LeitorPCAP.Tela.ViewModel;
using System.Windows;

namespace LeitorPCAP.Tela
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LeitorPCAPView : Window
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
    }
}
