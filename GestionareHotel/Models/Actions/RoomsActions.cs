using GestionareHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GestionareHotel.Models.Actions
{
    class RoomsActions
    {
        private RoomsViewModel _roomsViewModel;
        public RoomsActions(RoomsViewModel _roomsViewModel)
        {
            this._roomsViewModel = _roomsViewModel;
        }


        public void Add(object param)
        {
            if (_roomsViewModel.Descriere == null || _roomsViewModel.NrCamere == null || 
                _roomsViewModel.NrPersoane == null || _roomsViewModel.Price == null)
            {
                System.Windows.MessageBox.Show("All fields must be completed", "Rooms", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddRoom", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@denumire", _roomsViewModel.Denumire);
                        cmd.Parameters.AddWithValue("@descriere", _roomsViewModel.Descriere);
                        cmd.Parameters.AddWithValue("@numarcamere", _roomsViewModel.NrCamere);
                        cmd.Parameters.AddWithValue("@numarpersoane", _roomsViewModel.NrPersoane);
                        cmd.Parameters.AddWithValue("@imagine", (object)Tools.ReadImage(_roomsViewModel.ImagePath));
                        cmd.Parameters.AddWithValue("@pret", _roomsViewModel.Price);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Incorect values");
            }
            System.Windows.MessageBox.Show("Room added!", "Rooms", MessageBoxButton.OK, MessageBoxImage.Information);

            Debug.WriteLine("Executat");
        }


        public void Browse(object param)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                Debug.WriteLine(sFileName);
                _roomsViewModel.ImagePath = sFileName;

                _roomsViewModel.Image = (BitmapSource)new ImageSourceConverter().ConvertFrom(Tools.ReadImage(sFileName));
            }

        }
    }
}
