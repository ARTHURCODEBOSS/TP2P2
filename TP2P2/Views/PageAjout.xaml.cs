using Microsoft.UI.Xaml.Controls;
using TP2P2.ViewsModels;

namespace TP2P2.Views
{
    public sealed partial class PageAjout : Page
    {
        public PageAjout()
        {
            this.InitializeComponent();
            ModelPageAjout viewModel = new ModelPageAjout();
            this.DataContext = viewModel;
        }
    }
}