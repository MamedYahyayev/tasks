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
                    const string sql = "select t.Id teacherId, t.Name teacherName, t.Surname teacherSurname, t.BirthDate teacherBirthDate, " +
                        " t.License license, t.SubjectCode subjectCode" +
                        " from Teacher t";
                    _command = new SqlCommand(sql, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("teacherId");
                        teacher.Name = _reader.GetString("teacherName");
                        teacher.Surname = _reader.GetString("teacherSurname");
                        teacher.BirthDate = _reader.GetDateTime("teacherBirthDate");
                        teacher.License = _reader.GetString("License");

                        var subjectCode = _reader.GetIntValueOrDefault("subjectCode");
                        if (subjectCode != null)
                        {
                            teacher.Subject = (Subject)_reader.GetInt32("subjectCode");
                            teacher.SubjectName = teacher.Subject.ToString();
                        }

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
                    string sql = "Select Id, Name, Surname, BirthDate, License, SubjectCode from Teacher Where Id=@Id";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("Id", id);
                    _reader = _command.ExecuteReader();
                    if (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("Id");
                        teacher.Name = _reader.GetString("Name");
                        teacher.Surname = _reader.GetString("Surname");
                        teacher.BirthDate = _reader.GetDateTime("BirthDate");
                        teacher.License = _reader.GetString("License");

                        var subjectCode = _reader.GetIntValueOrDefault("SubjectCode");
                        if (subjectCode != null)
                        {
                            teacher.Subject = (Subject)_reader.GetInt32("SubjectCode");
                            teacher.SubjectName = teacher.Subject.ToString();
                        }

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
                    string sql = "INSERT INTO Teacher(Name, Surname, BirthDate, License, SubjectCode) " +
                        " VALUES(@Name, @Surname, @BirthDate, @License, @SubjectCode) ";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Name", teacher.Name);
                    _command.Parameters.AddWithValue("@Surname", teacher.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate);
                    _command.Parameters.AddWithValue("@License", teacher.License);
                    _command.Parameters.AddWithValue("@SubjectCode", teacher.Subject == null ? DBNull.Value : teacher.Subject);

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
                    string sql = "UPDATE Teacher SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate, " +
                        " License = @License, SubjectCode = @SubjectCode WHERE Id = @Id";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Id", teacher.Id);
                    _command.Parameters.AddWithValue("@Name", teacher.Name);
                    _command.Parameters.AddWithValue("@Surname", teacher.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate);
                    _command.Parameters.AddWithValue("@License", teacher.License);
                    _command.Parameters.AddWithValue("@SubjectCode", teacher.Subject == null ? DBNull.Value : teacher.Subject);

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
                    string sql = "select Id, Name, Surname, BirthDate, License, SubjectCode from Teacher " +
                    " where LOWER(Name) like LOWER(@Keyword) OR LOWER(Surname) like LOWER(@Keyword)";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var teacher = new Teacher();
                        teacher.Id = _reader.GetInt32("id");
                        teacher.Name = _reader.GetString("Name");
                        teacher.Surname = _reader.GetString("Surname");
                        teacher.BirthDate = _reader.GetDateTime("BirthDate");
                        teacher.License = _reader.GetString("License");

                        var subjectCode = _reader.GetIntValueOrDefault("SubjectCode");
                        if (subjectCode != null)
                        {
                            teacher.Subject = (Subject)_reader.GetInt32("SubjectCode");
                            teacher.SubjectName = teacher.Subject.ToString();
                        }

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

        public int FindStudentCount(int teacherId)
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Open();
                    const string sql = "select Count(*) studentCount from Student where TeacherId = @TeacherId";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@TeacherId", teacherId);
                    _reader = _command.ExecuteReader();
                    
                    if (_reader.Read())
                        return _reader.GetInt32("studentCount");
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
