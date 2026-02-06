using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TP2P2.ViewsModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TP2P2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageSeries : Page
    {
        public PageSeries()
        {
            this.InitializeComponent();

            var viewModel = new TP2P2.ViewsModels.ModelPageSerie();

            viewModel.ActionOuvrirDialog = async () =>
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Title = "Ajouter";
                dialog.CloseButtonText = "Fermer";
                dialog.Content = new PageAjout();

                await dialog.ShowAsync();
                viewModel.GetDataOnLoadAsync();
            };

            viewModel.ActionOuvrirRechModDialog = async () =>
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Title = "ModDelRech";
                dialog.CloseButtonText = "Fermer";
                dialog.Content = new PageModDelRech();

                await dialog.ShowAsync();
                viewModel.GetDataOnLoadAsync();
            };

            this.DataContext = viewModel;
        }
    }
}
