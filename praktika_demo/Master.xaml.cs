using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace praktika_demo
{
   
    public partial class Master : Window
    {
        private praktika_vesnaEntities4 db;
        int user_id;
        public Master(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            db = new praktika_vesnaEntities4(); 
            LoadZayvkaIDs(); 
        }

        
        private void LoadZayvkaIDs()
        {
            List<int> zayvkaIds = db.request_client
                                    .Where(rc => rc.masterID == this.user_id)
                                    .Select(rc => rc.request.requestID)
                                    .ToList();

            nomerzayavki.ItemsSource = zayvkaIds;
        }

        private void Otpravt(object sender, RoutedEventArgs e)
        {
            if (nomerzayavki.SelectedItem == null || string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                MessageBox.Show("Please select a request ID and enter a comment.");
                return;
            }

            int selectedRequestId = (int)nomerzayavki.SelectedItem;
            string message = CommentTextBox.Text;

            var newComment = new comments
            {
                message = message,
                masterID = this.user_id,
                requestID = selectedRequestId
            };

            db.comments.Add(newComment);
            db.SaveChanges();

            MessageBox.Show("Comment saved successfully.");
        }

        private void sozdat_z(object sender, RoutedEventArgs e)
        {
            MasterView masterView = new MasterView(user_id);
            masterView.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
