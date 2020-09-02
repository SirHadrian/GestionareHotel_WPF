using GestionareHotel.ViewModels;
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

namespace GestionareHotel.Models.Actions
{
    class BookOfferActions
    {
        SqlDataAdapter _sda;
        //SqlCommandBuilder _scb;
        DataTable _dt;


        private BookOfferViewModel _bookOfferViewModel;
        public BookOfferActions(BookOfferViewModel _bookOfferViewModel)
        {
            this._bookOfferViewModel = _bookOfferViewModel;
        }


        public void LoadOffers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, OfferImage AS Image, Offer_Description, Price, StartDate, EndDate FROM Offers WHERE StartDate <= GETDATE() AND EndDate >= GETDATE();";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _bookOfferViewModel.OffersDataTable = _dt;

            Debug.WriteLine("Executat");
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
                    if (_bookOfferViewModel.BookID == null)
                        throw new Exception("BookId value is null");

                    using (SqlCommand cmd = new SqlCommand("updateRezervationsOffers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@username", Props.curentuser);

                        cmd.Parameters.AddWithValue("@idOffer", _bookOfferViewModel.BookID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Offer Booked!");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    MessageBox.Show("Insert a correct value");
                }


                con.Close();
            }
            Debug.WriteLine("Executat");
        }
    }
}
