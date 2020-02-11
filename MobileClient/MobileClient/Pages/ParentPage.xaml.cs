using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileClient.Classes;

namespace MobileClient.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParentPage : ContentPage
	{
        public Parent Model { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public ParentPage (Parent model, ApplicationViewModel viewModel)
		{
            InitializeComponent();
            Model = model;
            ViewModel = viewModel;
            this.BindingContext = this;
        }
	}
}