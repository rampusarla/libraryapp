using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonashDemo.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;
using MonashDemo.Tests.Fakes;
using System.Collections.Generic;
using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using MonashDemo.Core.ServiceLayer;

namespace MonashDemo.Tests
{
    [TestClass]
    public class BorrowerControllerTest
    {

        public BorrowerController CreateAndReturnBorrowerControllerObject()
        {
            var mockRepository = new FakeBorrowerRepository();
            var mockBorrowerService = new FakeBorrowerService(new ModelStateWrapper(), mockRepository);
            var controller = new BorrowerController(mockBorrowerService);

            return controller;
        }
        
        [TestMethod]
        public void Borrower_Create_Get_Action_Returns_Create_ViewResult()
        {
            //Arrange
            var controller = CreateAndReturnBorrowerControllerObject();

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void Borrower_Create_Post_Action_Redirects_To_Index_ViewResult_When_Valid()
        {
            //Arrange
            var controller = CreateAndReturnBorrowerControllerObject();

            var borrower = new Borrower() { 
                BorrowerId = 1,
                FirstName = "Rajesh",
                LastName = "Pusarla"
            };

            //Act
            var result = controller.Create(borrower) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void Borrower_Create_Post_Action_Redirects_To_SameViewResult_When_InValid()
        {
            //Arrange
            var controller = CreateAndReturnBorrowerControllerObject();

            var borrower = new Borrower()
            {
                BorrowerId = 1,                
                LastName = "Pusarla"
            };
            //controller.ModelState.AddModelError("", "");
            
            //Act
            var result = controller.Create(borrower) as ViewResult;            

            //Assert
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void BookBorrow_Get_Action_Returns_BookBorrow_ViewResult()
        {
            //Arrange
            var controller = CreateAndReturnBorrowerControllerObject();

            //Act
            var result = controller.BookBorrow() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }        
        [TestMethod]
        public void BookBorrow_Get_Action_Returns_Empty_ViewResult_When_No_Borrowers()
        {
            //Arrange            

            var mockRepository = new FakeBorrowerRepository();
            mockRepository.context.Borrowers.Clear();
            var modelState = new Mock<IValidationDictionary>();            
            var mockBorrowerService = new FakeBorrowerService(modelState.Object, mockRepository);            
            var controller = new BorrowerController(mockBorrowerService);            
              
            //Act
            var result = controller.BookBorrow() as ViewResult;

            //Assert
            Assert.AreEqual("Empty", result.ViewName);
        }
        [TestMethod]
        public void BookBorrow_Get_Action_Returns_Empty_ViewResult_When_No_Books()
        {
            //Arrange            
            var repository = new FakeBorrowerRepository();
            repository.context.Books.Clear();
            var modelState = new Mock<IValidationDictionary>();            
            var mockBorrowerService = new FakeBorrowerService(modelState.Object, repository);
            var controller = new BorrowerController(mockBorrowerService);            

            //Act
            var result = controller.BookBorrow() as ViewResult;

            //Assert
            Assert.AreEqual("Empty", result.ViewName);
        }
        [TestMethod]
        public void BookBorrow_Post_Action_Returns_Book_Index_Result_When_Valid()
        {
            //Arrange            
            var bookBorrower = new AddBookBorrowerViewModel() { 
                 Id = 1,
                 BookId = 1,
                 BorrowerId = 1
            };
            var controller = CreateAndReturnBorrowerControllerObject();
            

            //Act
            var result = controller.BookBorrow(bookBorrower) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Book", result.RouteValues["controller"]);            
        }

        [TestMethod]
        public void BookBorrow_Post_Action_Redirects_Same_ViewResult_When_BookOrBorower_InValid()
        {
            //Arrange            
            var bookBorrower = new AddBookBorrowerViewModel()
            {
                Id = 1,
                BookId = 54,
                BorrowerId = 23
            };
            var controller = CreateAndReturnBorrowerControllerObject();

            //Act
            var result = controller.BookBorrow(bookBorrower) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);            
        }
        [TestMethod]
        public void BookBorrow_Post_Action_Redirects_Same_ViewResult_When_BookAlreadyBorrowed()
        {
            //Arrange            
            var bookBorrower = new AddBookBorrowerViewModel()
            {
                Id = 1,
                BookId = 4,
                BorrowerId = 1
            };
            var controller = CreateAndReturnBorrowerControllerObject();

            //Act
            var result = controller.BookBorrow(bookBorrower) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }
    }
}
