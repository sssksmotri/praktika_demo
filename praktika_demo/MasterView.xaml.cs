using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace praktika_demo
{
    /// <summary>
    /// Логика взаимодействия для MasterView.xaml
    /// </summary>
    public partial class MasterView : Window
    {
        private praktika_vesnaEntities4 db;
        int user_id;
        public MasterView(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            db = new praktika_vesnaEntities4();
            LoadRequests(); // Загрузка заявок при открытии окна
        }

        private void LoadRequests()
        {
            try
            {
                using (praktika_vesnaEntities4 db = new praktika_vesnaEntities4())
                {
                    var requests = db.request
                        //.Where(r => r.request_client.Any(rc => rc.clientID == this.user_id))
                        .ToList();

                    // Prepare the message
                    StringBuilder messageBuilder = new StringBuilder();
                    foreach (var request in requests)
                    {
                        string homeTechType = request.homeTechType.Any() ? request.homeTechType.First().homeTechType1 : "";
                        string homeTechModel = request.homeTechType.Any() ? request.homeTechType.First().homeTechModel : "";
                        string statusDescription = request.status.Any() ? request.status.First().statusDescription : "";


                        // Add formatted data to the ListBox
                        RepairRequestsListBox.Items.Add(
                            $"Request ID: {request.requestID}\n" +
                            $"Start Date: {request.startDate}\n" +
                            $"Home Tech Type: {homeTechType}\n" +
                            $"Home Tech Model: {homeTechModel}\n" +
                            $"Problem Description: {request.problemDescription}\n" +
                            $"Repair Parts: {request.repairParts}\n" +
                            $"Status Description: {statusDescription}\n\n"
                        );
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {

        }

        private void master(object sender, RoutedEventArgs e)
        {
            
            Master master = new Master(user_id);
            master.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
