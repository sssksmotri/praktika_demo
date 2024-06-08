using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace praktika_demo
{
  
    public partial class Naznach : Window
    {
        private praktika_vesnaEntities4 db;
        int user_id;
        public Naznach(int user_id)
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
                                       
                                        .Select(rc => rc.request.requestID)
                                        .ToList();
                nomerzaiyavki.ItemsSource = zayvkaIds;

                List<string> statusIDs = db.status.Select(s => s.statusDescription).ToList();
                statustxt.ItemsSource = statusIDs;

         
                var masters = db.user
                                .Where(u => u.type.type_id == 2) 
                                .Select(u => new { u.userID, u.fio })
                                .ToList();
                masteridtxt.ItemsSource = masters;

             
                masteridtxt.DisplayMemberPath = "fio";
                masteridtxt.SelectedValuePath = "userID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ID заявок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Naznach1(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedZayvkaId = (int)nomerzaiyavki.SelectedValue;
                int selectedMasterId = (int)masteridtxt.SelectedValue;

              
                var zayvka = db.request_client
                               .FirstOrDefault(rc => rc.request.requestID == selectedZayvkaId);

                if (zayvka != null)
                {
                 
                    zayvka.masterID = selectedMasterId;
                    db.SaveChanges();

                    MessageBox.Show("Мастер успешно назначен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Заявка не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void sozdat_z(object sender, RoutedEventArgs e)
        {
            oper oper = new oper(user_id);
            oper.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
