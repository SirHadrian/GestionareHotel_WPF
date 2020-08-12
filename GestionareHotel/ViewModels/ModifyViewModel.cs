using GestionareHotel.Commands;
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
                return;

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


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
            DeleteOfferID = null;
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
                return;

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

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
            DeleteRoomID = null;
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
            string querry = "SELECT Id_room, Denumire, Descriere, NumarCamere, NumarPersoane, Pret FROM Rooms;";

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
            string querry = "SELECT Id_Offer, Offer, Price, StartDate, EndDate FROM Offers;";

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
        //==================
        #endregion
    }
}
