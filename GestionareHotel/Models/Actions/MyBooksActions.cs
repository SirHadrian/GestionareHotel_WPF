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
    class MyBooksActions
    {
        SqlDataAdapter _sda;
        //SqlCommandBuilder _scb;
        DataTable _dt;

        int money;


        private MyBooksViewModel _myBooksViewModel;
        public MyBooksActions(MyBooksViewModel _myBooksViewModel)
        {
            this._myBooksViewModel = _myBooksViewModel;
        }


        public void LoadRezervations(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            //==============================
            SqlConnection con = new SqlConnection(regularConnectionString);
            con.Open();
            using (var cmd = new SqlCommand("getRezervationsRooms", con))
            {
                _dt = new DataTable();
                using (_sda = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _sda.Fill(_dt);
                    _myBooksViewModel.RoomsRezervationsDataTable = _dt;
                }
            }

            foreach (DataRow dr in _myBooksViewModel.RoomsRezervationsDataTable.Rows)
            {
                string name = dr["Pret"].ToString();
                _myBooksViewModel.ToPay.Add(name);
            }
            //================================
            using (var cmd = new SqlCommand("getRezervationsOffers", con))
            {
                _dt = new DataTable();
                using (_sda = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _sda.Fill(_dt);
                    _myBooksViewModel.OffersRezervationsDataTable = _dt;
                }
            }

            foreach (DataRow dr in _myBooksViewModel.OffersRezervationsDataTable.Rows)
            {
                string name = dr["Price"].ToString();
                _myBooksViewModel.ToPay.Add(name);
            }
            //===================================
            using (var cmd = new SqlCommand("getRezervationsServicii", con))
            {
                _dt = new DataTable();
                using (_sda = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _sda.Fill(_dt);
                    _myBooksViewModel.ServicesRezervationsDataTable = _dt;
                }
            }

            foreach (DataRow dr in _myBooksViewModel.ServicesRezervationsDataTable.Rows)
            {
                string name = dr["Price"].ToString();
                _myBooksViewModel.ToPay.Add(name);
            }

            List<string> temp = new List<string>();

            foreach (string st in _myBooksViewModel.ToPay)
            {
                string str = "";
                for (int i = 0; i < st.Length; ++i)
                {

                    if (st[i] != '.')
                    {
                        str = str + st[i];

                    }
                    else
                    {
                        temp.Add(str);
                    }
                }
            }

            _myBooksViewModel.ToPay.Clear();
            _myBooksViewModel.ToPay = temp;
            money = 0;

            foreach (string st in _myBooksViewModel.ToPay)
            {
                money += Int32.Parse(st);
            }

            _myBooksViewModel.Money = "$" + money.ToString();
            //Debug.WriteLine("Executat");
            con.Close();
        }
    }
}
