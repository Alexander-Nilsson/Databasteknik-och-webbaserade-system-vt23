using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Laboration_3___Databasdriven_webbapplikation.Models {
    public class PersonMetoder {

        public PersonMetoder() { }

        public int InsertPerson(PersonDetalj pd, out string errormsg) {


            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "INSERT INTO Person (Fornamn, Efternamn, Fodelsear, Epost, Bor) VALUES (@fornamn, @efternamn, @fodelsear, @epost, @bor)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("fornamn", SqlDbType.NVarChar, 30).Value = pd.Fornamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 30).Value = pd.Efternamn;
            dbCommand.Parameters.Add("epost", SqlDbType.NVarChar, 50).Value = pd.Epost;
            dbCommand.Parameters.Add("fodelsear", SqlDbType.Int).Value = pd.Fodelsear;
            dbCommand.Parameters.Add("bor", SqlDbType.Int).Value = pd.Bor;

            try {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapas inte en person i databasen.";}
                return (i);
            }

            catch (Exception e) { 
                errormsg= e.Message;
                return 0;
            }
            finally { dbConnection.Close(); }
        }


        public int DeletePerson(out string errormsg) {
            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "DELETE FROM Person WHERE Efternamn = Karlsson";

            // TODO change sql statement so it matches: DELETE FROM table_name WHERE condition;

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);


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



            public List<PersonDetalj> GetPersonWithDataSet(out string errormsg){

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM Person";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);


            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<PersonDetalj> PersonList = new List<PersonDetalj>();
            
            try {
                dbConnection.Open();

                myAdapter.Fill(myDS,"myPerson");

                int count = 0;
                int i = 0;
                
                count = myDS.Tables["myPerson"].Rows.Count;


                if (count > 0) { 
                    while (i < count) {
                        PersonDetalj pd = new PersonDetalj();
                        
                        pd.Fornamn = myDS.Tables["myPerson"].Rows[i]["Fornamn"].ToString();
                        pd.Efternamn = myDS.Tables["myPerson"].Rows[i]["Efternamn"].ToString();
                        pd.Epost = myDS.Tables["myPerson"].Rows[i]["Epost"].ToString();
                        pd.Bor = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Bor"]);
                        pd.Fodelsear = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Fodelsear"]);
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


        public List<PersonDetalj> GetPersonWithDataReader(out string errormsg) {

            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM Person";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);


            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader reader= null;

            List<PersonDetalj> PersonList = new List<PersonDetalj>();

            errormsg = "";

            try {
   
                    // open the connection
                    dbConnection.Open();
                    // 1. get an instance of the SqlDataReader
                    reader = dbCommand.ExecuteReader();
                // 2. read necessary columns of I each record
                while (reader.Read()) {

                    PersonDetalj Person = new PersonDetalj();
                    Person.Fornamn = reader["Fornam"].ToString();
                    Person.Efternamn = reader["Efternamn"].ToString();
                    Person.Epost = reader["Epost"].ToString();
                    Person.Bor = Convert.ToInt16(reader["Bor"]);
                    Person.Fodelsear = Convert.ToInt16(reader["Fodelsear"]);
                    Person.Id = Convert.ToInt16(reader["Id"]);
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
}
