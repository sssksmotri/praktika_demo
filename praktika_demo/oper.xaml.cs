using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace praktika_demo
{
 
    public partial class oper : Window
    {
        int user_id;

        public oper(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            LoadRequests();
        }
        private void LoadRequests()
        {
            try
            {
                using (praktika_vesnaEntities4 db = new praktika_vesnaEntities4())
                {
                    var requests = db.request
                        .ToList();

                   
                    StringBuilder messageBuilder = new StringBuilder();
                    foreach (var request in requests)
                    {
                        string homeTechType = request.homeTechType.Any() ? request.homeTechType.First().homeTechType1 : "";
                        string homeTechModel = request.homeTechType.Any() ? request.homeTechType.First().homeTechModel : "";
                        string statusDescription = request.status.Any() ? request.status.First().statusDescription : "";

                      

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
        private void Naznach(object sender, RoutedEventArgs e)
        {

            
            Naznach naznach = new Naznach(user_id);
            naznach.Show();
            this.Close();
        }

        private void redact(object sender, RoutedEventArgs e)
        {
            izmenenie_oper izmenenie_Oper = new izmenenie_oper(user_id);
            izmenenie_Oper.Show();  
            this.Close();
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {

        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
