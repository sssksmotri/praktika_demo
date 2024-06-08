using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using praktika_demo;
using System.Linq.Expressions;

namespace sozdat_zayvkuTests
{
    public class FakeDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    {
        private readonly List<T> _data;

        public FakeDbSet()
        {
            _data = new List<T>();
        }

        public override T Add(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        Type IQueryable.ElementType => _data.AsQueryable().ElementType;
        Expression IQueryable.Expression => _data.AsQueryable().Expression;
        IQueryProvider IQueryable.Provider => _data.AsQueryable().Provider;
    }

    public class FakeDbContext : DbContext
    {
        public FakeDbContext()
        {
            request = new FakeDbSet<request>();
            request_client = new FakeDbSet<request_client>();
            user = new FakeDbSet<User>();
        }

        public DbSet<request> request { get; set; }
        public DbSet<request_client> request_client { get; set; }
        public DbSet<User> user { get; set; }

        public override int SaveChanges()
        {
            return 1;
        }
    }

    [TestClass]
    public class sozdat_zayvkuTests
    {
        [TestMethod]
        public void TestSozdatMethod()
        {
            // Arrange
            var datePicker = new DatePicker { SelectedDate = DateTime.Now };
            var techComboBox = new ComboBox();
            techComboBox.Items.Add(new ComboBoxItem { Content = "YourTechType" });
            techComboBox.SelectedItem = techComboBox.Items[0];

            var problemDescription = new TextBox { Text = "YourProblemDescription" };
            var statusComboBox = new ComboBox();
            statusComboBox.Items.Add(new ComboBoxItem { Content = "Status" });
            statusComboBox.SelectedItem = statusComboBox.Items[0];

            var user = new User { userID = 1 };

            var fakeDb = new FakeDbContext();
            fakeDb.user.Add(user);

            var sozdat_zayvkuInstance = new sozdat_zayvku(1)
            {
                datePicker = datePicker,
                techComboBox = techComboBox,
                problemDescription = problemDescription,
                statusComboBox = statusComboBox,
                db = fakeDb
            };

            var sender = new object();
            var e = new RoutedEventArgs();

            // Act
            sozdat_zayvkuInstance.sozdat(sender, e);

            // Assert
            // ѕровер€ем, что за€вка была успешно добавлена в базу данных
            Assert.AreEqual(1, fakeDb.request.Count());
            Assert.AreEqual(1, fakeDb.request_client.Count());

            // ѕровер€ем, что данные были правильно сохранены
            var newRequest = fakeDb.request.First();
            Assert.AreEqual("YourProblemDescription", newRequest.problemDescription);
            Assert.AreEqual(datePicker.SelectedDate, newRequest.startDate);

            var newRequestClient = fakeDb.request_client.First();
            Assert.AreEqual(1, newRequestClient.clientID);

            // ѕровер€ем, что пользователю показываетс€ сообщение об успешном создании за€вки
            // “ут может потребоватьс€ перехват вызова MessageBox.Show
            // ¬ данном примере мы не можем протестировать MessageBox.Show напр€мую
            // Ётот метод лучше протестировать с использованием фреймворков дл€ UI-тестировани€
        }
    }
}
