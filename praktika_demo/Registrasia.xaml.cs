using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace praktika_demo
{
    public partial class Registrasia : Window
    {
        int user_id;
        private praktika_vesnaEntities4 db;
  
        private readonly Dictionary<string, int> roleTypes = new Dictionary<string, int>
        {
            { "Менеджер", 1 },
            { "Мастер", 2 },
            { "Оператор", 3 },
            { "Заказчик", 4 }
        };

        public Registrasia()
        {
            InitializeComponent();
            db = new praktika_vesnaEntities4();
        }

        private void vhod(object sender, RoutedEventArgs e)
        {
            avtoriz avtoriz = new avtoriz();
            avtoriz.Show();
            this.Close();
        }

        private bool ValidateData()
        {
            StringBuilder errorMessage = new StringBuilder();
            bool hasError = false;

           
            if (!Regex.IsMatch(logintext.Text, @"^[a-zA-Z0-9_]{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный логин (только буквы, цифры и _).");
                hasError = true;
            }

            if (!Regex.IsMatch(passwordtext.Text, @"^(?=.*[a-zA-Z])(?=.*[0-9!()-_]).{8,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный пароль (минимум 8 символов, хотя бы 1 буква, цифра или символ !()-_).");
                hasError = true;
            }

            
            if (!Regex.IsMatch(fiotext.Text, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное имя (только буквы).");
                hasError = true;
            }

            
            if (!Regex.IsMatch(phonetext.Text, @"^\d{11}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный номер телефона (ровно 11 цифр).");
                hasError = true;
            }

            
            string selectedRole = (roltext.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(selectedRole) || !roleTypes.ContainsKey(selectedRole))
            {
                errorMessage.AppendLine("Пожалуйста, выберите корректную роль.");
                hasError = true;
            }

            
            if (hasError)
            {
                MessageBox.Show(errorMessage.ToString(), "Ошибка валидации");
                return false;
            }

            return true;
        }

        private void registr(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            using (praktika_vesnaEntities4 db = new praktika_vesnaEntities4())
            {
                string selectedRole = (roltext.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (!roleTypes.ContainsKey(selectedRole))
                {
                    MessageBox.Show("Выберите корректную роль.");
                    return;
                }

                int typeId = roleTypes[selectedRole];

                user user = new user()
                {
                    fio = fiotext.Text,
                    phone = phonetext.Text,
                    type_id = typeId // Присваиваем type_id
                };

                auth auth = new auth()
                {
                    user = user,
                    login = logintext.Text,
                    password = passwordtext.Text
                };

                db.user.Add(user);
                db.auth.Add(auth);
                db.SaveChanges();
                avtoriz avtoriz = new avtoriz();
                avtoriz.Show();
                this.Close();
                
            }
        }

      

        private void roltext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
