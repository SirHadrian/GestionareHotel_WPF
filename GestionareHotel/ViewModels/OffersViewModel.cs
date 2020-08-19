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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GestionareHotel.ViewModels
{
    class OffersViewModel: BaseViewModel
    {
        private string ImagePath;

        public OffersViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        

        #region Properties
        //=================
        private ImageSource _image = null;
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private string _offer = null;
        public string Offer
        {
            get
            {
                return _offer;
            }
            set
            {
                _offer = value;
                OnPropertyChanged("Offer");
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

        private DateTime _startDate;
        
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        //=================
        #endregion


        #region Commands
        //=================
        public void AddOffer(object param)
        {
            if (Offer == null || Price == null) 
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

                        cmd.Parameters.AddWithValue("@offer", Offer);
                        cmd.Parameters.AddWithValue("@price", Price);
                        cmd.Parameters.AddWithValue("@from", StartDate);
                        cmd.Parameters.AddWithValue("@to", EndDate);
                        cmd.Parameters.AddWithValue("@img", (object)Tools.ReadImage(ImagePath));


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

        private ICommand _addOffer;
        public ICommand AddOfferCommand
        {
            get
            {
                if (_addOffer == null)
                    _addOffer = new RelayCommand(AddOffer);
                return _addOffer;
            }
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
                ImagePath = sFileName;

                Image = (BitmapSource)new ImageSourceConverter().ConvertFrom(Tools.ReadImage(sFileName));
            }

        }

        private ICommand _browse;
        public ICommand BrowseCommand
        {
            get
            {
                if (_browse == null)
                    _browse = new RelayCommand(Browse);
                return _browse;
            }
        }
        //=================
        #endregion
    }
}
