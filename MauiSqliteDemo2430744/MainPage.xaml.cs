//using Android.App.Job;

namespace MauiSqliteDemo2430744
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDBservice _dbService;
        private int _editClientesId;

        public MainPage(LocalDBservice dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listView.ItemsSource = await _dbService.GetClientes());
        }


        private async void userButton_Clicked(object sender, EventArgs e)
        {
            if (_editClientesId == 0)
            {
                //Agrega cliente
                await _dbService.Create(new Clientes
                {
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                });
            }
            else
            {
                //Edita cliente
                await _dbService.Update(new Clientes
                {
                    Id = _editClientesId,
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                });
                _editClientesId = 0;
            }

            nombreEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            movilEntryField.Text = string.Empty;

            listView.ItemsSource = await _dbService.GetClientes();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Clientes)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editClientesId = clientes.Id;
                    nombreEntryField.Text = clientes.NombreCliente;
                    emailEntryField.Text = clientes.Email;
                    movilEntryField.Text = clientes.Movil;
                    break;

                case "Delete":
                    await _dbService.Delete(clientes);
                    listView.ItemsSource = await _dbService.GetClientes();
                    break;
            }
        }
    }
}
