using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanteWebController : ControllerBase
    {
        private IConfiguration _configuraction;

        public DanteWebController(IConfiguration configuraction)
        {
            _configuraction = configuraction;
        }

        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            string query = "select * from dbo.usersDB";
            DataTable table = new DataTable();
            string sqlDatasource = _configuraction.GetConnectionString("DanteWebDBCon");
            SqlDataReader myReader;
            using(SqlConnection myCon= new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }

        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] string userEmail, [FromForm] string userLogin, [FromForm] string userPassword)
        {
            string query = "insert into dbo.usersDB (email,login,password) values(@userEmail, @userLogin, @userPassword)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuraction.GetConnectionString("DanteWebDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@userEmail", userEmail);
                    myCommand.Parameters.AddWithValue("@userlogin", userLogin);
                    myCommand.Parameters.AddWithValue("@userpassword", userPassword);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Sucessfully");

        }

        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult DeleteNotes(int id)
        {
            string query = "delete from dbo.usersDB where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuraction.GetConnectionString("DanteWebDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Sucessfully");

        }


        [HttpPost]
        [Route("AddOrder")]
        public JsonResult AddOrder([FromForm] int userId,[FromForm] string orderId, [FromForm] string orderDetails, [FromForm] DateTime orderDate)
        {
            string query = "insert into dbo.ordersDB (user_id, order_id, order_details, order_date) values(@userId, @orderId, @orderDetails, @orderDate)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuraction.GetConnectionString("DanteWebDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@userId", userId);
                    myCommand.Parameters.AddWithValue("@orderId", orderId);
                    myCommand.Parameters.AddWithValue("@orderDetails", orderDetails);
                    myCommand.Parameters.AddWithValue("@orderDate", orderDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Sucessfully");

        }
    }
}
