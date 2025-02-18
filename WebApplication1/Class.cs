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

        public string AddPatient(string Fname, string M_I, string LName, string DOB, string Gender, string Phone, string STREET, string CITY, string STATE_ADD, string ZIP, string COUNTRY, string Insurance)
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

                // Define input parameter 

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
                    return "Invalid format for First Name (Use only Letters)";
                }

                if (IsAllLetters(LName))
                {
                    cmdString.Parameters.Add("@Lname", SqlDbType.VarChar, 25).Value = LName;
                }
                else
                {
                    return "Invalid format for Last Name (Use only Letters)";
                }

                if (IsAllLetters(M_I))
                {
                    cmdString.Parameters.Add("@M_I", SqlDbType.VarChar, 1).Value = M_I;
                }
                else
                {
                    return "Invalid format for Middle Initial (Use only Letters)";
                }

                if (DateTime.TryParse(DOB, out DateTime parsedDOB))
                {
                    cmdString.Parameters.Add("@DOB", SqlDbType.Date).Value = parsedDOB;
                }
                else
                {
                    return "Invalid date format for DOB.";
                }

                if (IsAllLetters(Gender))
                {
                    cmdString.Parameters.Add("@Gender", SqlDbType.VarChar, 60).Value = Gender;
                }
                else
                {
                    return "Invalid format for Gender (Use only Letters)";
                }

                cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;

                if (IsLettersDigitsOrSpaces(STREET))
                {
                    cmdString.Parameters.Add("@STREET", SqlDbType.VarChar, 60).Value = STREET;
                }
                else
                {
                    return "Invalid format for Street (Use only Letters, Numbers, and Spaces)";
                }

                if (IsAllLetters(CITY))
                {
                    cmdString.Parameters.Add("@CITY", SqlDbType.VarChar, 60).Value = CITY;
                }
                else
                {
                    return "Invalid format for City (Use only Letters)";
                }

                if (IsAllLetters(STATE_ADD))
                {
                    cmdString.Parameters.Add("@STATE_ADD", SqlDbType.VarChar, 2).Value = STATE_ADD;
                }
                else
                {
                    return "Invalid format for State (Use only Letters)";
                }

                cmdString.Parameters.Add("@ZIP", SqlDbType.VarChar, 5).Value = ZIP;

                if (IsAllLetters(COUNTRY))
                {
                    cmdString.Parameters.Add("@COUNTRY", SqlDbType.VarChar, 3).Value = COUNTRY;
                }
                else
                {
                    return "Invalid format for Country (Use only Letters)";
                }

                if (IsAllLetters(Insurance) && (Insurance == "Yes" || Insurance == "No" || Insurance == "Y" || Insurance == "N"))
                {
                    cmdString.Parameters.Add("@Insurance", SqlDbType.VarChar, 3).Value = Insurance;
                }
                else
                {
                    return "Invalid format for Insurance Yes, No, Y, N";
                }

                cmdString.ExecuteNonQuery();
                return "Patient added successfully!";
            }

            catch (Exception ex)

            {
                return (ex.Message);

            }

            finally

            {
                myConn.Close();
            }
        }

        public string DeletePhysician(string PhysicianID)
        {
            try
            {
                myConn.Open();

                cmdString.Parameters.Clear();


                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "DeletePhysician";


                cmdString.Parameters.Add("@PhysicianID", SqlDbType.VarChar, 20).Value = PhysicianID;



                int rowsAffected = cmdString.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Physician deleted successfully.";
                }
                else
                {
                    return "Physician ID not found.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                myConn.Close();
            }
        }
        public string DeletePatient(string PatientID)
        {
            try
            {
                myConn.Open();

                cmdString.Parameters.Clear();


                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "DeletePatient";


                cmdString.Parameters.Add("@PatientID", SqlDbType.VarChar, 20).Value = PatientID;



                int rowsAffected = cmdString.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Patient deleted successfully.";
                }
                else
                {
                    return "Patient ID not found.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                myConn.Close();
            }
        }

        public string AddPhysician(string Fname, string LName, string Email, string Phone)
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

            bool IsAllDigits(string input)
            {
                return input.All(char.IsDigit);
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
                if (Fname != null && IsAllLetters(Fname) && Fname != "")
                {
                    cmdString.Parameters.Add("@Fname", SqlDbType.VarChar, 50).Value = Fname;
                }
                else
                {
                    return "Invalid format for First Name (Use only Letters)";
                }

                if (LName != null && IsAllLetters(LName) && LName != "")
                {
                    cmdString.Parameters.Add("@LName", SqlDbType.VarChar, 50).Value = LName;
                }
                else
                {
                    return "Invalid format for Last Name (Use only Letters)";
                }

                if (Email != null && IsValidEmailFormat(Email))
                {
                    cmdString.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;
                }
                else
                {
                    return "Invalid Email Format";
                }

                if (Phone != null && IsAllDigits(Phone))
                {
                    cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;
                }
                else
                {
                    return "Invalid Phone Number (Use only digits)";
                }


                cmdString.ExecuteNonQuery();

                return "Physician added successfully!";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
            finally
            {
                myConn.Close();
            }
        }


        public string AddPrescription(string patientID, string physicianID, string medName, string dosage, string intMethod, int refillsLeft, DateTime initialRefillDate)
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

                    // Output parameters for success flag and error message
                    SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    SqlParameter errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(successParam);
                    cmd.Parameters.Add(errorParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    bool success = (bool)cmd.Parameters["@Success"].Value;
                    string errorMessage = cmd.Parameters["@ErrorMessage"].Value.ToString();

                    if (success)
                        return "✅ Prescription added successfully!";
                    else
                        return "❌ " + errorMessage;
                }
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

        public bool CheckPhyID(string phyid)
        {
            try
            {

                // myConn.Open();

                // Clear parameters and set up command
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.Text;

                cmdString.CommandText = @"SELECT COUNT(1) FROM PHYSICIAN WHERE PHYSICIANID = @PHYSICIANId";
                cmdString.Parameters.Add("@PHYSICIAN", SqlDbType.VarChar, 25).Value = phyid;

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
        }


        public string UpdatePatient(string patientId, string Fname, string M_I, string LName, string DOB, string Gender, string Phone, string STREET, string CITY, string STATE_ADD, string ZIP, string COUNTRY, string Insurance)
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
                    return "Patient ID cannot be empty or must exist";
                }

                if (!string.IsNullOrEmpty(Fname) && IsAllLetters(Fname))
                {
                    cmdString.Parameters.Add("@Fname", SqlDbType.VarChar, 25).Value = Fname;
                }
                else if (!string.IsNullOrEmpty(Fname))
                {
                    return "First name must contain only letters.";
                }

                if (!string.IsNullOrEmpty(LName) && IsAllLetters(LName))
                {
                    cmdString.Parameters.Add("@Lname", SqlDbType.VarChar, 25).Value = LName;
                }
                else if (!string.IsNullOrEmpty(LName))
                {
                    return "Last name must contain only letters.";
                }

                if (!string.IsNullOrEmpty(M_I) && M_I.Length == 1 && char.IsLetter(M_I[0]))
                {
                    cmdString.Parameters.Add("@M_I", SqlDbType.VarChar, 1).Value = M_I;
                }
                else if (!string.IsNullOrEmpty(M_I))
                {
                    return "Middle initial must be a single letter.";
                }

                if (DateTime.TryParse(DOB, out DateTime parsedDOB))
                {
                    cmdString.Parameters.Add("@DOB", SqlDbType.Date).Value = parsedDOB;
                }
                else if (!string.IsNullOrEmpty(DOB))
                {
                    return "Invalid date format for DOB.";
                }

                if (!string.IsNullOrEmpty(Gender) && (Gender == "Male" || Gender == "Female"))
                {
                    cmdString.Parameters.Add("@Gender", SqlDbType.VarChar, 6).Value = Gender;
                }
                else if (!string.IsNullOrEmpty(Gender))
                {
                    return "Gender must be 'Male' or 'Female'.";
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
                    return "City name must contain only letters.";
                }

                if (!string.IsNullOrEmpty(STATE_ADD) && STATE_ADD.Length == 2 && IsAllLetters(STATE_ADD))
                {
                    cmdString.Parameters.Add("@STATE_ADD", SqlDbType.VarChar, 2).Value = STATE_ADD;
                }
                else if (!string.IsNullOrEmpty(STATE_ADD))
                {
                    return "State abbreviation must be two letters.";
                }

                if (!string.IsNullOrEmpty(ZIP) && ZIP.All(char.IsDigit) && ZIP.Length == 5)
                {
                    cmdString.Parameters.Add("@ZIP", SqlDbType.VarChar, 5).Value = ZIP;
                }
                else if (!string.IsNullOrEmpty(ZIP))
                {
                    return "ZIP code must contain only 5 digits.";
                }

                if (!string.IsNullOrEmpty(COUNTRY) && IsAllLetters(COUNTRY) && COUNTRY.Length == 3)
                {
                    cmdString.Parameters.Add("@COUNTRY", SqlDbType.VarChar, 3).Value = COUNTRY;
                }
                else if (!string.IsNullOrEmpty(COUNTRY))
                {
                    return "Country must contain only 3 letters.";
                }

                if (!string.IsNullOrEmpty(Insurance) && IsAllLetters(Insurance) && (Insurance == "Yes" || Insurance == "No" || Insurance == "Y" || Insurance == "N"))
                {
                    cmdString.Parameters.Add("@Insurance", SqlDbType.VarChar, 3).Value = Insurance;
                }
                else if (!string.IsNullOrEmpty(Insurance))
                {
                    return "Insurance must contain only 3 letters (Yes or No).";
                }

                cmdString.ExecuteNonQuery();
                return "Patient updated successfully!";

            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }

        public string UpdatePhysician(string physicianId, string Fname, string LName, string Email, string Phone)
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

            bool IsAllDigits(string input)
            {
                foreach (char c in input)
                {
                    if (!char.IsDigit(c))
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

                myConn.Open();
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandTimeout = 1500;
                cmdString.CommandText = "UpdatePhysician";

                if (string.IsNullOrEmpty(physicianId) || !CheckPhyID(physicianId))
                {
                    return "Invalid Physician ID.";
                }
                else
                {
                    cmdString.Parameters.Add("@PhysicianId", SqlDbType.VarChar, 25).Value = physicianId;
                }

                if (!string.IsNullOrEmpty(Fname) && !IsAllLetters(Fname))
                {
                    return "First name should contain only letters.";
                }
                else if (!string.IsNullOrEmpty(Fname))
                {
                    cmdString.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = Fname;
                }

                if (!string.IsNullOrEmpty(LName) && !IsAllLetters(LName))
                {
                    return "Last name should contain only letters.";
                }
                else if (!string.IsNullOrEmpty(LName))
                {
                    cmdString.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LName;
                }

                if (!string.IsNullOrEmpty(Email) && !IsValidEmailFormat(Email))
                {
                    return "Invalid email format.";
                }
                else if (!string.IsNullOrEmpty(Email))
                {
                    cmdString.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;
                }

                if (!string.IsNullOrEmpty(Phone) && !IsAllDigits(Phone))
                {
                    return "Phone number should contain only digits.";
                }
                else if (!string.IsNullOrEmpty(Phone))
                {
                    cmdString.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;
                }
                cmdString.ExecuteNonQuery();

                return "Physician details updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            finally
            {
                myConn.Close();
            }
        }


        public string UpdatePrescription(string prescriptionID, string patientID, string physicianID, string medName, string dosage, string intMethod, DateTime refillDate, string status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@PHYSICIANID", physicianID);
                    cmd.Parameters.AddWithValue("@MedName", medName);
                    cmd.Parameters.AddWithValue("@Dosage", dosage); // Fixed typo from Doseage
                    cmd.Parameters.AddWithValue("@IntMethod", intMethod);
                    cmd.Parameters.AddWithValue("@RefillDate", refillDate);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? DBNull.Value : (object)status);

                    // Output parameters for success/error handling
                    SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    SqlParameter errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(successParam);
                    cmd.Parameters.Add(errorParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    bool success = (bool)cmd.Parameters["@Success"].Value;
                    string errorMessage = cmd.Parameters["@ErrorMessage"].Value.ToString();

                    return success ? "✅ Prescription updated successfully!" : "❌ " + errorMessage;
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

        public bool DoesPrescriptionExist(string medicine, string patientID)
        {
            bool exists = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT COUNT(*) FROM Prescriptions WHERE MedName = @MedName AND PatientID = @PatientID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MedName", medicine);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();  // Returns the number of matching records

                    exists = count > 0; // If count > 0, prescription exists
                }
            }

            return exists;
        }
        public DataSet SearchPhysician(string searchTerm)
        {
            try
            {
                // Open connection
                myConn.Open();

                // Clear any parameters
                cmdString.Parameters.Clear();

     
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandText = "SearchPhysician";

                // Parse search term
                string[] terms = searchTerm.Trim().Split(' ');

      
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

         
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdString;

                DataSet dataSet = new DataSet();

          
                adapter.Fill(dataSet);

           
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



        public void TriggerRefillProcedure(int prescriptionId, string status)
        {
            try
            {

                myConn.Open();



             
                cmdString.Parameters.Clear();
                cmdString.Connection = myConn;
                cmdString.CommandType = CommandType.StoredProcedure;
                cmdString.CommandText = "dbo.GETREFILLS";

              
                cmdString.Parameters.AddWithValue("@PrescriptionID", prescriptionId > 0 ? (object)prescriptionId : DBNull.Value);
                cmdString.Parameters.AddWithValue("@status", !string.IsNullOrEmpty(status) ? (object)status : DBNull.Value);

              
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
          
            gridViewPrescriptions.DataSource = null;
            gridViewPrescriptions.DataBind();
            lblPatientName.Text = string.Empty;
            lblDOB.Text = string.Empty;
            lblAddress.Text = string.Empty;
            lblPhone.Text = string.Empty;

            try
            {
              
                using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
                {
                    myConn.Open(); 

                
                    using (SqlCommand cmdString = new SqlCommand("GetPatientInfoAndPrescriptions", myConn))
                    {
                        cmdString.CommandType = CommandType.StoredProcedure;
                        cmdString.Parameters.AddWithValue("@PatientID", patientID);

                        using (SqlDataReader reader = cmdString.ExecuteReader())
                        {
                          
                            if (reader.Read())
                            {
                                lblPatientName.Text = $"{reader["FirstName"]} {reader["MiddleInitial"]} {reader["LastName"]}";
                                lblDOB.Text = reader["DOB"].ToString();
                                lblPhone.Text = reader["Phone"].ToString();
                                lblAddress.Text = $"{reader["Street"]}, {reader["City"]}, {reader["State"]} {reader["Zip"]}, {reader["Country"]}";
                            }
                            else
                            {
                               
                                string message = "Patient not found.";

                             
                                if (HttpContext.Current.Handler is Page page)
                                {
                                    page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                                }
                                return;
                            }

                           
                            if (reader.NextResult()) 
                            {
                                DataTable prescriptionsTable = new DataTable();
                                prescriptionsTable.Load(reader);
                                gridViewPrescriptions.DataSource = prescriptionsTable;
                                gridViewPrescriptions.DataBind(); 
                            }
                        }
                    }
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
        }
        public string AddRefill(string prescriptionID, DateTime refillDate)
{
    using (SqlConnection con = new SqlConnection(connString))
    {
        using (SqlCommand cmd = new SqlCommand("AddRefills", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);
            cmd.Parameters.AddWithValue("@RefillDate", refillDate);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            
            return rowsAffected > 0 ? "✅ Refill added successfully!" : "❌ Error adding refill.";
        }
    }
}


        public string DeletePrescription(string prescriptionID)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);

                    // Output parameters
                    SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    SqlParameter errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(successParam);
                    cmd.Parameters.Add(errorParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    bool success = (bool)cmd.Parameters["@Success"].Value;
                    string errorMessage = cmd.Parameters["@ErrorMessage"].Value.ToString();

                    if (success)
                        return "✅ Prescription deleted successfully!";
                    else
                        return "❌ " + errorMessage;
                }
            }
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

        public string DeleteRefill(int RXNO)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteRefill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RX_NO", RXNO);

                    con.Open();
                    object result = cmd.ExecuteScalar(); // Get the returned row count

                    int rowsAffected = (result != null) ? Convert.ToInt32(result) : 0;

                    return rowsAffected > 0 ? "✅ Refill deleted successfully!" : "❌ Refill not found.";
                }
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

                    // Capture the stored procedure return value
                    SqlParameter returnParameter = new SqlParameter();
                    returnParameter.ParameterName = "@ReturnVal";
                    returnParameter.SqlDbType = SqlDbType.Int;
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParameter);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Read the return value
                    int result = (int)returnParameter.Value;

                    return result > 0; // If rows were affected, return true (success)
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
        private void ClearAndEnableTextBoxes(Control parent)
{
    foreach (Control c in parent.Controls)
    {
        if (c is TextBox txt)
        {
            txt.Text = "";  // Clear text
            txt.Enabled = true;  // Enable the textbox
        }
        else if (c.HasControls()) // Recursively check child controls (inside panels, group boxes, etc.)
        {
            ClearAndEnableTextBoxes(c);
        }
    }
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




    }
}