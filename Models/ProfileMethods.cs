using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


namespace NotBlocket2.Models {
    public class ProfileMethods {

        public ProfileMethods() { }

        public int InsertProfile(Profile pd, out string errormsg) {
            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotBlocket;Integrated Security=True;Pooling=False";
            String sqlstring = "INSERT INTO [NotBlocket].[dbo].[Profiles] (Name, Email, Password) VALUES (@Name, @Email, @Password)";
            // For now no way of adding location, 


            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Name", SqlDbType.NVarChar, 30).Value = pd.Name;
            dbCommand.Parameters.Add("Email", SqlDbType.NVarChar, 50).Value = pd.Email;
            dbCommand.Parameters.Add("Password", SqlDbType.NVarChar, 50).Value = pd.Password;


            try {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapas inte en person i databasen."; }
                return (i);
            }

            catch (Exception e) {
                errormsg = e.Message;
                return 0;
            }
            finally { dbConnection.Close(); }
        }


        public List<Profile> GetPersonWithDataSet(out string errormsg) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM [NotBlocket].[dbo].[Profiles]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);


            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<Profile> PersonList = new List<Profile>();

            try {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPerson");

                int count = 0;
                int i = 0;

                count = myDS.Tables["myPerson"].Rows.Count;


                if (count > 0) {
                    while (i < count) {
                        Profile pd = new Profile();

                        pd.Name = myDS.Tables["myPerson"].Rows[i]["Name"].ToString();
                        pd.Email = myDS.Tables["myPerson"].Rows[i]["Email"].ToString();
                        pd.Password = myDS.Tables["myPerson"].Rows[i]["Password"].ToString();
                        pd.Id = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Id"]);
                        i++;
                        PersonList.Add(pd);
                    }
                    errormsg = "";
                    return PersonList;
                }
                else { errormsg = "Det hämtas Ingen person"; }
                return PersonList;
            }

            catch (Exception e) {
                errormsg = e.Message;
                return null;
            }

            finally { dbConnection.Close(); }




        }


    }

}
