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
    class BookRoomActions
    {
        SqlDataAdapter _sda;
        //SqlCommandBuilder _scb;
        DataTable _dt;


        private BookRoomViewModel _bookRoomViewModel;
        public BookRoomActions(BookRoomViewModel _bookRoomViewModel)
        {
            this._bookRoomViewModel = _bookRoomViewModel;
        }


        public void LoadRooms(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Imagine AS Image, Denumire, Descriere, NumarCamere, NumarPersoane, Pret FROM Rooms;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _bookRoomViewModel.RoomsDataTable = _dt;

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
                    if (_bookRoomViewModel.BookID == null)
                        throw new Exception("BookId value is null");

                    using (SqlCommand cmd = new SqlCommand("updateRezervationsRooms", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@username", Props.curentuser);

                        cmd.Parameters.AddWithValue("@idRoom", _bookRoomViewModel.BookID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Room Booked!");
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
