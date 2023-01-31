using System.Data.SqlClient;
using System.Data;
using NotBlocket2.Models;

namespace NotBlocket2.Models {
    public class Ad {

        public Ad() { }

        //Publika egenskaper
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
    }

    public class AdMethods {

        public AdMethods() { }

        public List<Ad> GetAdsWithDataSet(out string errormsg) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM [NotBlocket].[dbo].[Ads]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();
            List<Ad> AdList = new List<Ad>();

            try {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myAd");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myAd"].Rows.Count;


                if (count > 0) {
                    while (i < count) {
                        Ad pd = new Ad();

                        pd.Name = myDS.Tables["myAd"].Rows[i]["Name"].ToString();
                        pd.Description = myDS.Tables["myAd"].Rows[i]["Description"].ToString();
                        pd.Category = myDS.Tables["myAd"].Rows[i]["Category"].ToString();
                        pd.Price = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Price"]);
                        pd.Id = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Id"]);
                        i++;
                        AdList.Add(pd);
                    }
                    errormsg = "";
                    return AdList;
                }
                else { errormsg = "Det hämtas Ingen person"; }
                return AdList;
            }

            catch (Exception e) {
                errormsg = e.Message;
                return null;
            }

            finally { dbConnection.Close(); }





        }
    }
}


