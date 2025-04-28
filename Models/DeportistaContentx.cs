using Microsoft.Data.SqlClient;
using System.Data;

namespace PrimerParcial.Models
{
    public class DeportistaContentx
    {

        private readonly string _connectionString;

        public DeportistaContentx(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DeportesDB");
        }

        public List<Deportista> ObtenerParticipantes()
        {
            var lista = new List<Deportista>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_MostrarParticipantes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Deportista
                        {
                            id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Disciplina = reader["Disciplina"].ToString(),
                            Edad = (int)reader["Edad"],
                            CiudadResidencia = reader["CiudadResidencia"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public void AgregarParticipante(Deportista p)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AgregarParticipante", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Disciplina", p.Disciplina);
                cmd.Parameters.AddWithValue("@Edad", p.Edad);
                cmd.Parameters.AddWithValue("@CiudadResidencia", p.CiudadResidencia);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void EditarParticipante(Deportista p)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_EditarParticipante", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", p.id);
                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Disciplina", p.Disciplina);
                cmd.Parameters.AddWithValue("@Edad", p.Edad);
                cmd.Parameters.AddWithValue("@CiudadResidencia", p.CiudadResidencia);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarParticipante(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_EliminarParticipante", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
