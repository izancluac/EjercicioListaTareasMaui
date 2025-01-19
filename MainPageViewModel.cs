using listaTareas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace listaTareas.ViewModels
{
    public class MainPageViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> Items { get; set; }
        
       

        public ICommand DeleteItemCommand { get; set; }
        public ICommand NavigateCommand { get; }




        
        public MainPageViewModel()
        {
            //dotnet add package Newtonsoft.Json dentro del bash del proyecto , para poder realizar esta conversio de json
            NavigateCommand = new Command(async () => await OnNavigate());

            Items = new ObservableCollection<TodoItem>();
            Items.Add(new TodoItem { Title = "Aprender .NET MAUI", IsCompleted = false });
            Items.Add(new TodoItem { Title = "Diseñar una aplicación con .NET MAUI", IsCompleted = true });

            DeleteItemCommand = new Command<TodoItem>(Borrar);
            
        }

        private void Borrar(TodoItem item)
        {
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        private async Task OnNavigate()
        {
            //Lo tengo que pasar a json porque no puedo pasar observable collection de un view model a otro
            //Es una manera de convertir la lista en una string y podre pasarlo
            string serializedItems = JsonConvert.SerializeObject(Items);
            await Shell.Current.GoToAsync("///AddItemNewWindow?pItems={Uri.EscapeDataString(serializedItems)}");
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

