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
    class BookServiceViewModel: BaseViewModel
    {
        SqlDataAdapter sda;
        //SqlCommandBuilder scb;
        DataTable dt;


        #region Properties
        //======================
        private DataTable _servicesDataTable;
        public DataTable ServicesDataTable
        {
            get { return _servicesDataTable; }
            set
            {
                _servicesDataTable = value;
                OnPropertyChanged("ServicesDataTable");
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
        //======================
        #endregion


        #region Commands
        //=================
        public void LoadService(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Descriere, Pret FROM Servicii;";

            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);

            ServicesDataTable = dt;
        }

        private ICommand _loadServices;
        public ICommand LoadServicesCommand
        {
            get
            {
                if (_loadServices == null)
                    _loadServices = new RelayCommand(LoadService);
                return _loadServices;
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

                    using (SqlCommand cmd = new SqlCommand("updateRezervationsService", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@username", Props.curentuser);
                        
                        cmd.Parameters.AddWithValue("@idService", BookID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Service Booked!");
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
        //=======================
        #endregion
    }
}
