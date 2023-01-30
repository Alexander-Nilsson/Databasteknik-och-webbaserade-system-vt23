using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace NotBlocket2.Models {
    public class Profile {

        //Konstruktor
        public Profile() {
            Name = "";
            Email = "";
            Password = "";
        }

        //Publika egenskaper
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Location_Id { get; set; }
        public int Id { get; set; }


        public int InsertPerson(Profile pd, out string errormsg) {
            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotBlocket;Integrated Security=True;Pooling=False";
            String sqlstring = "INSERT INTO Profiles (Name, Email, Password ) VALUES (@Name, @Email, @Password)";
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

        public List<Profile> GetProfileWithDataReader(out string errormsg) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM Person";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);


            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader reader = null;

            List<Profile> PersonList = new List<Profile>();

            errormsg = "";

            try {

                // open the connection
                dbConnection.Open();
                // 1. get an instance of the SqlDataReader
                reader = dbCommand.ExecuteReader();
                // 2. read necessary columns of I each record
                while (reader.Read()) {

                    Profile Person = new Profile();
                    Person.Id = Convert.ToInt16(reader["Id"]);
                    Person.Name = reader["Name"].ToString();
                    Person.Location_Id = Convert.ToInt16(reader["Location_Id"]);
                    Person.Email = reader["Email"].ToString();
                    Person.Password = reader["Password"].ToString();
                    
                    PersonList.Add(Person);
                }
                reader.Close();
                return PersonList;

            }

            catch (Exception e) {
                errormsg = e.Message;
                return null;
            }

            finally { dbConnection.Close(); }
        }





    }
    

    // TODO: Create Methods to add new profiles to the table
    // Admin view of all profiles
    // 


}
