using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileClient.Classes;

namespace MobileClient
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MarriedPersonPage : ContentPage
	{
        public MarriedPerson Model { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public MarriedPersonPage(MarriedPerson model, ApplicationViewModel viewModel)
		{
            InitializeComponent();
            Model = model;
            ViewModel = viewModel;
            this.BindingContext = this;
        }
	}
}