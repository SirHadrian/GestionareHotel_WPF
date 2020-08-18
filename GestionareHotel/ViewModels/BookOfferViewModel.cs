using GestionareHotel.Commands;
using GestionareHotel.Models;
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
        SqlDataAdapter sda;
        //SqlCommandBuilder scb;
        DataTable dt;



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
        public void LoadOffers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, OfferImage AS Image, Offer_Description, Price, StartDate, EndDate FROM Offers WHERE StartDate <= GETDATE() AND EndDate >= GETDATE();";

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


        public void Book(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;



            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();


                using (SqlCommand cmd = new SqlCommand("hasRezervation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", Props.curentuser);


                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (!(status == 1))
                    {
                        using (SqlCommand cmd2 = new SqlCommand("newRezervation", con))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;

                            cmd2.Parameters.AddWithValue("@Username", Props.curentuser);

                            cmd2.ExecuteNonQuery();
                        }
                    }

                }
                //===================================

                try
                {
                    if (BookID == null)
                        throw new Exception("BookId value is null");

                    using (SqlCommand cmd = new SqlCommand("updateRezervationsOffers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@username", Props.curentuser);
                       
                        cmd.Parameters.AddWithValue("@idOffer", BookID);
                  
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Offer Booked!");
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    MessageBox.Show("Insert a correct value");
                }


                con.Close();
            }
            Debug.WriteLine("Executat");
        }

        private ICommand _bookRoom;
        public ICommand BookCommand
        {
            get
            {
                if (_bookRoom == null)
                    _bookRoom = new RelayCommand(Book);
                return _bookRoom;
            }
        }
        //=================
        #endregion
    }
}
