using GestionareHotel.Commands;
using GestionareHotel.Models.Actions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class AngajatViewModel: BaseViewModel
    {
        private AngajatActions _operations;
        public AngajatViewModel()
        {
            _operations = new AngajatActions(this);
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


        private ICommand _updateRezervations;
        public ICommand UpdateRezervationsCommand
        {
            get
            {
                if (_updateRezervations == null)
                {
                    _updateRezervations = new RelayCommand(_operations.UpdateRezervations);
                }
                return _updateRezervations;
            }
        }
        //==============
        #endregion
    }
}
