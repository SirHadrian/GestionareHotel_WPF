using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class MyBooksViewModel: BaseViewModel
    {
        SqlDataAdapter sda;
        //SqlCommandBuilder scb;
        DataTable dt;

        List<string> _toPay = new List<string>();

        int money = 0;

        #region Proprieties
        //==================
        private string _money;
        public string Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                OnPropertyChanged("Money");
            }
        }


        private DataTable _roomsRezervationsDataTable = null;
        public DataTable RoomsRezervationsDataTable
        {
            get { return _roomsRezervationsDataTable; }
            set
            {
                _roomsRezervationsDataTable = value;
                OnPropertyChanged("RoomsRezervationsDataTable");
            }
        }

        private DataTable _offersRezervationsDataTable = null;
        public DataTable OffersRezervationsDataTable
        {
            get { return _offersRezervationsDataTable; }
            set
            {
                _offersRezervationsDataTable = value;
                OnPropertyChanged("OffersRezervationsDataTable");
            }
        }

        private DataTable _servicesRezervationsDataTable = null;
        public DataTable ServicesRezervationsDataTable
        {
            get { return _servicesRezervationsDataTable; }
            set
            {
                _servicesRezervationsDataTable = value;
                OnPropertyChanged("ServicesRezervationsDataTable");
            }
        }
        //==================
        #endregion


        #region Commands
        //================
        public void LoadRezervations(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            //==============================
            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = @"SELECT Rezervations.ID AS ID, Active, Canceled, Paid, 
                            Users.UserName AS UserName,
                            Users.EmailAdress AS EmailAdress, 
                            Rooms.Denumire AS Camera, 
                            Rooms.Descriere AS Detalii,
                            Rooms.NumarPersoane AS NrPersoane,
                            Rooms.NumarCamere AS NrCamere,
                            Rooms.Pret AS Pret

                            FROM Rezervations 
                            INNER JOIN Users ON Rezervations.ID_User = Users.ID
                            INNER JOIN Rooms ON Rezervations.ID_Room = Rooms.ID
                            WHERE Rezervations.Canceled = 0 AND Rezervations.Active = 1
                            ;";


            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);
            RoomsRezervationsDataTable = dt;

            foreach (DataRow dr in RoomsRezervationsDataTable.Rows)
            {
                string name = dr["Pret"].ToString();
                _toPay.Add(name);
            }

            //================================

            con = new SqlConnection(regularConnectionString);
            querry = @"SELECT Rezervations.ID AS ID, Active, Canceled, Paid, 
                            Users.UserName AS UserName,
                            Users.EmailAdress AS EmailAdress, 
                            Offers.Offer_Description AS Offer,
                            Offers.Price AS Price,
                            Offers.StartDate AS StartDate,
                            Offers.EndDate AS EndDate

                            FROM Rezervations 
                            INNER JOIN Users ON Rezervations.ID_User = Users.ID
                            INNER JOIN Offers ON Rezervations.ID_Offer = Offers.ID
                            WHERE Rezervations.Canceled = 0 AND Rezervations.Active = 1
                            ;";


            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);
            OffersRezervationsDataTable = dt;

            foreach (DataRow dr in OffersRezervationsDataTable.Rows)
            {
                string name = dr["Price"].ToString();
                _toPay.Add(name);
            }
            //===================================

            con = new SqlConnection(regularConnectionString);
            querry = @"SELECT Rezervations.ID AS ID, Active, Canceled, Paid, 
                            Users.UserName AS UserName,
                            Users.EmailAdress AS EmailAdress, 
                            Servicii.Descriere AS Service,
                            Servicii.Pret AS Price

                            FROM Rezervations 
                            INNER JOIN Users ON Rezervations.ID_User = Users.ID
                            INNER JOIN Servicii ON Rezervations.ID_Service = Servicii.ID
                            WHERE Rezervations.Canceled = 0 AND Rezervations.Active = 1
                            ;";


            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);
            ServicesRezervationsDataTable = dt;

            foreach (DataRow dr in ServicesRezervationsDataTable.Rows)
            {
                string name = dr["Price"].ToString();
                _toPay.Add(name);
            }

            List<string> temp = new List<string>();
            
            foreach (string st in _toPay)
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

            _toPay.Clear();
            _toPay = temp;
            money = 0;
            
            foreach(string st in _toPay)
            {
                money += Int32.Parse(st);
            }

            Money = "$" + money.ToString();
            //Debug.WriteLine("Executat");
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
        //================
        #endregion
    }
}
