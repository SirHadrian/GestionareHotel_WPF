using GestionareHotel.Commands;
using GestionareHotel.Models;
using GestionareHotel.Models.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Linq;
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
    class PrivilegesViewModel: BaseViewModel
    {
        private PrivilegesActions _operations;
        public PrivilegesViewModel()
        {
            _operations = new PrivilegesActions(this);
        }

        #region Properties

        private DataTable _usersDataTable;
        public DataTable UsersDataTable
        {
            get { return _usersDataTable; }
            set
            {
                _usersDataTable = value;
                OnPropertyChanged("UsersDataTable");
            }
        }

        private string _userToBeDeleted = null;
        public string DeleteThis
        {
            get
            {
                return _userToBeDeleted;
            }
            set
            {
                _userToBeDeleted = value;
                OnPropertyChanged("DeleteThis");
            }
        }

        #endregion


        #region Commands
        //====================
        private ICommand _loadUsers;
        public ICommand LoadUsersCommand
        {
            get
            {
                if (_loadUsers == null)
                {
                    _loadUsers = new RelayCommand(_operations.LoadUsers);
                }
                return _loadUsers;
            }
        }


        private ICommand _updateUsers;
        public ICommand UpdateUsersCommand
        {
            get
            {
                if (_updateUsers == null)
                {
                    _updateUsers = new RelayCommand(_operations.UpdateUsers);
                }
                return _updateUsers;
            }
        }


        private ICommand _deleteUsers;
        public ICommand DeleteUsersCommand
        {
            get
            {
                if (_deleteUsers == null)
                {
                    _deleteUsers = new RelayCommand(_operations.DeleteUser);
                }
                return _deleteUsers;
            }
        }
        //====================
        #endregion
    }
}
