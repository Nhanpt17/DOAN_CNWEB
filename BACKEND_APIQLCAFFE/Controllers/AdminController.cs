using BACKEND_APIQLCAFFE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace BACKEND_APIQLCAFFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select admin_id,tk,mk from admins";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("testwebcapfe");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource)) 
            { 
                    myConn.Open();
                using(SqlCommand  myCommand = new SqlCommand(query,myConn)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                } 
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Admin admin)
        {
            string query = @"Insert into admins(tk,mk) values ('" + admin.UserName + "','" + admin.PassWord + "') ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("testwebcapfe");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Them moi thanh cong");
        }

        [HttpPut]
        public JsonResult Put(Admin admin)
        {
           
            string query = @"Update admins set tk ='" + admin.UserName + "', mk ='" + admin.PassWord + "' where admin_id =" + admin.Id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("testwebcapfe");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("cap nhat thanh cong");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"Delete From admins where admin_id=" + id ; 
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("testwebcapfe");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Xoa thanh cong");
        }
    }
}
