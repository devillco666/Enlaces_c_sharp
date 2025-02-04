using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace Enlaces_C_sharp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string nom;
        private string tel;

        public string Nombre
        {
            get { return nom; }
            set { nom = value; OnPropertyChanged(nameof(Nombre)); OnPropertyChanged(nameof(Salvar)); }
        }

        public string NumTel
        {
            get { return tel; }
            set { tel = value; OnPropertyChanged(nameof(NumTel)); OnPropertyChanged(nameof(Salvar)); }
        }

        public bool Salvar => !string.IsNullOrWhiteSpace(Nombre) && !string.IsNullOrWhiteSpace(NumTel);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveToXml(object sender, RoutedEventArgs e)
        {
            string filePath = "contacts.xml";
            XElement xmlData;

            if (File.Exists(filePath))
            {
                xmlData = XElement.Load(filePath);
            }
            else
            {
                xmlData = new XElement("Contactos");
            }

            XElement newContact = new XElement("Contacto",
                new XElement("Nombre", nom),
                new XElement("Telefono", tel)
            );

            xmlData.Add(newContact);
            xmlData.Save(filePath);

            MessageBox.Show("Datos guardados en XML", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            Nombre = string.Empty;
            NumTel = string.Empty;
        }
    }
}
