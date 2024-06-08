//using Moq;
//using praktika_demo;
//using System;
//using System.Windows;
//using System.Windows.Controls;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Windows.Controls.Primitives;

//namespace praktika_demo
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void Test_sozdat_zayavka_Successfully()
//        {
//            // Arrange
//            var fakeDbSet = new Mock<DbSet<request>>();
//            var dbMock = new Mock<Entities>();
//            dbMock.Setup(db => db.request).Returns(fakeDbSet.Object);

//            var request = new request(8)
//            {
//                db = dbMock.Object
//            };

//            // Ваши параметры для создания заявки
//            var datePicker = new DatePicker();
//            var techComboBox = new ComboBox();
//            var modelTextBox = new TextBox();
//            var problemDescription = new TextBox();
//            var statusComboBox = new ComboBox();
//            int user_id = 1; // Пример значения для user_id

//            // Act
//            request.sozdat_zayavka(datePicker, techComboBox, modelTextBox, problemDescription, statusComboBox, user_id);

//            // Assert
//            fakeDbSet.Verify(db => db.Add(It.IsAny<request>()), Times.Once);
//            dbMock.Verify(db => db.SaveChanges(), Times.Once);
//        }
//    }
//}
