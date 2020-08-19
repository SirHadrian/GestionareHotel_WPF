using GestionareHotel.Commands;
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
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;


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
        public void DeleteOffer(object param)
        {
            if (DeleteOfferID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteOffer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", DeleteOfferID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }catch
            {
                System.Windows.MessageBox.Show("Cannot delete, required for Rezervations");
            }
            DeleteOfferID = null;
            System.Windows.MessageBox.Show("Offer Deleted!", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ICommand _deleteOffer;
        public ICommand DeleteOfferCommand
        {
            get
            {
                if (_deleteOffer == null)
                    _deleteOffer = new RelayCommand(DeleteOffer);
                return _deleteOffer;
            }
        }


        public void DeleteRoom(object param)
        {
            if (DeleteRoomID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Modify", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteRoom", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", DeleteRoomID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot delete, required for Rezervations");
            }
            DeleteRoomID = null;
            System.Windows.MessageBox.Show("Room Deleted!", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ICommand _deleteRoom;
        public ICommand DeleteRoomCommand
        {
            get
            {
                if (_deleteRoom == null)
                    _deleteRoom = new RelayCommand(DeleteRoom);
                return _deleteRoom;
            }
        }


        public void LoadRooms(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Imagine AS Image, Denumire, Descriere, NumarCamere, NumarPersoane, Pret FROM Rooms;";

            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);

            RoomsDataTable = dt;

            Debug.WriteLine("Executat");
        }

        private ICommand _loadRooms;
        public ICommand LoadRoomsCommand
        {
            get
            {
                if (_loadRooms == null)
                    _loadRooms = new RelayCommand(LoadRooms);
                return _loadRooms;
            }
        }


        public void LoadOffers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, OfferImage AS Image, Offer_Description, Price, StartDate, EndDate FROM Offers;";

            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);

            OffersDataTable = dt;

            Debug.WriteLine("Executat");
        }

        private ICommand _loadOffers;
        public ICommand LoadOffersCommand
        {
            get
            {
                if (_loadOffers == null)
                    _loadOffers = new RelayCommand(LoadOffers);
                return _loadOffers;
            }
        }


        public void UpdateOffers(object param)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(OffersDataTable);
        }

        private ICommand _updateOffers;
        public ICommand UpdateOffersCommand
        {
            get
            {
                if (_updateOffers == null)
                    _updateOffers = new RelayCommand(UpdateOffers);
                return _updateOffers;
            }
        }


        public void UpdateRooms(object param)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(RoomsDataTable);
        }

        private ICommand _updateRooms;
        public ICommand UpdateRoomsCommand
        {
            get
            {
                if (_updateRooms == null)
                    _updateRooms = new RelayCommand(UpdateRooms);
                return _updateRooms;
            }
        }
        //==================
        #endregion
    }
}
