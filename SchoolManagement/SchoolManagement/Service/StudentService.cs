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
                var students = new List<Student>();
                if (_connection != null)
                {
                    _connection.Open();
                    const string sql = "SELECT s.Id studentId, s.Name studentName, s.Surname studentSurname," +
                        " s.BirthDate studentBirthDate, s.RegisterDate studentRegisterDate, t.Name teacherName, t.Surname teacherSurname " +
                        "FROM Student s left join Teacher t on t.Id = s.TeacherId";
                    _command = new SqlCommand(sql, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("studentId");
                        student.Name = _reader.GetStringValueOrDefault("studentName");
                        student.Surname = _reader.GetStringValueOrDefault("studentSurname");
                        student.BirthDate = _reader.GetDateTime("studentBirthDate");
                        student.RegisterDate = _reader.GetDateTime("studentRegisterDate");

                        var teacher = new Teacher();
                        teacher.Name = _reader.GetStringValueOrDefault("teacherName");
                        teacher.Surname = _reader.GetStringValueOrDefault("teacherSurname");

                        student.Teacher = teacher;

                        students.Add(student);
                    }

                    return students;
                }

                throw new Exception("Sorry, we couldn't connect the database");
            }
            catch (Exception ex)
            {
                throw ex;
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
                    const string sql = "SELECT s.Id studentId, s.Name studentName, s.Surname, s.BirthDate, s.RegisterDate, t.Name, t.Surname" +
                        " FROM Student s left join Teacher t on t.id = s.TeacherId Where s.Id=@Id";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("Id", id);
                    _reader = _command.ExecuteReader();
                    if (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("s.id");
                        student.Name = _reader.GetString("s.Name");
                        student.Surname = _reader.GetString("s.Surname");
                        student.BirthDate = _reader.GetDateTime("s.BirthDate");
                        student.RegisterDate = _reader.GetDateTime("s.RegisterDate");

                        var teacher = new Teacher();
                        teacher.Name = _reader.GetString("t.Name");
                        teacher.Surname = _reader.GetString("t.Surname");

                        student.Teacher = teacher;

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
                    string sql = "INSERT INTO Student(Name,Surname,BirthDate,RegisterDate, TeacherId) " +
                        " VALUES(@Name, @Surname, @BirthDate, @RegisterDate, @TeacherId)";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Name", student.Name);
                    _command.Parameters.AddWithValue("@Surname", student.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    _command.Parameters.AddWithValue("@RegisterDate", student.RegisterDate);
                    _command.Parameters.AddWithValue("@TeacherId", student.Teacher == null ? DBNull.Value : student.Teacher.Id);

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
                        "BirthDate = @BirthDate, RegisterDate=@RegisterDate, TeacherId=@TeacherId WHERE Id = @Id ";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Id", student.Id);
                    _command.Parameters.AddWithValue("@Name", student.Name);
                    _command.Parameters.AddWithValue("@Surname", student.Surname);
                    _command.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    _command.Parameters.AddWithValue("@RegisterDate", student.RegisterDate);
                    _command.Parameters.AddWithValue("@TeacherId", student.Teacher.Id);

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
                var students = new List<Student>();
                if (_connection != null)
                {
                    _connection.Open();
                    string sql = "SELECT s.Id, s.Name, s.Surname, s.BirthDate, s.RegisterDate, t.Name, t.Surname " +
                        "FROM Student s left join Teacher t on t.Id = s.TeacherId " +
                        "where LOWER(s.Name) like LOWER('%@Keyword%') OR LOWER(s.Surname) like LOWER('%@Keyword%')";
                    _command = new SqlCommand(sql, _connection);
                    _command.Parameters.AddWithValue("@Keyword", keyword);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var student = new Student();
                        student.Id = _reader.GetInt32("s.Id");
                        student.Name = _reader.GetString("s.Name");
                        student.Surname = _reader.GetString("s.Surname");
                        student.BirthDate = _reader.GetDateTime("s.BirthDate");
                        student.RegisterDate = _reader.GetDateTime("s.RegisterDate");

                        var teacher = new Teacher();
                        teacher.Name = _reader.GetString("t.Name");
                        teacher.Surname = _reader.GetString("t.Surname");

                        student.Teacher = teacher;

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
