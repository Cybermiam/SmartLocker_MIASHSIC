using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker
{




    internal class DataService
    {
        private EventLog eventLog;
        private string connectionString = "Server=CYBERMIAM;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;";


        public DataService()
        {
            eventLog = new EventLog();
            if (!EventLog.SourceExists("SmartLockerSource"))
            {
                EventLog.CreateEventSource("SmartLockerSource", "SmartLockerLog");
            }
            eventLog.Source = "SmartLockerSource";
            eventLog.Log = "SmartLockerLog";
        }




        public List<string> GetUtilisateurs()
        {
            List<string> utilisateurs = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Utilisateurs";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                utilisateurs.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in GetUtilisateurs: {ex.Message}", EventLogEntryType.Error);
            }

            return utilisateurs;
        }

        public void addUtilisateur(String name, int role)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO Utilisateurs (Name, Role) VALUES ('{name}', {role})";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in addUtilisateur: {ex.Message}", EventLogEntryType.Error);
            }
        }

        public void addApp(String name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO Apps (Name) VALUES ('{name}')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in addApp: {ex.Message}", EventLogEntryType.Error);
            }
        }

        public List<string> getApps()
        {
            List<string> apps = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Apps";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                apps.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getApps: {ex.Message}", EventLogEntryType.Error);
            }

            return apps;
        }

        public List<ContrainteHoraire> getAllContraintesHoraire()
        {
            List<ContrainteHoraire> contraintesHoraires = new List<ContrainteHoraire>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM ContrainteHoraires";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContrainteHoraire contrainteHoraire = new ContrainteHoraire
                                {
                                    Id = (int)reader["Id"],
                                    UserId = (int)reader["UserId"],
                                    AppId = (int)reader["AppId"],
                                    MaxTime = (int)reader["MaxTime"],
                                    BlockTime = (int)reader["BlockTime"],
                                    UsedTime = (int)reader["UsedTime"]
                                };
                                contraintesHoraires.Add(contrainteHoraire);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getAllContraintesHoraire: {ex.Message}", EventLogEntryType.Error);
            }

            return contraintesHoraires;
        }

        public List<ContrainteJour> getAllContraintesJour()
        {
            List<ContrainteJour> contraintesJours = new List<ContrainteJour>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM ContrainteJours";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContrainteJour contrainteJour = new ContrainteJour
                                {
                                    Id = (int)reader["Id"],
                                    UserId = (int)reader["UserId"],
                                    AppId = (int)reader["AppId"],
                                    MaxTime = (int)reader["MaxTime"],
                                    UsedTime = (int)reader["UsedTime"]
                                };
                                contraintesJours.Add(contrainteJour);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getAllContraintesJour: {ex.Message}", EventLogEntryType.Error);
            }

            return contraintesJours;
        }

        public List<ContrainteSemaine> getAllContraintesSemaine()
        {
            List<ContrainteSemaine> contraintesSemaines = new List<ContrainteSemaine>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM ContrainteSemaines";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContrainteSemaine contrainteSemaine = new ContrainteSemaine
                                {
                                    Id = (int)reader["Id"],
                                    UserId = (int)reader["UserId"],
                                    AppId = (int)reader["AppId"],
                                    MondayTime = (int)reader["MondayTime"],
                                    TuesdayTime = (int)reader["TuesdayTime"],
                                    WednesdayTime = (int)reader["WednesdayTime"],
                                    ThursdayTime = (int)reader["ThursdayTime"],
                                    FridayTime = (int)reader["FridayTime"],
                                    SaturdayTime = (int)reader["SaturdayTime"],
                                    SundayTime = (int)reader["SundayTime"],
                                    UsedTime = (int)reader["UsedTime"]
                                };
                                contraintesSemaines.Add(contrainteSemaine);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getAllContraintesSemaine: {ex.Message}", EventLogEntryType.Error);
            }

            return contraintesSemaines;
        }

        public string getAppNameFromId(int id)
        {
            string appName = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Apps WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                appName = reader["Name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getAppNameFromId: {ex.Message}", EventLogEntryType.Error);
            }

            return appName;
        }

        public string getUserNameFromId(int id)
        {
            string userName = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Utilisateurs WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userName = reader["Name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in getUserNameFromId: {ex.Message}", EventLogEntryType.Error);
            }
            return userName;
        }

        

        public void updateContrainteHoraire(ContrainteHoraire contrainte)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE ContrainteHoraires SET UserId = @UserId, AppId = @AppId, MaxTime = @MaxTime, BlockTime = @BlockTime, UsedTime = @UsedTime WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", contrainte.Id);
                        command.Parameters.AddWithValue("@UserId", contrainte.UserId);
                        command.Parameters.AddWithValue("@AppId", contrainte.AppId);
                        command.Parameters.AddWithValue("@MaxTime", contrainte.MaxTime);
                        command.Parameters.AddWithValue("@BlockTime", contrainte.BlockTime);
                        command.Parameters.AddWithValue("@UsedTime", contrainte.UsedTime);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in updateContrainteHoraire: {ex.Message}", EventLogEntryType.Error);
            }
        }

        public void updateContrainteJour(ContrainteJour contrainte)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE ContrainteJours SET UserId = @UserId, AppId = @AppId, MaxTime = @MaxTime, UsedTime = @UsedTime WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", contrainte.Id);
                        command.Parameters.AddWithValue("@UserId", contrainte.UserId);
                        command.Parameters.AddWithValue("@AppId", contrainte.AppId);
                        command.Parameters.AddWithValue("@MaxTime", contrainte.MaxTime);
                        command.Parameters.AddWithValue("@UsedTime", contrainte.UsedTime);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in updateContrainteJour: {ex.Message}", EventLogEntryType.Error);
            }
        }

        public void updateContrainteSemaine(ContrainteSemaine contrainte)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE ContrainteSemaines SET UserId = @UserId, AppId = @AppId, MondayTime = @MondayTime, TuesdayTime = @TuesdayTime, WednesdayTime = @WednesdayTime, ThursdayTime = @ThursdayTime, FridayTime = @FridayTime, SaturdayTime = @SaturdayTime, SundayTime = @SundayTime, UsedTime = @UsedTime WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", contrainte.Id);
                        command.Parameters.AddWithValue("@UserId", contrainte.UserId);
                        command.Parameters.AddWithValue("@AppId", contrainte.AppId);
                        command.Parameters.AddWithValue("@MondayTime", contrainte.MondayTime);
                        command.Parameters.AddWithValue("@TuesdayTime", contrainte.TuesdayTime);
                        command.Parameters.AddWithValue("@WednesdayTime", contrainte.WednesdayTime);
                        command.Parameters.AddWithValue("@ThursdayTime", contrainte.ThursdayTime);
                        command.Parameters.AddWithValue("@FridayTime", contrainte.FridayTime);
                        command.Parameters.AddWithValue("@SaturdayTime", contrainte.SaturdayTime);
                        command.Parameters.AddWithValue("@SundayTime", contrainte.SundayTime);
                        command.Parameters.AddWithValue("@UsedTime", contrainte.UsedTime);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error in updateContrainteSemaine: {ex.Message}", EventLogEntryType.Error);
            }
        }

    }
}
