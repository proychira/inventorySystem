using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace inventorySystem.Pages.Siam
{
    public class IndexSiamModel : PageModel
    {
        public List<StockInfo> listStock = new List<StockInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:inventory-sever-1650707563.database.windows.net,1433;Initial Catalog=inventory-1650707563;Persist Security Info=False;User ID=proychira;Password=Proy1212312121;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE storeid=2";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.itemid = "" + reader.GetInt32(0);
                                stockInfo.item = reader.GetString(1);
                                stockInfo.storeid = reader.GetString(2);
                                stockInfo.supplier = reader.GetString(3);
                                stockInfo.amount = reader.GetString(4);
                                stockInfo.create_at = reader.GetDateTime(5).ToString();

                                listStock.Add(stockInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class StockInfo
    {
        public String itemid;
        public String item;
        public String storeid;
        public String supplier;
        public String amount;
        public String create_at;
    }
}