using CRUDAPP.Models;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CRUDAPP.Repository
{

    public class UsersRepo
    {

        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            con = new SqlConnection(constr);
        }




        public bool AddNewUser(User obj, HttpPostedFileBase image)
        {
            connection();
            using (SqlCommand cmd = new SqlCommand("AddNewUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@city", obj.City);
                cmd.Parameters.AddWithValue("@state", obj.State);
                cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
                cmd.Parameters.AddWithValue("@Country", obj.Country);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                cmd.Parameters.AddWithValue("@Username", obj.Username);
                cmd.Parameters.AddWithValue("@Password", obj.Password);

                byte[] profilePictureBytes = ConvertToBytes(image);
                cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureBytes);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                return i >= 1;
            }
        }

        public List<User> GetUserDetails()
        {
            connection();
            List<User> userDetailsList = new List<User>();
            SqlCommand com = new SqlCommand("GetUserDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                userDetailsList.Add(
                    new User
                    {
                        UserID = Convert.ToInt32(dr["UserID"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Address = Convert.ToString(dr["Address"]),
                        City = Convert.ToString(dr["City"]),
                        State = Convert.ToString(dr["State"]),
                        PostalCode = Convert.ToString(dr["PostalCode"]),
                        Country = Convert.ToString(dr["Country"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Email = Convert.ToString(dr["Email"]),
                        Username = Convert.ToString(dr["Username"]),
                        Password = Convert.ToString(dr["Password"]),
                        ProfilePicture = dr["ProfilePicture"] as byte[]
                    }
                );
            }

            return userDetailsList;
        }





        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            if (image != null)
            {
                byte[] imageBytes = new byte[image.ContentLength];
                image.InputStream.Read(imageBytes, 0, image.ContentLength);
                return imageBytes;
            }
            return null;
        }


        public bool UpdateUser(User obj, HttpPostedFileBase image)
        {
            connection();
            using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
                cmd.Parameters.AddWithValue("@Country", obj.Country);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                cmd.Parameters.AddWithValue("@Username", obj.Username);
                cmd.Parameters.AddWithValue("@Password", obj.Password);

                byte[] profilePictureBytes = ConvertToBytes(image);
                cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureBytes);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool DeleteUser(int UserId)
        {
            connection();
            using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserId);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }


            }
        }

        public User GetUserById(int userId)
        {
            User user = null;
            connection();

            using (SqlCommand cmd = new SqlCommand("GetUserById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = reader["Gender"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        PostalCode = reader["PostalCode"].ToString(),
                        Country = reader["Country"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        ProfilePicture = (byte[])reader["ProfilePicture"]
                    };
                }

                reader.Close();
                con.Close();
            }

            return user;
        }

        int i;
        public int value()
        {
            return i;
        }

        public bool Login(User user)
        {
            connection();
            try
            {
                SqlCommand com = new SqlCommand("spLogin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Username", user.Username); 
                com.Parameters.AddWithValue("@Password", user.Password); 
                con.Open();
                object result = com.ExecuteScalar(); 
                if (result != null && result != DBNull.Value)
                {
                    int i = (int)result;
                    con.Close(); 
                    if (i != 0)
                    {
                        return true;
                    }
                }
            }
            catch{
               
                return false;
            }


            return false; 
        }




    }
    }







