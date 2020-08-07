using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class PrivilegesViewModel: BaseViewModel
    {
        ObservableCollection<UsersClass> users;
        GestionareHotelEntities objContext;

        public PrivilegesViewModel()
        {
            users = new ObservableCollection<UsersClass>();
            objContext = new GestionareHotelEntities();
            
        }

        #region Properties

        public ObservableCollection<UsersClass> UsersList
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged("UsersList");
            }
        }


        #endregion


        #region Commands

        public void LoadUsers(object param)
        {
            users.Clear();

            try
            {
                var ObjQuery = from obj in objContext.Users
                               select obj;
                foreach (var User in ObjQuery)
                {
                    users.Add(new UsersClass { UserName = User.UserName, EmailAdress = User.EmailAdress, Angajat = User.Angajat, Admin = User.Admin });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            for (int i = 0; i < users.Count(); ++i)
            { 
                Debug.WriteLine(users[i].UserName);
                Debug.WriteLine(users[i].EmailAdress);
                
            }
        }

        private ICommand _loadUsers;
        public ICommand LoadUsersCommand
        {
            get
            {
                if (_loadUsers == null)
                    _loadUsers = new RelayCommand(LoadUsers);
                return _loadUsers;
            }
        }
        #endregion
    }
}
