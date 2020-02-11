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
	public partial class PersonListPage : ContentPage
	{
        ApplicationViewModel ViewModel;
		public PersonListPage ()
		{
			InitializeComponent ();
            ViewModel = new ApplicationViewModel() { Navigation = this.Navigation };
            BindingContext = ViewModel;
		}

        protected override async void OnAppearing()
        {
            await ViewModel.GetPersons();
            base.OnAppearing();
        }

        private void OnButtonAddClicked(object sender, EventArgs args)
        {
            if ((sender as Button).Text.Equals("Добавить человека"))
            {
                ViewModel.CreatePerson(0);
            }
            else if ((sender as Button).Text.Equals("Добавить женатого/замужнего человека"))
            {
                ViewModel.CreatePerson(1);
            }
            else
            {
                ViewModel.CreatePerson(2);
            }
        }

        private void OnButtonEditClicked(object sender, EventArgs e)
        {
            if (!entryNum.Text.Trim().Equals(""))
            {
                int selectedIndex = int.Parse(entryNum.Text) - 1;
                ViewModel.EditPerson(selectedIndex);
                entryNum.Text = "";
            }
        }

        private void OnButtonDeleteClicked(object sender, EventArgs e)
        {
            if (!entryNum.Text.Trim().Equals(""))
            {
                int selectedIndex = int.Parse(entryNum.Text) - 1;
                ViewModel.DeletePerson(selectedIndex);
                entryNum.Text = "";
            }
        }

        private async void OnButtonRequest1Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Запрос 1", "Всего детей - " + requestresult, "OK");
            string requestresult = await ViewModel.Request1();
            DisplayAlert("Запрос 1", "Всего детей - " + requestresult, "OK");
        }

        private async void OnButtonRequest2Clicked(object sender, EventArgs e)
        {
            string requestresult = await ViewModel.Request2();
            DisplayAlert("Запрос 2", "Число тех пар, у которых возраст мужа и жены равен (с точностью до 2 лет)  - " + requestresult, "OK");
        }

        private async void OnButtonCustomPrintingClicked(object sender, EventArgs e)
        {
            if (ButtonCustomPrinting.Text.Equals("Выборочная печать"))
            {
                await ViewModel.GetMarriedPersons();
                ButtonCustomPrinting.Text = "Назад";
            }
            else
            {
                await ViewModel.GetPersons();
                ButtonCustomPrinting.Text = "Выборочная печать";
            }
        }

        private void PersonsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    Person person = (Person)e.SelectedItem;
                    entryNum.Text = (ViewModel.Persons.IndexOf(person) + 1).ToString();
                }
                // снятие выделения
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex) { }
        }
    }
}