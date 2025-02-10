using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    internal class Class
    {
        static String connString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        static SqlConnection myConn = new SqlConnection(connString);

        static System.Data.SqlClient.SqlCommand cmdString = new System.Data.SqlClient.SqlCommand();


        public void AddPatient(string Fname, string M_I, string LName, string DOB, string Gender, string Phone, string STREET, string CITY, string STATE_ADD, string ZIP, string COUNTRY, string Insurance)
        {


            try

            {
                // open connection 

                myConn.Open();

                //clear any parameters 

                cmdString.Parameters.Clear();

                // command 

                cmdString.Connection = myConn;

                cmdString.CommandType = CommandType.StoredProcedure;

                cmdString.CommandTimeout = 1500;

                cmdString.CommandText = "AddPatient";

                //        Define input parameter 

                bool IsAllLetters(string input)
                {
                    foreach (char c in input)
                    {
                        if (!char.IsLetter(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }

                bool IsLettersDigitsOrSpaces(string input)
                {
                    foreach (char c in input)
                    {
                        if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }


                // Check and add parameters
                if (IsAllLetters(Fname))
                {
                    cmdString.Parameters.Add("@Fname", SqlDbType.VarChar, 25).Value = Fname;
                }
                else
                {
                    throw new ArgumentException("Invalid format for First Name (Use only Letters)");
                }

                if (IsAllLetters(LName))
                {
                    cmdString.Parameters.Add("@Lname", SqlDbType.VarChar, 25).Value = LName;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Last Name (Use only Letters)");
                }

                if (IsAllLetters(M_I))
                {
                    cmdString.Parameters.Add("@M_I", SqlDbType.VarChar, 1).Value = M_I;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Middle Initial (Use only Letters)");
                }

                if (DateTime.TryParse(DOB, out DateTime parsedDOB))
                {
                    cmdString.Parameters.Add("@DOB", SqlDbType.Date).Value = parsedDOB;
                }
                else
                {
                    throw new ArgumentException("Invalid date format for DOB.");
                }

                if (IsAllLetters(Gender))
                {
                    cmdString.Parameters.Add("@Gender", SqlDbType.VarChar, 60).Value = Gender;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Gender (Use only Letters)");
                }

                cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;

                if (IsLettersDigitsOrSpaces(STREET))
                {
                    cmdString.Parameters.Add("@STREET", SqlDbType.VarChar, 60).Value = STREET;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Street (Use only Letters, Numbers, and Spaces)");
                }

                if (IsAllLetters(CITY))
                {
                    cmdString.Parameters.Add("@CITY", SqlDbType.VarChar, 60).Value = CITY;
                }
                else
                {
                    throw new ArgumentException("Invalid format for City (Use only Letters)");
                }

                if (IsAllLetters(STATE_ADD))
                {
                    cmdString.Parameters.Add("@STATE_ADD", SqlDbType.VarChar, 2).Value = STATE_ADD;
                }
                else
                {
                    throw new ArgumentException("Invalid format for State (Use only Letters)");
                }

                cmdString.Parameters.Add("@ZIP", SqlDbType.VarChar, 5).Value = ZIP;

                if (IsAllLetters(COUNTRY))
                {
                    cmdString.Parameters.Add("@COUNTRY", SqlDbType.VarChar, 3).Value = COUNTRY;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Country (Use only Letters)");
                }

                if (IsAllLetters(Insurance) && (Insurance == "Yes" || Insurance == "No" || Insurance == "Y" || Insurance == "N"))
                {
                    cmdString.Parameters.Add("@Insurance", SqlDbType.VarChar, 3).Value = Insurance;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Insurance Yes, No, Y, N");
                }


                cmdString.ExecuteNonQuery();

            }

            catch (Exception ex)

            {
                throw new ArgumentException(ex.Message);

            }

            finally

            {

                myConn.Close();

            }

        }

        public void AddPhysician(string Fname, string LName, string Email, string Phone)
        {

            bool IsAllLetters(string input)
            {
                foreach (char c in input)
                {
                    if (!char.IsLetter(c))
                    {
                        return false;
                    }
                }
                return true;
            }


            bool IsValidEmailFormat(string input)
            {
                foreach (char c in input)
                {
                    if (!(char.IsLetterOrDigit(c) || c == '@' || c == '.' || c == '-' || c == '_'))
                    {
                        return false;
                    }
                }
                return input.Contains('@');
            }

            try
            {
                // Open connection
                myConn.Open();

                // Clear any parameters
                cmdString.Parameters.Clear();

                // Command setup
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "AddPhysician";

                // Validate and add parameters
                if (IsAllLetters(Fname))
                {
                    cmdString.Parameters.Add("@Fname", SqlDbType.VarChar, 50).Value = Fname;
                }
                else
                {
                    throw new ArgumentException("Invalid format for First Name (Use only Letters)");
                }

                if (IsAllLetters(LName))
                {
                    cmdString.Parameters.Add("@Lname", SqlDbType.VarChar, 50).Value = LName;
                }
                else
                {
                    throw new ArgumentException("Invalid format for Last Name (Use only Letters)");
                }

                if (IsValidEmailFormat(Email))
                {
                    cmdString.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Email;
                }
                else if (!string.IsNullOrEmpty(Email))
                {
                    throw new ArgumentException("Invalid format for Email (Use only Letters, Numbers, '@', '.', '-', and '_')");
                }
                else
                {
                    cmdString.Parameters.Add("@EMAIL", SqlDbType.VarChar, 100).Value = DBNull.Value;
                }

                cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = Phone;


                cmdString.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }

        public bool AddPrescription(string patientID, string physicianID, string medName, string dosage, string intMethod, int refillsLeft, DateTime initialRefillDate)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("AddPrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@PhysicianID", physicianID);
                    cmd.Parameters.AddWithValue("@MedName", medName);
                    cmd.Parameters.AddWithValue("@Dosage", dosage);
                    cmd.Parameters.AddWithValue("@IntMethod", intMethod);
                    cmd.Parameters.AddWithValue("@RefillsLeft", refillsLeft);
                    cmd.Parameters.AddWithValue("@InitialRefillDate", initialRefillDate);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the insert was successful
                }
            }
        }





        public bool CheckPatID(string patid)
        {
            try
            {

                // myConn.Open();

                // Clear parameters and set up command
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.Text;

                cmdString.CommandText = @"SELECT COUNT(1) FROM PATIENTS WHERE PATIENTID = @patientId";
                cmdString.Parameters.Add("@patientId", SqlDbType.VarChar, 25).Value = patid;

                int exists = (int)cmdString.ExecuteScalar();
                if (exists > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = "An error occurred. Make sure you enter a valid patient ID. Dev notes: " + ex.Message;

                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                return false;
            }
            //finally
            //{ 
            //    myConn.Close(); 
            //}
        }


        public void UpdatePatient(string patientId, string Fname, string M_I, string LName, string DOB, string Gender, string Phone, string STREET, string CITY, string STATE_ADD, string ZIP, string COUNTRY, string Insurance)
        {
            bool IsAllLetters(string input)
            {
                foreach (char c in input)
                {
                    if (!char.IsLetter(c))
                    {
                        return false;
                    }
                }
                return true;
            }

            bool IsLettersDigitsOrSpaces(string input)
            {
                foreach (char c in input)
                {
                    if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                    {
                        return false;
                    }
                }
                return true;
            }

            try
            {
                // Open connection
                myConn.Open();

                // Clear any parameters
                cmdString.Parameters.Clear();

                // Command configuration
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "UpdatePatient";

                // Validate and add parameters
                if (!string.IsNullOrEmpty(patientId) && CheckPatID(patientId))
                {
                    // Clear any parameters
                    cmdString.Parameters.Clear();

                    // Command configuration
                    cmdString.Connection = myConn;
                    cmdString.CommandType = CommandType.StoredProcedure;
                    cmdString.CommandTimeout = 1500;
                    cmdString.CommandText = "UpdatePatient";
                    cmdString.Parameters.Add("@patientId", SqlDbType.VarChar, 25).Value = patientId;
                }
                else if (string.IsNullOrEmpty(patientId))
                {
                    throw new ArgumentException("Patient ID cannot be empty or must exist");
                }

                if (!string.IsNullOrEmpty(Fname) && IsAllLetters(Fname))
                {
                    cmdString.Parameters.Add("@Fname", SqlDbType.VarChar, 25).Value = Fname;
                }
                else if (!string.IsNullOrEmpty(Fname))
                {
                    throw new ArgumentException("First name must contain only letters.");
                }

                if (!string.IsNullOrEmpty(LName) && IsAllLetters(LName))
                {
                    cmdString.Parameters.Add("@Lname", SqlDbType.VarChar, 25).Value = LName;
                }
                else if (!string.IsNullOrEmpty(LName))
                {
                    throw new ArgumentException("Last name must contain only letters.");
                }

                if (!string.IsNullOrEmpty(M_I) && M_I.Length == 1 && char.IsLetter(M_I[0]))
                {
                    cmdString.Parameters.Add("@M_I", SqlDbType.VarChar, 1).Value = M_I;
                }
                else if (!string.IsNullOrEmpty(M_I))
                {
                    throw new ArgumentException("Middle initial must be a single letter.");
                }

                if (DateTime.TryParse(DOB, out DateTime parsedDOB))
                {
                    cmdString.Parameters.Add("@DOB", SqlDbType.Date).Value = parsedDOB;
                }
                else if (!string.IsNullOrEmpty(DOB))
                {
                    throw new ArgumentException("Invalid date format for DOB.");
                }

                if (!string.IsNullOrEmpty(Gender) && (Gender == "Male" || Gender == "Female"))
                {
                    cmdString.Parameters.Add("@Gender", SqlDbType.VarChar, 6).Value = Gender;
                }
                else if (!string.IsNullOrEmpty(Gender))
                {
                    throw new ArgumentException("Gender must be 'Male' or 'Female'.");
                }

                if (!string.IsNullOrEmpty(Phone) && IsLettersDigitsOrSpaces(Phone))
                {
                    cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;
                }

                if (!string.IsNullOrEmpty(STREET))
                {
                    cmdString.Parameters.Add("@STREET", SqlDbType.VarChar, 60).Value = STREET;
                }

                if (!string.IsNullOrEmpty(CITY) && IsAllLetters(CITY))
                {
                    cmdString.Parameters.Add("@CITY", SqlDbType.VarChar, 60).Value = CITY;
                }
                else if (!string.IsNullOrEmpty(CITY))
                {
                    throw new ArgumentException("City name must contain only letters.");
                }

                if (!string.IsNullOrEmpty(STATE_ADD) && STATE_ADD.Length == 2 && IsAllLetters(STATE_ADD))
                {
                    cmdString.Parameters.Add("@STATE_ADD", SqlDbType.VarChar, 2).Value = STATE_ADD;
                }
                else if (!string.IsNullOrEmpty(STATE_ADD))
                {
                    throw new ArgumentException("State abbreviation must be two letters.");
                }

                if (!string.IsNullOrEmpty(ZIP) && ZIP.All(char.IsDigit) && ZIP.Length == 5)
                {
                    cmdString.Parameters.Add("@ZIP", SqlDbType.VarChar, 5).Value = ZIP;
                }
                else if (!string.IsNullOrEmpty(ZIP))
                {
                    throw new ArgumentException("ZIP code must contain only 5 digits.");
                }

                if (!string.IsNullOrEmpty(COUNTRY) && IsAllLetters(COUNTRY) && COUNTRY.Length == 3)
                {
                    cmdString.Parameters.Add("@COUNTRY", SqlDbType.VarChar, 3).Value = COUNTRY;
                }
                else if (!string.IsNullOrEmpty(COUNTRY))
                {
                    throw new ArgumentException("Country must contain only 3 letters.");
                }

                if (!string.IsNullOrEmpty(Insurance) && IsAllLetters(Insurance) && (Insurance == "Yes" || Insurance == "No" || Insurance == "Y" || Insurance == "N"))
                {
                    cmdString.Parameters.Add("@Insurance", SqlDbType.VarChar, 3).Value = Insurance;
                }
                else if (!string.IsNullOrEmpty(Insurance))
                {
                    throw new ArgumentException("Insurance must contain only 3 letters (Yes or No).");
                }

                cmdString.ExecuteNonQuery();
                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Updated Patient');", true);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
                }
            }
            finally
            {
                myConn.Close();
            }
        }

        public bool UpdatePrescription(string prescriptionID, string medName, string dosage, string intMethod, int? refillsLeft)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);
                    cmd.Parameters.AddWithValue("@MedName", string.IsNullOrWhiteSpace(medName) ? (object)DBNull.Value : medName);
                    cmd.Parameters.AddWithValue("@Dosage", string.IsNullOrWhiteSpace(dosage) ? (object)DBNull.Value : dosage);
                    cmd.Parameters.AddWithValue("@IntMethod", string.IsNullOrWhiteSpace(intMethod) ? (object)DBNull.Value : intMethod);
                    cmd.Parameters.AddWithValue("@RefillsLeft", refillsLeft.HasValue ? (object)refillsLeft.Value : DBNull.Value);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if the update was successful
                }
            }
        }



        public DataSet SearchPatient(string searchTerm)
        {
            try
            {
                // Open connection
                myConn.Open();

                // Clear any parameters
                cmdString.Parameters.Clear();

                // Set up command for stored procedure
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandText = "SearchPatient";

                // Parse search term
                string[] terms = searchTerm.Trim().Split(' ');

                // Add parameters for first and last name
                if (terms.Length > 0)
                {
                    cmdString.Parameters.AddWithValue("@FName", terms[0]);
                }
                else
                {
                    cmdString.Parameters.AddWithValue("@FName", DBNull.Value);
                }

                if (terms.Length > 1)
                {
                    cmdString.Parameters.AddWithValue("@LName", terms[1]);
                }
                else
                {
                    cmdString.Parameters.AddWithValue("@LName", DBNull.Value);
                }

                // Set up adapter and dataset
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdString;

                DataSet dataSet = new DataSet();

                // Fill adapter
                adapter.Fill(dataSet);

                // Return dataset
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }


        public DataSet SearchPhysician(string searchTerm)
        {
            try
            {
                // Open connection
                myConn.Open();

                // Clear any parameters
                cmdString.Parameters.Clear();

                // Set up command for stored procedure
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandText = "SearchPhysician";

                // Parse search term
                string[] terms = searchTerm.Trim().Split(' ');

                // Add parameters for first and last name
                if (terms.Length > 0)
                {
                    cmdString.Parameters.AddWithValue("@FName", terms[0]);
                }
                else
                {
                    cmdString.Parameters.AddWithValue("@FName", DBNull.Value);
                }

                if (terms.Length > 1)
                {
                    cmdString.Parameters.AddWithValue("@LName", terms[1]);
                }
                else
                {
                    cmdString.Parameters.AddWithValue("@LName", DBNull.Value);
                }

                // Set up adapter and dataset
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdString;

                DataSet dataSet = new DataSet();

                // Fill adapter
                adapter.Fill(dataSet);

                // Return dataset
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }


        public DataSet LoadRefills(string prescriptionId)
        {
            try
            {

                myConn.Open();

                // Clear parameters and set up command
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.Text;
                cmdString.CommandText = @"
                SELECT R.*, P.RefillsLeft
                FROM Refills AS R
                INNER JOIN Prescriptions AS P ON R.PrescriptionID = P.PrescriptionID
                WHERE R.PrescriptionID = @PrescriptionID";

                // Add prescription ID parameter
                cmdString.Parameters.AddWithValue("@PrescriptionID", prescriptionId);

                // Set up adapter and dataset
                SqlDataAdapter adapter = new SqlDataAdapter(cmdString);
                DataSet dataSet = new DataSet();

                // Fill the dataset
                adapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception ex)
            {
                string message = "An error occurred. Make sure you enter a valid prescription ID. Dev notes: " + ex.Message;

                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                return null;
            }
            finally
            { myConn.Close(); }
        }



        public void TriggerRefillProcedure(int prescriptionId, string status)
        {
            try
            {

                myConn.Open();



                // Clear parameters and set up for stored procedure
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandText = "dbo.GETREFILLS";

                // Add parameters for the stored procedure
                cmdString.Parameters.AddWithValue("@PrescriptionID", prescriptionId > 0 ? (object)prescriptionId : DBNull.Value);
                cmdString.Parameters.AddWithValue("@status", !string.IsNullOrEmpty(status) ? (object)status : DBNull.Value);

                // Execute the stored procedure
                cmdString.ExecuteNonQuery();

                string message = "Refill pickup recorded successfully.";

                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred: " + ex.Message;

                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + errorMessage + "');", true);
                }
            }
            finally
            { myConn.Close(); }
        }



        public void LoadPatientInfo(string patientID, GridView gridViewPrescriptions, Label lblPatientName, Label lblDOB, Label lblAddress, Label lblPhone)
        {
            // Clear UI elements
            gridViewPrescriptions.DataSource = null;
            gridViewPrescriptions.DataBind(); // Clear any existing data
            lblPatientName.Text = string.Empty;
            lblDOB.Text = string.Empty;
            lblAddress.Text = string.Empty;
            lblPhone.Text = string.Empty;

            try
            {
                // Open the connection inside a using statement to ensure proper disposal
                using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
                {
                    myConn.Open(); // Ensure the connection is open

                    // Create a new command and associate it with the connection
                    using (SqlCommand cmdString = new SqlCommand("GetPatientInfoAndPrescriptions", myConn))
                    {
                        cmdString.CommandType = CommandType.StoredProcedure;
                        cmdString.Parameters.AddWithValue("@PatientID", patientID);

                        using (SqlDataReader reader = cmdString.ExecuteReader())
                        {
                            // Retrieve patient info
                            if (reader.Read())
                            {
                                lblPatientName.Text = $"{reader["FirstName"]} {reader["MiddleInitial"]} {reader["LastName"]}";
                                lblDOB.Text = reader["DOB"].ToString();
                                lblPhone.Text = reader["Phone"].ToString();
                                lblAddress.Text = $"{reader["Street"]}, {reader["City"]}, {reader["State"]} {reader["Zip"]}, {reader["Country"]}";
                            }
                            else
                            {
                                // If patient not found
                                string message = "Patient not found.";

                                // Register a JavaScript alert to show the message in the browser
                                if (HttpContext.Current.Handler is Page page)
                                {
                                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                                }
                                return;
                            }

                            // Load prescriptions into GridView
                            if (reader.NextResult()) // Move to the prescriptions result set
                            {
                                DataTable prescriptionsTable = new DataTable();
                                prescriptionsTable.Load(reader);
                                gridViewPrescriptions.DataSource = prescriptionsTable;
                                gridViewPrescriptions.DataBind(); // Bind the data to the GridView
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the operation
                string errorMessage = "An error occurred: " + ex.Message;

                // Display the error message using JavaScript alert
                if (HttpContext.Current.Handler is Page page)
                {
                    page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + errorMessage + "');", true);
                }
            }
        }
        


        public void DeletePatient(string PatID)
        {
            try

            {
                // open connection
                myConn.Open();
                //clear any parameters
                cmdString.Parameters.Clear();
                // command
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "DeletePatient";
                // Define input parameter
                cmdString.Parameters.Add("@PATIENTID", SqlDbType.VarChar, 25).Value = PatID;
                // adapter and dataset
                SqlDataAdapter aAdapter = new SqlDataAdapter();
                cmdString.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }

        }
        public void DeleteDoctor(string docID)
        {
            try
            {
                // open connection
                myConn.Open();
                //clear any parameters
                cmdString.Parameters.Clear();
                // command
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "DeleteDoctor";
                // Define input parameter
                cmdString.Parameters.Add("@DOCTORID", SqlDbType.VarChar, 20).Value = docID;
                // adapter and dataset
                SqlDataAdapter aAdapter = new SqlDataAdapter();
                cmdString.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }

        }
        public bool DeletePresc(string prescriptionID)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PrescriptionID", SqlDbType.VarChar, 7).Value = prescriptionID;

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // Returns the number of rows affected

                    return rowsAffected > 0; // Return true if a row was deleted, otherwise false
                }
            }
        }

        public DataTable SearchPrescriptions(string searchTerm)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SearchPrescriptions", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public DataTable GetAllPrescriptions()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllPrescriptions", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }




        public DataTable GetAllRefills()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllRefills", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public DataTable SearchRefills(string searchTerm)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SearchRefills", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }



        public void DeleteRefill(string RXNO, string PrescID)
        {
            try

            {
                // open connection
                myConn.Open();
                //clear any parameters
                cmdString.Parameters.Clear();
                // command
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "DeletRefill";
                // Define input parameter
                cmdString.Parameters.Add("@RXNO", SqlDbType.VarChar, 20).Value = RXNO;
                cmdString.Parameters.Add("@PrescID", SqlDbType.VarChar, 20).Value = PrescID;
                // adapter and dataset
                SqlDataAdapter aAdapter = new SqlDataAdapter();
                cmdString.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }

        }
        public bool UpdateRefill(string rxNo, string prescriptionID, string refillDate, string status, int refillsLeft)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateRefill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RXNO", rxNo);
                    cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);
                    cmd.Parameters.AddWithValue("@RefillDate", DateTime.Parse(refillDate));
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? DBNull.Value : (object)status);
                    cmd.Parameters.AddWithValue("@RefillsLeft", refillsLeft);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the update is successful
                }
            }
        }


        public DataSet RXByID(string rxno)

        {
            try

            {
                // open connection
                myConn.Open();
                //clear any parameters
                cmdString.Parameters.Clear();
                // command
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "RXByID";
                // Define input parameter
                cmdString.Parameters.Add("@rxid", SqlDbType.VarChar, 6).Value = rxno;
                // adapter and dataset
                SqlDataAdapter aAdapter = new SqlDataAdapter();

                aAdapter.SelectCommand = cmdString;
                DataSet aDataSet = new DataSet();

                // fill adapater
                aAdapter.Fill(aDataSet);

                // return dataSet
                return aDataSet;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }


    }
}