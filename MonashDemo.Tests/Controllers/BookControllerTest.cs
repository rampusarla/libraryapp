using MonashDemo.Controllers;
using MonashDemo.Core.Models;
using MonashDemo.Core.ServiceLayer;
using MonashDemo.Core.ViewModels;
using MonashDemo.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MonashDemo.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        public BookController CreateAndReturnBookControllerObject()
        {
            var mockRepository = new FakeBookRepository();            
            var mockBookService = new FakeBookService(new ModelStateWrapper(), mockRepository);
            var controller = new BookController(mockBookService);            

            return controller;
        }

        [TestMethod]
        public void Book_Create_Get_Action_Returns_Create_ViewResult()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void Book_Create_Post_Action_Redirects_To_Index_ViewResult_When_Valid()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();

            var bookVM = new BookViewModel()
            {
                BookId = 1,
                Title = ".NET Complete Reference",
                Author = "Martin Fowler"
            };

            //Act
            var result = controller.Create(bookVM) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Book_Create_Post_Action_Redirects_To_SameViewResult_When_InValid()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();

            var book = new BookViewModel()
            {
                BookId = 1,
                Author = "Martin Fowler"
            };            
            
            //Act
            var result = controller.Create(book) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void OverdueBooks_Get_Action_Returns_SameViewResult_When_Available()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();
            
            //Act
            var result = controller.ListOverdueBooks() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void OverdueBooks_Get_Action_Returns_Empty_ViewResult_When_Not_Available()
        {
            //Arrange
            var repository = new FakeBookRepository();
            repository.context.BooksBorrowed.Clear();
            var modelState = new Mock<IValidationDictionary>();            
            var mockBookService = new FakeBookService(modelState.Object, repository);            
            var controller = new BookController(mockBookService);

            //Act
            var result = controller.ListOverdueBooks() as ViewResult;

            //Assert
            Assert.AreEqual("Empty", result.ViewName);
        }

        [TestMethod]
        public void Book_Index_Get_Action_For_Search_Returns_Index_ViewResult_When_Available()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();

            //Act
            var result = controller.Index("","","patterns",1) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Book_Index_Get_Action_For_Search_Returns_Empty_ViewResult_When_NotAvailable()
        {
            //Arrange
            var controller = CreateAndReturnBookControllerObject();

            //Act
            var result = controller.Index("", "", "searchnotavailable", 0) as ViewResult;

            //Assert
            Assert.AreEqual("Empty", result.ViewName);
        }
    }
}
