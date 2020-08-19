using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GestionareHotel.ViewModels
{
    class RoomsViewModel: BaseViewModel
    {
        #region Properties
        //=============
        private string _denumire = null;
        public string Denumire
        {
            get
            {
                return _denumire;
            }
            set
            {
                _denumire = value;
                OnPropertyChanged("Denumire");
            }
        }

        private string _descriere = null;
        public string Descriere
        {
            get
            {
                return _descriere;
            }
            set
            {
                _descriere = value;
                OnPropertyChanged("Descriere");
            }
        }

        private string _nrCamere = null;
        public string NrCamere
        {
            get
            {
                return _nrCamere;
            }
            set
            {
                _nrCamere = value;
                OnPropertyChanged("NrCamere");
            }
        }

        private string _nrPersoane = null;
        public string NrPersoane
        {
            get
            {
                return _nrPersoane;
            }
            set
            {
                _nrPersoane = value;
                OnPropertyChanged("NrPersoane");
            }
        }

        private string _imagePath = null;
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        private string _price = null;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private ImageSource _image = null;
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        //=============
        #endregion


        #region Commands
        //==============
        public void Add(object param)
        {
            if (Descriere == null || NrCamere == null || NrPersoane == null || Price == null) 
            {
                System.Windows.MessageBox.Show("All fields must be completed", "Rooms" , MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddRoom", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@denumire", Denumire);
                        cmd.Parameters.AddWithValue("@descriere", Descriere);
                        cmd.Parameters.AddWithValue("@numarcamere", NrCamere);
                        cmd.Parameters.AddWithValue("@numarpersoane", NrPersoane);
                        cmd.Parameters.AddWithValue("@imagine", (object)Tools.ReadImage(ImagePath));
                        cmd.Parameters.AddWithValue("@pret", Price);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Incorect values");
            }
            System.Windows.MessageBox.Show("Room added!", "Rooms", MessageBoxButton.OK, MessageBoxImage.Information);

            Debug.WriteLine("Executat");
        }

        private ICommand _addRoom;
        public ICommand AddRoom
        {
            get
            {
                if (_addRoom == null)
                    _addRoom = new RelayCommand(Add);
                return _addRoom;
            }
        }

        //==============
        public void Browse(object param)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                Debug.WriteLine(sFileName);
                ImagePath = sFileName;

                Image = (BitmapSource)new ImageSourceConverter().ConvertFrom(Tools.ReadImage(sFileName));
            }

        }

        private ICommand _browse;
        public ICommand BrowseCommand
        {
            get
            {
                if (_browse == null)
                    _browse = new RelayCommand(Browse);
                return _browse;
            }
        }
        //==============
        #endregion
    }
}
