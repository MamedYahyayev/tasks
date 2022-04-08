using SchoolManagement.Config;
using SchoolManagement.Exceptions;
using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public class StudentService : ICrudOperation<Student>
    {
        private readonly SqlConnection _connection = DbConfig.Connect();
        private SqlCommand _command;
        private SqlDataReader _reader;

        public bool Delete(int id)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "DELETE FROM Student Where Id=" + id;
                    _command = new SqlCommand(sql, _connection);
                    var affectedRow = _command.ExecuteNonQuery();
                    if (affectedRow == 1) return true;

                    return false;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }

        }

        public IList<Student> GetAll()
        {
            try
            {
                IList<Student> students = new List<Student>();
                if (_connection != null)
                {
                    _connection.Open();
                    const string sql = "SELECT Id, Name, Surname, BirthDate, RegisterDate FROM Student";
                    _command = new SqlCommand(sql, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("id");
                        student.Name = _reader.GetString("Name");
                        student.Surname = _reader.GetString("Surname");
                        student.BirthDate = _reader.GetDateTime("BirthDate");
                        student.RegisterDate = _reader.GetDateTime("RegisterDate");
                        students.Add(student);
                    }

                    return students;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Student GetById(int id)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "Select Id, Name, Surname, BirthDate, RegisterDate Where Id=" + id;
                    _command = new SqlCommand(sql, _connection);
                    _reader = _command.ExecuteReader();
                    if (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("id");
                        student.Name = _reader.GetString("Name");
                        student.Surname = _reader.GetString("Surname");
                        student.BirthDate = _reader.GetDateTime("BirthDate");
                        student.RegisterDate = _reader.GetDateTime("RegisterDate");
                        return student;
                    }

                    throw new ItemNotFoundException("Student with " + id + " not found!");
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool Insert(Student student)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "INSERT INTO Student(Name,Surname,BirthDate,RegisterDate) " +
                        " VALUES(@Name, @Surname, @BirthDate, @RegisterDate)";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Name", student.Name);
                    _command.Parameters.AddWithValue("@Surname", student.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    _command.Parameters.AddWithValue("@RegisterDate", student.RegisterDate);

                    var isAdded = _command.ExecuteNonQuery();
                    if (isAdded == 1) return true;

                    return false;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }


        public bool Update(Student student)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "UPDATE Student SET Name = @Name, Surname = @Surname, " +
                        "BirthDate = @BirthDate, RegisterDate=@RegisterDate WHERE Id = @Id ";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Id", student.Id);
                    _command.Parameters.AddWithValue("@Name", student.Name);
                    _command.Parameters.AddWithValue("@Surname", student.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    _command.Parameters.AddWithValue("@RegisterDate", student.RegisterDate);

                    var isAdded = _command.ExecuteNonQuery();
                    if (isAdded == 1) return true;

                    return false;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public IList<Student> Search(string keyword)
        {
            try
            {
                IList<Student> students = new List<Student>();
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "select Id, Name, Surname, BirthDate, RegisterDate from Student " +
                    "where LOWER(Name) like LOWER('%@Keyword%') OR LOWER(Surname) like LOWER('%@Keyword%')";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Keyword", keyword);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("id");
                        student.Name = _reader.GetString("Name");
                        student.Surname = _reader.GetString("Surname");
                        student.BirthDate = _reader.GetDateTime("BirthDate");
                        student.RegisterDate = _reader.GetDateTime("RegisterDate");
                        students.Add(student);
                    }

                    return students;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }

        }





    }
}
