using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;
using AuthorBooksBcs6CWebApplication1.Models;
                     //***************After Mid Work****************************************
namespace AuthorBooksBcs6CWebApplication1.Controllers
{               
    public class BooksController : Controller
    {
        // GET: Books
        string constr = "Data Source=DESKTOP-FMO6LUK;Initial Catalog=AuthorBook;Integrated Security=True";
        //private List<SelectListItem> getCat()
        //{
        //    List<SelectListItem> bookCat = new List<SelectListItem>();
        //    SqlConnection con = new SqlConnection(constr);
        //    con.Open();
        //    string q;
            
        //     q = "select distinct category  from Books";
            
        //    SqlCommand cmd = new SqlCommand(q, con);
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    while (sdr.Read())
        //    {
        //        bookCat.Add(new SelectListItem { Text = sdr[0].ToString(), Value = sdr[0].ToString() });
        //    }
        //    sdr.Close();
        //    con.Close();
          
            
        //    return bookCat;
        //}
        //private List<Book> getBook(string cat)
        //{
        //    List<Book> alist = new List<Book>();
        //    SqlConnection con = new SqlConnection(constr);
        //    con.Open();
        //    string q;
        //    if (!String.IsNullOrEmpty(cat))
        //    {
        //        q = "select b.bid,b.title,b.category,b.publishyear,b.aid,a.name from author a inner join books b on a.aid=b.aid where b.category = '" + cat + "'";
        //    }
        //    else
        //    {
        //        q = "select b.bid,b.title,b.category,b.publishyear,b.aid,a.name from author a inner join books b on a.aid=b.aid";
        //    }
        //    SqlCommand cmd = new SqlCommand(q, con);
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    while (sdr.Read())
        //    {
        //        Book b = new Book();
        //        b.bid = int.Parse(sdr["bid"].ToString());
        //        b.title = sdr["title"].ToString();
        //        b.category = sdr["category"].ToString();
        //        b.publishyear = int.Parse(sdr["publishyear"].ToString());
        //        b.aid = int.Parse(sdr["aid"].ToString());
        //        b.name = sdr["name"].ToString();

        //        alist.Add(b);
        //    }

        //    con.Close();
        //    return alist;
        //}



        //[HttpGet]
        //public ActionResult BooksSearch(String cattb, int? page)
        //{
        //    ViewBag.Cat = getCat();
        //    List<Book> bs = getBook(cattb);
        //    return View(bs.ToPagedList(page ?? 1, 3));    //one represent from where it start and 3 how many record on one page show

        //}


        private List<Book> getBook1(string cont, string SearchBy)
        {
            List<Book> alist = new List<Book>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string q;
            if (!String.IsNullOrEmpty(cont))
            {
                //q = "select * from books b inner join author a on a.aid=b.bid where "+ SearchBy + " like '"+ cont + "%'";
                q = "select name,gender,email from author where "+ SearchBy + " like '" + cont + "%'";
            }
            else
            {
                //q = "select * from books b inner join author a on a.aid=b.bid ";
                q = "select name,gender,email from author";
            }


            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book b = new Book();
                //b.bid = int.Parse(sdr["bid"].ToString());
                //b.title = sdr["title"].ToString();
               // b.category = sdr["category"].ToString();
                //b.publishyear = int.Parse(sdr["publishyear"].ToString());
                //b.aid = int.Parse(sdr["aid"].ToString());
                b.name = sdr["name"].ToString();
                //b.country = sdr["country"].ToString();
                b.Gender = sdr["gender"].ToString();
                b.email = sdr["email"].ToString();
                alist.Add(b);
            }
            con.Close();
            return alist;
        }


        [HttpGet]
        public ActionResult BooksSearch(string cont, string SearchBy, int? page)
        {
            List<Book> bs = getBook1(cont, SearchBy);

            return View(bs.ToPagedList(page ?? 1, 3));    //one represent from where it start and 3 how many record on one page show

        }
    }
}