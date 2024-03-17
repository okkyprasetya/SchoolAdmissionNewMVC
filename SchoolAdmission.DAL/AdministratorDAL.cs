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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAdmission.DAL
{
    public class AdministratorDAL : IAdministratorDAL,IVerificatorsDAL,IUsersDAL
    {
        private string GetConnectionString()
        {
            return Helper.GetConnectionString();
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

        public void deleteVerificator(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                conn.Execute("dbo.deleteVerificator", new { uid = UserID }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void finalizeLeaderboard(int quota)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.finalizeLeaderboard", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@quota", quota);

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
            List<ApplicantBO> users = new List<ApplicantBO>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM dbo.UserData as A
                                JOIN DBO.ApplicantData as B on A.UserID = B.UserID
                                JOIN DBO.ScholarshipData as C ON B.ScholarshipID = C.ScholarshipID
                                WHERE RoleID = 2";

                using (SqlCommand command = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ApplicantBO user = new ApplicantBO();
                                user.UGDataID = reader.GetInt32(reader.GetOrdinal("UGDataID"));
                                int NISOrdinal = reader.GetOrdinal("NIS");
                                user.NIS = !reader.IsDBNull(NISOrdinal) ? reader.GetString(NISOrdinal) : "-";
                                user.isFinal = reader.GetBoolean("isFinal");

                                UserBO usert = new UserBO();
                                usert.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                usert.MiddleName = reader.GetString(reader.GetOrdinal("MiddleName"));
                                usert.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.User = usert;

                                ScholarshipDataBO scholarships = new ScholarshipDataBO();
                                scholarships.ScholarshipID = reader.GetInt32(reader.GetOrdinal("ScholarshipID"));
                                scholarships.Name = reader.GetString(reader.GetOrdinal("Name"));
                                user.Scholarship = scholarships;

                                AcademicDataBO academicDatas = new AcademicDataBO();
                                academicDatas.UADataID = reader.GetInt32(reader.GetOrdinal("UADataID"));
                                user.AcademicData = academicDatas;

                                PersonalDataBO personalDatas = new PersonalDataBO();
                                personalDatas.UPDataID = reader.GetInt32(reader.GetOrdinal("UPDataID"));
                                user.PersonalData = personalDatas;

                                users.Add(user);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                }

            }
            return users;
        }

        public IEnumerable<VerificatorBO> getAllVerificator()
        {
            List<VerificatorBO> users = new List<VerificatorBO>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string strSql = @"SELECT * FROM dbo.UserData as a 
                          LEFT JOIN dbo.VerificatorData as b ON a.UserID = b.UserID 
                          LEFT JOIN dbo.RoleData as c ON a.RoleID = c.RoleID WHERE A.RoleID=1";

                using (SqlCommand command = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VerificatorBO user = new VerificatorBO();
                                user.VerificatorID = reader.GetInt32(reader.GetOrdinal("VerificatorID"));
                                int positionOrdinal = reader.GetOrdinal("Position");
                                user.Position = !reader.IsDBNull(positionOrdinal) ? reader.GetString(positionOrdinal) : "Position not set yet";

                                // Ternary check for SKNumber
                                int skNumberOrdinal = reader.GetOrdinal("SKNumber");
                                user.SKNumber = !reader.IsDBNull(skNumberOrdinal) ? reader.GetString(skNumberOrdinal) : "SKNumber not set yet";

                                // Assuming RoleBO properties here
                                RoleBO role = new RoleBO();
                                role.RoleId = reader.GetInt32(reader.GetOrdinal("RoleID"));
                                role.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
                                user.Role = role;

                                UserBO usert = new UserBO();
                                usert.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                usert.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                usert.MiddleName = reader.GetString(reader.GetOrdinal("MiddleName"));
                                usert.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.User = usert;

                                users.Add(user);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                }
            }

            return users;
        }

        public VerificatorBO getVerificator(int verid)
        {
            VerificatorBO verificator = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string strSql = @"SELECT * FROM dbo.UserData as a 
                          JOIN dbo.VerificatorData as b ON a.UserID = b.UserID
                          JOIN dbo.RoleData as c ON a.RoleID = c.RoleID 
                          WHERE VerificatorID = @verid";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@verid", verid);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate VerificatorBO properties from reader
                        verificator = new VerificatorBO
                        {
                            VerificatorID = (int)reader["VerificatorID"],
                            SKNumber = reader["SKNumber"].ToString(),
                            Position = reader["Position"].ToString(),
                            // Populate other VerificatorBO properties accordingly
                        };

                        // Populate UserBO properties from reader
                        verificator.User = new UserBO
                        {
                            UserID = (int)reader["UserID"],
                            FirstName = reader["FirstName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            UserEmail = reader["UserEmail"].ToString()
                            // Populate other UserBO properties accordingly
                        };

                        // Populate RoleBO properties from reader
                        verificator.Role = new RoleBO
                        {
                            RoleId = (int)reader["RoleID"],
                            RoleName = reader["RoleName"].ToString(),
                            // Populate other RoleBO properties accordingly
                        };
                    }

                    reader.Close();
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
                using (SqlCommand cmd = new SqlCommand("dbo.GetRank", conn))
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
                                TotalScore = (int)dr["TotalScore"],
                                status = dr["Status"].ToString()
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

        public UserBO login(string email, string password)
        {
            UserBO user = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("dbo.LoginUser", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new UserBO
                        {
                            FirstName = reader["FirstName"].ToString(),
                            Role = new RoleBO
                            {
                                RoleId = Convert.ToInt32(reader["RoleID"]),
                                RoleName = reader["RoleName"].ToString()
                            }
                        };
                    }
                }
            }

            return user;
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
                    command.Parameters.AddWithValue("@UADataID", UGDataID);
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
                    command.Parameters.AddWithValue("@UPDataID", UGDataID);
                    command.Parameters.AddWithValue("@verificatorID", verificatorID);

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        public AcademicDataBO getAcademicDataByID(int UGDataID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM ApplicantAcademicData AS A JOIN ApplicantData AS B ON A.UADataID=B.UADataID WHERE UGDataID=@UGDataId";
                try
                {
                    conn.Open();
                    var result = conn.Query<AcademicDataBO, ApplicantBO, AcademicDataBO>(
                        strSql,
                        (academicData, applicantData) =>
                        {
                            AcademicDataBO academicDataBO = new AcademicDataBO
                            {
                                UADataID = academicData.UADataID,
                                RaportSummaries = academicData.RaportSummaries,
                                RaportDocument = academicData.RaportDocument,
                                isVerified = academicData.isVerified
                            };

                            return academicDataBO;
                        },
                        new { UGDataID },
                        splitOn: "UADataID" // Specify the column name to split the results on
                    ).FirstOrDefault();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public PersonalDataBO getPersonalDataByID(int UGDataID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM ApplicantPersonalData AS A JOIN ApplicantData AS B ON A.UPDataID=B.UPDataID WHERE UGDataID=@UGDataId";
                try
                {
                    conn.Open();
                    var result = conn.Query<PersonalDataBO, ApplicantBO, PersonalDataBO>(
                        strSql,
                        (personalData, applicantData) =>
                        {
                            PersonalDataBO academicDataBO = new PersonalDataBO
                            {
                                UPDataID=personalData.UPDataID,
                                FatherName = personalData.FatherName,
                                FatherAddress = personalData.FatherAddress,
                                FatherJob=personalData.FatherJob,
                                FatherSalary=personalData.FatherSalary,
                                MotherName = personalData.MotherName,
                                MotherAddress = personalData.MotherAddress,
                                MotherJob = personalData.MotherJob,
                                MotherSalary = personalData.MotherSalary,
                                SiblingsNumber = personalData.SiblingsNumber,
                                Hobi = personalData.Hobi,
                                KKDocument =personalData.KKDocument,
                                BirthDocument = personalData.BirthDocument,
                                isVerified = personalData.isVerified
                            };

                            return academicDataBO;
                        },
                        new { UGDataID },
                        splitOn: "UPDataID" // Specify the column name to split the results on
                    ).FirstOrDefault();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public List<AchievementRecordsBO> GetAchievementRecords(int UGDataID)
        {
            List<AchievementRecordsBO> achievementRecords = new List<AchievementRecordsBO>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string query = "SELECT * FROM dbo.UserData AS A LEFT JOIN dbo.ApplicantData AS B ON A.UserID = B.UserID LEFT JOIN dbo.ApplicantAchievementRecord AS C ON B.UGDataID = C.UGDataID WHERE b.UGDataID = @UGDataID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UGDataID", UGDataID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AchievementRecordsBO applicantAchievementData = new AchievementRecordsBO
                            {
                                AchievementID = reader["AchievementID"] == DBNull.Value ? 0 : (int)reader["AchievementID"],
                                Title = reader["Title"] == DBNull.Value ? "" : reader["Title"].ToString(),
                                Level = reader["Level"] == DBNull.Value ? "" : reader["Level"].ToString(),
                                Description = reader["Description"] == DBNull.Value ? "" : reader["Description"].ToString(),
                                isVerified = reader["isVerified"] == DBNull.Value ? false : (bool)reader["isVerified"]
                            };

                            achievementRecords.Add(applicantAchievementData);
                        }
                    }
                }
            }

            return achievementRecords;
        }
    }
}
