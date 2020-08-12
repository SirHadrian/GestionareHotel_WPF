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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class ServicesViewModel: BaseViewModel
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
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

        private string _service = null;
        public string Service
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
                OnPropertyChanged("Service");
            }
        }

        private string _price = null;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private string _deleteServiceID = null;
        public string DeleteServiceID
        {
            get
            {
                return _deleteServiceID;
            }
            set
            {
                _deleteServiceID = value;
                OnPropertyChanged("DeleteServiceID");
            }
        }
        //=============
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


        public void UpdateServices(object param)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(ServicesDataTable);
        }

        private ICommand _updateServices;
        public ICommand UpdateServicesCommand
        {
            get
            {
                if (_updateServices == null)
                    _updateServices = new RelayCommand(UpdateServices);
                return _updateServices;
            }
        }


        public void DeleteService(object param)
        {
            if (DeleteServiceID == null)
                return;

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;

            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteServ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", DeleteServiceID);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            DeleteServiceID = null;
            System.Windows.MessageBox.Show("Service Deleted");
        }

        private ICommand _deleteService;
        public ICommand DeleteServiceCommand
        {
            get
            {
                if (_deleteService == null)
                    _deleteService = new RelayCommand(DeleteService);
                return _deleteService;
            }
        }


        public void AddService(object param)
        {
            if (Service == null || Price == null) 
            {
                System.Windows.MessageBox.Show("Missing parameters!");
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;

            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AddServ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@descriere", Service);
                    cmd.Parameters.AddWithValue("@pret", Price);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            System.Windows.MessageBox.Show("Service added!");

            Debug.WriteLine("Executat");
        }

        private ICommand _addService;
        public ICommand AddServiceCommand
        {
            get
            {
                if (_addService == null)
                    _addService = new RelayCommand(AddService);
                return _addService;
            }
        }
        //=================
        #endregion
    }
}
