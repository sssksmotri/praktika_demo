using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace praktika_demo
{
    
    public partial class redaktirovat_zayvki : Window
    {
        private praktika_vesnaEntities4 db;
        int user_id;

        public redaktirovat_zayvki(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            db = new praktika_vesnaEntities4(); 
            LoadZayvkaIDs(); 
        }

      
        private void LoadZayvkaIDs()
        {
            try
            {
                
                List<int> zayvkaIds = db.request_client
                                        .Where(rc => rc.clientID == this.user_id)
                                        .Select(rc => rc.request.requestID)
                                        .ToList();
              
                requestidtxt.ItemsSource = zayvkaIds;
           
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ID заявок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void redact(object sender, RoutedEventArgs e)
        {
            int selectedZayvkaId = (int)requestidtxt.SelectedItem;
            string selectedStatus = (requestidtxt.SelectedItem as ComboBoxItem)?.Content?.ToString();
            request requestupdate = db.request.FirstOrDefault(r => r.requestID == selectedZayvkaId);
            if (requestupdate != null)
            {
                requestupdate.problemDescription = opisan.Text.ToString();
                db.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
            }

        }

        private void redactirovat(object sender, RoutedEventArgs e)
        {

        }

        private void sozdat_z(object sender, RoutedEventArgs e)
        {
            sozdat_zayvku sozdat_Zayvku = new sozdat_zayvku(user_id);
            sozdat_Zayvku.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {
            zakashik zakashik = new zakashik(user_id);  
            zakashik.Show();
                this.Close();
        }
    }
}
