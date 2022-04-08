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
    public class TeacherService : ICrudOperation<Teacher>
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
                    const string sql = "DELETE FROM Teacher Where Id=@Id";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Id", id);
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

        public IList<Teacher> GetAll()
        {
            try
            {
                var teachers = new List<Teacher>();
                if (_connection != null)
                {
                    _connection.Open();
                    const string sql = "SELECT Id, Name, Surname, BirthDate, License FROM Teacher";
                    _command = new SqlCommand(sql, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("id");
                        teacher.Name = _reader.GetString("Name");
                        teacher.Surname = _reader.GetString("Surname");
                        teacher.BirthDate = _reader.GetDateTime("BirthDate");
                        teacher.License = _reader.GetString("License");
                        teachers.Add(teacher);
                    }

                    return teachers;
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

        public Teacher GetById(int id)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "Select Id, Name, Surname, BirthDate, License Where Id=@Id";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("Id", id);
                    _reader = _command.ExecuteReader();
                    if (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("id");
                        teacher.Name = _reader.GetString("Name");
                        teacher.Surname = _reader.GetString("Surname");
                        teacher.BirthDate = _reader.GetDateTime("BirthDate");
                        teacher.License = _reader.GetString("License");
                        return teacher;
                    }

                    throw new ItemNotFoundException("Teacher with " + id + " not found!");
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

        public bool Insert(Teacher teacher)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "INSERT INTO Teacher(Name,Surname,BirthDate,License) " +
                        " VALUES(@Name, @Surname, @BirthDate, @License)";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Name", teacher.Name);
                    _command.Parameters.AddWithValue("@Surname", teacher.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate);
                    _command.Parameters.AddWithValue("@License", teacher.License);

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


        public bool Update(Teacher teacher)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "UPDATE Teacher SET Name = @Name, Surname = @Surname, " +
                        "BirthDate = @BirthDate, License=@License WHERE Id = @Id ";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Id", teacher.Id);
                    _command.Parameters.AddWithValue("@Name", teacher.Name);
                    _command.Parameters.AddWithValue("@Surname", teacher.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate);
                    _command.Parameters.AddWithValue("@License", teacher.License);

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

        public IList<Teacher> Search(string keyword)
        {
            try
            {
                var teachers = new List<Teacher>();
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "select Id, Name, Surname, BirthDate, License from Teacher " +
                    "where LOWER(Name) like LOWER('%@Keyword%') OR LOWER(Surname) like LOWER('%@Keyword%')";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Keyword", keyword);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("id");
                        teacher.Name = _reader.GetString("Name");
                        teacher.Surname = _reader.GetString("Surname");
                        teacher.BirthDate = _reader.GetDateTime("BirthDate");
                        teacher.License = _reader.GetString("License");
                        teachers.Add(teacher);
                    }

                    return teachers;
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
