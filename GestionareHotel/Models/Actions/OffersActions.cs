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
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GestionareHotel.Models.Actions
{
    class OffersActions
    {
        private OffersViewModel _offersViewModel;
        public OffersActions(OffersViewModel _offersViewModel)
        {
            this._offersViewModel = _offersViewModel;
        }


        public void AddOffer(object param)
        {
            if (_offersViewModel.Offer == null || _offersViewModel.Price == null)
            {
                System.Windows.MessageBox.Show("Complete all the fields!", "Offers", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    using (SqlCommand cmd = new SqlCommand("AddOffer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@offer", _offersViewModel.Offer);
                        cmd.Parameters.AddWithValue("@price", _offersViewModel.Price);
                        cmd.Parameters.AddWithValue("@from", _offersViewModel.StartDate);
                        cmd.Parameters.AddWithValue("@to", _offersViewModel.EndDate);
                        cmd.Parameters.AddWithValue("@img", (object)Tools.ReadImage(_offersViewModel.ImagePath));


                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Incorect values");
            }
            System.Windows.MessageBox.Show("Offer added!", "Offers", MessageBoxButton.OK, MessageBoxImage.Information);

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
                _offersViewModel.ImagePath = sFileName;

                _offersViewModel.Image = (BitmapSource)new ImageSourceConverter().ConvertFrom(Tools.ReadImage(sFileName));
            }

        }
    }
}
