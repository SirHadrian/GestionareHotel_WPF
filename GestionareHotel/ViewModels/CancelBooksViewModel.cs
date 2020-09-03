using GestionareHotel.Commands;
using GestionareHotel.Models.Actions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class CancelBooksViewModel: BaseViewModel
    {
        private CancelBooksActions _operations;
        public CancelBooksViewModel()
        {
            _operations = new CancelBooksActions(this);
        }


        #region Properties
        //===================
        private DataTable _rezervationsDataTable;
        public DataTable RezervationsDataTable
        {
            get
            {
                return _rezervationsDataTable;
            }
            set
            {
                _rezervationsDataTable = value;
                OnPropertyChanged("RezervationsDataTable");
            }
        }

        private string _id = null;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        //=====================
        #endregion


        #region Commands
        //==============
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


        private ICommand _cancelRezervation;
        public ICommand CancelRezervationCommand
        {
            get
            {
                if (_cancelRezervation == null)
                {
                    _cancelRezervation = new RelayCommand(_operations.CancelRezervation);
                }
                return _cancelRezervation;
            }
        }
        //==============
        #endregion
    }
}
