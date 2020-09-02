using GestionareHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionareHotel.Models.Actions
{
    class AngajatActions
    {
        SqlDataAdapter _sda;
        SqlCommandBuilder _scb;
        DataTable _dt;


        private AngajatViewModel _angajatViewModel;
        public AngajatActions(AngajatViewModel _angajatViewModel)
        {
            this._angajatViewModel = _angajatViewModel;
        }


        public void LoadRezervations(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Active, Canceled, Paid, ID_User, ID_Room, ID_Offer, ID_Service FROM Rezervations;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _angajatViewModel.RezervationsDataTable = _dt;
        }


        public void UpdateRezervations(object param)
        {
            _scb = new SqlCommandBuilder(_sda);
            _sda.Update(_angajatViewModel.RezervationsDataTable);
        }
    }
}
