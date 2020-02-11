using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using MobileClient.Classes;
using System;


using MobileClient.Classes;
using MobileClient.Pages;
using Newtonsoft.Json;

namespace MobileClient
{
    public class ApplicationViewModel: INotifyPropertyChanged
    {
        private bool isBusy;

        public ObservableCollection<Person> Persons { get; set; }
        public PersonsService personsService = new PersonsService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SavePersonCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }

        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel() 
        {
            Persons = new ObservableCollection<Person>();
            IsBusy = false;
            SavePersonCommand = new Command(SavePerson);
            BackCommand = new Command(Back);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public async void CreatePerson(int indexOfPage)
        {
            if (indexOfPage == 0)
            {
                await Navigation.PushAsync(new PersonPage(new Person(), this));
            } 
            else if (indexOfPage == 1)
            {
                await Navigation.PushAsync(new MarriedPersonPage(new MarriedPerson(), this));
            }
            else
            {
                await Navigation.PushAsync(new ParentPage(new Parent(), this));
            }
        }

        public async void EditPerson(int selectedIndex)
        {
            Person p = Persons.ElementAt(selectedIndex);
            if (p is Parent)
            {
                await Navigation.PushAsync(new ParentPage((Parent)p, this));
            }
            else if (p is MarriedPerson)
            {
                await Navigation.PushAsync(new MarriedPersonPage((MarriedPerson)p, this));
            }
            else
            {
                await Navigation.PushAsync(new PersonPage(p, this));
            }
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task GetPersons()
        {
            IsBusy = true;
            List<string> personsJsons = await personsService.Get();
            while (Persons.Any())
            {
                Persons.RemoveAt(Persons.Count - 1);
            }
            for (int i = 0; i < personsJsons.Count; i++)
            {
                if (personsJsons[i].IndexOf("PartnerName") != -1)
                {
                    MarriedPerson mPerson = JsonConvert.DeserializeObject<MarriedPerson>(personsJsons[i]);
                    Persons.Add(mPerson);
                }
                else if (personsJsons[i].IndexOf("ChildCount") != -1)
                {
                    Parent pPerson = JsonConvert.DeserializeObject<Parent>(personsJsons[i]);
                    Persons.Add(pPerson);
                }
                else
                {
                    Person person = JsonConvert.DeserializeObject<Person>(personsJsons[i]);
                    Persons.Add(person);
                }
            }
            IsBusy = false;
        }

        // для выборочной печати
        public async Task GetMarriedPersons()
        {
            IsBusy = true;
            List<string> personsJsons = await personsService.Get();
            while (Persons.Any())
            {
                Persons.RemoveAt(Persons.Count - 1);
            }
            for (int i = 0; i < personsJsons.Count; i++)
            {
                if (personsJsons[i].IndexOf("PartnerName") != -1)
                {
                    MarriedPerson mPerson = JsonConvert.DeserializeObject<MarriedPerson>(personsJsons[i]);
                    Persons.Add(mPerson);
                }
            }
            IsBusy = false;
        }

        public async void SavePerson(object objectPerson)
        {
            if (objectPerson != null)
            {
                Person person = (Person)objectPerson;
                IsBusy = true;
                if (person.Id > 0)
                {
                    int pos = Persons.IndexOf(person);
                    if (person is MarriedPerson)
                    {
                        MarriedPerson updatedPerson = await personsService.UpdateMarriedPerson((MarriedPerson)person);
                        if (updatedPerson != null)
                        {
                            Persons.RemoveAt(pos);
                            Persons.Insert(pos, updatedPerson);
                        }
                    }
                    else if (person is Parent)
                    {
                        Parent updatedPerson = await personsService.UpdateParent((Parent)person);
                        if (updatedPerson != null)
                        {
                            Persons.RemoveAt(pos);
                            Persons.Insert(pos, updatedPerson);
                        }
                    }
                    else
                    {
                        Person updatedPerson = await personsService.UpdatePerson(person);
                        if (updatedPerson != null)
                        {
                            Persons.RemoveAt(pos);
                            Persons.Insert(pos, updatedPerson);
                        }
                    }
                }
                else
                {
                    if (person is MarriedPerson)
                    {
                        MarriedPerson addedPerson = await personsService.AddMarriedPerson((MarriedPerson)person);
                        if (addedPerson != null)
                            Persons.Add(addedPerson);
                    }
                    else if (person is Parent)
                    {
                        Parent addedPerson = await personsService.AddParent((Parent)person);
                        if (addedPerson != null)
                            Persons.Add(addedPerson);
                    }
                    else
                    {
                        Person addedPerson = await personsService.AddPerson(person);
                        if (addedPerson != null)
                            Persons.Add(addedPerson);
                    }
                }
                IsBusy = false;
            }
            Back();
        }

        public void DeletePerson(int listIndex)
        {
            Person person = Persons.ElementAt(listIndex);
            if (person != null)
            {
                IsBusy = true;
                personsService.Delete(person.Id);
                Persons.Remove(person);
                IsBusy = false;
            }
        }

        public async Task<string> Request1()
        {
            return await personsService.PerformRequest1();
        }

        public async Task<string> Request2()
        {
            return await personsService.PerformRequest2();
        }
    }
}
