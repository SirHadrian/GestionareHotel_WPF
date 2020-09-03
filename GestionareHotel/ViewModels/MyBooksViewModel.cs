using GestionareHotel.Commands;
using GestionareHotel.Models;
using GestionareHotel.Models.Actions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class MyBooksViewModel: BaseViewModel
    {
        private MyBooksActions _operations;
        public MyBooksViewModel()
        {
            _operations = new MyBooksActions(this);
        }

        

        #region Proprieties
        //==================
        private List<string> _toPay = new List<string>();
        public List<string> ToPay
        {
            get
            {
                return _toPay;
            }
            set
            {
                _toPay = value;
                OnPropertyChanged("ToPay");
            }
        }

        private string _money;
        public string Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                OnPropertyChanged("Money");
            }
        }


        private DataTable _roomsRezervationsDataTable = null;
        public DataTable RoomsRezervationsDataTable
        {
            get { return _roomsRezervationsDataTable; }
            set
            {
                _roomsRezervationsDataTable = value;
                OnPropertyChanged("RoomsRezervationsDataTable");
            }
        }

        private DataTable _offersRezervationsDataTable = null;
        public DataTable OffersRezervationsDataTable
        {
            get { return _offersRezervationsDataTable; }
            set
            {
                _offersRezervationsDataTable = value;
                OnPropertyChanged("OffersRezervationsDataTable");
            }
        }

        private DataTable _servicesRezervationsDataTable = null;
        public DataTable ServicesRezervationsDataTable
        {
            get { return _servicesRezervationsDataTable; }
            set
            {
                _servicesRezervationsDataTable = value;
                OnPropertyChanged("ServicesRezervationsDataTable");
            }
        }
        //==================
        #endregion


        #region Commands
        //================
        private ICommand _loadRezervations;
        public ICommand LoadRezervationsCommand
        {
            get
            {
                if (_loadRezervations == null)
                {
                    _loadRezervations = new RelayCommand(_operations.LoadRezervations);
                }
                return _loadRezervations;
            }
        }
        //================
        #endregion
    }
}
