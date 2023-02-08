using System.Data.SqlClient;
using System.Data;
using NotBlocket2.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;

namespace NotBlocket2.Models {
    public class Ad {

        public Ad() { }

        public int Id { get; set; }
        //Publika egenskaper
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public int Price { get; set; }

        public int? Profile_Id { get; set; }
        public string? ImagePath { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? file { get; set; }
    }

    public class AdMethods {

        public AdMethods() { }

        public int InsertAd(Ad ad, out string errormsg) {
            //Create SQL Connection
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotBlocket;Integrated Security=True;Pooling=False";
            String sqlstring = "INSERT INTO [NotBlocket].[dbo].[Ads] (Name, Description, Category, Price, Profile_Id, ImagePath) VALUES (@Name, @Description, @Category, @Price, @Profile_Id, @ImagePath)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Name", SqlDbType.NVarChar, 30).Value = ad.Name;
            dbCommand.Parameters.Add("Description", SqlDbType.NVarChar, 50).Value = ad.Description ?? (object)DBNull.Value;
            dbCommand.Parameters.Add("Category", SqlDbType.NVarChar, 30).Value = ad.Category ?? (object)DBNull.Value;
            dbCommand.Parameters.Add("Price", SqlDbType.Int).Value = ad.Price;

            dbCommand.Parameters.Add("ImagePath", SqlDbType.NVarChar, 50).Value = ad.ImagePath ?? (object)DBNull.Value;
            dbCommand.Parameters.Add("Profile_Id", SqlDbType.Int).Value = ad.Profile_Id ?? (object)DBNull.Value;



            try {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "The ad was not inserted into the database."; }
                return (i);
            }

            catch (Exception e) {
                errormsg = e.Message;
                return 0;
            }
            finally { dbConnection.Close(); }
        }


        public List<Ad> GetAdsWithDataSet(out string errormsg, in string filterByCategorystring = null) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotBlocket;Integrated Security=True";

            String sqlstring = "SELECT * FROM [NotBlocket].[dbo].[Ads]";

            if (filterByCategorystring != null) {
                sqlstring = sqlstring + "WHERE [NotBlocket].[dbo].[Ads].[Category] = '" + filterByCategorystring + "'";
            };

            errormsg = sqlstring;
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
                        pd.ImagePath = myDS.Tables["myAd"].Rows[i]["ImagePath"].ToString();
                        pd.Category = myDS.Tables["myAd"].Rows[i]["Category"].ToString();
                        pd.Price = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Price"]);
                        pd.Id = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Id"]);
                        i++;
                        AdList.Add(pd);
                    }
                    errormsg = "";
                    return AdList;
                }
                else { errormsg = errormsg + "Det hämtas Ingen ad"; }
                return AdList;
            }

            catch (Exception e) {
                errormsg = e.Message;
                return null;
            }

            finally { dbConnection.Close(); }

        }

        public List<Ad> GetAdsWithDataSet2(string sortString, string searchstring, out string errormsg) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotBlocket;Integrated Security=True";


            //add ASC vs DESC?
        
            String sqlstring = @"
                                SELECT *
                                FROM [NotBlocket].[dbo].[Ads]
                                WHERE [NotBlocket].[dbo].[Ads].[Name] LIKE '%'+'" + searchstring + @"'+'%'
                                ORDER BY 
                                    [NotBlocket].[dbo].[Ads].[" + sortString + "] ASC";



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
                        pd.ImagePath = myDS.Tables["myAd"].Rows[i]["ImagePath"].ToString();
                        pd.Price = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Price"]);
                        pd.Id = Convert.ToInt32(myDS.Tables["myAd"].Rows[i]["Id"]);
                        i++;
                        AdList.Add(pd);
                    }
                    errormsg = "";
                    return AdList;
                }
                else { errormsg = "There are no results for your search"; }
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


