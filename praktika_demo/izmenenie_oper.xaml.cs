using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace praktika_demo
{
    public partial class izmenenie_oper : Window
    {
        private praktika_vesnaEntities4 db;
        int user_id;
        public izmenenie_oper(int user_id)
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

                requestidtxt.ItemsSource = zayvkaIds;

                List<string> statusIDs = db.status.Select(s => s.statusDescription).ToList();
                statustxt.ItemsSource = statusIDs;

                var masters = db.user
                                .Where(u => u.type.type_id == 2)
                                .Select(u => new { u.userID, u.fio })
                                .ToList();
                mastertxt.ItemsSource = masters;

                mastertxt.DisplayMemberPath = "fio";
                mastertxt.SelectedValuePath = "userID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ID заявок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {
            oper oper = new oper(user_id);
            oper.Show();
            this.Close();
        }

        private void Naznach(object sender, RoutedEventArgs e)
        {
            Naznach naznach = new Naznach(user_id);
            naznach.Show();
            this.Close();
        }

        private void redact(object sender, RoutedEventArgs e)
        {
            try
            {
                if (requestidtxt.SelectedItem == null || statustxt.SelectedItem == null || mastertxt.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите значения для всех полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int selectedZayvkaId = (int)requestidtxt.SelectedItem;
                string selectedStatus = (string)statustxt.SelectedItem;
                int selectedMasterId = (int)mastertxt.SelectedValue;

                var requestClient = db.request_client
                                      .FirstOrDefault(rc => rc.requestID == selectedZayvkaId && rc.clientID == this.user_id);

                if (requestClient != null)
                {
                    requestClient.masterID = selectedMasterId;
                    requestClient.request.status.FirstOrDefault().statusDescription = selectedStatus;
                    db.SaveChanges();

                 
                    string clientPhoneNumber = db.user.Where(u => u.userID == requestClient.clientID).Select(u => u.phone).FirstOrDefault();
                    string masterPhoneNumber = db.user.Where(u => u.userID == requestClient.masterID).Select(u => u.phone).FirstOrDefault();

                   
                    SimulateSmsNotification(clientPhoneNumber, $"Статус вашей заявки был изменен на: {selectedStatus}");
                    SimulateSmsNotification(masterPhoneNumber, $"Статус заявки был изменен на: {selectedStatus}");

                    MessageBox.Show("Данные успешно сохранены и уведомления отправлены");
                }
                else
                {
                    MessageBox.Show("Заявка не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SimulateSmsNotification(string phoneNumber, string message)
        {
            try
            {
                
                string simulatedSmsLog = $"Sending SMS to {phoneNumber}: {message}";

                
                System.IO.File.AppendAllText("sms_log.txt", simulatedSmsLog + Environment.NewLine);

               
                Console.WriteLine(simulatedSmsLog);

                
                MessageBox.Show(simulatedSmsLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при  отправки SMS: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
