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

        private readonly string _connectionString;

        //Dependancy Injection
        public TemplateDAL(string connectionString)
        {
            this._connectionString = connectionString;
        }

        //Get list of available ECard templates, shown on Index page
        public IList<Template> GetTemplates(int id, string templateName, string imageName, string message)
        {
            string _sqlGetTemplates = "SELECT * FROM card_templates;";
            IList<Template> ecards = new List<Template>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlGetTemplates, conn);
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

        //Get single Template per templateID,
        public Template GetSingleTemplate (int templateID)
        {
            Template template = null;

            string _sqlGetTemplate = "SELECT * FROM card_templates WHERE card_templates.id = @templateID; ";
          
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlGetTemplate);
                    cmd.Parameters.AddWithValue("@templateID", templateID);
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        template = MapTemplateFromReader(reader);
                    }
                }
              
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return template;
        }

        //Create ECard per user input from Form.cshtml
        public Ecard CreateUserECard(string toName, string froName, string toEmail, string froEmail,
                                    string message, int templateID)
        {
            string _sqlCreateUserECard = "INSERT INTO cards (template_id, to_email, to_name, message, from_name, from_email)" +
                                         "VALUES(@templateID, @toEmail, @toName, @message, @froName, @froEmail); ";

            Ecard eCard = new Ecard(toName, froName, toEmail, froEmail, message, templateID);
            try
            {              

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlCreateUserECard, conn);
                    cmd.Parameters.AddWithValue("@templateID", templateID);
                    cmd.Parameters.AddWithValue("@toName", toName);
                    cmd.Parameters.AddWithValue("@toEmail", toEmail);
                    cmd.Parameters.AddWithValue("@froName", froName);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters.AddWithValue("@froEmail", froEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        eCard = MapEcardFromReader(reader); 
                    }                                        
                }
            }
            catch (SqlException ex)
            {       
                throw ex;
            }
            return eCard;
        }

        //Retrieve ECardVM per user input from Form.cshtml
        public EcardViewModel RetrieveECardVM(Ecard ecard)
        {
            EcardViewModel eCardVM = new EcardViewModel(ecard);

            string _sqlRetrieveECardVM = "SELECT cards.template_id, cards.to_email, cards.to_name, cards.message, cards.from_name, " +
                                               "cards.from_email, card_templates.name, card_templates.imageName, card_templates.fontColor " +
                                               "FROM cards JOIN card_templates on cards.template_id = card_templates.id" +
                                               "WHERE cards.template_id = '@templateID' AND cards.to_email = '@toEmail' " +
                                               "AND cards.to_name = '@toName' AND cards.message = '@message' " +
                                               "AND cards.from_name = '@froName' AND cards.from_email = '@froEmail' " +
                                               "AND card_templates.id = '@templateID'; ";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_sqlRetrieveECardVM, conn);                 
                    cmd.Parameters.AddWithValue("@templateID", ecard.TemplateID);
                    cmd.Parameters.AddWithValue("@toEmail", ecard.ToEmail);
                    cmd.Parameters.AddWithValue("@toName", ecard.ToName);
                    cmd.Parameters.AddWithValue("@message", ecard.Message);
                    cmd.Parameters.AddWithValue("@froName", ecard.FroName);
                    cmd.Parameters.AddWithValue("@froEmail", ecard.FroEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        eCardVM = MapEcardVMFromReader(reader);
                    }
                    return eCardVM;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        //Data Mapping for ECard and Template
        public Ecard MapEcardFromReader(SqlDataReader reader)
        {
            Ecard e = new Ecard();

            //e.Id = Convert.ToInt32(reader["id"]);
            e.ToName = Convert.ToString(reader["to_name"]);
            e.ToEmail = Convert.ToString(reader["to_email"]);
            e.Message = Convert.ToString(reader["message"]);
            e.FroName = Convert.ToString(reader["from_name"]);
            e.FroEmail = Convert.ToString(reader["from_email"]);
            e.TemplateID = Convert.ToInt32(reader["template_id"]);

            return e;
        }
        public EcardViewModel MapEcardVMFromReader(SqlDataReader reader)
        {
            EcardViewModel evm = new EcardViewModel();

            //Id = Convert.ToInt32(reader["id"]);
            evm.ToName = Convert.ToString(reader["to_name"]);
            evm.ToEmail = Convert.ToString(reader["to_email"]);
            evm.Message = Convert.ToString(reader["message"]);
            evm.FroEmail = Convert.ToString(reader["from_email"]);
            evm.TemplateID = Convert.ToInt32(reader["template_id"]);
            evm.TemplateName = Convert.ToString(reader["name"]);
            evm.ImageName = Convert.ToString(reader["imageName"]);
            evm.FontColor = Convert.ToString(reader["fontColor"]);

            return evm;
        }

        public Template MapTemplateFromReader(SqlDataReader reader)
        {
            Template t = new Template();

            t.Id = Convert.ToInt32(reader["id"]);
            t.TemplateName = Convert.ToString(reader["name"]);
            t.ImageName = Convert.ToString(reader["imageName"]);
            t.FontColor = Convert.ToString(reader["fontColor"]);

            return t;
        }
    }
}