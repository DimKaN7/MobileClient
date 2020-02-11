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
	public partial class PersonPage : ContentPage
	{
        public Person Model { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public PersonPage(Person model, ApplicationViewModel viewModel)
        {
            InitializeComponent();
            Model = model;
            ViewModel = viewModel;
            this.BindingContext = this;
        }

        private void OnButtonSaveClicked(object sender, EventArgs e)
        {
            ViewModel.SavePerson(Model);
        }
    }
}