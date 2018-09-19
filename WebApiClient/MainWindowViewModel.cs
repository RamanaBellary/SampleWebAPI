using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebApiClient
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        public string Title { get; set; }

        private string status;
        public string Status { get { return status; }
            set { status = value; RaisePropertyChanged("Status"); } }

        public ICommand InvokeSvcCmd { get; set; }

        public MainWindowViewModel()
        {
            InvokeSvcCmd = new RelayCommand(null, InvokeSvc);
            Status = "Ready..";
        }

        private void M1(string s)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InvokeSvc(object param)
        {
            GetDetails();
            MessageBox.Show("After invoking SVC");
        }

        private async void GetDetails()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:59540/api/customer/1");
            if (response.IsSuccessStatusCode)
            {
                var cus = await response.Content.ReadAsAsync<Customer>();
                Status = "Fetched customer details..";
                MessageBox.Show($"Response from svc :{Status}");
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MyCmd : ICommand
    {
        private Func<object,bool> _canExec;
        private Action<object> _exec;
        public MyCmd(Func<object, bool> canExec, Action<object> exec)
        {
            _canExec = canExec;
            _exec = exec;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
           return _canExec != null ? _canExec(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _exec(parameter);
        }
    }
}
