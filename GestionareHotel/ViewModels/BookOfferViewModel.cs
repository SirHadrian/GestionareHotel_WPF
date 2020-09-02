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
    class BookOfferViewModel: BaseViewModel
    {
        private BookOfferActions _operations;
        public BookOfferViewModel()
        {
            _operations = new BookOfferActions(this);
        }


        #region Properties
        //====================
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
        //====================
        #endregion


        #region Commands
        //=================
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
        //=================
        #endregion
    }
}
