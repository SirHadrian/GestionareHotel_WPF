using GestionareHotel.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class AngajatViewModel: BaseViewModel
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;


        #region Properties
        //===================
        private DataTable _rezervationsDataTable;
        public DataTable RezervationsDataTable
        {
            get
            {
                return _rezervationsDataTable;
            }
            set
            {
                _rezervationsDataTable = value;
                OnPropertyChanged("RezervationsDataTable");
            }
        }
        //=====================
        #endregion


        #region Commands
        //==============
        public void LoadRezervations(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Active, Canceled, Paid, ID_User, ID_Room, ID_Offer, ID_Service FROM Rezervations;";

            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);

            RezervationsDataTable = dt;
        }

        private ICommand _loadRezervations;
        public ICommand LoadRezervationsCommand
        {
            get
            {
                if (_loadRezervations == null)
                    _loadRezervations = new RelayCommand(LoadRezervations);
                return _loadRezervations;
            }
        }


        public void UpdateRezervations(object param)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(RezervationsDataTable);
        }

        private ICommand _updateRezervations;
        public ICommand UpdateRezervationsCommand
        {
            get
            {
                if (_updateRezervations == null)
                    _updateRezervations = new RelayCommand(UpdateRezervations);
                return _updateRezervations;
            }
        }
        //==============
        #endregion
    }
}
