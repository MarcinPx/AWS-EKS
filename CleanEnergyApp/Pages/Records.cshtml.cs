using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CleanEnergy.Pages
{
    public class RecordsModel : PageModel
    {

        public List<RecordsInfo> records = new List<RecordsInfo>();

        public bool hasData = false;
        public bool tried = false;
        public string username = "";
        public string password = "";

        public class RecordsInfo
        {
            public int PersonID;
            public string FirstName;
            public string LastName;
            public decimal Energy;
            public decimal Size;

        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            
            password = Request.Form["password"];
            username = Request.Form["username"];
            if(username == "Apollo" && password == "Pokaz!mi!calosc2")
            {
                hasData = true;


                string connectionstring = "Server=databaseeksprojectv2.c0l2bn8ji4on.us-east-1.rds.amazonaws.com;Initial Catalog=records;Persist Security Info=False;User ID=admin;Password=Pouy%ter362!we;MultipleActiveResultSets=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Person";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecordsInfo record = new RecordsInfo();
                                record.PersonID = reader.GetInt32(0);
                                record.FirstName = reader.GetString(1);
                                record.LastName = reader.GetString(2);
                                record.Energy = reader.GetDecimal(3);
                                record.Size = reader.GetDecimal(4);
                                records.Add(record);
                                
                            }
                        }

                        connection.Close();
                    }
                }
                       
                
                
            }
            else
            {
                hasData = false;
                tried = true;
            }

        }
    }
}
