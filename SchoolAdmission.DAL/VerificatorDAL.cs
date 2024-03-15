using Dapper;
using SchoolAdmission.DAL.BOs;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.Users;
using SchoolAdmission.DAL.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolAdmission.DAL
{
    public class VerificatorDAL : IVerificator,IUser
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
        }
        public void AssignBills()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.AssignBillsToStudents", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public void completeVerificatorData(int verificatorID, string position, string SKNumber)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var procedure = "dbo.completeApplicantData";
                var parameter = new
                {
                    verificatorID = verificatorID,
                    position = position,
                    SKNumber = SKNumber
                };
                try
                {
                    int result = conn.Execute(procedure, parameter, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new ArgumentException("Update data failed");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public void finalizeLeaderboard()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.finalizeLeaderboard", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public IEnumerable<ApplicantBO> GetAllApplicants()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM dbo.UserData as A
                                    JOIN DBO.ApplicantData as B on A.UserID = B.UserID
                                    JOIN DBO.ApplicantAcademicData AS C ON B.UADataID = C.UADataID
                                    JOIN DBO.ApplicantPersonalData AS D ON B.UPDataID = D.UPDataID
                                    RIGHT JOIN DBO.ApplicantAchievementRecord AS E ON B.UGDataID = E.UGDataID
                                    WHERE RoleID = 2";
                try
                {
                    var result = conn.Query<ApplicantBO, AcademicDataBO, PersonalDataBO, AchievementRecordsBO, ApplicantBO>(
                        strSql,
                        (applicant, academic, personal, achievement) =>
                        {
                            applicant.AcademicData = academic;
                            applicant.PersonalData = personal;
                            applicant.AchievementRecords = achievement;
                            return applicant;
                        },
                        splitOn: "UADataID,UPDataID,UGDataID" // Split the result based on these keys
                    );
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public ApplicantBO getApplicantByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"
                    SELECT A.*, B.*, C.*, D.*, E.*
                    FROM dbo.UserData AS A
                    JOIN DBO.ApplicantData AS B ON A.UserID = B.UserID
                    JOIN DBO.ApplicantAcademicData AS C ON B.UADataID = C.UADataID
                    JOIN DBO.ApplicantPersonalData AS D ON B.UPDataID = D.UPDataID
                    RIGHT JOIN DBO.ApplicantAchievementRecord AS E ON B.UGDataID = E.UGDataID
                    WHERE A.UserID = @UserID AND A.RoleID=2";
                try
                {
                    var result = conn.Query<ApplicantBO, AcademicDataBO, PersonalDataBO, AchievementRecordsBO, ApplicantBO>(
                            strSql,(applicant, academicData, personalData, achievementRecords) =>
                            {
                                applicant.AcademicData = academicData;
                                applicant.PersonalData = personalData;
                                applicant.AchievementRecords = achievementRecords;
                                return applicant;
                            },
                        splitOn: "AcademicData_UADataID,PersonalData_UPDataID,AchievementID",
                        param: new { UserID = id },
                        commandType: System.Data.CommandType.Text
                    ).FirstOrDefault();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public List<RankBO> GetRank()
        {
            List<RankBO> rankList = new List<RankBO>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.AssignBillsToStudent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            RankBO rank = new RankBO()
                            {
                                Rank = Convert.ToInt32(dr["Rank"]),
                                RegistrationID = (int)dr["RegistrationID"],
                                Name = dr["Name"].ToString(),
                                TotalScore = (int)dr["TotalScore"]
                            };
                            rankList.Add(rank);
                        }
                    }

                    dr.Close();
                }
            }

            return rankList;
        }

        public void verifyAcademicData(int verificatorID, int UGDataID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.verifyAcademicData", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UGDataID", UGDataID);
                    command.Parameters.AddWithValue("@verificatorID", verificatorID);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public void verifyAchievementRecord(int verificatorID, int UGDataID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.verifyAchievementRecord", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UGDataID", UGDataID);
                    command.Parameters.AddWithValue("@verificatorID", verificatorID);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public void verifyPersonalData(int verificatorID, int UGDataID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.verifyPersonalData", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UGDataID", UGDataID);
                    command.Parameters.AddWithValue("@verificatorID", verificatorID);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public string login(string email, string password)
        {
            string status = string.Empty;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.Login", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        status = reader["Status"].ToString();
                    }
                }
                return status;
            }
        }

        public void register(UserBO entity)
        {
            throw new NotImplementedException();
        }

        public UserBO getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserBO> getAll()
        {
            throw new NotImplementedException();
        }
    }
}
