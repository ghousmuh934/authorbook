using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using AuthorBooksBcs6CWebApplication1.Models;
namespace AuthorBooksBcs6CWebApplication1.Controllers
{
    public class BookController : Controller
    {
         string constr = "Data Source=DESKTOP-FMO6LUK;Initial Catalog=AuthorBook;Integrated Security=True";
        // GET: Book
        [HttpGet]
        public ActionResult BookEntry()
        {
            return View();
        }
        //For data insertion in book table 
        [HttpPost]
        public ActionResult BookEntry(Book a)
        {         
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //Response.Write("<script> alert('Connect with server!');</script>");
            string q = "insert into Books (title,category,publishyear,aid) values ('" + a.title + "','" + a.category + "','" + a.publishyear + "',"+a.aid+")";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script> alert('Record saved!');</script>");
            return RedirectToAction("BooksRecords");
        }

        //For showing book data from database to view
        private List<Book> getBook()
        {
            List<Book> alist = new List<Book>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q = "Select bid,title,category,publishyear,aid from books";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book b = new Book();
                b.bid = int.Parse(sdr["bid"].ToString());
                b.title = sdr["title"].ToString();
                b.category = sdr["category"].ToString();
                b.publishyear = int.Parse(sdr["publishyear"].ToString());
                b.aid = int.Parse(sdr["aid"].ToString());
                alist.Add(b);
            }

            con.Close();
            return alist;
        }
        [HttpGet]
        public ActionResult BooksRecords()
        {
            List<Book> bk = getBook();
            return View(bk);
        }
        private List<Book> getBook(string cat)
        {
            List<Book> alist = new List<Book>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q = "Select bid,title,category,publishyear from books where category='" + cat + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book b = new Book();
                b.bid = int.Parse(sdr["bid"].ToString());
                b.title = sdr["title"].ToString();
                b.category = sdr["category"].ToString();
                b.publishyear = int.Parse(sdr["publishyear"].ToString());
                
                alist.Add(b);
            }

            con.Close();
            return alist;
        }
        [HttpPost]
        public ActionResult BooksRecords(string cattb)
        {
            List<Book> bk = getBook(cattb);
            return View(bk);
        }
        

    }
}