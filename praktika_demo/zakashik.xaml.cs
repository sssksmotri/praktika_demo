using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;


namespace praktika_demo
{

    public partial class zakashik : Window
    {
        int user_id;
        public zakashik(int user_id)
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
                        .Where(r => r.request_client.Any(rc => rc.clientID == this.user_id))
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
        private void sozdat(object sender, RoutedEventArgs e)
        {
            sozdat_zayvku sozdat_Zayvku = new sozdat_zayvku(user_id);
            sozdat_Zayvku.Show();
            this.Close();
        }
        private void redactirovat(object sender, RoutedEventArgs e)
        {
            redaktirovat_zayvki redaktirovat_Zayvki = new redaktirovat_zayvki(user_id);
            redaktirovat_Zayvki.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GenerateQRCode_Click(object sender, RoutedEventArgs e)
        {
           MainWindow mainWindow = new MainWindow(user_id);
            mainWindow.Show();
            this.Close();
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {
            zakashik zakashik = new zakashik(user_id);
            zakashik.Show();
            zakashik.Close();
        }
    }
}