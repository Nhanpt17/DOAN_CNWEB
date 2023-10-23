using BACKEND_APIQLCAFFE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BACKEND_APIQLCAFFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ThucDonController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

    

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select thucdon_id,ten_mon,don_gia,loai_id from thuc_don";
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
            return new JsonResult(table);
        }

        


        [HttpPost]
        public JsonResult Post(ThucDon thucdon)
        {
            string query = @"Insert into thuc_don(ten_mon,images,don_gia,loai_id) values 
                             (
                                N'"+ thucdon.TenMon+"',"+
                                "'"+ thucdon.Images+"',"+
                                thucdon.DonGia+","+
                                thucdon.Id_Loai +
                                ")";



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
        public JsonResult Put(ThucDon thucdon)
        {

            string query = @"Update thuc_don set ten_mon ='" + thucdon.TenMon + 
                "',images ='" + thucdon.Images +
                "',don_gia ="+ thucdon.DonGia +
                ",loai_id ="+thucdon.Id_Loai +
                " where thucdon_id =" + 
                    thucdon.Id;
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
            string query = @"Delete From thuc_don where thucdon_id=" + id;
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {

            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("com.jpg");
            }
        }

        [Route("GetAllLoaiMonAn")]
        [HttpGet]
        public JsonResult GetAllLoaiMonAn()
        {
            string query = "Select ten_loai from loai";
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
            return new JsonResult(table);
        }
    }
}
