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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GestionareHotel.ViewModels
{
    class BookRoomViewModel: BaseViewModel
    {
        private BookRoomActions _operations;
        public BookRoomViewModel()
        {
            _operations = new BookRoomActions(this);
        }


        #region Properties
        //=======================
        private DataTable _roomsDataTable;
        public DataTable RoomsDataTable
        {
            get { return _roomsDataTable; }
            set
            {
                _roomsDataTable = value;
                OnPropertyChanged("RoomsDataTable");
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
        //=======================
        #endregion


        #region Commands
        //================
        private ICommand _loadRooms;
        public ICommand LoadRoomsCommand
        {
            get
            {
                if (_loadRooms == null)
                {
                    _loadRooms = new RelayCommand(_operations.LoadRooms);
                }
                return _loadRooms;
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
        //================
        #endregion

    }
}
