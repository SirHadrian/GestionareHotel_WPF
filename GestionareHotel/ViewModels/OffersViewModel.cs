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
    class OffersViewModel: BaseViewModel
    {
        private OffersActions _operations;

        public OffersViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            _operations = new OffersActions(this);
        }


        #region Properties
        //=================
        private string _imagePath;
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

        private string _offer = null;
        public string Offer
        {
            get
            {
                return _offer;
            }
            set
            {
                _offer = value;
                OnPropertyChanged("Offer");
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        //=================
        #endregion


        #region Commands
        //=================
        private ICommand _addOffer;
        public ICommand AddOfferCommand
        {
            get
            {
                if (_addOffer == null)
                {
                    _addOffer = new RelayCommand(_operations.AddOffer);
                }
                return _addOffer;
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
        //=================
        #endregion
    }
}
