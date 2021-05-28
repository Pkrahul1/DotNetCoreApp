using CoreAPIEmpty.Models;
using CoreAPIEmpty.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIEmpty.Controllers
{
    public class HomeController:Controller
    {

        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {//this interface needs to be registerd in startup using singleton
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {// this is the default method will be executed if we are using app.UseMvcWithDefaultRoute mvc middle ware
            return _employeeRepository.GetEmployee(2).Name;
        }
        #region[details method returning json data for API]
        //public JsonResult Details()//to return jsonresult Controller sud be inhereted
        //{//will be executed when home/details is used in url
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return Json(model);
        //}
        #endregion[closed]

        #region[details method to return view,explaining view name]
        //public ViewResult Details()//viewResult is defined in Controller class
        //{
        //    Employee model = _employeeRepository.GetEmployee(3);
        //    return View(model);//we are only passing data, not view name so default view will be rendered.
        //    //return View();//will not send any model data but corrsponding details.cshtml is rendered.
        //    //these are the way controllers selecting view and passing model to view
        //    //return View("../../OtherViews/Demo");// curretly in home folder of views, ..will take from home to views,.. will take
        //    //from views to CoreApiEmpty then OtherViews then Demo.no extension used in relative path
        //    //"MyViews/test.cshtml" is same as "/MyViews/test.cshtml" or "~/MyViews/test.cshtml"
        //    //return View("MyViews/test.cshtml");//this is absolute path so extension cshtml is  used, view name is passed as path
        //    //return View("Test");//Test is passes as view name
        //    //return View(model);//corresponfing cshtml file sud be there in views\Home , by default that is rendered
        //}
        #endregion

        #region[explaining passing data to view-ViewData,ViewBag,Strongly Type]
        //public ViewResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(3);
        //    ViewData["Employee"] = model;
        //    ViewData["PageTitle"] = "Employee Details";//these data can be used in details.cshtml or any other view name given in view method
        //    ViewBag.PageTitle = "Employee Details";
        //    ViewBag.Employee = model;
        //    return View(model);//if we declare this model at top of view using @model , it will create strongly typed view
        //                        //means we will get intellisense support.
        //}
        #endregion

        #region[ViewModel to contain data other than model object]
        //public ViewResult Details()
        //{
        //    HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        //    {
        //        Employee = _employeeRepository.GetEmployee(3),
        //        PageTitle = "Employee List"
        //    };
        //    return View("../../OtherViews/checking", homeDetailsViewModel);//this viewModel object is used when view need data apart from model
        //    //1st param is vie name , we are not using default view name details inside views/home/details.cshtml
        //}
        #endregion

        #region[list View]
        //public ViewResult Details()
        //{
        //    var model = _employeeRepository.GetAllEmployees();
        //    return View("../../MyViews/list", model);//model is IEnumerate type so used same type at top of view using @model
        //}
        #endregion

        #region
        public ViewResult MoreDetails()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }
        #endregion
    }
}
