using SchoolAdmission.DAL.BOs;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.Users;
using SchoolAdmission.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Security.Cryptography;
using SchoolAdmission.DAL.BOs.GeneralObject;

namespace SchoolAdmission.DAL
{
    public class AdministratorDAL : IAdministrator,IVerificator,IUser
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
        }
        public void addVerificator(VerificatorBO entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "addVerificator";
                var param = new
                {
                    email = entity.User.UserEmail,
                    password = entity.User.Password,
                    fname = entity.User.FirstName,
                    midname = entity.User.MiddleName,
                    lname = entity.User.LastName
                };
                try
                {
                    int result = conn.Execute(strSql, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new ArgumentException("Insert data failed..");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message}-{sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
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
            throw new NotImplementedException();
        }

        public void deleteApplicantData(int UserId)
        {
            throw new NotImplementedException();
        }

        public void deleteVerificator(int verificatorId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                conn.Execute("dbo.deleteVerificator", new { verificatorId }, commandType: System.Data.CommandType.StoredProcedure);
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

        public IEnumerable<UserBO> getAll()
        {
            throw new NotImplementedException();
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

        public IEnumerable<VerificatorBO> getAllVerificator()
        {
            List<VerificatorBO> users = new List<VerificatorBO>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string strSql = @"SELECT * FROM dbo.UserData as a 
                                    join dbo.VerificatorData as b on a.UserID=b.UserID
                                    join dbo.RoleData as c on a.RoleID = c.RoleID";

                try
                {
                    users = conn.Query<VerificatorBO, VerificatorBO, RoleBO, VerificatorBO>(
                    strSql,
                    (user, verificator, role) =>
                    {
                        user.VerificatorID = verificator.VerificatorID;
                        user.Position = verificator.Position;
                        user.SKNumber = verificator.SKNumber;
                        user.Role = role;
                        return user;
                    },
                    splitOn: "VerificatorID,RoleID"
                    ).AsList();
                }
                catch (SqlException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }

            return users;
        }

        public VerificatorBO getVerificator(int verid)
        {
            VerificatorBO? verificator = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string strSql = @"SELECT * FROM dbo.UserData as a 
                                    join dbo.VerificatorData as b on a.UserID=b.UserID
                                    join dbo.RoleData as c on a.RoleID = c.RoleID WHERE VerificatorID=@id";

                try
                {
                    verificator = conn.Query<VerificatorBO, RoleBO, VerificatorBO>(
                        strSql,(user, role) =>
                        {
                            user.Role = role;
                            return user;
                        },
                        new { id = verid },
                        splitOn: "VerificatorID,RoleID"
                    ).FirstOrDefault();
                }
                catch (SqlException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }

            return verificator;
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
                            strSql, (applicant, academicData, personalData, achievementRecords) =>
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

        public UserBO getUserByEmail(string email)
        {
            throw new NotImplementedException();
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

        public void updateVerificator(VerificatorBO entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                var parameters = new
                {
                    fname = entity.User.FirstName,
                    mname = entity.User.MiddleName,
                    lname = entity.User.LastName,
                    uid = entity.User.UserID
                };

                conn.Execute("dbo.editVerificatorData", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
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
    }
}
