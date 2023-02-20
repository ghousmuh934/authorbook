using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBooksBcs6CWebApplication1.Models;
using System.Data.SqlClient;
namespace AuthorBooksBcs6CWebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        string constr = "Data Source=DESKTOP-FMO6LUK;Initial Catalog=AuthorBook;Integrated Security=True";
        // GET: Author
        public ActionResult AuthorDetails()
        {
            //Filling author Object using C# Object Initializer Syntax
            Author author = new Author()
            {
                aid = 1,
                Name = "Ali",
                Country = "Pakistan",
                Gender = "Male",
                Married = true
            };
            //Passing author Object to AuthorDetails.cshtml
            return View(author);
        }
        public ActionResult AuthorsList()
        {
            //Creating 3 author Objects using C# Object Initializer Syntax
            Author author1 = new Author()
            {
                aid = 1,
                Name = "Ali",
                Country = "Pakistan",
                Gender = "Male",
                Married = true
            };
            Author author2 = new Author
            {
                aid = 2,
                Name = "Aliza",
                Country = "Pakistan",
                Gender = "Feale",
                Married = true
            };
            Author author3 = new Author
            {
                aid = 3,
                Name = "Umer",
                Country = "BD",
                Gender = "Male",
                Married = false
            };
            List<Author> AuthorsList = new List<Author>() { author1, author2, author3 };
            return View(AuthorsList);
        }
        private List<SelectListItem> getCountries()
        {
            List<SelectListItem> bookTypeList = new List<SelectListItem>();
            bookTypeList.Add(new SelectListItem { Text = "Pakistan", Value = "Pakstan" });
            bookTypeList.Add(new SelectListItem { Text = "India", Value = "India" });
            bookTypeList.Add(new SelectListItem { Text = "Afghanistan", Value = "Afghanistan" });
            return bookTypeList;
        }
        [HttpGet]
        public ActionResult authorEntry()
        {
            ViewBag.Countries = getCountries();
            return View();
        }
        [HttpPost]
        public ActionResult authorEntry(Author a)
        {
            string msta = "";
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //Response.Write("<script> alert('Connect with server!');</script>");
            if (a.Married == true)
            {
                msta = "Yes";
            }
            else
            {
                msta = "No";
            }
            string query = "insert into author(name,country,Gender,mstaus) values('" + a.Name + "','" + a.Country + "','" + a.Gender + "','" + msta + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script> alert('Record saved!');</script>");
            return RedirectToAction("AuthorsRecords");
        }

        private List<Author> getAuthors()
        {
            List<Author> alist = new List<Author>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q = "Select aid,name from author";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Author a = new Author();
                a.aid = int.Parse(sdr["aid"].ToString());
                a.Name = sdr["name"].ToString();
                alist.Add(a);
            }

            con.Close();
            return alist;
        }
        [HttpGet]
        public ActionResult AuthorsRecords()
        {
            List<Author> al = getAuthors();
            return View(al);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q = "Select aid,name,country,gender,mstaus from author where aid='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Author a = new Author();
            a.aid = int.Parse(sdr["aid"].ToString());
            a.Name = sdr["name"].ToString();
            a.Country = sdr["country"].ToString();
            if (sdr["gender"].ToString() == "M")
                a.Gender = "Male";
            else
                a.Gender = "Female";

            if (sdr["mstaus"].ToString() == "Yes")
                a.Married = true;
            else
                a.Married = false;

            sdr.Close();
            con.Close();

            return View(a);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from author where aid ='" + id + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AuthorsRecords");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q = "Select aid,name,country,gender from author where aid ='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Author a = new Author();
            a.aid = int.Parse(sdr["aid"].ToString());
            a.Name = sdr["name"].ToString();
            a.Country = sdr["country"].ToString();
            a.Gender = sdr["gender"].ToString();
            sdr.Close();
            con.Close();

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(Author a)
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("update author set name='" + a.Name + "',country='" + a.Country + "',gender='" + a.Gender + "' where aid ='" + a.aid + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AuthorsRecords");
        }
       
    }
}