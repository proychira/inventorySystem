using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace inventorySystem.Pages.Siam
{
	public class CreateSiamModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {
        }


        public void OnPost()
        {
            stockInfo.item = Request.Form["item"];
            stockInfo.storeid = Request.Form["storeid"];
            stockInfo.supplier = Request.Form["supplier"];
            stockInfo.amount = Request.Form["amount"];

            if (stockInfo.itemid.Length == 0 || stockInfo.item.Length == 0 ||
                stockInfo.storeid.Length == 0 || stockInfo.supplier.Length == 0 ||
                stockInfo.amount.Length == 0)
            {
                errorMessage = "All the file are required";
                return;
            }

            try
            {
                String connectionString = "Server=tcp:inventory-sever-1650707563.database.windows.net,1433;Initial Catalog=inventory-1650707563;Persist Security Info=False;User ID=proychira;Password=Proy1212312121;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO stocks " +
                                 "(item, storeid, supplier, amount) VALUES" +
                                 "(@item, @storeid, @supplier, @amount);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
                        command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
                        command.Parameters.AddWithValue("@amount", stockInfo.amount);

                        command.ExecuteNonQuery();
                         
                         

                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            stockInfo.item = "";
            stockInfo.storeid = "";
            stockInfo.supplier = "";
            stockInfo.amount = "";
            successMessage = "New item Added Correctly";

            Response.Redirect("/Siam/IndexSiam");
        }

    }
}
