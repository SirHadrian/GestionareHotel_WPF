using GestionareHotel.Commands;
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

namespace GestionareHotel.ViewModels
{
    class ServicesViewModel: BaseViewModel
    {
        private ServicesActions _operations;
        public ServicesViewModel()
        {
            _operations = new ServicesActions(this);
        }

        #region Properties
        //======================
        private DataTable _servicesDataTable;
        public DataTable ServicesDataTable
        {
            get { return _servicesDataTable; }
            set
            {
                _servicesDataTable = value;
                OnPropertyChanged("ServicesDataTable");
            }
        }

        private string _service = null;
        public string Service
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
                OnPropertyChanged("Service");
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

        private string _deleteServiceID = null;
        public string DeleteServiceID
        {
            get
            {
                return _deleteServiceID;
            }
            set
            {
                _deleteServiceID = value;
                OnPropertyChanged("DeleteServiceID");
            }
        }
        //=============
        #endregion


        #region Commands
        //=================
        private ICommand _loadServices;
        public ICommand LoadServicesCommand
        {
            get
            {
                if (_loadServices == null)
                {
                    _loadServices = new RelayCommand(_operations.LoadService);
                }
                return _loadServices;
            }
        }


        private ICommand _updateServices;
        public ICommand UpdateServicesCommand
        {
            get
            {
                if (_updateServices == null)
                {
                    _updateServices = new RelayCommand(_operations.UpdateServices);
                }
                return _updateServices;
            }
        }


        private ICommand _deleteService;
        public ICommand DeleteServiceCommand
        {
            get
            {
                if (_deleteService == null)
                {
                    _deleteService = new RelayCommand(_operations.DeleteService);
                }
                return _deleteService;
            }
        }
                

        private ICommand _addService;
        public ICommand AddServiceCommand
        {
            get
            {
                if (_addService == null)
                {
                    _addService = new RelayCommand(_operations.AddService);
                }
                return _addService;
            }
        }
        //=================
        #endregion
    }
}
