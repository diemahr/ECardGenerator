using ECardGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace ECardGenerator.DAL
{
    public class TemplateDAL : ITemplateDAL
    {

        private string _connectionString;
        //Dependancy Injection
        public TemplateDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Get list of available ECard templates, shown on Index page
        public IList<Template> GetTemplates(int id, string templateName, string imageName, string message)
        {
            string _sqlGetUserECard = "SELECT * FROM card_templates;";
            IList<Template> ecards = new List<Template>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlGetUserECard, conn);
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ecards.Add(MapTemplateFromReader(reader));
                    }
                    return ecards;
                }
            }
            catch
            {
                throw;
            }
        }

        //Create ECard per user input on Form.cshtml
        public Ecard CreateUserCard(int id, string toName, string froName, string toEmail, string froEmail,
                                    string message, string templateName, int templateID)
        {
            string _sqlCreateUserECard = "INSERT INTO cards VALUES(@id, @toEmail, @toName, @message, @fromName," +
                                                                  "@fromEmail);";

            string _sqlGetUserECard = "SELECT * FROM cards JOIN card_templates on cards.template_id = card_templates.id";


            Ecard eCard = new Ecard();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlCreateUserECard, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@toName", toName);
                    cmd.Parameters.AddWithValue("@toEmail", toEmail);
                    cmd.Parameters.AddWithValue("@froName", froEmail);
                    cmd.Parameters.AddWithValue("@message", message);

                    cmd = new SqlCommand(_sqlGetUserECard, conn);
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MapEcardFromReader(reader);
                    }
                    return eCard;
                }
            }
            catch (SqlException ex)
            {       
                throw ex;
            }
        }


        //Data Mapping for ECard and Template
        private Ecard MapEcardFromReader(SqlDataReader reader)
        {
            return new Ecard()
            {
                Id = Convert.ToInt32(reader["template_id"]),
                ToName = Convert.ToString(reader["to_name"]),
                ToEmail = Convert.ToString(reader["to_email"]),
                Message = Convert.ToString(reader["message"]),
                FroName = Convert.ToString(reader["from_name"]),
                FroEmail = Convert.ToString(reader["from_email"]),
                TemplateID = Convert.ToInt32(reader["template_id"]),
                TemplateName = Convert.ToString(reader["imageName"]),
                ImageName = Convert.ToString(reader["imageName"]),
                FontColor = Convert.ToString(reader["fontColor"]),
            };
        }
        private Template MapTemplateFromReader(SqlDataReader reader)
        {
            return new Template()
            {
                Id = Convert.ToInt32(reader["id"]),
                TemplateName = Convert.ToString(reader["name"]),
                ImageName = Convert.ToString(reader["imageName"]),
                FontColor = Convert.ToString(reader["fontColor"]),
            };
        }
    }
}