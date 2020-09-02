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
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class BookServiceViewModel: BaseViewModel
    {
        private BookServiceActions _operations;
        public BookServiceViewModel()
        {
            _operations = new BookServiceActions(this);
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

        private string _bookID;
        public string BookID
        {
            get
            {
                return _bookID;
            }
            set
            {
                _bookID = value;
                OnPropertyChanged("BookID");
            }
        }
        //======================
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


        private ICommand _bookRoom;
        public ICommand BookCommand
        {
            get
            {
                if (_bookRoom == null)
                {
                    _bookRoom = new RelayCommand(_operations.Book);
                }
                return _bookRoom;
            }
        }
        //=======================
        #endregion
    }
}
