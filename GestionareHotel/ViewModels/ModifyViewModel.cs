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
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class ModifyViewModel: BaseViewModel
    {
        private ModifyActions _operations;
        public ModifyViewModel()
        {
            _operations = new ModifyActions(this);
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

        private DataTable _offersDataTable;
        public DataTable OffersDataTable
        {
            get { return _offersDataTable; }
            set
            {
                _offersDataTable = value;
                OnPropertyChanged("OffersDataTable");
            }
        }

        private string _deleteOfferID = null;
        public string DeleteOfferID
        {
            get
            {
                return _deleteOfferID;
            }
            set
            {
                _deleteOfferID = value;
                OnPropertyChanged("DeleteOfferID");
            }
        }

        private string _deleteRoomID = null;
        public string DeleteRoomID
        {
            get
            {
                return _deleteRoomID;
            }
            set
            {
                _deleteRoomID = value;
                OnPropertyChanged("DeleteRoomID");
            }
        }
        //=======================
        #endregion


        #region Commands
        //==================
        private ICommand _deleteOffer;
        public ICommand DeleteOfferCommand
        {
            get
            {
                if (_deleteOffer == null)
                {
                    _deleteOffer = new RelayCommand(_operations.DeleteOffer);
                }
                return _deleteOffer;
            }
        }


        private ICommand _deleteRoom;
        public ICommand DeleteRoomCommand
        {
            get
            {
                if (_deleteRoom == null)
                {
                    _deleteRoom = new RelayCommand(_operations.DeleteRoom);
                }
                return _deleteRoom;
            }
        }


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


        private ICommand _loadOffers;
        public ICommand LoadOffersCommand
        {
            get
            {
                if (_loadOffers == null)
                {
                    _loadOffers = new RelayCommand(_operations.LoadOffers);
                }
                return _loadOffers;
            }
        }


        private ICommand _updateOffers;
        public ICommand UpdateOffersCommand
        {
            get
            {
                if (_updateOffers == null)
                {
                    _updateOffers = new RelayCommand(_operations.UpdateOffers);
                }
                return _updateOffers;
            }
        }


        private ICommand _updateRooms;
        public ICommand UpdateRoomsCommand
        {
            get
            {
                if (_updateRooms == null)
                {
                    _updateRooms = new RelayCommand(_operations.UpdateRooms);
                }
                return _updateRooms;
            }
        }
        //==================
        #endregion
    }
}
