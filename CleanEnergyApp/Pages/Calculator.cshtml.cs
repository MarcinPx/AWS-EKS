using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CleanEnergy.Pages
{
    public class CalculatorModel : PageModel
    {
        public bool hasData = false;
        public string firstname = "";
        public string lastname = "";
        public string size = "";


        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;
            firstname = Request.Form["firstname"];
            lastname = Request.Form["lastname"];
            size = Request.Form["size"];

            Double.TryParse(size, out double sizex);
            double result = ((sizex * 0.157 * 970 * 0.82) * 100) / 100;

            string connectionstring = "Server=databaseeksprojectv2.c0l2bn8ji4on.us-east-1.rds.amazonaws.com;Initial Catalog=records;Persist Security Info=False;User ID=admin;Password=Pouy%ter362!we;MultipleActiveResultSets=False;Connection Timeout=30;";

            string sqlQuery = "INSERT INTO Person (FirstName, LastName, Energy, Size) VALUES (" + "'" + firstname + "'" + "," + "'" + lastname + "'" + "," + "'" + result + "'" + "," + "'" + size + "'" + ")";

            SqlConnection con = new SqlConnection(connectionstring);

            con.Open();

            SqlCommand sc = new SqlCommand(sqlQuery, con);

            sc.ExecuteNonQuery();

            con.Close();




        }
    }
}
