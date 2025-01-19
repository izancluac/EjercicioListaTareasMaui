using listaTareas.Models;
using listaTareas.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace listaTareas.ViewModels
{
    [QueryProperty(nameof(SerializedItems), "pItems")]
    public class AddItemNewWindowViewModel : INotifyPropertyChanged

    {
        public ObservableCollection<TodoItem> Items { get; set; }
        private String _serializedItems;

        public String SerializedItems
        {



            get { return _serializedItems; }
            set
            {
                if (_serializedItems != value)
                {
                    _serializedItems = value;

                    OnPropertyChanged();
                    //Convierto el json en lo que era , un Observable collection
                    //Items = JsonSerializer.Deserialize<ObservableCollection<TodoItem>>(_serializedItems);
                }

            }
        }

        private string _EntryTasca;

        public string MyEntryTasca
        {
            get => _EntryTasca;
            set
            {
                _EntryTasca = value;
                OnPropertyChanged();
            }
        }

        private Boolean _completada;
        public Boolean Completada
        {
            get => _completada;
            set
            {
                _completada = value;
                OnPropertyChanged();
            }
        }



        public ICommand SalirCommand { get; }
        public ICommand AnyadoTask { get; set; }

        public AddItemNewWindowViewModel()
        {
            AnyadoTask = new Command(AnyadirTask);

            SalirCommand = new Command(async () => await Salir());

        }

        private async Task Salir()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        private void AnyadirTask()
        {
            
            TodoItem nuevo= new TodoItem { Title = MyEntryTasca , IsCompleted = Completada };
            Items.Add(nuevo);

            MyEntryTasca = "";
            
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
