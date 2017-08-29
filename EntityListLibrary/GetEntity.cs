using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Web;
using Newtonsoft.Json.Linq;

namespace EntityListLibrary
{
    public class GetEntity
    {
        private string _path;
        private string _type;

        public GetEntity(string path, string type)
        {
            this._path = path;
            this._type = type;
        }

        public IEnumerable<Entity> EntityList(List<Entity> entities)
        {
            if(entities.Where(a => a.Type.Trim() == _type.Trim()).Count() > 0)
                return entities;

            return null;
        }

        //retrieve data from database
        public List<Entity> getData()
        {
            //assumption: Did not use entityframework
            JObject o1 = JObject.Parse(File.ReadAllText(_path));
            string connectionString = "";
            foreach (var x in o1)
            {
                if (x.Key == "ConnectionStrings")
                    connectionString = x.Value.ToString();
            }

            //http://www.c-sharpcorner.com/UploadFile/5d065a/use-business-library-as-modal-in-Asp-Net-mvc/
            List<Entity> entities = new List<Entity>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //assumption: stored procedure sp_getEntity is present in the database where the connectionString is pointing
                SqlCommand cmd = new SqlCommand("sp_getEntity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Type", SqlDbType.Char);
                cmd.Parameters["Type"].Value = _type.Trim();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Entity entity = new Entity();
                    entity.Id = Convert.ToInt32(rdr["Id"]);
                    entity.Created = Convert.ToDateTime(rdr["Created"]);
                    entity.Type = rdr["Type"].ToString();
                    entity.Content = rdr["Content"].ToString();
                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}
