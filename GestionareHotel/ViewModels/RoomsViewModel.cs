using GestionareHotel.Commands;
using GestionareHotel.Models;
using GestionareHotel.Models.Actions;
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
        private RoomsActions _operations;
        public RoomsViewModel()
        {
            _operations = new RoomsActions(this);
        }


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
        private ICommand _addRoom;
        public ICommand AddRoom
        {
            get
            {
                if (_addRoom == null)
                {
                    _addRoom = new RelayCommand(_operations.Add);
                }
                return _addRoom;
            }
        }

       
        private ICommand _browse;
        public ICommand BrowseCommand
        {
            get
            {
                if (_browse == null)
                {
                    _browse = new RelayCommand(_operations.Browse);
                }
                return _browse;
            }
        }
        //==============
        #endregion
    }
}
