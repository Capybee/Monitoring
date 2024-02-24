using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Monitoring.Models
{
    public class DBController
    {
        static string ConnectionString = "Server=NIKITA;Database=MonitoringDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static ObservableCollection<Users> GetUsers() 
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "SELECT * FROM Users";

                SqlCommand Command = new SqlCommand(Request, Connection);

                using(SqlDataReader Reader = Command.ExecuteReader()) 
                {
                    if (Reader.HasRows)
                    {
                        ObservableCollection<Users> Result = new ObservableCollection<Users>();

                        while (Reader.Read())
                        {
                            Users User = new Users();

                            User.Id = Reader.GetInt32(0);
                            User.Surname = Reader.GetString(1);
                            User.Name = Reader.GetString(2);
                            User.MidleName = Reader.GetString(3);
                            User.Login = Reader.GetString(4);
                            User.Password = Reader.GetString(5);
                            User.IsAdmin = Reader.GetBoolean(6);

                            Result.Add(User);
                        }
                        return Result;
                    }
                    else
                        throw new Exception("Данные не найдены!");
                }
            }
        }

        public static ObservableCollection<Reports> GetReports() 
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "SELECT * FROM Reports";

                SqlCommand Command = new SqlCommand(Request, Connection);

                using (SqlDataReader Reader = Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        ObservableCollection<Reports> Result = new ObservableCollection<Reports>();

                        while (Reader.Read())
                        {
                            Reports Report = new Reports();

                            Report.Id = Reader.GetInt32(0);
                            Report.Title = Reader.GetString(1);
                            Report.Content = Reader.GetString(2);
                            Report.WhoRequestedIt = Reader.GetInt32(3);

                            Result.Add(Report);
                        }
                        return Result;
                    }
                    else
                        throw new Exception("Данные не найдены!");
                }
            }
        }

        public static ObservableCollection<Results> GetResults() 
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "SELECT * FROM Results";

                SqlCommand Command = new SqlCommand(Request, Connection);

                using (SqlDataReader Reader = Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        ObservableCollection<Results> ResultCollection = new ObservableCollection<Results>();

                        while (Reader.Read())
                        {
                            Results Result = new Results();

                            Result.Id = Reader.GetInt32(0);
                            Result.Title = Reader.GetString(1);
                            Result.Description = Reader.GetString(2);
                            Result.Result = Reader.GetBoolean(3);
                            Result.WhoContributed = Reader.GetInt32(4);
                            Result.WhoChangedIt = Reader.GetInt32(5);

                            ResultCollection.Add(Result);
                        }
                        return ResultCollection;
                    }
                    else
                        throw new Exception("Данные не найдены!");
                }
            }
        }

        public static bool AddUser( string Surname, string Name, string MidleName,string Login, string Password, bool IsAdmin = false)
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            { 
                Connection.Open();

                string Request = "INSERT INTO FROM Users ( surname, name, midle_name, login, password, is_admin) VALUES (@surname, @name, @midle_name, @login, @password, @is_admin)";

                SqlParameter SurnameParam = new SqlParameter("@surname", Surname);
                SqlParameter NameParam = new SqlParameter("@name", Name);
                SqlParameter MidleNameParam = new SqlParameter("@midle_name", MidleName);
                SqlParameter LoginParam = new SqlParameter("@login", Login);
                SqlParameter PasswordParam = new SqlParameter("@password", Password);
                SqlParameter IsAdminParam = new SqlParameter("@is_admin", IsAdmin);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(SurnameParam);
                Command.Parameters.Add(NameParam);
                Command.Parameters.Add(MidleNameParam);
                Command.Parameters.Add(LoginParam);
                Command.Parameters.Add(PasswordParam);
                Command.Parameters.Add(IsAdminParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool AddReport(string Title, string Content, int WhoRequestIt)
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString)) 
            {
                Connection.Open();

                string Request = "INSERT INTO FROM Reports (title, content, who_request_it) VALUES (@title, @content, @who_request_it)";

                SqlParameter TitleParam = new SqlParameter("@title", Title);
                SqlParameter ContentParam = new SqlParameter("@content", Content);
                SqlParameter WhoParam = new SqlParameter("@who_request_it", WhoRequestIt);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(TitleParam);
                Command.Parameters.Add(ContentParam);
                Command.Parameters.Add(WhoParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool AddResult(string Title, string Description, bool Result, int WhoContributed, int WhoChangedIt)
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "INSERT INTO FROM Results (title, description, result, who_contributed, who_changed_it) VALUES (@title, @description, @result, @who_contributed, @who_changed_it)";

                SqlParameter TitleParam = new SqlParameter("@title", Title);
                SqlParameter DescParam = new SqlParameter("@description", Description);
                SqlParameter ResParam = new SqlParameter("@result", Result);
                SqlParameter WhoParam = new SqlParameter("@who_contributed", WhoContributed);
                SqlParameter ChangeParam = new SqlParameter("@who_changed_it", WhoChangedIt);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(TitleParam);
                Command.Parameters.Add(DescParam);
                Command.Parameters.Add(WhoParam);
                Command.Parameters.Add(ResParam);
                Command.Parameters.Add(ChangeParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool DeleteUser(int Id)
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            { 
                Connection.Open();

                string Request = "DELETE FROM Users WHERE id = @id";

                SqlParameter IdParam = new SqlParameter("@id", Id);
                
                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(IdParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool DeleteResult(int Id)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "DELETE FROM Results WHERE id = @id";

                SqlParameter IdParam = new SqlParameter("@id", Id);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(IdParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool DeleteReport(int Id)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "DELETE FROM Reports WHERE id = @id";

                SqlParameter IdParam = new SqlParameter("@id", Id);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(IdParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool UpdateUser(int Id, string Surname, string Name, string MidleName, string Login, string Password, bool IsAdmin)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "UPDATE Users SET surname=@surname, name=@name, midle_name=@midle_name, login=@login, password=@password, is_admin=@is_admin WHERE id=@id";

                SqlParameter SurnameParam = new SqlParameter("@surname", Surname);
                SqlParameter NameParam = new SqlParameter("@name", Name);
                SqlParameter MidleNameParam = new SqlParameter("@midle_name", MidleName);
                SqlParameter LoginParam = new SqlParameter("@login", Login);
                SqlParameter PasswordParam = new SqlParameter("@password", Password);
                SqlParameter IsAdminParam = new SqlParameter("@is_admin", IsAdmin);
                SqlParameter IdParam = new SqlParameter("@id", Id);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(SurnameParam);
                Command.Parameters.Add(NameParam);
                Command.Parameters.Add(MidleNameParam);
                Command.Parameters.Add(LoginParam);
                Command.Parameters.Add(PasswordParam);
                Command.Parameters.Add(IsAdminParam);
                Command.Parameters.Add(IdParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }

        public static bool UpdateResult(int Id,string Title, string Description, bool Result, int WhoContributed, int WhoChangedIt)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                string Request = "UPDATE Results SET title=@title, description=@description, result=@result, who_contributed=@who_contributed, who_changed_it=@who_changed_it WHERE id=@id";

                SqlParameter TitleParam = new SqlParameter("@title", Title);
                SqlParameter DescParam = new SqlParameter("@description", Description);
                SqlParameter ResParam = new SqlParameter("@result", Result);
                SqlParameter WhoParam = new SqlParameter("@who_contributed", WhoContributed);
                SqlParameter ChangeParam = new SqlParameter("@who_changed_it", WhoChangedIt);
                SqlParameter IdParam = new SqlParameter("@id", Id);

                SqlCommand Command = new SqlCommand(Request, Connection);

                Command.Parameters.Add(TitleParam);
                Command.Parameters.Add(DescParam);
                Command.Parameters.Add(WhoParam);
                Command.Parameters.Add(ResParam);
                Command.Parameters.Add(ChangeParam);
                Command.Parameters.Add(IdParam);

                Command.ExecuteNonQuery();

                return true;
            }
        }
    }
}
